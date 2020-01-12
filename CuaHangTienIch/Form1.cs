using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            var chatLieu = from c in db.ChatLieux select new { ID = c.MaChatLieu, Name = c.TenChatLieu };
            dgvChatLieu.DataSource = chatLieu.ToList();

            var hangHoa = from d in db.Hangs select new { ID = d.MaHang, Name = d.TenHang, ID_Material = d.MaChatLieu, Name_Material = d.ChatLieu.TenChatLieu, Amount = d.SoLuong, Price_Enter = d.DonGiaNhap, Price = d.DonGiaBan, Note = d.GhiChu };
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
                dlg.Filter = "JPG Files (*.jpg) | *.jpg | GIF Files (*.gif) | *.gif | All Files (*.*) | *.*";
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
    }
}
