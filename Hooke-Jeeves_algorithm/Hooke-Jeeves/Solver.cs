using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using info.lundin.math;

namespace Hooke_Jeeves
{
    class Solver
    {
        public static double Function(List<string> paramNames, List<double> paramValues, string formula)
        {
            if (paramNames.Count != paramValues.Count)
            {
                throw new InvalidProgramException("Error in function parameters");
            }
            ExpressionParser parser = new ExpressionParser();

            for (int i = 0; i < paramNames.Count; i++)
            {
                parser.Values.Add(paramNames[i], paramValues[i]);
            }
            return parser.Parse(formula);
        }
    }
}
