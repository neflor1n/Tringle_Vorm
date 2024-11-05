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
    public partial class Form2 : Form
    {
        Button btn;
        ListView listView;
        System.Drawing.Image[] img;
        double h, a;
        TextBox txtA, txtH;
        public Form2() 
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
            btn.Click += Run_button_Click; ;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(255, 255, 192);
            btn.ForeColor = Color.Black;
            btn.FlatAppearance.BorderSize = 3;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.Controls.Add(btn);

            txtA = new TextBox()
            {
                Location = new Point(150, 50),
                Width = 100,
                BackColor = ColorTranslator.FromHtml("#ff8f8f"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            txtH = new TextBox()
            {
                Location = new Point(150, 80),
                Width = 100,
                BackColor = ColorTranslator.FromHtml("#ff8f8f"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            this.Controls.Add(txtH);
            this.Controls.Add(txtA);

            Label lblA = new Label()
            {
                Text = "Külg A:",
                Location = new Point(50, 50),
                AutoSize = true,
                ForeColor = Color.Blue,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline)
            };
            Label lblH = new Label()
            {
                Text = "Kõrgus:",
                Location = new Point(50, 80),
                AutoSize = true,
                ForeColor = Color.Blue,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Underline)
            };
            this.Controls.Add(lblA);
            this.Controls.Add(lblH);

            listView = new ListView
            {
                Location = new Point(50, 200),
                Width = 400,
                Height = 300,
                View = View.Details,
                BackColor = ColorTranslator.FromHtml("#ff8f8f")
            };

            listView.Columns.Add("Väli", 150);
            listView.Columns.Add("Tähendus", 150);
            this.Controls.Add(listView);
        }

        private void Run_button_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();

            try
            {
                a = Convert.ToDouble(txtA.Text);
                h = Convert.ToDouble(txtH.Text);

                if (a <= 0 || h <= 0)
                {
                    MessageBox.Show("Külg A ja Kõrgus peavad olema positiivsed numbrid.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Tringle triangle = new Tringle(a, h);

                listView.Items.Add("Külg A");
                listView.Items.Add("Kõrgus");
                listView.Items.Add("Pindala");

                listView.Items[0].SubItems.Add(triangle.outputA());
                listView.Items[1].SubItems.Add(triangle.outputH());
                listView.Items[2].SubItems.Add(triangle.Surface2().ToString("F2")); // Площадь с 2 знаками после запятой

                Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Palun sisestage korrektne number!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
