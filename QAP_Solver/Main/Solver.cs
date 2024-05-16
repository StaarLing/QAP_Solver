using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class Solver
    {
        double Fitness;
        Agent agent;
        List<double> history;
        public void AddHistory(double value)
        {
            history.Add(value);
        }
        public void SetFitness(double fitness)
        {
            this.Fitness = fitness;
        }
        public void SetAgent(Agent agent)
        {
            this.agent = agent;
        }
        public Solver GetAlg(List<int> indexAlg, Task task)
        {
            AlgParam algParam = new AlgParam();
            Solver solver = new Solver();
            foreach (var alg in indexAlg)
            {
                switch (alg)
                {
                    case 0:
                        {
                            solver = GWO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 1:
                        {
                            solver = ABC(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 2:
                        {
                            solver = CS(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 3:
                        {
                            solver = ALO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 4:
                        {
                            solver = SSO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 5:
                        {
                            solver = IWO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 6:
                        {
                            solver = ACO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 7:
                        {
                            solver = BFO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 8:
                        {
                            solver = FA(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 9:
                        {
                            solver = CSO(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    default:
                        {
                            solver = new Solver();
                            break;
                        }
                }
            }
            return solver;
        }
        public Solver GWO(List<double> param, Task task)
        {
            Solver solver = new Solver();
            

            return solver;
        }
        public Solver ABC(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver CS(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver ALO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver SSO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver IWO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver ACO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver BFO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver FA(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }
        public Solver CSO(List<double> param, Task task)
        {
            Solver solver = new Solver();


            return solver;
        }


    }
}
