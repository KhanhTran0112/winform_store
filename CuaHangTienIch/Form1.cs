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
        void loadData()
        {
            storeEntities db = new storeEntities();

            var nhanVien = from a in db.NhanViens select new { ID = a.MaNhanVien, Name = a.TenNhanVien, Sex = a.GioiTinh, Address = a.DiaChi, Phone = a.SoDienThoai, Date = a.NgaySinh };
            dgvNhanVien.DataSource = nhanVien.ToList();

            var khachHang = from b in db.KhachHangs select new { ID = b.MaKhach, Name = b.TenKhach, Phone = b.SoDienThoai, Address = b.DiaChi };
            dgvKhachHang.DataSource = khachHang.ToList();

            var hangHoa = from d in db.Hangs select new { ID = d.MaHang, Name = d.TenHang, Name_Material = d.TenChatLieu, Amount = d.SoLuong, Price_Enter = d.DonGiaNhap, Price = d.DonGiaBan, Note = d.GhiChu, anh = d.Anh };
            
            dgvHangHoa.DataSource = hangHoa.ToList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            txtMaNV.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
            cbGioiTinh.SelectedItem = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
            txtDiaChiNV.Text = dgvNhanVien.Rows[index].Cells[3].Value.ToString();
            txtSdtNV.Text = dgvNhanVien.Rows[index].Cells[4].Value.ToString();
            DateTime time = Convert.ToDateTime(dgvNhanVien.Rows[index].Cells[5].Value.ToString());
            dtpNgaySinhNV.Value = time;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private void btnThemNV_Click(object sender, EventArgs e)
        {
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

            txtMaHang.Text = "H02";
            txtTenHang.Text = "Cốc nhựa";
            txtSoLuongHang.Text = "20";
            txtGiaNhap.Text = "2500";
            txtGiaBan.Text = "3000";
            txtGhiChu.Text = "";

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
            storeEntities db = new storeEntities();
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
    }
}
