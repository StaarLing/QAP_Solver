using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        private List<string> selectedItems = new List<string>();
        private AlgParam algParam;

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string selectedItem = checkedListBox1.Items[e.Index].ToString();

            if (e.NewValue == CheckState.Checked) // Если элемент был отмечен
            {
                if (!selectedItems.Contains(selectedItem))
                {
                    selectedItems.Add(selectedItem); // Добавляем в список выбранных элементов
                }
            }
            else // Если элемент был снят с выбора
            {
                if (selectedItems.Contains(selectedItem))
                {
                    selectedItems.Remove(selectedItem); // Удаляем из списка выбранных элементов
                }
            }
            CreateTabsFromSelectedItems();
        }
        private void CreateTabsFromSelectedItems()
        {
            tabControl1.TabPages.Clear(); // Очищаем все существующие вкладки
            algParam = new AlgParam();

            foreach (string selectedItem in selectedItems)
            {
                // Получаем параметры для выбранного алгоритма (если они есть)
                int algorithmIndex = checkedListBox1.Items.IndexOf(selectedItem);
                Dictionary<string, double> parameters = algParam.GetParameters(algorithmIndex);

                if (parameters != null && parameters.Count > 0)
                {
                    // Создаем новую вкладку с именем выбранного элемента
                    TabPage newTab = new TabPage(selectedItem);

                    int verticalOffset = 10; // Начальное смещение по вертикали для размещения элементов

                    foreach (var kvp in parameters)
                    {
                        string parameterName = kvp.Key;
                        double defaultValue = kvp.Value;

                        // Создаем Label для отображения названия параметра
                        Label label = new Label();
                        label.Text = $"{parameterName}:";
                        label.AutoSize = true;
                        label.Location = new Point(10, verticalOffset); // Задаем расположение Label на вкладке

                        // Создаем TextBox для ввода значения параметра
                        TextBox textBox = new TextBox();
                        textBox.Multiline = true;
                        textBox.Location = new Point(label.Right-10, verticalOffset); // Располагаем TextBox справа от Label
                        textBox.Text = defaultValue.ToString(); // Устанавливаем значение по умолчанию

                        // Добавляем Label и TextBox на вкладку
                        newTab.Controls.Add(label);
                        newTab.Controls.Add(textBox);

                        // Увеличиваем вертикальное смещение для следующего элемента
                        verticalOffset += label.Height + 10; // Учитываем высоту Label и добавляем отступ
                    }

                    // Добавляем вкладку в TabControl
                    tabControl1.TabPages.Add(newTab);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool selectAll = checkBox2.Checked;

            // Устанавливаем или снимаем отметки со всех элементов в checkedListBox1
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, selectAll);
            }

            // Если флаг включен и выбраны все элементы, обновляем вкладки
            if (selectAll)
            {
                UpdateTabsFromCheckedListBox();
            }
        }
        private void UpdateTabsFromCheckedListBox()
        {
            selectedItems.Clear();

            // Добавляем все выбранные элементы в список
            foreach (string selectedItem in checkedListBox1.CheckedItems)
            {
                selectedItems.Add(selectedItem);
            }

            // Создаем вкладки на основе выбранных элементов
            CreateTabsFromSelectedItems();
        }
    }
}
