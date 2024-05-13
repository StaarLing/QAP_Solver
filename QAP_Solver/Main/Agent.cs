using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class Agent
    {
        List<int> Permutation = new List<int>();

        public double Fitness(Agent agent, Task task)
        {
            int n = task.GetN(); // Размерность задачи
            List<int> permutation = agent.Permutation; // Перестановка, заданная агентом

            double fitness = 0.0;

            // Рассчитываем общую стоимость (fitness) на основе перестановки объектов
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int location1 = i; // Локация объекта i
                    int location2 = j; // Локация объекта j

                    int object1 = permutation[i]; // Объект, распределенный на локацию location1
                    int object2 = permutation[j]; // Объект, распределенный на локацию location2

                    // Расстояние между локациями location1 и location2
                    double distance = task.GetDistance()[location1][location2];

                    // Стоимость распределения объекта object1 на локацию location2 и объекта object2 на локацию location1
                    double cost = task.GetCost()[object1][object2];

                    // Учитываем стоимость в fitness
                    fitness += distance * cost;
                }
            }

            return fitness;
        }
    }
}
