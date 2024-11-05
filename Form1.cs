using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tringle_Vorm
{
    public partial class Form1 : Form
    {
        Button btn;
        ListView listView1;
        TextBox txtA, txtB, txtC;
        System.Drawing.Image[] img;
        PictureBox pictureBox; 
        double a, b, c, h;

        public Form1()
        {
            this.Text = "Töö kolmnurkuga";
            this.Width = 700;
            this.Height = 600;
            this.BackColor = ColorTranslator.FromHtml("#fa5c5c");


            btn = new Button();
            btn.Text = "Alusta";
            btn.Height = 50;
            btn.Width = 150;
            btn.Location = new Point(450, 120);
            btn.Click += Run_button_Click;

            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(255, 255, 192);
            btn.ForeColor = Color.Black;
            btn.FlatAppearance.BorderSize = 3;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.Controls.Add(btn);

            img = new Image[] {
                Image.FromFile("Nurinurkne.png"),
                Image.FromFile("Teravnurkne.jpg"),
                Image.FromFile("Taisnurkne.png"),
                Image.FromFile("Vordhaarne.png"),
                Image.FromFile("Nurinurkne.png")
            };

            txtA = new TextBox() { Location = new Point(150, 50), Width = 100,
                BackColor = ColorTranslator.FromHtml("#ff8f8f"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            txtB = new TextBox() { Location = new Point(150, 80), Width = 100,
                BackColor = ColorTranslator.FromHtml("#ff8f8f"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            txtC = new TextBox() { Location = new Point(150, 110), Width = 100,
                BackColor = ColorTranslator.FromHtml("#ff8f8f"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            this.Controls.Add(txtA);
            this.Controls.Add(txtB);
            this.Controls.Add(txtC);

            Label lblA = new Label()
            {
                Text = "Külg A:",
                Location = new Point(50, 50),
                AutoSize = true,
                ForeColor = Color.Blue,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline)
            };

            Label lblB = new Label()
            {
                Text = "Külg B:",
                Location = new Point(50, 80),
                AutoSize = true,
                ForeColor = Color.Blue,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline)
            };

            Label lblC = new Label()
            {
                Text = "Külg C:",
                Location = new Point(50, 110),
                AutoSize = true,
                ForeColor = Color.Blue,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline)
            };


            Button btnOpenForm2 = new Button()
            {
                Text = "Arvuta Pindala (Põhi ja Kõrgus)",
                Location = new Point(450, 230),
                Width = 200,
                BackColor = Color.FromArgb(255, 255, 192),
                FlatStyle = FlatStyle.Flat
            };
            btnOpenForm2.Click += BtnOpenForm2_Click;
            this.Controls.Add(btnOpenForm2);

            


            this.Controls.Add(lblA);
            this.Controls.Add(lblB);
            this.Controls.Add(lblC);


            listView1 = new ListView
            {
                Location = new Point(50, 200),
                Width = 400,
                Height = 300,
                View = View.Details ,
                BackColor = ColorTranslator.FromHtml("#ff8f8f")
            };

            listView1.Columns.Add("Väli", 150);
            listView1.Columns.Add("Tähendus", 150);

            
            this.Controls.Add(listView1);

            pictureBox = new PictureBox
            {
                Size = new Size(180, 160),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(480, 400)
            };
            this.Controls.Add(pictureBox); 
        }

        private void BtnOpenForm2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void Run_button_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            try
            {
                a = Convert.ToDouble(txtA.Text);
                b = Convert.ToDouble(txtB.Text);
                c = Convert.ToDouble(txtC.Text);

                // Проверка на положительные значения сторон
                if (a <= 0 || b <= 0 || c <= 0)
                {
                    MessageBox.Show("Külg A, Külg B ja Külg C peavad olema positiivsed numbrid.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                


                Tringle triangle = new Tringle(a, b, c);

                listView1.Items.Add("Külg A");
                listView1.Items.Add("Külg B");
                listView1.Items.Add("Külg C");
                listView1.Items.Add("Perimeter");
                listView1.Items.Add("Pindala");
                listView1.Items.Add("Kõrgus");
                listView1.Items.Add("Kas on olemas?");
                listView1.Items.Add("Täpsustaja");

                listView1.Items[0].SubItems.Add(triangle.outputA());
                listView1.Items[1].SubItems.Add(triangle.outputB());
                listView1.Items[2].SubItems.Add(triangle.outputC());
                listView1.Items[3].SubItems.Add(Convert.ToString(triangle.Perimeter()));
                listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Surface()));
                listView1.Items[5].SubItems.Add(Convert.ToString(triangle.Height()));
                listView1.Items[6].SubItems.Add(triangle.ExistTriange ? "Olemas" : "Ei ole olemas");

                string triangleType = GetTriangleType(triangle);
                listView1.Items[7].SubItems.Add(triangleType);

                int imgIndex = GetImageIndex(triangleType);

                // Обновляем изображение в PictureBox
                pictureBox.Image = img[imgIndex];

                Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Palun sisestage korrektne number!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private string GetTriangleType(Tringle triangle)
        {
            // Если треугольник не существует
            if (!triangle.ExistTriange)
                return "Ei ole kolmnurk"; 

            // Если все стороны равны - равносторонний
            if (triangle.a == triangle.b && triangle.b == triangle.c)
                return "Võrgkülgne"; 

            // Если хотя бы две стороны равны - равнобедренный
            if (triangle.a == triangle.b || triangle.b == triangle.c || triangle.a == triangle.c)
                return "Võrdhaarane"; 

            // Если треугольник не равносторонний и не равнобедренный, классифицируем по углам
            return ClassifyByAngles(triangle);
        }

        private string ClassifyByAngles(Tringle triangle)
        {
            double aSquared = triangle.a * triangle.a;
            double bSquared = triangle.b * triangle.b;
            double cSquared = triangle.c * triangle.c;

            // Прямоугольный треугольник (по теореме Пифагора)
            if (aSquared + bSquared == cSquared || aSquared + cSquared == bSquared || bSquared + cSquared == aSquared)
            {
                return "Täisnurkne";
            }
            // Остроугольный треугольник (все углы острые)
            else if (aSquared + bSquared > cSquared &&
                     aSquared + cSquared > bSquared &&
                     bSquared + cSquared > aSquared)
            {
                return "Teravnurkne";
            }
            // Тупоугольный треугольник (один угол тупой)
            else
            {
                return "Nürinurkne";
            }
        }

        // Метод для получения индекса изображения для типа треугольника
        private int GetImageIndex(string triangleType)
        {
            switch (triangleType)
            {
                case "Võrgkülgne":
                    return 0;
                case "Teravnurkne":
                    return 1;
                case "Täisnurkne":
                    return 2;
                case "Võrdhaarane":
                    return 3;
                case "Nürinurkne":
                    return 4;
                default:
                    return -1; // Если нет такого типа
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
