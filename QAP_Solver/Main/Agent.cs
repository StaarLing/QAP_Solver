using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Main
{
    internal class Agent
    {
        List<int> Permutation = new List<int>();

        public Agent(int n)
        {
            this.Permutation = GenerateRandomPermutation(n);
        }
        public Agent(List<int> permutation)
        {
            this.Permutation = permutation;
        }
        public List<int> GetPermutation()
        {
            return this.Permutation;
        }
        public void RandAgent()
        {
            this.Permutation = GenerateRandomPermutation(this.Permutation.Count);
        }
        public void UpdatePermutation(List<int> newPermutation)
        {
            this.Permutation = newPermutation;
        }
        public double Fitness(Task task)
        {
            int n = task.GetN();
            List<int> permutation = this.Permutation;
            double fitness = 0.0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int location1 = i;
                    int location2 = j;
                    int object1 = permutation[i];
                    int object2 = permutation[j];
                    double distance = task.GetDistance()[location1][location2];
                    double cost = task.GetCost()[object1][object2];
                    fitness += distance * cost;
                }
            }

            return fitness;
        }
        public List<int> GenerateRandomPermutation(int n)
        {
            List<int> permutation = Enumerable.Range(0, n).ToList();
            Random rng = new Random();
            int k = n;
            while (k > 1)
            {
                k--;
                int randIndex = rng.Next(k + 1);
                int value = permutation[randIndex];
                permutation[randIndex] = permutation[k];
                permutation[k] = value;
            }
            return permutation;
        }
        public List<Agent> InitializePopulation(int populationSize, int n)
        {
            List<Agent> population = new List<Agent>();

            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Agent(n));
            }

            return population;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Agent other = (Agent)obj;
            return Permutation.SequenceEqual(other.Permutation);
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            // Используем простую хеш-функцию для списка
            unchecked
            {
                int hash = 19;
                foreach (int value in Permutation)
                {
                    hash = hash * 31 + value.GetHashCode();
                }
                return hash;
            }
        }
    }
}
