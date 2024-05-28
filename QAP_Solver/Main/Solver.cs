using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class Solver
    {
        public double BestCost { get; set; }
        public List<int> BestSolution { get; set; }
        public List<double> History { get; set; }
        public Solver()
        {
            BestCost = double.MaxValue;
            BestSolution = new List<int>();
            History = new List<double>();
        }
        public override string ToString()
        {
            // Разбиваем History на группы по 3 элемента и объединяем их в строку с переводом на новую строку
            string historyString = string.Join(Environment.NewLine, History.Select((x, i) => new { Value = x, Index = i })
                                                       .GroupBy(x => x.Index / 3)
                                                       .Select(group => string.Join(", ", group.Select(x => x.Value))));

            // Формируем строку результата
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Лучшая стоимость: " + BestCost);
            sb.AppendLine("Лучшее решение: " + string.Join(", ", BestSolution));
            sb.AppendLine("История поиска: ");
            sb.Append(historyString);
            return sb.ToString();
        }
        public List<Solver> GetAlg(List<int> indexAlg, Task task, AlgParam algParam)
        {
            List<Solver> solvers = new List<Solver>();
            Solver solver = new Solver();
            foreach (var alg in indexAlg)
            {
                switch (alg)
                {
                    case 0:
                        {
                            GWO gwo = new GWO();
                            solver = gwo.Solve(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 1:
                        {
                            ABC abc = new ABC();
                            solver = abc.Solve(algParam.GetParameters(alg).Values.ToList(), task);
                            break;
                        }
                    case 2:
                        {
                            CS cs = new CS();
                            solver = cs.Solve(algParam.GetParameters(alg).Values.ToList(), task);
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
                solvers.Add(solver);
            }
            return solvers;
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
