using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    internal class Solver
    {
        public string NameAlg { get; set; }
        public double BestCost { get; set; }
        public List<int> BestSolution { get; set; }
        public List<double> History { get; set; }
        public long Time { get; set; }
        public Solver()
        {
            BestCost = double.MaxValue;
            BestSolution = new List<int>();
            History = new List<double>();
            Time = 0;
        }
        public override string ToString()
        {
            // Разбиваем History на группы по 3 элемента и объединяем их в строку с переводом на новую строку
            string historyString = string.Join(Environment.NewLine, History.Select((x, i) => new { Value = x, Index = i })
                                                       .GroupBy(x => x.Index / 3)
                                                       .Select(group => string.Join(", ", group.Select(x => x.Value))));

            // Формируем строку результата
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Название алгоритма: " + NameAlg);
            sb.AppendLine("Лучшая стоимость: " + BestCost);
            sb.AppendLine("Лучшее решение: " + string.Join(", ", BestSolution));
            sb.AppendLine("Время выполнения: " + Time + " ms");
            sb.AppendLine("История поиска: ");
            sb.Append(historyString);
            return sb.ToString();
        }
        public async Task<List<Solver>> GetAlgAsync(List<int> indexAlg, Task task, AlgParam algParam, System.Windows.Forms.Label label, ProgressBar progressBar)
        {
            List<Solver> solvers = new List<Solver>();
            Solver solver = new Solver();
            Stopwatch timer = new Stopwatch();
            progressBar.Maximum = indexAlg.Count();

            foreach (var alg in indexAlg)
            {
                timer.Restart();
                label.Text = $"Выполняется алгоритм: {algParam.GetParameters(alg).Keys.ToList()[0]}...";
                label.Refresh();

                switch (alg)
                {
                    case 0:
                        {
                            timer.Start();
                            GWO gwo = new GWO();
                            solver = gwo.Solve(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 1:
                        {
                            timer.Start();
                            ABC abc = new ABC();
                            solver = abc.Solve(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 2:
                        {
                            timer.Start();
                            CS cs = new CS();
                            solver = cs.Solve(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 3:
                        {
                            timer.Start();
                            ALO alo = new ALO();
                            solver = alo.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 4:
                        {
                            timer.Start();
                            SSO sso = new SSO();
                            solver = sso.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 5:
                        {
                            timer.Start();
                            IWO iwo = new IWO();
                            solver = iwo.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 6:
                        {
                            timer.Start();
                            ACO aco = new ACO();
                            solver = aco.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 7:
                        {
                            timer.Start();
                            BFO bfo = new BFO();
                            solver = bfo.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 8:
                        {
                            timer.Start();
                            FA fa = new FA();
                            solver = fa.Solver(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    case 9:
                        {
                            timer.Start();
                            CSO cso = new CSO();
                            solver =  await cso.SolverAsync(algParam.GetParameters(alg).Values.ToList(), task);
                            timer.Stop();
                            solver.Time = timer.ElapsedMilliseconds;
                            break;
                        }
                    default:
                        {
                            solver = new Solver();
                            break;
                        }
                }
                solver.NameAlg = algParam.GetParameters(alg).Keys.ToList()[0];
                solvers.Add(solver);
                progressBar.Value += 1;
                progressBar.Refresh();
            }
            label.Text = "Выполнение завершено!";
            label.Refresh();
            return solvers;
        }
    }
}
