using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tringle_Vorm
{
    public partial class Form1 : Form
    {
        Button btn;
        ListView listView1;
        TextBox txtA, txtB, txtC;
        double a, b, c;
        public Form1()
        {
            InitializeComponent();


            this.Text = "Töö kolmnurkuga";
            this.Width = 700;
            this.Height = 600;
            btn = new Button();
            btn.Text = "Alusta";
            btn.Height = 50;
            btn.Width = 150;
            btn.Location = new Point(450, 150);
            btn.Click += Run_button_Click;

            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(255, 255, 192);

            btn.ForeColor = Color.Black;
            btn.FlatAppearance.BorderSize = 3;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.Controls.Add(btn);



            txtA = new TextBox() { Location = new Point(150, 50), Width = 100 };
            txtB = new TextBox() { Location = new Point(150, 80), Width = 100 };
            txtC = new TextBox() { Location = new Point(150, 110), Width = 100 };
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
            this.Controls.Add(lblA);
            this.Controls.Add(lblB);
            this.Controls.Add(lblC);


            listView1 = new ListView
            {
                Location = new Point(50, 200),
                Width = 400,
                Height = 200,
                View = View.Details
            };

            listView1.Columns.Add("Väli", 150);
            listView1.Columns.Add("Tähendus", 150);

            this.Controls.Add(listView1);

        }

        private void Run_button_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            try
            {
                a = Convert.ToDouble(txtA.Text);
                b = Convert.ToDouble(txtB.Text);
                c = Convert.ToDouble(txtC.Text);

                
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
                listView1.Items.Add("Ruut");
                listView1.Items.Add("Kas on olemas?");
                listView1.Items.Add("Täpsustaja");

                listView1.Items[0].SubItems.Add(triangle.outputA());
                listView1.Items[1].SubItems.Add(triangle.outputB());
                listView1.Items[2].SubItems.Add(triangle.outputC());
                listView1.Items[3].SubItems.Add(Convert.ToString(triangle.Perimeter()));
                listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Surface()));
                listView1.Items[5].SubItems.Add(triangle.ExistTriange ? "Olemas" : "Ei ole olemas");

               
                listView1.Items[6].SubItems.Add(GetTriangleType(triangle));

                Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Palun sisestage korrektne number!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTriangleType(Tringle triangle)
        {
            if (!triangle.ExistTriange)
                return "Ei ole kolmnurk"; 
           
            if (triangle.a == triangle.b && triangle.b == triangle.c)
                return "Võrgkülgne"; 
            else if (triangle.a == triangle.b || triangle.b == triangle.c || triangle.a == triangle.c)
                return "Võrdhaarane";
            else
                return ClassifyByAngles(triangle); 

        }

        private string ClassifyByAngles(Tringle triangle)
        {
            double aSquared = triangle.a * triangle.a;
            double bSquared = triangle.b * triangle.b;
            double cSquared = triangle.c * triangle.c;

            if (aSquared + bSquared > cSquared &&
                aSquared + cSquared > bSquared &&
                bSquared + cSquared > aSquared)
            {
                return "Teravnurkne"; 
            }
            else if (aSquared + bSquared == cSquared ||
                     aSquared + cSquared == bSquared ||
                     bSquared + cSquared == aSquared)
            {
                return "Täisnurkne"; 
            }
            else
            {
                return "Nürinurkne"; 
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

  

