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
            int populationSize = (int)param[1];
            int maxIterations = (int)param[2];
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
    internal class ABC
    {
        Random random = new Random();
        public Solver Solve(List<double> param, Task task)
        {
            Solver solver = new Solver();
            int n = task.GetN();
            List<Agent> bees = new List<Agent>();

            for (int i = 0; i < param[1]; i++)
            {
                Agent bee = new Agent(n);
                bee.RandAgent();
                bees.Add(bee);
            }

            solver.BestSolution = bees[0].GetPermutation();
            solver.BestCost = bees[0].Fitness(task);

            int iteration = 0;
            int[] trial = new int[Convert.ToInt16(param[1])];

            while (iteration < param[2])
            {
                // Employed bees phase
                for (int i = 0; i < param[1]; i++)
                {
                    Agent newBee = GenerateNeighbor(bees[i]);
                    double newCost = newBee.Fitness(task);
                    if (newCost < bees[i].Fitness(task))
                    {
                        bees[i] = newBee;
                        trial[i] = 0;
                    }
                    else
                    {
                        trial[i]++;
                    }
                }

                // Calculate probabilities
                double[] fitnesses = bees.Select(bee => 1 / (1 + bee.Fitness(task))).ToArray();
                double sumFitness = fitnesses.Sum();
                double[] probabilities = fitnesses.Select(f => f / sumFitness).ToArray();

                // Onlooker bees phase
                for (int i = 0; i < param[1]; i++)
                {
                    if (random.NextDouble() < probabilities[i])
                    {
                        Agent newBee = GenerateNeighbor(bees[i]);
                        double newCost = newBee.Fitness(task);
                        if (newCost < bees[i].Fitness(task))
                        {
                            bees[i] = newBee;
                            trial[i] = 0;
                        }
                        else
                        {
                            trial[i]++;
                        }
                    }
                }

                // Scout bees phase
                for (int i = 0; i < param[1]; i++)
                {
                    if (trial[i] > param[3])
                    {
                        bees[i] = new Agent(n);
                        bees[i].RandAgent();
                        trial[i] = 0;
                    }
                }

                // Update best solution
                foreach (var bee in bees)
                {
                    double cost = bee.Fitness(task);
                    if (cost < solver.BestCost)
                    {
                        solver.BestCost = cost;
                        solver.BestSolution = new List<int>(bee.GetPermutation());
                    }
                }

                solver.History.Add(solver.BestCost);
                iteration++;
            }
            return solver;
        }
        private Agent GenerateNeighbor(Agent agent)
        {
            Random random = new Random();
            List<int> permutation = new List<int>(agent.GetPermutation());
            int n = permutation.Count;

            // Swap two random positions to create a neighbor
            int pos1 = random.Next(n);
            int pos2 = random.Next(n);
            int temp = permutation[pos1];
            permutation[pos1] = permutation[pos2];
            permutation[pos2] = temp;

            return new Agent(permutation);
        }
    }
    internal class CS
    {
        Random random = new Random();
        public Solver Solve(List<double> param, Task task)
        {
            Solver solver = new Solver();
            int numNests = ((int)param[1]);
            int n = task.GetN();
            List<Agent> nests = new List<Agent>();

            // Initialize population
            for (int i = 0; i < numNests; i++)
            {
                Agent nest = new Agent(n);
                nest.RandAgent();
                nests.Add(nest);
            }

            solver.BestSolution = nests[0].GetPermutation();
            solver.BestCost = nests[0].Fitness(task);

            for (int iter = 0; iter < param[2]; iter++)
            {
                // Generate new solutions (cuckoos) via Lévy flights
                for (int i = 0; i < numNests; i++)
                {
                    Agent newNest = LevyFlight(nests[i], param[3]);
                    double newCost = newNest.Fitness(task);
                    if (newCost < nests[i].Fitness(task))
                    {
                        nests[i] = newNest;
                    }
                }

                // Abandon some nests and generate new ones
                for (int i = 0; i < numNests; i++)
                {
                    if (random.NextDouble() < param[4])
                    {
                        nests[i] = new Agent(n);
                        nests[i].RandAgent();
                    }
                }

                // Update best solution
                foreach (var nest in nests)
                {
                    double cost = nest.Fitness(task);
                    if (cost < solver.BestCost)
                    {
                        solver.BestCost = cost;
                        solver.BestSolution = new List<int>(nest.GetPermutation());
                    }
                }

                solver.History.Add(solver.BestCost);
            }
            return solver;
        }

        private Agent LevyFlight(Agent currentAgent, double stepSize)
        {
            List<int> permutation = new List<int>(currentAgent.GetPermutation());
            int n = permutation.Count;

            for (int i = 0; i < n; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    int j = (int)(n * Levy(stepSize));
                    if (j < 0) j = 0;
                    if (j >= n) j = n - 1;

                    int temp = permutation[i];
                    permutation[i] = permutation[j];
                    permutation[j] = temp;
                }
            }

            return new Agent(permutation);
        }

        private double Levy(double stepSize)
        {
            double u = NormalDistribution();
            double v = NormalDistribution();
            return stepSize * (u / Math.Pow(Math.Abs(v), 1.0 / 3.0));
        }

        private double NormalDistribution()
        {
            return Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) * Math.Sin(2.0 * Math.PI * random.NextDouble());
        }
    }
    internal class ALO
    {
        Random random = new Random();
        public Solver Solver(List<double> param, Task task)
        {
            Solver solver = new Solver();
            int n = task.GetN();
            int numAntLions = (int)param[1];
            int maxIterations = (int)param[2];
            List<Agent> antLions = new List<Agent>();

            // Инициализация популяции
            for (int i = 0; i < numAntLions; i++)
            {
                Agent antLion = new Agent(n);
                antLion.RandAgent();
                antLions.Add(antLion);
            }

            solver.BestSolution = antLions[0].GetPermutation();
            solver.BestCost = antLions[0].Fitness(task);

            for (int iter = 0; iter < maxIterations; iter++)
            {
                // Обновление муравьев
                for (int i = 0; i < numAntLions; i++)
                {
                    List<int> newPermutation = AntWalk(antLions[random.Next(numAntLions)].GetPermutation());
                    Agent ant = new Agent(newPermutation);
                    double antCost = ant.Fitness(task);
                    if (antCost < antLions[i].Fitness(task))
                    {
                        antLions[i] = ant;
                    }
                }

                // Обновление лучшего решения
                foreach (var antLion in antLions)
                {
                    double cost = antLion.Fitness(task);
                    if (cost < solver.BestCost)
                    {
                        solver.BestCost = cost;
                        solver.BestSolution = new List<int>(antLion.GetPermutation());
                    }
                }

                solver.History.Add(solver.BestCost);
            }
            return solver;
        }

        private List<int> AntWalk(List<int> currentPermutation)
        {
            List<int> newPermutation = new List<int>(currentPermutation);
            int n = newPermutation.Count;
            for (int i = 0; i < n; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    int j = random.Next(n);
                    int temp = newPermutation[i];
                    newPermutation[i] = newPermutation[j];
                    newPermutation[j] = temp;
                }
            }
            return newPermutation;
        }
    }
    internal class SSO
    {
        Random random = new Random();
        Solver solver = new Solver();
        Agent a = new Agent(0);
        public Solver Solver(List<double> param, Task task)
        {
            
            int n = task.GetN();
            int populationSize = (int)param[1];
            int maxIterations = (int)param[3];
            int groupCount = (int)param[2];

            List<Agent> population = new List<Agent>();
            List<double> fitness = new List<double>(new double[populationSize]);
            population = a.InitializePopulation(populationSize, n);
            List<List<Agent>> groups = new List<List<Agent>>();
            List<Agent> LL = new List<Agent>();
            Agent HL;
            for (int i = 0; i < maxIterations; i++)
            {
                groups.Clear();
                LL.Clear();
                for (int j = 0; j < populationSize; j++)
                {
                    fitness[j] = population[j].Fitness(task);
                }
                HL = population[fitness.IndexOf(fitness.Min())];

                int groupPopulation = populationSize / groupCount;
                int remainingAgents = populationSize % groupCount;
                for (int j = 0; j < groupCount; j++)
                {
                    var currentGroupSize = groupPopulation + (remainingAgents-- > 0 ? 1 : 0);
                    groups.Add(population.Skip(j * currentGroupSize).Take(currentGroupSize).ToList());
                    LL.Add(groups[j].OrderBy(agent => agent.Fitness(task)).Last());
                }
                foreach (var agent in population)
                {
                    if (groups.Last().Contains(agent))
                    {
                        agent.RandAgent();
                        if (agent.Fitness(task) < HL.Fitness(task))
                        {
                            var minIndex = LL.Select((x, index) => new { x, index })
                                             .OrderBy(item => item.x.Fitness(task))
                                             .First()
                                             .index;
                            LL[minIndex] = HL;
                            HL = agent;
                        }
                        else if (!LL.Contains(agent) && !agent.Equals(HL))
                        {
                            int groupIndex = -1;
                            for (int j = 0; j < groups.Count; j++)
                            {
                                if (groups[j].Contains(agent))
                                {
                                    groupIndex = j;
                                    break;
                                }
                                
                            }
                            List<int> VLL = CalculateVelocity(agent.GetPermutation(), LL[groupIndex].GetPermutation());
                            List<int> VHL = CalculateVelocity(agent.GetPermutation(), HL.GetPermutation());
                            List<int> Vi = CombineVelocities(VLL, VHL);

                            agent.UpdatePermutation(MoveAgent(agent.GetPermutation(), Vi));
                        }
                    }
                }
                solver.BestCost = HL.Fitness(task);
                solver.BestSolution = new List<int>(HL.GetPermutation());
                solver.History.Add(solver.BestCost);
            }
            return solver;
        }
        private List<int> CalculateVelocity(List<int> current, List<int> target)
        {
            List<int> velocity = new List<int>(current);
            for (int i = 0; i < current.Count; i++)
            {
                velocity[i] = target[i] - current[i];
            }
            return velocity;
        }

        private List<int> CombineVelocities(List<int> VLL, List<int> VHL)
        {
            List<int> combinedVelocity = new List<int>(VLL.Count);
            for (int i = 0; i < VLL.Count; i++)
            {
                combinedVelocity.Add((VLL[i] + VHL[i]) / 2);
            }
            return combinedVelocity;
        }

        private List<int> MoveAgent(List<int> permutation, List<int> velocity)
        {
            List<int> newPermutation = new List<int>(permutation);
            for (int i = 0; i < permutation.Count; i++)
            {
                newPermutation[i] = permutation[i] + velocity[i];
            }
            return newPermutation;
        }
    }
}