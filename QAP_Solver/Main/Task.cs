using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public class Task
    {
        int n;
        List<List<double>> distance = new List<List<double>>();
        List<List<double>> cost = new List<List<double>>();

        public List<List<double>> GetDistance()
        {
            return distance;
        }
        public List<List<double>> GetCost()
        {
            return cost;
        }
        public int GetN()
        {
            return n;
        }
        public Task GetTask()
        {
            return this;
        }
        public Task(int n)
        {
            this.n = n;
            this.distance = new List<List<double>>();
            this.cost = new List<List<double>>();

            // Инициализируем матрицы расстояний и стоимостей
            for (int i = 0; i < n; i++)
            {
                distance.Add(new List<double>());
                cost.Add(new List<double>());
                for (int j = 0; j < n; j++)
                {
                    distance[i].Add(0.0); // Заполняем начально нулями
                    cost[i].Add(0.0);
                }
            }
        }
        public void RandomTask()
        {
            Random rand = new Random();

            // Генерация случайных матриц расстояний и стоимостей
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        // Генерируем случайное расстояние
                        distance[i][j] = Math.Round(rand.NextDouble() * 100.0, 3); // Пример: случайное число от 0 до 100

                        // Генерируем случайную стоимость
                        cost[i][j] = Math.Round(rand.NextDouble() * 10.0, 3); // Пример: случайное число от 0 до 10
                    }
                }
            }
        }
        public void ReadTaskFromFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Считываем значение n
                    string firstLine = sr.ReadLine();
                    if (firstLine == null)
                    {
                        MessageBox.Show("Invalid file format: missing value of n.");
                        return;
                    }

                    if (!int.TryParse(firstLine.Trim(), out n))
                    {
                        MessageBox.Show("Invalid value of n in the file.");
                        return;
                    }

                    InitializeMatrices();

                    // Считываем матрицу расстояний
                    for (int i = 0; i < n; i++)
                    {
                        string[] distanceValues = sr.ReadLine().Split();
                        for (int j = 0; j < n; j++)
                        {
                            distance[i][j] = Convert.ToDouble(distanceValues[j]);
                        }
                    }

                    // Считываем матрицу стоимостей
                    for (int i = 0; i < n; i++)
                    {
                        string[] costValues = sr.ReadLine().Split();
                        for (int j = 0; j < n; j++)
                        {
                            cost[i][j] = Convert.ToDouble(costValues[j]);
                        }
                    }
                }

                MessageBox.Show("Task read successfully from file.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading task from file: {ex.Message}");
            }
        }
        private void InitializeMatrices()
        {
            // Инициализируем матрицы расстояний и стоимостей с новым значением n
            distance = new List<List<double>>();
            cost = new List<List<double>>();

            for (int i = 0; i < n; i++)
            {
                distance.Add(new List<double>());
                cost.Add(new List<double>());
                for (int j = 0; j < n; j++)
                {
                    distance[i].Add(0.0); // Заполняем начально нулями
                    cost[i].Add(0.0);
                }
            }
        }
        public void DrawTask(PictureBox pictureBox, Task task)
        {
            int cityRadius = 10; // Радиус круга (города)

            // Получаем размерности задачи
            int n = task.GetN();
            List<List<double>> distance = task.GetDistance();
            List<List<double>> cost = task.GetCost(); // Матрица стоимостей

            // Проверяем, что расстояние и размерность задачи корректны
            if (n <= 0 || distance.Count != n || distance.Any(row => row.Count != n) ||
                cost.Count != n || cost.Any(row => row.Count != n))
            {
                MessageBox.Show("Некорректные данные для отрисовки задачи.");
                return;
            }

            // Очищаем изображение PictureBox
            pictureBox.Image?.Dispose();
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);

            // Список координат для каждого города
            List<Point> cityCoordinates = new List<Point>();

            // Генерируем случайные координаты для каждого города (локации)
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = random.Next(cityRadius, pictureBox.Width - cityRadius);
                int y = random.Next(cityRadius, pictureBox.Height - cityRadius);
                cityCoordinates.Add(new Point(x, y));
            }
            // Отрисовка ребер (линий) между городами с учетом матрицы расстояний и стоимостей
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    // Расстояние между городами i и j
                    double dist = distance[i][j];

                    // Стоимость между городами i и j
                    double costValue = cost[i][j];

                    // Координаты центров городов i и j
                    Point cityCenter1 = cityCoordinates[i];
                    Point cityCenter2 = cityCoordinates[j];

                    // Вычисляем толщину линии на основе стоимости
                    float penWidth = (float)(costValue * 0.5); // Пропорциональная толщина линии

                    // Определяем цвет линии (чем выше стоимость, тем краснее)
                    Color lineColor = Color.FromArgb((int)(costValue * 10), 0, 0); // Пропорциональный красный цвет

                    using (Pen pen = new Pen(lineColor))
                    {
                        // Рисуем линию (ребро) между городами i и j
                        g.DrawLine(pen, cityCenter1, cityCenter2);
                    }
                }
            }

            // Отрисовка городов (кругов) на PictureBox
            for (int i = 0; i < n; i++)
            {
                Point cityCenter = cityCoordinates[i];

                // Рисуем круг (город)
                g.FillEllipse(Brushes.BlueViolet, cityCenter.X - cityRadius, cityCenter.Y - cityRadius, cityRadius * 2, cityRadius * 2);

                // Номер города (локации)
                string cityNumber = (i + 1).ToString();
                SizeF textSize = g.MeasureString(cityNumber, pictureBox.Font);
                g.DrawString(cityNumber, pictureBox.Font, Brushes.White, cityCenter.X - textSize.Width / 2, cityCenter.Y - textSize.Height / 2);
            }

            

            // Устанавливаем изображение в PictureBox
            pictureBox.Image = bitmap;
        }
        public void DisplayTaskInfo(Task task, TextBox textBox)
        {
            StringBuilder sb = new StringBuilder();

            // Размерность задачи
            int n = task.GetN();
            sb.AppendLine($"Dimension (n): {n}");
            sb.AppendLine();

            // Матрица расстояний
            sb.AppendLine("Distance Matrix:");
            List<List<double>> distance = task.GetDistance();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.AppendFormat("{0,8:F2} ", distance[i][j]); // Форматированный вывод с двумя знаками после запятой
                }
                sb.AppendLine();
            }
            sb.AppendLine();

            // Матрица стоимостей
            sb.AppendLine("Cost Matrix:");
            List<List<double>> cost = task.GetCost();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.AppendFormat("{0,8:F2} ", cost[i][j]); // Форматированный вывод с двумя знаками после запятой
                }
                sb.AppendLine();
            }

            // Выводим сформированную информацию в TextBox
            textBox.Text = sb.ToString();
        }
    }
}