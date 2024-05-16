using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        Point picLoc;
        Size picSize;
        Task qapTask;
        public Form1()
        {
            InitializeComponent();
            picLoc = pictureBox1.Location;
            picSize = pictureBox1.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solver solver= new Solver();
            List<int> a = checkedListBoxAlg.CheckedIndices.Cast<int>().ToList();
            solver.GetAlg(a, qapTask);
        }

        private void GenTask_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите размерность задачи (целое число):", "Размерность задачи", "10");

            // Преобразуем введенный текст в целое число (размерность)
            if (!int.TryParse(input, out int dimension) || dimension <= 0)
            {
                MessageBox.Show("Некорректная размерность задачи.");
                return;
            }
            qapTask = new Task(dimension);
            qapTask.RandomTask();

            qapTask.DrawTask(pictureBox1, qapTask);
            qapTask.DisplayTaskInfo(qapTask, textBoxTask);
        }

        private void UpTask_Click(object sender, EventArgs e)
        {
            // Устанавливаем фильтр для диалога выбора файла (только текстовые файлы, например)
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Показываем диалоговое окно для выбора файла
            DialogResult result = openFileDialog1.ShowDialog();

            // Проверяем, был ли выбран файл
            if (result == DialogResult.OK)
            {
                // Получаем путь к выбранному файлу
                string filePath = openFileDialog1.FileName;

                // Создаем экземпляр задачи QAP
                Task qapTask = new Task(1); // Например, указываем размерность задачи

                // Считываем задачу из выбранного файла
                qapTask.ReadTaskFromFile(filePath);

                qapTask.DrawTask(pictureBox1,qapTask);
                qapTask.DisplayTaskInfo(qapTask, textBoxTask);
            }
        }

        private void groupBoxAlg_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripMenuAboutQAP_Click(object sender, EventArgs e)
        {
            try
            {
                // Укажите ссылку на страницу, которую вы хотите открыть
                string url = "https://en.wikipedia.org/wiki/Quadratic_assignment_problem";

                // Открываем ссылку в стандартном веб-браузере
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии ссылки: {ex.Message}");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = radioButton1.Checked;
            textBoxTask.Visible = radioButton2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = radioButton1.Checked;
            textBoxTask.Visible = radioButton2.Checked;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

            if(pictureBox1.Size != this.ClientSize)
            {
                pictureBox1.Size = this.ClientSize;
                pictureBox1.Location = new Point(0, 0);
            }
            else
            {
                pictureBox1.Size = picSize;
                pictureBox1.Location = picLoc;
            }
        }


    }
}
