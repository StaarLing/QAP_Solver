using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

            List<Agent> wolves = Agent.InitializePopulation(populationSize, n);
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

        public Solver Solver(List<double> param, Task task)
        {
            Solver solver = new Solver();
            int n = task.GetN();
            int populationSize = (int)param[1];
            int maxIterations = (int)param[3];
            int groupCount = (int)param[2];

            List<Agent> population = new List<Agent>();
            List<double> fitness = new List<double>(new double[populationSize]);
            population = Agent.InitializePopulation(populationSize, n);
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

    public struct Weed
    {
        public int[] Permutation; // Permutation representing a solution
        public double Fitness;    // Fitness value
        public int Seeds;         // Number of seeds
    }

    internal class IWO
    {
        private Task instance;
        private Random rnd = new Random();
        private bool sowing;
        private int numberSeeds; // Number of seeds
        private int numberWeeds; // Number of weeds
        private int totalNumWeeds; // Total number of weeds
        private int maxNumberSeeds; // Maximum number of seeds
        private int minNumberSeeds; // Minimum number of seeds
        private double maxDispersion; // Maximum dispersion
        private double minDispersion; // Minimum dispersion
        private int maxIteration; // Maximum iterations

        public Weed[] Weeds; // Weeds
        public Weed[] WeedsT; // Temporary weeds
        public Weed[] Seeds; // Seeds
        public int[] BestPermutation; // Best permutation
        public double BestFitness; // Fitness of the best permutation

        public Solver Solver(List<double> param, Task task)
        {
            Solver solver = new Solver();
            IWO iwo = new IWO();
            iwo.Init(task, (int)param[1], (int)param[2], (int)param[3], (int)param[4], param[5], param[6], (int)param[7]);

            for (int iter = 0; iter < (int)param[7]; iter++)
            {
                iwo.Sowing(iter);
                iwo.Germination();
                solver.History.Add(iwo.BestFitness);
            }
            solver.BestCost = iwo.BestFitness;
            solver.BestSolution = iwo.BestPermutation.ToList();

            return solver;
        }
        public void Init(Task instance, int numberSeedsP, int numberWeedsP, int maxNumberSeedsP, int minNumberSeedsP,
            double maxDispersionP, double minDispersionP, int maxIterationP)
        {
            this.instance = instance;
            sowing = false;
            BestFitness = double.MaxValue; // Since we are minimizing, start with a high value

            numberSeeds = numberSeedsP;
            numberWeeds = numberWeedsP;
            maxNumberSeeds = maxNumberSeedsP;
            minNumberSeeds = minNumberSeedsP;
            maxDispersion = maxDispersionP;
            minDispersion = minDispersionP;
            maxIteration = maxIterationP;

            if (minNumberSeeds < 1) minNumberSeeds = 1;
            if (numberWeeds * minNumberSeeds > numberSeeds) numberWeeds = numberSeeds / minNumberSeeds;
            else numberWeeds = numberWeedsP;

            totalNumWeeds = numberWeeds + numberSeeds;

            Weeds = new Weed[totalNumWeeds];
            WeedsT = new Weed[totalNumWeeds];
            Seeds = new Weed[numberSeeds];

            for (int i = 0; i < numberWeeds; i++)
            {
                Weeds[i].Permutation = new int[instance.GetN()];
                WeedsT[i].Permutation = new int[instance.GetN()];
                Weeds[i].Fitness = double.MaxValue;
                Weeds[i].Seeds = 0;
            }
            for (int i = 0; i < numberSeeds; i++)
            {
                Seeds[i].Permutation = new int[instance.GetN()];
                Seeds[i].Seeds = 0;
            }

            BestPermutation = new int[instance.GetN()];
        }

        public void Sowing(int iter)
        {
            if (!sowing)
            {
                BestFitness = double.MaxValue; // Since we are minimizing, start with a high value

                for (int s = 0; s < numberSeeds; s++)
                {
                    Seeds[s].Permutation = GenerateRandomPermutation(instance.GetN());
                    Seeds[s].Fitness = double.MaxValue;
                    Seeds[s].Seeds = 0;
                }

                sowing = true;
                return;
            }

            int pos = 0;
            double r = 0.0;
            double dispersion = ((maxIteration - iter) / (double)maxIteration) * (maxDispersion - minDispersion) + minDispersion;

            for (int w = 0; w < numberWeeds; w++)
            {
                Weeds[w].Seeds = 0;

                for (int s = 0; s < minNumberSeeds; s++)
                {
                    Seeds[pos].Permutation = GenerateNeighborPermutation(Weeds[w].Permutation, dispersion);
                    pos++;
                    Weeds[w].Seeds++;
                }
            }

            bool seedingLimit = false;
            int weedsPos = 0;

            for (int s = pos; s < numberSeeds; s++)
            {
                r = rnd.NextDouble() * (Weeds[numberWeeds - 1].Fitness - Weeds[0].Fitness) + Weeds[0].Fitness;

                for (int f = 0; f < numberWeeds; f++)
                {
                    if (Weeds[f].Fitness <= r && r < Weeds[f].Fitness + (Weeds[0].Fitness - Weeds[numberWeeds - 1].Fitness))
                    {
                        weedsPos = f;
                        break;
                    }
                }

                if (Weeds[weedsPos].Seeds >= maxNumberSeeds)
                {
                    seedingLimit = false;
                    while (!seedingLimit)
                    {
                        weedsPos++;
                        if (weedsPos >= numberWeeds)
                        {
                            weedsPos = 0;
                            seedingLimit = true;
                        }
                        else
                        {
                            if (Weeds[weedsPos].Seeds < maxNumberSeeds)
                            {
                                seedingLimit = true;
                            }
                        }
                    }
                }

                Seeds[s].Permutation = GenerateNeighborPermutation(Weeds[weedsPos].Permutation, dispersion);
                Seeds[s].Seeds = 0;
                Weeds[weedsPos].Seeds++;
            }
        }

        public void Germination()
        {
            for (int s = 0; s < numberSeeds; s++)
            {
                Weeds[numberWeeds + s] = Seeds[s];
                Weeds[numberWeeds + s].Fitness = CalculateFitness(Weeds[numberWeeds + s].Permutation);
            }

            Sorting();

            if (Weeds[0].Fitness < BestFitness)
            {
                BestFitness = Weeds[0].Fitness;
                Array.Copy(Weeds[0].Permutation, BestPermutation, instance.GetN());
            }
        }

        private void Sorting()
        {
            Array.Sort(Weeds, (a, b) => a.Fitness.CompareTo(b.Fitness)); // Sorting in ascending order of fitness
        }

        private double CalculateFitness(int[] permutation)
        {
            double fitness = 0.0;

            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = 0; j < permutation.Length; j++)
                {
                    fitness += instance.GetCost()[i][j] * instance.GetDistance()[permutation[i]][permutation[j]];
                }
            }

            return fitness; // Minimization problem, so no negative sign here
        }

        private int[] GenerateRandomPermutation(int size)
        {
            return Enumerable.Range(0, size).OrderBy(x => rnd.Next()).ToArray();
        }

        private int[] GenerateNeighborPermutation(int[] permutation, double dispersion)
        {
            int[] newPermutation = (int[])permutation.Clone();
            int swapCount = (int)(dispersion * permutation.Length);
            for (int i = 0; i < swapCount; i++)
            {
                int pos1 = rnd.Next(permutation.Length);
                int pos2 = rnd.Next(permutation.Length);
                int temp = newPermutation[pos1];
                newPermutation[pos1] = newPermutation[pos2];
                newPermutation[pos2] = temp;
            }
            return newPermutation;
        }
    }
    internal class ACO
    {
        static Random random = new Random();

        public Solver Solver(List<double> param, Task task)
        {
            int numObjects = task.GetN();
            double[][] pheromones = InitPheromones(numObjects);

            Agent a = new Agent(0);
            List<Agent> agents = Agent.InitializePopulation((int)param[1], numObjects);

            Solver solver = new Solver();
            double bestFitness = double.MaxValue;

            for (int iter = 0; iter < (int)param[6]; iter++)
            {
                UpdateAnts(agents, pheromones, task, param[2], param[3]);
                UpdatePheromones(pheromones, agents, task, param[4], param[5]);

                foreach (Agent agent in agents)
                {
                    double fitness = agent.Fitness(task);
                    if (fitness < bestFitness)
                    {
                        bestFitness = fitness;
                        solver.BestCost = bestFitness;
                        solver.BestSolution = new List<int>(agent.GetPermutation());
                    }
                }
                solver.History.Add(bestFitness);
            }

            return solver;
        }

        private static double[][] InitPheromones(int numObjects)
        {
            double[][] pheromones = new double[numObjects][];
            for (int i = 0; i < numObjects; i++)
            {
                pheromones[i] = new double[numObjects];
                for (int j = 0; j < numObjects; j++)
                {
                    pheromones[i][j] = 0.01;
                }
            }
            return pheromones;
        }

        private static void UpdateAnts(List<Agent> agents, double[][] pheromones, Task task, double alpha, double beta)
        {
            foreach (Agent agent in agents)
            {
                agent.UpdatePermutation(BuildAssignment(agent, pheromones, task, alpha, beta));
            }
        }

        private static List<int> BuildAssignment(Agent agent, double[][] pheromones, Task task, double alpha, double beta)
        {
            int numObjects = pheromones.Length;
            List<int> permutation = agent.GetPermutation();
            List<int> assignment = new List<int>();
            bool[] visited = new bool[numObjects];

            assignment.Add(permutation[0]);
            visited[permutation[0]] = true;

            for (int i = 1; i < numObjects; i++)
            {
                int nextObject = NextObject(agent, assignment[i - 1], visited, pheromones, task, alpha, beta);
                assignment.Add(nextObject);
                visited[nextObject] = true;
            }

            return assignment;
        }

        private static int NextObject(Agent agent, int prevObject, bool[] visited, double[][] pheromones, Task task, double alpha, double beta)
        {
            double[] probs = MoveProbs(agent, prevObject, visited, pheromones, task, alpha, beta);
            double[] cumul = new double[probs.Length + 1];
            for (int i = 0; i < probs.Length; i++)
            {
                cumul[i + 1] = cumul[i] + probs[i];
            }
            double p = random.NextDouble();
            for (int i = 0; i < cumul.Length - 1; i++)
            {
                if (p >= cumul[i] && p < cumul[i + 1])
                {
                    return i;
                }
            }
            throw new Exception("Failure to return valid object in NextObject");
        }

        private static double[] MoveProbs(Agent agent, int prevObject, bool[] visited, double[][] pheromones, Task task, double alpha, double beta)
        {
            int numObjects = pheromones.Length;
            double[] taueta = new double[numObjects];
            double sum = 0.0;
            List<int> permutation = agent.GetPermutation();

            for (int i = 0; i < numObjects; i++)
            {
                if (visited[i])
                {
                    taueta[i] = 0.0;
                }
                else
                {
                    taueta[i] = Math.Pow(pheromones[prevObject][i], alpha) *
                        Math.Pow(1.0 / (task.GetDistance()[prevObject][i] * task.GetCost()[permutation[prevObject]][permutation[i]]), beta);
                    if (taueta[i] < 0.0001)
                    {
                        taueta[i] = 0.0001;
                    }
                    sum += taueta[i];
                }
            }

            double[] probs = new double[numObjects];
            for (int i = 0; i < numObjects; i++)
            {
                probs[i] = taueta[i] / sum;
            }
            return probs;
        }

        private static void UpdatePheromones(double[][] pheromones, List<Agent> agents, Task task, double rho, double Q)
        {
            int numObjects = pheromones.Length;
            foreach (Agent agent in agents)
            {
                List<int> permutation = agent.GetPermutation();
                double decrease = (1.0 - rho) * pheromones[permutation[0]][permutation[numObjects - 1]];
                double increase = 0.0;
                double cost = agent.Fitness(task);

                for (int i = 0; i < numObjects - 1; i++)
                {
                    increase += Q / cost;
                    pheromones[permutation[i]][permutation[i + 1]] = decrease + increase;
                    pheromones[permutation[i + 1]][permutation[i]] = pheromones[permutation[i]][permutation[i + 1]];
                }
            }
        }
    }
    public static class Extensions
    {
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return source.Aggregate((minItem, nextItem) => selector(nextItem).CompareTo(selector(minItem)) < 0 ? nextItem : minItem);
        }
    }
    internal class BFO
    {
        public Solver Solver(List<double> param, Task task)
        {
            Solver solver = new Solver();
            int populationSize = (int)param[1];
            int numEliminationDispersalSteps = (int)param[2];
            int numReproductionSteps = (int)param[3];
            int numChemotacticSteps = (int)param[4];
            double eliminationDispersalProbability = param[5];

            int n = task.GetN();
            List<Agent> agents = Agent.InitializePopulation(populationSize, n);

            for (int l = 0; l < numEliminationDispersalSteps; l++)
            {
                for (int k = 0; k < numReproductionSteps; k++)
                {
                    for (int j = 0; j < numChemotacticSteps; j++)
                    {
                        foreach (var agent in agents)
                        {
                            Chemotactic(agent, task);
                        }
                    }

                    agents = agents.OrderBy(a => a.Fitness(task)).ToList();
                    Reproduce(agents, task);
                }

                var bestAgent = agents.MinBy(a => a.Fitness(task));
                solver.History.Add(bestAgent.Fitness(task));
                EliminateAndDisperse(agents, eliminationDispersalProbability, n);
            }

            var finalBestAgent = agents.MinBy(a => a.Fitness(task));
            solver.BestSolution = finalBestAgent.GetPermutation();
            solver.BestCost = finalBestAgent.Fitness(task);

            return solver;
        }

        private void Chemotactic(Agent agent, Task task)
        {
            List<int> currentPermutation = agent.GetPermutation();
            double currentFitness = agent.Fitness(task);

            List<int> newPermutation = GenerateNeighborPermutation(currentPermutation);
            agent.UpdatePermutation(newPermutation);
            double newFitness = agent.Fitness(task);

            if (newFitness > currentFitness) // Swap if new fitness is worse
            {
                agent.UpdatePermutation(currentPermutation);
            }
        }

        private List<int> GenerateNeighborPermutation(List<int> permutation)
        {
            Random rng = new Random();
            int n = permutation.Count;
            int i = rng.Next(n);
            int j = rng.Next(n);
            while (i == j)
            {
                j = rng.Next(n);
            }
            List<int> newPermutation = new List<int>(permutation);
            int temp = newPermutation[i];
            newPermutation[i] = newPermutation[j];
            newPermutation[j] = temp;
            return newPermutation;
        }

        private void Reproduce(List<Agent> agents, Task task)
        {
            agents = agents.OrderBy(a => a.Fitness(task)).ToList();
            int halfSize = agents.Count / 2;
            for (int i = 0; i < halfSize; i++)
            {
                agents[halfSize + i] = new Agent(new List<int>(agents[i].GetPermutation()));
            }
        }

        private void EliminateAndDisperse(List<Agent> agents, double probability, int n)
        {
            Random rng = new Random();
            for (int i = 0; i < agents.Count; i++)
            {
                if (rng.NextDouble() < probability)
                {
                    agents[i] = new Agent(n);
                }
            }
        }
    }
    internal class FA
    {

        public Solver Solver(List<double> param, Task task)
        {
            int n = task.GetN();
            int populationSize = (int)param[1];
            double alpha = param[2];
            double beta0 = param[3];
            double gamma = param[4];
            int maxGenerations = (int)param[5];

            Random random = new Random(); // Added Random initialization

            Solver solver = new Solver(); // Create a new instance of Solver

            List<Agent> population = InitializePopulation(n, populationSize); // Pass populationSize to InitializePopulation

            for (int gen = 0; gen < maxGenerations; gen++)
            {
                for (int i = 0; i < populationSize; i++)
                {
                    for (int j = 0; j < populationSize; j++)
                    {
                        if (population[i].Fitness(task) > population[j].Fitness(task))
                        {
                            double beta = beta0 * Math.Exp(-gamma * Distance(population[i].GetPermutation(), population[j].GetPermutation()));
                            MoveFirefly(population[i], population[j], beta, alpha, n, task, random); // Pass 'random' to MoveFirefly
                        }
                    }
                }

                // Update best solution
                var bestAgent = population.OrderBy(a => a.Fitness(task)).First();
                if (bestAgent.Fitness(task) < solver.BestCost || gen == 0)
                {
                    solver.BestCost = bestAgent.Fitness(task);
                    solver.BestSolution = new List<int>(bestAgent.GetPermutation());
                }
                solver.History.Add(solver.BestCost);
            }
            return solver;
        }

        private List<Agent> InitializePopulation(int n, int populationSize) // Added 'populationSize' parameter
        {
            List<Agent> population = new List<Agent>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Agent(n));
            }
            return population;
        }

        private double Distance(List<int> perm1, List<int> perm2)
        {
            int n = perm1.Count;
            int distance = 0;
            for (int i = 0; i < n; i++)
            {
                if (perm1[i] != perm2[i])
                {
                    distance++;
                }
            }
            return distance;
        }

        private void MoveFirefly(Agent firefly, Agent betterFirefly, double beta, double alpha, int n, Task task, Random random) // Added 'random' parameter
        {
            List<int> newPermutation = new List<int>(firefly.GetPermutation());

            // Move firefly towards the better one
            for (int i = 0; i < n; i++)
            {
                if (random.NextDouble() < beta)
                {
                    int temp = newPermutation[i];
                    newPermutation[i] = betterFirefly.GetPermutation()[i];
                    newPermutation[i] = temp;
                }
            }

            // Apply random walk
            for (int i = 0; i < n; i++)
            {
                if (random.NextDouble() < alpha)
                {
                    int idx1 = random.Next(n);
                    int idx2 = random.Next(n);
                    int temp = newPermutation[idx1];
                    newPermutation[idx1] = newPermutation[idx2];
                    newPermutation[idx2] = temp;
                }
            }

            firefly.UpdatePermutation(newPermutation);
        }
    }
    internal class CSO
    {
        public async Task<Solver> SolverAsync(List<double> param, Task task)
        {
            return await System.Threading.Tasks.Task.Run(() => Solver(param, task));
        }
        public Solver Solver(List<double> param, Task task)
        {
            int n = task.GetN();
            int populationSize = (int)param[1];
            double G = param[2];
            double roosterPart = param[3];
            double henPart = param[4];
            double roosterLearningFactor = param[5];
            double hensLearningFactor = param[6];
            double selfLearningFactor = param[7];
            int maxIterations = (int)param[8];

            List<Agent> agents = Agent.InitializePopulation(populationSize, n);
            List<Agent> roosters = new List<Agent>();
            List<Agent> hens = new List<Agent>();
            List<Agent> chicks = new List<Agent>();

            Solver solver = new Solver();

            for (int iter = 0; iter < maxIterations; iter++)
            {
                // Rank agents by fitness
                agents = agents.OrderBy(a => a.Fitness(task)).ToList();

                // Establish hierarchical order
                int roosterCount = (int)(roosterPart * populationSize);
                int henCount = (int)(henPart * populationSize);
                int chickCount = populationSize - roosterCount - henCount;

                roosters = agents.Take(roosterCount).ToList();
                hens = agents.Skip(roosterCount).Take(henCount).ToList();
                chicks = agents.Skip(roosterCount + henCount).ToList();

                // Update positions
                foreach (var rooster in roosters)
                {
                    // Rooster maintains its position
                }

                foreach (var hen in hens)
                {
                    int randomRoosterIndex = new Random().Next(roosterCount);
                    int randomHenIndex = new Random().Next(henCount);
                    Agent randomRooster = roosters[randomRoosterIndex];
                    Agent randomHen = hens[randomHenIndex];

                    hen.UpdatePermutation(UpdateHenPosition(hen, randomRooster, randomHen, hensLearningFactor));
                }

                foreach (var chick in chicks)
                {
                    int randomRoosterIndex = new Random().Next(roosterCount);
                    int randomHenIndex = new Random().Next(henCount);
                    Agent randomRooster = roosters[randomRoosterIndex];
                    Agent motherHen = hens[randomHenIndex];

                    chick.UpdatePermutation(UpdateChickPosition(chick, motherHen, randomRooster, roosterLearningFactor,
                        hensLearningFactor, selfLearningFactor));
                }
                // Update global best solution
                Agent bestAgent = agents.OrderBy(a => a.Fitness(task)).First();
                solver.BestSolution = bestAgent.GetPermutation();
                solver.BestCost = bestAgent.Fitness(task);
                solver.History.Add(bestAgent.Fitness(task));

                // Regularly update the hierarchical order
                if (iter % G == 0)
                {
                    agents = agents.OrderBy(a => a.Fitness(task)).ToList();
                }
            }
            return solver;
        }

        private List<int> UpdateChickPosition(Agent chick, Agent motherHen, Agent randomRooster, double FL, double C, double W)
        {
            List<int> newPosition = new List<int>(chick.GetPermutation());
            int n = newPosition.Count;
            Random random = new Random();

            // Используем вспомогательный массив для хранения промежуточных значений
            double[] intermediatePosition = new double[n];

            for (int i = 0; i < n; i++)
            {
                // Применяем формулу
                intermediatePosition[i] = W * chick.GetPermutation()[i]
                                        + FL * (motherHen.GetPermutation()[i] - chick.GetPermutation()[i])
                                        + C * (randomRooster.GetPermutation()[i] - chick.GetPermutation()[i]);
            }

            // Преобразуем промежуточные значения в индексы
            List<int> sortedIndices = intermediatePosition
                                        .Select((value, index) => new { value, index })
                                        .OrderBy(x => x.value)
                                        .Select(x => x.index)
                                        .ToList();

            // Создаем новую перестановку на основе отсортированных индексов
            for (int i = 0; i < n; i++)
            {
                newPosition[i] = sortedIndices[i];
            }
            sortedIndices = null;
            GC.Collect();
            return newPosition;
        }

        private List<int> UpdateHenPosition(Agent hen, Agent randomRooster, Agent randomHen, double FL)
        {
            List<int> newPosition = new List<int>(hen.GetPermutation());
            int n = newPosition.Count;
            Random random = new Random();

            // Используем вспомогательный массив для хранения промежуточных значений
            double[] intermediatePosition = new double[n];

            for (int i = 0; i < n; i++)
            {
                // Применяем формулу
                intermediatePosition[i] = hen.GetPermutation()[i]
                                        + FL * (randomRooster.GetPermutation()[i] - randomHen.GetPermutation()[i]);
            }

            // Преобразуем промежуточные значения в индексы
            List<int> sortedIndices = intermediatePosition
                                        .Select((value, index) => new { value, index })
                                        .OrderBy(x => x.value)
                                        .Select(x => x.index)
                                        .ToList();

            // Создаем новую перестановку на основе отсортированных индексов
            for (int i = 0; i < n; i++)
            {
                newPosition[i] = sortedIndices[i];
            }
            sortedIndices = null;
            GC.Collect();
            return newPosition;
        }
    }
    internal class BruteForceSolver
    {
        private double bestCost;
        private List<int> bestSolution;

        public BruteForceSolver()
        {
            bestCost = double.MaxValue;
            bestSolution = new List<int>();
        }

        public Solver Solve(Task task)
        {
            int n = task.GetN();
            List<int> permutation = new List<int>(n);
            for (int i = 0; i < n; i++)
            {
                permutation.Add(i);
            }

            Permute(permutation, 0, n - 1, task);

            Solver solver = new Solver();
            solver.BestCost = bestCost;
            solver.BestSolution = new List<int>(bestSolution);
            return solver;
        }

        private void Permute(List<int> permutation, int left, int right, Task task)
        {
            if (left == right)
            {
                double currentCost = task.CalculateCost(permutation);
                if (currentCost < bestCost)
                {
                    bestCost = currentCost;
                    bestSolution = new List<int>(permutation);
                }
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    Swap(permutation, left, i);
                    Permute(permutation, left + 1, right, task);
                    Swap(permutation, left, i); // backtrack
                }
            }
        }

        private void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}