using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienIch
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }
        #region Key word
        private String keyHH = "";
        private String keyNV = "";
        private String keyKH = "";
        private String keyHD = "";
        #endregion

        #region Form Load
        storeEntities db = new storeEntities();

        void loadData()
        {
            var nhanVien = from a in db.NhanViens select new { ID = a.MaNhanVien, Name = a.TenNhanVien, Sex = a.GioiTinh, Address = a.DiaChi, Phone = a.SoDienThoai, Date = a.NgaySinh };
            dgvNhanVien.DataSource = nhanVien.ToList();

            var khachHang = from b in db.KhachHangs select new { ID = b.MaKhach, Name = b.TenKhach, Phone = b.SoDienThoai, Address = b.DiaChi };
            dgvKhachHang.DataSource = khachHang.ToList();

            var hangHoa = from d in db.Hangs select new { ID = d.MaHang, Name = d.TenHang, Name_Material = d.TenChatLieu, Amount = d.SoLuong, Price_Enter = d.DonGiaNhap, Price = d.DonGiaBan, Note = d.GhiChu, anh = d.Anh };
            dgvHangHoa.DataSource = hangHoa.ToList();

            var hoadon = from e in db.HoaDons select new { ID = e.MaNhanVien, MaNhanVien = e.MaNhanVien, NgayBan = e.NgayBan, MaKhach = e.MaKhach, TongTien = e.TongTien };
            dgvHoaDon.DataSource = hoadon.ToList();
        }
        #endregion

        #region Nhân Viên
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaNV.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
            cboGioiTinh.SelectedItem = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
            txtDiaChiNV.Text = dgvNhanVien.Rows[index].Cells[3].Value.ToString();
            txtSdtNV.Text = dgvNhanVien.Rows[index].Cells[4].Value.ToString();
            DateTime time = Convert.ToDateTime(dgvNhanVien.Rows[index].Cells[5].Value.ToString());
            dtpNgaySinhNV.Value = time;
        }
        

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtDiaChiNV.Enabled = true;
            txtSdtNV.Enabled = true;
            cboGioiTinh.Enabled = true;
            dtpNgaySinhNV.Enabled = true;
            keyNV = "add";
        }
        private void btnLuuNV_Click(object sender, EventArgs e)
        {
            if(keyNV == "add")
            {
                NhanVien nv = new NhanVien();
                nv.MaNhanVien = txtMaNV.Text;
                nv.TenNhanVien = txtTenNV.Text;
                nv.GioiTinh = cboGioiTinh.SelectedItem.ToString();
                nv.DiaChi = txtDiaChiNV.Text;
                nv.SoDienThoai = txtSdtNV.Text;
                nv.NgaySinh = dtpNgaySinhNV.Value;
                db.NhanViens.Add(nv);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!");
                loadData();
            }
            if(keyNV == "edit")
            {
                String id = txtMaNV.Text;
                NhanVien nv = db.NhanViens.Find(id);
                if(nv != null)
                {
                    nv.TenNhanVien = txtTenNV.Text;
                    nv.GioiTinh = cboGioiTinh.SelectedItem.ToString();
                    nv.DiaChi = txtDiaChiNV.Text;
                    nv.SoDienThoai = txtSdtNV.Text;
                    nv.NgaySinh = dtpNgaySinhNV.Value;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa thành công");
                    loadData();
                }
            }
        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = true;
            txtDiaChiNV.Enabled = true;
            txtSdtNV.Enabled = true;
            cboGioiTinh.Enabled = true;
            dtpNgaySinhNV.Enabled = true;
            keyNV = "edit";
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            NhanVien nv = db.NhanViens.Find(txtMaNV.Text);
            db.NhanViens.Remove(nv);
            db.SaveChanges();
            MessageBox.Show("Xóa thành công!");
            loadData();
        }
        private void txtTimKiemNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            String search = txtTimKiemNV.Text;
            var nv = from a in db.NhanViens where a.TenNhanVien.ToUpper().Contains(search.ToUpper()) select new { ID = a.MaNhanVien, Name = a.TenNhanVien, Sex = a.GioiTinh, Address = a.DiaChi, Phone = a.SoDienThoai, Date = a.NgaySinh };
            dgvNhanVien.DataSource = nv.ToList();
            if(txtTimKiemNV.Text == "")
            {
                loadData();
            }
        }
        #endregion

        #region Hàng hóa
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            string img = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "All Files (*.*) | *.*";
                dlg.Title = "Select image of goods";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    img = dlg.FileName.ToString();
                    picHangHoa.ImageLocation = img;
                }
            }
            catch
            {

            }
        }

        private void btnThemHH_Click(object sender, EventArgs e)
        {
            txtMaHang.Enabled = true;
            txtTenHang.Enabled = true;
            cobChatLieu.Enabled = true;
            txtSoLuongHang.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGhiChu.Enabled = true;
            btnBrowser.Enabled = true;
            keyHH = "add";
        }

        private byte[] ConvertFillToByte(string sPath)
        {
            byte[] data = null;
            FileInfo flinfo = new FileInfo(sPath);
            long numByte = flinfo.Length;
            FileStream fstream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fstream);
            data = br.ReadBytes((int)numByte);
            return data;
        }
        private void btnLuuHH_Click(object sender, EventArgs e)
        {
            if (keyHH == "add")
            {
                Hang hang = new Hang();
                hang.MaHang = txtMaHang.Text;
                hang.TenHang = txtTenHang.Text;
                hang.TenChatLieu = cobChatLieu.SelectedItem.ToString();
                hang.SoLuong = Convert.ToInt32(txtSoLuongHang.Text);
                hang.DonGiaBan = Convert.ToInt32(txtGiaBan.Text);
                hang.DonGiaNhap = Convert.ToInt32(txtGiaNhap.Text);
                hang.GhiChu = txtGhiChu.Text;
                hang.Anh = ConvertFillToByte(picHangHoa.ImageLocation);
                db.Hangs.Add(hang);
                db.SaveChanges();
                MessageBox.Show("Thêm hàng thành công!");
                loadData();

                txtMaHang.Enabled = true;
                txtTenHang.Enabled = true;
                cobChatLieu.Enabled = true;
                txtSoLuongHang.Enabled = true;
                txtGiaNhap.Enabled = true;
                txtGiaBan.Enabled = true;
                txtGhiChu.Enabled = true;
                btnBrowser.Enabled = true;
            }
            if(keyHH == "edit")
            {
                String id = txtMaHang.Text;
                Hang hang = db.Hangs.Find(id);
                if (hang != null)
                {
                    hang.TenHang = txtTenHang.Text;
                    hang.TenChatLieu = cobChatLieu.SelectedItem.ToString();
                    hang.SoLuong = Convert.ToInt32(txtSoLuongHang.Text);
                    hang.DonGiaBan = Convert.ToInt32(txtGiaBan.Text);
                    hang.DonGiaNhap = Convert.ToInt32(txtGiaNhap.Text);
                    hang.GhiChu = txtGhiChu.Text;
                    hang.Anh = ConvertFillToByte(picHangHoa.ImageLocation);
                    db.SaveChanges();
                    loadData();
                }
                else
                {
                    MessageBox.Show("Không ton tai hang can chinh sua");
                }
            }
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaHang.Text = dgvHangHoa.Rows[index].Cells[0].Value.ToString();
            txtTenHang.Text = dgvHangHoa.Rows[index].Cells[1].Value.ToString();
            cobChatLieu.SelectedItem = dgvHangHoa.Rows[index].Cells[2].Value.ToString();
            txtSoLuongHang.Text = dgvHangHoa.Rows[index].Cells[3].Value.ToString();
            txtGiaNhap.Text = dgvHangHoa.Rows[index].Cells[4].Value.ToString();
            txtGiaBan.Text = dgvHangHoa.Rows[index].Cells[5].Value.ToString();
            txtGhiChu.Text = dgvHangHoa.Rows[index].Cells[6].Value.ToString();


            storeEntities db = new storeEntities();
            var anh = from d in db.Hangs where d.MaHang == txtMaHang.Text select new { d.Anh };
            byte[] img = (byte[])dgvHangHoa.Rows[index].Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            picHangHoa.Image = Image.FromStream(ms);
        }

        private void btnSuaHH_Click(object sender, EventArgs e)
        {
            txtMaHang.Enabled = false;
            txtTenHang.Enabled = true;
            cobChatLieu.Enabled = true;
            txtSoLuongHang.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGhiChu.Enabled = true;
            btnBrowser.Enabled = true;
            keyHH = "edit";
        }

        private void btnXoaHH_Click(object sender, EventArgs e)
        {
            Hang hang = db.Hangs.Find(txtMaHang.Text);
            db.Hangs.Remove(hang);
            db.SaveChanges();
            MessageBox.Show("Xóa thành công");
            loadData();
        }

        private void txtTimHiemHangHoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            String search = txtTimHiemHangHoa.Text;
            var hangHoa = from d in db.Hangs where d.TenHang.ToUpper().Contains(search.ToUpper()) select new { ID = d.MaHang, Name = d.TenHang, Name_Material = d.TenChatLieu, Amount = d.SoLuong, Price_Enter = d.DonGiaNhap, Price = d.DonGiaBan, Note = d.GhiChu, anh = d.Anh };

            dgvHangHoa.DataSource = hangHoa.ToList();
            if (txtTimHiemHangHoa.Text == "")
            {
                loadData();
            }
        }

        private void btnHienThiHH_Click(object sender, EventArgs e)
        {
            loadData();
        }


        #endregion

        #region Khách Hàng
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtSDTKH.Enabled = true;
            txtDiaChiKH.Enabled = true;
            keyKH = "add";
        }
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = true;
            txtSDTKH.Enabled = true;
            txtDiaChiKH.Enabled = true;
            keyKH = "edit";
        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            if (keyKH == "add")
            {
                KhachHang kh = new KhachHang();
                kh.MaKhach = txtMaKH.Text;
                kh.TenKhach = txtTenKH.Text;
                kh.SoDienThoai = txtSDTKH.Text;
                kh.DiaChi = txtDiaChiKH.Text;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!");
                loadData();
            }
            if (keyKH == "edit")
            {
                String id = txtMaKH.Text;
                KhachHang kh = db.KhachHangs.Find(id);
                kh.TenKhach = txtTenKH.Text;
                kh.SoDienThoai = txtSDTKH.Text;
                kh.DiaChi = txtDiaChiKH.Text;
                db.SaveChanges();
                MessageBox.Show("Chỉnh sửa thành công!");
                loadData();
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            String id = txtMaKH.Text;
            KhachHang kh = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            MessageBox.Show("Xóa thành công!");
            loadData();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaKH.Text = dgvKhachHang.Rows[index].Cells[0].Value.ToString();
            txtTenKH.Text = dgvKhachHang.Rows[index].Cells[1].Value.ToString();
            txtSDTKH.Text = dgvKhachHang.Rows[index].Cells[2].Value.ToString();
            txtDiaChiKH.Text = dgvKhachHang.Rows[index].Cells[3].Value.ToString();
        }
        private void txtTimKiemKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            String search = txtTimKiemKH.Text;
            var khachHang = from b in db.KhachHangs where b.TenKhach.ToUpper().Contains(search.ToUpper()) select new { ID = b.MaKhach, Name = b.TenKhach, Phone = b.SoDienThoai, Address = b.DiaChi };
            dgvKhachHang.DataSource = khachHang.ToList();

            if (txtTimKiemKH.Text == "")
            {
                loadData();
            }
        }

        #endregion

        private void dgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaHoaDon.Text = dgvHoaDon.Rows[index].Cells[0].Value.ToString();
            txtMaNhanVien.Text = dgvHoaDon.Rows[index].Cells[1].Value.ToString();
            DateTime time = Convert.ToDateTime(dgvHoaDon.Rows[index].Cells[2].Value.ToString());
            dtpNgayBan.Value = time;
            txtMaKhachHang.Text = dgvHoaDon.Rows[index].Cells[3].Value.ToString();
            txtTongTien.Text = dgvHoaDon.Rows[index].Cells[4].Value.ToString();

            NhanVien nv = db.NhanViens.Find(txtMaNhanVien.Text);
            txtTenNhanVien.Text = nv.TenNhanVien;

            KhachHang kh = db.KhachHangs.Find(txtMaKhachHang.Text);
            txtTenKhachHang.Text = kh.TenKhach;
            txtSDTKhachHang.Text = kh.SoDienThoai;
            txtDiaChiKhachHang.Text = kh.DiaChi;
        }
    }
}
