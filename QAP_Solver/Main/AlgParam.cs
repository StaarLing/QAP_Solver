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
                { "Кол-во волков", 10 },
                { "Кол-во итераций", 100 },
            };
            param.Add(0, parametersAlgorithm0);

            Dictionary<string, double> parametersAlgorithm1 = new Dictionary<string, double>
            {
                { "Алгоритм искусственной пчелиной колонии", 1 },
                { "Размер колонии", 10 },
                { "Кол-во итераций", 100 },
                { "Кол-во попыток", 3 }
            };
            param.Add(1, parametersAlgorithm1);

            Dictionary<string, double> parametersAlgorithm2 = new Dictionary<string, double>
            {
                { "Алгоритм кукушки", 1 },
                { "Кол-во гнезд", 10 },
                { "Кол-во итераций", 100 },
                { "Размер шага", 1.0 },
                { "Доля заброшенных гнезд", 0.1 }
            };
            param.Add(2, parametersAlgorithm2);

            Dictionary<string, double> parametersAlgorithm3 = new Dictionary<string, double>
            {
                { "Алгоритм муравьиного льва", 1 },
                { "Размер колонии", 100 },
                { "Кол-во итераций", 100 }
            };
            param.Add(3, parametersAlgorithm3);

            Dictionary<string, double> parametersAlgorithm4 = new Dictionary<string, double>
            {
                { "Алгоритм роя ласточек", 1 },
                { "Размер колонии", 10 },
                { "Кол-во групп", 10 },
                { "Кол-во итераций", 100 }
            };
            param.Add(4, parametersAlgorithm4);

            Dictionary<string, double> parametersAlgorithm5 = new Dictionary<string, double>
            {
                { "Алгоритм инвазивных сорняков", 1 },
                { "Начальное кол-во растений", 10 },
                { "макс. кол-во растений в колонии", 1.5 },
                { "Коэф. модуляции", 2.5 },
                { "Кол-во итераций", 100 }
            };
            param.Add(5, parametersAlgorithm5);

            Dictionary<string, double> parametersAlgorithm6 = new Dictionary<string, double>
            {
                { "Муравьиный алгоритм", 1 },
                { "Вес следа феромона", 1.0 },
                { "Вес видимости", 2.0 },
                { "Скорость испарения феромона", 0.5 },
                { "Кол-во феромона", 10.0 },
                { "Кол-во муравьев", 50.0 },
                { "Кол-во итераций", 100.0 }
            };
            param.Add(6, parametersAlgorithm6);

            Dictionary<string, double> parametersAlgorithm7 = new Dictionary<string, double>
            {
                { "Бактериальный алгоритм поиска", 1 },
                { "Кол-во бактерий в популяции", 50.0 },
                { "Кол-во шагов хемотаксиса", 100.0 },
                { "Кол-во шагов размножения", 20.0 },
                { "Кол-во шагов устр./расс.", 10.0 },
                { "Вероятность устр./расс.", 0.1 },
                { "Кол-во итераций", 10.0 }
            };
            param.Add(7, parametersAlgorithm7);

            Dictionary<string, double> parametersAlgorithm8 = new Dictionary<string, double>
            {
                { "Алгоритм светлячков", 1 },
                { "Параметр случайной вариации", 0.25 },
                { "Привлекательность", 1.0 },
                { "Изменчивость привлекательности", 0.97 },
                { "Кол-во итераций", 10.0 },
                { "Кол-во светлячков", 20.0 }
            };
            param.Add(8, parametersAlgorithm8);

            Dictionary<string, double> parametersAlgorithm9 = new Dictionary<string, double>
            {
                { "Алгоритм куриной стаи", 1 },
                { "Размер популяции", 500 },
                { "Доля петухов", 0.1 },
                { "Доля кур", 0.21 },
                { "Доля цыплят", 0.69 },
                { "Количество туров", 2 },
                { "Фактор обучения петухов", 0.7 },
                { "Фактор обучения кур", 0.4 },
                { "Фактор самообучения", 0.5 }
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