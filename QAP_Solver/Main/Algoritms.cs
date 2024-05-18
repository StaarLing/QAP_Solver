using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class GWO
    {
        Random random = new Random();
       public Solver Solve(List<double> param, Task task)
        {
            Solver solver = new Solver();
            Agent agent = new Agent(0);
            int populationSize = (int)param[0];
            int maxIterations = (int)param[1];
            double a, r1, r2, A1, A2, C1, C2, D_alpha, D_beta, D_delta, X1, X2, X3;

            int n = task.GetN();

            List<Agent> wolves = agent.InitializePopulation(populationSize, n);
            List<double> fitness = new List<double>();

            for (int i = 0; i < populationSize; i++)
            {
                fitness.Add(wolves[i].Fitness(task));
            }

            Agent alphaWolf = wolves[fitness.IndexOf(fitness.Min())];
            double alphaCost = fitness.Min();
            Agent betaWolf = wolves[fitness.IndexOf(fitness.OrderBy(x => x).ElementAt(1))];
            double betaCost = fitness.OrderBy(x => x).ElementAt(1);
            Agent deltaWolf = wolves[fitness.IndexOf(fitness.OrderBy(x => x).ElementAt(2))];
            double deltaCost = fitness.OrderBy(x => x).ElementAt(2);

            solver.BestSolution = new List<int>(alphaWolf.GetPermutation());
            solver.BestCost = alphaCost;

            for (int t = 0; t < maxIterations; t++)
            {
                a = 2.0 - t * (2.0 / maxIterations);

                for (int i = 0; i < populationSize; i++)
                {
                    List<int> newPermutation = new List<int>(n);

                    for (int j = 0; j < n; j++)
                    {
                        r1 = random.NextDouble();
                        r2 = random.NextDouble();

                        A1 = 2.0 * a * r1 - a;
                        C1 = 2.0 * r2;

                        D_alpha = Math.Abs(C1 * alphaWolf.GetPermutation()[j] - wolves[i].GetPermutation()[j]);
                        X1 = alphaWolf.GetPermutation()[j] - A1 * D_alpha;

                        r1 = random.NextDouble();
                        r2 = random.NextDouble();

                        A2 = 2.0 * a * r1 - a;
                        C2 = 2.0 * r2;

                        D_beta = Math.Abs(C2 * betaWolf.GetPermutation()[j] - wolves[i].GetPermutation()[j]);
                        X2 = betaWolf.GetPermutation()[j] - A2 * D_beta;

                        r1 = random.NextDouble();
                        r2 = random.NextDouble();

                        A1 = 2.0 * a * r1 - a;
                        C1 = 2.0 * r2;

                        D_delta = Math.Abs(C1 * deltaWolf.GetPermutation()[j] - wolves[i].GetPermutation()[j]);
                        X3 = deltaWolf.GetPermutation()[j] - A1 * D_delta;

                        int newValue = (int)((X1 + X2 + X3) / 3.0);
                        newPermutation.Add(newValue);
                    }

                    newPermutation = EnsurePermutation(newPermutation, n);
                    wolves[i].UpdatePermutation(newPermutation);

                    fitness[i] = wolves[i].Fitness(task);

                    if (fitness[i] < alphaCost)
                    {
                        alphaCost = fitness[i];
                        alphaWolf = new Agent(new List<int>(wolves[i].GetPermutation()));
                    }
                    else if (fitness[i] < betaCost)
                    {
                        betaCost = fitness[i];
                        betaWolf = new Agent(new List<int>(wolves[i].GetPermutation()));
                    }
                    else if (fitness[i] < deltaCost)
                    {
                        deltaCost = fitness[i];
                        deltaWolf = new Agent(new List<int>(wolves[i].GetPermutation()));
                    }
                }

                solver.History.Add(alphaCost);
            }

            solver.BestSolution = alphaWolf.GetPermutation();
            solver.BestCost = alphaCost;

            return solver;
        }
        private List<int> EnsurePermutation(List<int> wolf, int n)
        {
            bool[] found = new bool[n];
            List<int> missing = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (wolf[i] < 0 || wolf[i] >= n || found[wolf[i]])
                {
                    wolf[i] = -1;
                }
                else
                {
                    found[wolf[i]] = true;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (!found[i])
                {
                    missing.Add(i);
                }
            }

            int mIndex = 0;
            for (int i = 0; i < n; i++)
            {
                if (wolf[i] == -1)
                {
                    wolf[i] = missing[mIndex++];
                }
            }

            return wolf;
        }
    }
}
