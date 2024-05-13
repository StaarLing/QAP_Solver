﻿using System;
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

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
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
            Task qapTask = new Task(dimension);
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
    }
}
