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
        PictureBox picBox;
        int gridSize = 20; // Размер сетки
        Point offset = new Point(700, 120); // Смещение для треугольника

        Point[] trianglePoints; // Массив для хранения вершин треугольника
        double a, b, c;



        public Form1()
        {
            this.Text = "Töö kolmnurkuga";
            this.Width = 1100;
            this.Height = 900;
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
                Location = new Point(50, 520)
            };
            this.Controls.Add(pictureBox);

            picBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            picBox.Paint += PictureBox_Paint;

            // Координаты вершин треугольника до применения смещения
            trianglePoints = new Point[]
            {
                new Point(0, 0),    // Вершина A
                new Point(100, 200), // Вершина B
                new Point(200, 0)    // Вершина C
            };

            // Принудительно перерисовать картинку при изменении размеров формы
            this.Resize += (sender, e) => pictureBox.Invalidate();

            Button btnMoveTriangle = new Button
            {
                Text = "Liiguta kolmnurk",
                Location = new Point(240, 520),
                Width = 150
            };
            btnMoveTriangle.Click += (s, e) => UpdateTrianglePosition(800, 200); // Пример нового смещения
            this.Controls.Add(picBox); 

            this.Controls.Add(btnMoveTriangle);
            btnMoveTriangle.BringToFront();




        }

        private void UpdateTrianglePosition(int offsetX, int offsetY)
        {
            offset = new Point(offsetX, offsetY); // Обновляем смещение
            picBox.Invalidate(); // Перерисовываем picBox
        }



        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Рисуем сетку
            DrawGrid(g, picBox.Width, picBox.Height, gridSize);

            // Выводим координаты точек для отладки
            Console.WriteLine($"Point A: {a}, Point B: {b}, Point C: {c}");

            // Определяем тип треугольника
            string triangleType = GetTriangleType(new Tringle(a, b, c));

            // Если треугольник равносторонний, пересчитываем его вершины
            if (triangleType == "Võrgkülgne")
            {
                trianglePoints = CalculateEquilateralTrianglePoints(a); // для равностороннего
            }
            else
            {
                // Вычисляем обычные координаты для произвольного треугольника
                trianglePoints = CalculateTrianglePoints(a, b, c).ToArray(); // Convert List to array
            }

            // Рисуем треугольник со смещением
            DrawTriangle(g, trianglePoints, offset, triangleType);
        }




        private void DrawGrid(Graphics g, int width, int height, int gridSize)
        {
            Pen gridPen = new Pen(Color.LightGray); // Перо
            for (int x = 0; x < width; x += gridSize) // от 0 до width с шагом gridSize
            {
                g.DrawLine(gridPen, x, 0, x, height); // Вертикальные линии
            }
            for (int y = 0; y < height; y += gridSize) // от 0 до height с шагом gridSize
            {
                g.DrawLine(gridPen, 0, y, width, y); // Горизонтальные линии
            }
        }
        private Point[] CalculateEquilateralTrianglePoints(double sideLength)
        {
            Point A = new Point(0, 0);
            Point B = new Point((int)sideLength, 0);
            double height = Math.Sqrt(3) / 2 * sideLength;
            Point C = new Point((int)(sideLength / 2), (int)height);

            return new Point[] { A, B, C };
        }

        private void DrawTriangle(Graphics g, Point[] points, Point offset, string triangleType)
        {
            if (points == null || points.Length != 3) return;

            Pen trianglePen;
            Brush vertexBrush = Brushes.Red;

            // Выбор цвета в зависимости от типа треугольника
            switch (triangleType)
            {
                case "Teravnurkne":
                    trianglePen = new Pen(Color.Green, 2); // Остроугольный
                    break;
                case "Täisnurkne":
                    trianglePen = new Pen(Color.Blue, 2); // Прямоугольный
                    break;
                case "Võrdhaarane":
                    trianglePen = new Pen(Color.Purple, 2); // Равнобедренный
                    break;
                case "Nürinurkne":
                    trianglePen = new Pen(Color.Orange, 2); // Тупоугольный
                    break;
                default:
                    trianglePen = new Pen(Color.Blue, 2); // Произвольный треугольник
                    break;
            }

            // Применяем смещение к каждой вершине треугольника
            Point[] offsetPoints = points.Select(p => new Point(p.X + offset.X, p.Y + offset.Y)).ToArray();

            // Рисуем треугольник
            g.DrawPolygon(trianglePen, offsetPoints);

            // Рисуем вершины треугольника
            foreach (Point point in offsetPoints)
            {
                g.FillEllipse(vertexBrush, point.X - 3, point.Y - 3, 6, 6);
            }
        }



        // метод, который выясняет тип триугольника
        private string DetermineTriangleType(double a, double b, double c)
        {
            // Проверка на существование треугольника
            if (a + b <= c || b + c <= a || c + a <= b)
            {
                throw new InvalidOperationException("Такой треугольник не существует.");
            }

            // Проверка на прямоугольный треугольник (по теореме Пифагора)
            if (Math.Abs(a * a + b * b - c * c) < 0.0001 ||
                Math.Abs(b * b + c * c - a * a) < 0.0001 ||
                Math.Abs(c * c + a * a - b * b) < 0.0001)
            {
                return "Täisnurkne";
            }

            // Проверка на остроугольный треугольник
            if (a * a + b * b > c * c && b * b + c * c > a * a && c * c + a * a > b * b)
            {
                return "Teravnurkne";
            }

            // Проверка на тупоугольный треугольник
            if (a * a + b * b < c * c || b * b + c * c < a * a || c * c + a * a < b * b)
            {
                return "Nürinurkne";
            }

            // Проверка на равнобедренный треугольник
            if (a == b || b == c || c == a)
            {
                return "Võrdhaarane";
            }

            // Если треугольник не совпадает с вышеуказанными типами, считаем его произвольным
            return "Võrgkülgne";
        }



        private Point[] CalculateTrianglePoints(double a, double b, double c)
        {
            Tringle tringle = new Tringle(a, b, c);
            List<Point> points = new List<Point>();

            if (!tringle.ExistTriange) // Проверяем существование треугольника
            {
                return points.ToArray(); // Возвращаем пустой массив, если треугольник не существует
            }

            // Координаты первой вершины (0, 0)
            double x1 = 0, y1 = 0;
            points.Add(new Point((int)x1, (int)y1));

            // Координаты второй вершины (a, 0)
            double x2 = a, y2 = 0;
            points.Add(new Point((int)x2, (int)y2));

            // Вычисление координат третьей вершины
            double angle = Math.Acos((Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b));
            double x3 = b * Math.Cos(angle);
            double y3 = b * Math.Sin(angle);
            points.Add(new Point((int)x3, (int)y3));

            // Масштабируем треугольник, чтобы он был видим в пределах окна
            double scaleFactor = 10; // Масштабирование (например, 10 вместо большого значения)

            // Добавим смещение, чтобы треугольник был в видимой области
            int offsetX = 100; // Смещение по X
            int offsetY = 100; // Смещение по Y

            for (int i = 0; i < points.Count; i++)
            {
                // Масштабируем и сдвигаем точки
                points[i] = new Point(
                    (int)(points[i].X * scaleFactor) + offsetX,
                    (int)(points[i].Y * scaleFactor) + offsetY
                );
            }

            return points.ToArray(); // Преобразуем список в массив
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

                Tringle triangle = new Tringle(a, b, c);

                if (a <= 0 || b <= 0 || c <= 0)
                {
                    MessageBox.Show("Külg A, Külg B ja Külg C peavad olema positiivsed numbrid.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!triangle.ExistTriange)
                {
                    MessageBox.Show("Selline kolmnurk ei saa eksisteerida!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Рисуем треугольник
                trianglePoints = CalculateTrianglePoints(a, b, c).ToArray();


                // Выводим данные в список
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
                pictureBox.Image = img[imgIndex];

                picBox.Invalidate();
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
                return "Võrgkülgne"; // Равносторонний

            // Если хотя бы две стороны равны - равнобедренный
            if (triangle.a == triangle.b || triangle.b == triangle.c || triangle.a == triangle.c)
                return "Võrdhaarane"; // Равнобедренный

            // Если треугольник не равносторонний и не равнобедренный, классифицируем по углам
            return ClassifyByAngles(triangle); 
        }


        private string ClassifyByAngles(Tringle triangle)
        {
            double aSquared = triangle.a * triangle.a;
            double bSquared = triangle.b * triangle.b;
            double cSquared = triangle.c * triangle.c;

            // Прямоугольный треугольник (по теореме Пифагора)
            if (Math.Abs(aSquared + bSquared - cSquared) < 0.0001 ||
                Math.Abs(bSquared + cSquared - aSquared) < 0.0001 ||
                Math.Abs(cSquared + aSquared - bSquared) < 0.0001)
            {
                return "Täisnurkne"; // Прямоугольный
            }

            // Остроугольный треугольник
            if (aSquared + bSquared > cSquared && bSquared + cSquared > aSquared && cSquared + aSquared > bSquared)
            {
                return "Teravnurkne"; // Остроугольный
            }

            // Тупоугольный треугольник
            return "Nürinurkne"; // Тупоугольный
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
