using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Main
{
    internal class AlgParam
    {
        Dictionary<int, Dictionary<string, double>> param = new Dictionary<int, Dictionary<string, double>>();

        public AlgParam() 
        {
            Dictionary<string, double> parametersAlgorithm0 = new Dictionary<string, double>
            {
                { "Parameter1", 1.0 },
                { "Parameter2", 2.0 },
                { "Parameter3", 3.0 }
            };
            param.Add(0, parametersAlgorithm0);

            Dictionary<string, double> parametersAlgorithm1 = new Dictionary<string, double>
            {
                { "ParameterA", 0.5 },
                { "ParameterB", 1.5 },
                { "ParameterC", 2.5 }
            };
            param.Add(1, parametersAlgorithm1);
            Dictionary<string, double> parametersAlgorithm2 = new Dictionary<string, double>
            {
                { "Parameter1", 1.0 },
                { "Parameter2", 2.0 },
                { "Parameter3", 3.0 }
            };
            param.Add(2, parametersAlgorithm2);

            Dictionary<string, double> parametersAlgorithm3 = new Dictionary<string, double>
            {
                { "ParameterA", 0.5 },
                { "ParameterB", 1.5 },
                { "ParameterC", 2.5 }
            };
            param.Add(3, parametersAlgorithm3);
            Dictionary<string, double> parametersAlgorithm4 = new Dictionary<string, double>
            {
                { "Parameter1", 1.0 },
                { "Parameter2", 2.0 },
                { "Parameter3", 3.0 }
            };
            param.Add(4, parametersAlgorithm4);

            Dictionary<string, double> parametersAlgorithm5 = new Dictionary<string, double>
            {
                { "ParameterA", 0.5 },
                { "ParameterB", 1.5 },
                { "ParameterC", 2.5 }
            };
            param.Add(5, parametersAlgorithm5);
            Dictionary<string, double> parametersAlgorithm6 = new Dictionary<string, double>
            {
                { "Parameter1", 1.0 },
                { "Parameter2", 2.0 },
                { "Parameter3", 3.0 }
            };
            param.Add(6, parametersAlgorithm6);

            Dictionary<string, double> parametersAlgorithm7 = new Dictionary<string, double>
            {
                { "ParameterA", 0.5 },
                { "ParameterB", 1.5 },
                { "ParameterC", 2.5 }
            };
            param.Add(7, parametersAlgorithm7);
            Dictionary<string, double> parametersAlgorithm8 = new Dictionary<string, double>
            {
                { "Parameter1", 1.0 },
                { "Parameter2", 2.0 },
                { "Parameter3", 3.0 }
            };
            param.Add(8, parametersAlgorithm8);

            Dictionary<string, double> parametersAlgorithm9 = new Dictionary<string, double>
            {
                { "ParameterA", 0.5 },
                { "ParameterB", 1.5 },
                { "ParameterC", 2.5 }
            };
            param.Add(9, parametersAlgorithm9);
        }
        public Dictionary<string, double> GetParameters(int algorithmIndex)
        {
            if (param.ContainsKey(algorithmIndex))
            {
                return param[algorithmIndex];
            }
            else
            {
                return new Dictionary<string, double>();
            }
        }
    }
}