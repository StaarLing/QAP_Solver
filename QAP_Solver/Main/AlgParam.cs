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
                { "Кол-во волков", 10 },
                { "Кол-во итераций", 100 },
            };
            param.Add(0, parametersAlgorithm0);

            Dictionary<string, double> parametersAlgorithm1 = new Dictionary<string, double>
            {
                { "Размер колонии", 10 },
                { "Кол-во итераций", 100 },
                { "Кол-во попыток", 3 }
            };
            param.Add(1, parametersAlgorithm1);
            Dictionary<string, double> parametersAlgorithm2 = new Dictionary<string, double>
            {
               { "Кол-во гнезд", 10 },
               { "Кол-во итераций", 100 },
               { "Размер шага", 1.0 },      // Размер шага (Step size) при перемещении
               { "Lambda", 1.5 },        // Параметр Lambda для распределения Levy flights
               { "Доля заброшенных гнезд", 0.1 }    // Доля гнёзд, заброшенных после каждой итерации
            };
            param.Add(2, parametersAlgorithm2);

            Dictionary<string, double> parametersAlgorithm3 = new Dictionary<string, double>
            {
                { "Размер колонии", 100 },
                { "Кол-во итераций", 100 }
            };
            param.Add(3, parametersAlgorithm3);
            Dictionary<string, double> parametersAlgorithm4 = new Dictionary<string, double>
            {
                { "Размер колонии", 10 },
                { "a_HL", 1.0 },
                { "a_HH", 2.0 },
                { "b_HL", 3.0 },
                { "b_LL", 4.0 },
                { "Кол-во итераций", 100 }

            };
            param.Add(4, parametersAlgorithm4);

            Dictionary<string, double> parametersAlgorithm5 = new Dictionary<string, double>
            {
                { "Начальное кол-во растений", 10 },
                { "макс. кол-во растений в колонии", 1.5 },
                { "Коэф. модуляции", 2.5 },
                { "Кол-во итераций", 100 }
            };
            param.Add(5, parametersAlgorithm5);
            Dictionary<string, double> parametersAlgorithm6 = new Dictionary<string, double>
            {
                { "Вес следа феромона", 1.0 },       // Вес следа феромона (alpha)
                { "Вес видимости", 2.0 },             // Вес видимости (beta)
                { "Скорость испарения феромона", 0.5 }, // Скорость испарения феромона (rho)
                { "Кол-во феромона", 10.0 }, // Количество феромона, добавляемого муравьем (Q)
                { "Кол-во муравьев", 50.0 },         // Количество муравьев (m)
                { "Максимальное количество итераций", 100.0 }
            };
            param.Add(6, parametersAlgorithm6);

            Dictionary<string, double> parametersAlgorithm7 = new Dictionary<string, double>
            {
                { "Кол-во бактерий в популяции", 50.0 },     // Количество бактерий в популяции (S)
                { "Кол-во шагов хемотаксиса", 100.0 },   // Количество шагов хемотаксиса для каждой бактерии (Nc)
                { "Кол-во шагов размножения", 20.0 },     // Количество шагов размножения (Nre)
                { "Кол-во шагов устр./расс.", 10.0 },   // Количество шагов устранения и рассеивания (Ned)
                { "Вероятность устр./расс.", 0.1 }
            };
            param.Add(7, parametersAlgorithm7);
            Dictionary<string, double> parametersAlgorithm8 = new Dictionary<string, double>
            {
                { "Параметр случайной вариации", 0.25 },                      // Параметр случайной вариации (default 0.25)
                { "Привлекательность", 1.0 },                        // Привлекательность при расстоянии = 0 (default 1)
                { "Изменчивость привлекательности", 0.97 },                      // Характеризует изменчивость привлекательности (default 0.97)
                { "Кол-во итераций", 10.0 },        // Количество итераций выполнения (default 10)
                { "Кол-во светлячков", 20.0 }
            };
            param.Add(8, parametersAlgorithm8);

            Dictionary<string, double> parametersAlgorithm9 = new Dictionary<string, double>
            {
                { "Размер популяции", 500 },                    // Размер популяции (population size)
                { "Доля петухов", 0.1 },                   // Доля петухов (Number of roosters) - 10% от общей популяции
                { "Доля кур", 0.21 },                  // Доля кур (Number of hens) - 21% от общей популяции
                { "Доля цыплят", 0.69 },                  // Доля цыплят (Number of chicks) - 69% от общей популяции
                { "Количество туров", 2 },                      // Количество туров для обновления алгоритма (Number of tours to update the algorithm)
                { "Фактор обучения петухов", 0.7 },                    // Фактор обучения петухов (Rooster learning factor) - 0.7
                { "Фактор обучения кур", 0.4 },                   // Фактор обучения кур (Hens learning factor) - 0.4
                { "Фактор самообучения", 0.5 }                     // Фактор самообучения (self-learning factor) - 0.5
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