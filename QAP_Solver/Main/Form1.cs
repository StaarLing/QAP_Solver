using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private async void button1_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver();
            List<int> alg = checkedListBoxAlg.CheckedIndices.Cast<int>().ToList();
            List<Solver> solvers = await solver.GetAlgAsync(alg, qapTask, algParam, label2, progressBar1);

            List<int> paramRes = checkedListBoxSolve.CheckedIndices.Cast<int>().ToList();
            Result result = new Result(solvers, paramRes);
            if (paramRes.Contains(2))
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                BruteForceSolver bruteForceSolver = new BruteForceSolver();
                Solver solv = bruteForceSolver.Solve(qapTask);
                timer.Stop();
                solv.Time = (timer.ElapsedMilliseconds);
                solv.NameAlg = "Полный перебор";
                result.solvers.Add(solv);
            }
            if (paramRes.Contains(0))
            {
                // Создаем новый экземпляр диалога выбора пути
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    // Устанавливаем заголовок диалога
                    folderDialog.Description = "Выберите папку для сохранения файла";

                    // Показываем диалог и проверяем результат
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Получаем выбранный пользователем путь
                        string selectedPath = folderDialog.SelectedPath;
                        result.Print(selectedPath);
                    }
                }
            }
            if(paramRes.Contains(1))
            {
                result.PlotSolversHistory(solvers);
            }
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
                qapTask = new Task(1);
                // Считываем задачу из выбранного файла
                qapTask.ReadTaskFromFile(filePath);

                qapTask.DrawTask(pictureBox1, qapTask);
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

            if (pictureBox1.Size != this.ClientSize)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Устанавливаем заголовок диалога
                folderDialog.Description = "Выберите папку для сохранения файла";

                // Показываем диалог и проверяем результат
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получаем выбранный пользователем путь
                    string selectedPath = folderDialog.SelectedPath;

                    qapTask.WriteTaskToFile("C:\\Users\\1\\Documents\\file.txt", qapTask);
                }
            }
        }
    }
}
