using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    internal class Result
    {
        List<Solver> solvers;
        Task task;
        List<int> paramRes;
        public Result(List<Solver> solvers, Task task, List<int> paramRes)
        {
            this.solvers = solvers;
            this.task = task;
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
                            break
                                ;
                        case 2:
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
            {
                foreach (var solver in solvers)
                {
                    writer.WriteLine(solver.ToString());
                    writer.WriteLine();
                }
            }
        }
    }
}
