using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Legends;

namespace Main
{
    internal class Result
    {
        List<Solver> solvers;
        List<int> paramRes;
        public Result(List<Solver> solvers, List<int> paramRes)
        {
            this.solvers = solvers;
            this.paramRes = paramRes;
        }
        public bool Print(string directoryPath)
        {
            try
            {
                foreach (var res in this.paramRes)
                {
                    switch (res)
                    {
                        case 0:
                            {
                                string filePath = Path.Combine(directoryPath, $"Results_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                                WriteResultsToFile(this.solvers, filePath);
                            }
                            break;
                        case 1:
                            {
                                PlotSolversHistory(this.solvers);
                            }
                            break;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void WriteResultsToFile(List<Solver> solvers, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {// Write summary table header
                writer.WriteLine("Сводная таблица");
                writer.WriteLine("Название алгоритма                       | Лучшая стоимость   | Время (ms)");
                writer.WriteLine(new string('-', 63));

                // Write summary for each solver
                foreach (var solver in solvers)
                {
                    writer.WriteLine($"{solver.NameAlg,-40} | {solver.BestCost,9:F6}       | {solver.Time,1}");
                }
                writer.WriteLine(new string('-', 63));
                writer.WriteLine();

                // Write detailed results for each solver
                foreach (var solver in solvers)
                {
                    writer.WriteLine(solver.ToString());
                    writer.WriteLine();
                }
            }
        }
        private static readonly OxyColor[] Colors = 
            {
        OxyColors.Red, OxyColors.Green, OxyColors.Blue,
        OxyColors.Orange, OxyColors.Purple, OxyColors.Brown,
        OxyColors.Pink, OxyColors.Yellow, OxyColors.Cyan, OxyColors.Magenta
        };
        public void PlotSolversHistory(List<Solver> solvers)
        {
            var myModel = new PlotModel { Title = "График сходимости" };

            var legend = new Legend
            {
                LegendPosition = LegendPosition.RightTop,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical
            };
            myModel.Legends.Add(legend);

            for (int i = 0; i < solvers.Count; i++)
            {
                var solver = solvers[i];
                var lineSeries = new LineSeries
                {
                    Title = solver.NameAlg,
                    Color = Colors[i % Colors.Length],
                    StrokeThickness = 1 // Thinner lines
                };
                for (int j = 0; j < solver.History.Count; j++)
                {
                    lineSeries.Points.Add(new DataPoint(j, solver.History[j]));
                }
                myModel.Series.Add(lineSeries);
            }

            var plotView = new PlotView
            {
                Model = myModel,
                Dock = DockStyle.Fill
            };

            var form = new Form
            {
                Text = "График сходимости",
                Width = 800,
                Height = 600
            };
            form.Controls.Add(plotView);
            form.Show();
        }
    }
}
