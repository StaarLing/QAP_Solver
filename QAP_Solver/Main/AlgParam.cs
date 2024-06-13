using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Main
{
    internal class AlgParam
    {
        Dictionary<int, Dictionary<string, double>> param = new Dictionary<int, Dictionary<string, double>>();

        public AlgParam()
        {
            Dictionary<string, double> parametersAlgorithm0 = new Dictionary<string, double>
            {
                { "Алгоритм серых волков", 1 },
                { "Кол-во волков", 240 },
                { "Кол-во итераций", 600 },
            };
            param.Add(0, parametersAlgorithm0);

            Dictionary<string, double> parametersAlgorithm1 = new Dictionary<string, double>
            {
                { "Алгоритм искусственной пчелиной колонии", 1 },
                { "Размер колонии", 80 },
                { "Кол-во итераций", 600 },
                { "Кол-во попыток", 8 }
            };
            param.Add(1, parametersAlgorithm1);

            Dictionary<string, double> parametersAlgorithm2 = new Dictionary<string, double>
            {
                { "Алгоритм кукушки", 1 },
                { "Кол-во гнезд", 150 },
                { "Кол-во итераций", 600 },
                { "Размер шага", 2 },
                { "Доля заброшенных гнезд", 0.4 }
            };
            param.Add(2, parametersAlgorithm2);

            Dictionary<string, double> parametersAlgorithm3 = new Dictionary<string, double>
            {
                { "Алгоритм муравьиного льва", 1 },
                { "Размер колонии", 140 },
                { "Кол-во итераций", 600 }
            };
            param.Add(3, parametersAlgorithm3);

            Dictionary<string, double> parametersAlgorithm4 = new Dictionary<string, double>
            {
                { "Алгоритм роя ласточек", 1 },
                { "Размер колонии", 150 },
                { "Кол-во групп", 50 },
                { "Кол-во итераций", 600 }
            };
            param.Add(4, parametersAlgorithm4);

            Dictionary<string, double> parametersAlgorithm5 = new Dictionary<string, double>
            {
                { "Алгоритм инвазивных сорняков", 1 },
                { "Начальное кол-во семян", 100 },
                { "Начальное кол-во растений", 140 },
                { "Макс. кол-во растений в колонии", 300 },
                { "Мин. кол-во растений в колонии", 60 },
                { "Макс. коэф. модуляции", 0.9 },
                { "Мин. коэф. модуляции", 0.2 },
                { "Кол-во итераций", 600 }
            };
            param.Add(5, parametersAlgorithm5);

            Dictionary<string, double> parametersAlgorithm6 = new Dictionary<string, double>
            {
                { "Муравьиный алгоритм", 1 },
                { "Кол-во муравьев", 40.0 },
                { "Коэф. влияния феромона", 1.0 },
                { "Коэф. влияния расстояния", 2.0 },
                { "Коэф. испарения феромона", 0.5 },
                { "Коэф. отложения феромона", 0.5 },
                { "Кол-во итераций", 600.0 }
            };
            param.Add(6, parametersAlgorithm6);

            Dictionary<string, double> parametersAlgorithm7 = new Dictionary<string, double>
            {
                { "Бактериальный алгоритм поиска", 1 },
                { "Кол-во бактерий в популяции", 45.0 },
                { "Кол-во шагов хемотаксиса", 600 },
                { "Кол-во шагов размножения", 2.0 },
                { "Кол-во шагов устр./расс.", 3 },
                { "Вероятность устр./расс.", 0.25 }
            };
            param.Add(7, parametersAlgorithm7);

            Dictionary<string, double> parametersAlgorithm8 = new Dictionary<string, double>
            {
                { "Алгоритм светлячков", 1 },
                { "Кол-во светлячков", 20 },
                { "Альфа", 0.623 },
                { "Бетта", 0.642 },
                { "Гамма", 0.534 },
                { "Кол-во итераций", 600 }
            };
            param.Add(8, parametersAlgorithm8);

            Dictionary<string, double> parametersAlgorithm9 = new Dictionary<string, double>
            {
                { "Алгоритм куриной стаи", 1 },
                { "Размер популяции", 60 },
                { "Количество туров", 3 },
                { "Доля петухов", 0.15 },
                { "Доля кур", 0.3 },
                { "Фактор обучения петухов", 0.9 },
                { "Фактор обучения кур", 0.5 },
                { "Фактор самообучения", 0.6 },
                { "Кол-во итераций", 600 }
            };
            param.Add(9, parametersAlgorithm9);
        }
        public Dictionary<string, double> GetParameters(int algorithmIndex)
        {
            if (param.ContainsKey(algorithmIndex))
            {
                return param[algorithmIndex];
            }
            else
            {
                return new Dictionary<string, double>();
            }
        }
        public void UpdateParameter(int algorithmIndex, string paramName, double value)
        {
            if (param.ContainsKey(algorithmIndex))
            {
                // Если параметр существует, обновляем его значение
                param[algorithmIndex][paramName] = value;
            }
            else
            {
                // Если параметр не существует, добавляем его
                param.Add(algorithmIndex, new Dictionary<string, double> { { paramName, value } });
            }
        }
    }
}