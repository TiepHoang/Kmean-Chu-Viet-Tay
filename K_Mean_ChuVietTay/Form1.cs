using KMeans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Mean_ChuVietTay
{
    public partial class Form1 : Form
    {
        Core _core;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lstImgData.Images.Clear();
        }
        void _show(List<Image> lst)
        {
            FlowLayoutPanel fpl = new FlowLayoutPanel();
            fpl.Dock = DockStyle.Top;
            fpl.Height = 30;
            pnImage.Controls.Add(fpl);
            for (int i = 0; i < lst.Count; i++)
            {
                PictureBox px = new PictureBox();
                px.Width = 30;
                px.Height = 30;
                px.SizeMode = PictureBoxSizeMode.Zoom;
                px.Image = lst[i];
                fpl.Controls.Add(px);
            }
        }
        private void btnInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<Image> lst = new List<Image>();
                foreach (var item in openFileDialog1.FileNames)
                {
                    lst.Add(Image.FromFile(item));
                }
                lstImgData.Images.AddRange(lst.ToArray());
                btnRun_Click(sender, e);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            pnImage.Controls.Clear();

            List<tPoint> lstPoint = new List<tPoint>();
            foreach (Image item in lstImgData.Images)
            {
                lstPoint.Add(new tPoint(_vector(new Bitmap(item))));
            }
            _core = new Core(lstPoint);
            _core.Run();
            foreach (var item in _core.lstCenter)
            {
                List<Image> lstImg = new List<Image>();
                lstImg.Add(_image(item));

                var lstP = _core.lstPoint.Where(q => q.Label.ID == item.ID).ToList();
                foreach (var p in lstP)
                {
                    lstImg.Add(_image(p));
                }
                _show(lstImg);
            }
        }

        private List<int> _vector(Bitmap img)
        {
            List<int> lst = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (i >= img.Width || j >= img.Height)
                    {
                        lst.Add(255);
                    }
                    else
                    {
                        var item = img.GetPixel(i, j);
                        lst.Add((item.A + item.B + item.G) /3);
                    }
                }
            }
            return lst;
        }

        public Image _image(tPoint point)
        {
            Bitmap img = new Bitmap(30, 30);
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    img.SetPixel(i, j, point.Vector[i * 30 + j] == (byte)255 ? Color.White : Color.Black);
                }
            }
            return img;
        }
    }
}
