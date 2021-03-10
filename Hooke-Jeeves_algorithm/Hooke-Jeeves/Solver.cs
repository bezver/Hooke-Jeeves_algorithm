using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private static void printData(TextBox output, List<string> paramNames, List<double> paramValues, string formula, double h, int iteration)
        {
            output.AppendText(String.Format($"Iteration#{iteration}"));
            output.AppendText(Environment.NewLine);
            output.AppendText(String.Format("f = {0:0.######}", Function(paramNames, paramValues, formula)));
            output.AppendText(Environment.NewLine);
            //List<string> tmp = paramValues.Select(Convert.ToString).ToList();
            List<string> tmp = paramValues.Select(n => String.Format("{0:0.######}", n)).ToList();
            output.AppendText("params = [" + String.Join(", ", tmp) + "]");
            output.AppendText(Environment.NewLine);
            output.AppendText(String.Format("h = {0:0.######}", h));
            output.AppendText(Environment.NewLine);
            output.AppendText("-----------------------");
            output.AppendText(Environment.NewLine);
        }

        public static double Alg_hooke(TextBox output, List<string> paramNames, List<double> paramValues, string formula,  double h, double eps, double a, int iterationsCount, bool findMin)
        {
            if (findMin)
            {
                printData(output, paramNames, paramValues, formula, h, 0);
                List<double> oldValues = null;
                for (int iteration = 0; iteration < iterationsCount; iteration++)
                {
                    oldValues = paramValues;
                    for (int i = 0; i < paramValues.Count; i++)
                    {
                        List<double> reduced = new List<double>(paramValues);
                        reduced[i] -= h;
                        List<double> increased = new List<double>(paramValues);
                        increased[i] += h;
                        if (Function(paramNames, reduced, formula) < Function(paramNames, paramValues, formula))
                        {
                            paramValues = reduced;
                        }
                        if (Function(paramNames, increased, formula) < Function(paramNames, paramValues, formula))
                        {
                            paramValues = increased;
                        }
                    }
                    if (Function(paramNames, oldValues, formula) != Function(paramNames, paramValues, formula))
                    {
                        List<double> pValues = new List<double>();
                        for (int i = 0; i < paramValues.Count; i++)
                        {
                            pValues.Add(oldValues[i] + 2 * (paramValues[i] - oldValues[i]));
                        }
                        if (Function(paramNames, pValues, formula) < Function(paramNames, paramValues, formula))
                        {
                            paramValues = pValues;
                        }
                    }
                    else
                    {
                        h /= a;
                    }

                    printData(output, paramNames, paramValues, formula, h, iteration + 1);

                    if (h < eps)
                    {
                        output.AppendText("h < ε");
                        return Function(paramNames, paramValues, formula);
                    }
                }
                output.AppendText("Answer not found, try to increase count of iteration or change step(h)");
                return Function(paramNames, paramValues, formula);
            }
            else
            {
                printData(output, paramNames, paramValues, formula, h, 0);
                List<double> oldValues = null;
                for (int iteration = 0; iteration < iterationsCount; iteration++)
                {
                    oldValues = paramValues;
                    for (int i = 0; i < paramValues.Count; i++)
                    {
                        List<double> reduced = new List<double>(paramValues);
                        reduced[i] -= h;
                        List<double> increased = new List<double>(paramValues);
                        increased[i] += h;
                        if (Function(paramNames, reduced, formula) > Function(paramNames, paramValues, formula))
                        {
                            paramValues = reduced;
                        }
                        if (Function(paramNames, increased, formula) > Function(paramNames, paramValues, formula))
                        {
                            paramValues = increased;
                        }
                    }
                    if (Function(paramNames, oldValues, formula) != Function(paramNames, paramValues, formula))
                    {
                        List<double> pValues = new List<double>();
                        for (int i = 0; i < paramValues.Count; i++)
                        {
                            pValues.Add(oldValues[i] + 2 * (paramValues[i] - oldValues[i]));
                        }
                        if (Function(paramNames, pValues, formula) > Function(paramNames, paramValues, formula))
                        {
                            paramValues = pValues;
                        }
                    }
                    else
                    {
                        h /= a;
                    }

                    printData(output, paramNames, paramValues, formula, h, iteration + 1);

                    if (h < eps)
                    {
                        output.AppendText("h < ε");
                        return Function(paramNames, paramValues, formula);
                    }
                }
                output.AppendText("Answer not found, try to increase count of iteration or change step(h)");
                return Function(paramNames, paramValues, formula);
            }
        }

    }
}
