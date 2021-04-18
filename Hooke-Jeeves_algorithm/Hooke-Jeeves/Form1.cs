using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using info.lundin.math;

namespace Hooke_Jeeves
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBox8.Text = "";
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                List<string> paramNames =  this.textBox1.Text.Split(' ').ToList<string>();
                string formula =           this.textBox2.Text;
                List<double> paramValues = this.textBox3.Text.Split(' ').Select((d) => Convert.ToDouble(d, formatter)).ToList<double>();
                bool findMin =             radioButton1.Checked;
                double h =                 Convert.ToDouble(this.textBox4.Text, formatter);
                double epsilon =           Convert.ToDouble(this.textBox5.Text, formatter);
                double a =                 Convert.ToDouble(this.textBox6.Text, formatter);
                int iterationsCount =      Convert.ToInt32(this.textBox7.Text);

                Solver.Alg_hooke(this.textBox8, paramNames, paramValues, formula, h, epsilon, a, iterationsCount, findMin);
            }
            catch(InvalidProgramException exc)
            {
                this.textBox8.Text = "###Error###";
                MessageBox.Show(exc.Message, "Error");
            }
            catch (Exception exc)
            {
                this.textBox8.Text = "###Error###";
                MessageBox.Show(exc.Message, "Error");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool newValue = !this.label3.Enabled;

            this.label3.Enabled = newValue;
            this.label3.Visible = newValue;
            this.label5.Enabled = newValue;
            this.label5.Visible = newValue;
            this.label6.Enabled = newValue;
            this.label6.Visible = newValue;
            this.label7.Enabled = newValue;
            this.label7.Visible = newValue;
            this.label8.Enabled = newValue;
            this.label8.Visible = newValue;

            this.textBox4.Enabled = newValue;
            this.textBox4.Visible = newValue;
            this.textBox5.Enabled = newValue;
            this.textBox5.Visible = newValue;
            this.textBox6.Enabled = newValue;
            this.textBox6.Visible = newValue;
            this.textBox7.Enabled = newValue;
            this.textBox7.Visible = newValue;

            this.radioButton1.Enabled = newValue;
            this.radioButton1.Visible = newValue;
            this.radioButton2.Enabled = newValue;
            this.radioButton2.Visible = newValue;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string msg = "Ця програма розроблена для розрахунку задач " +
                "n-вимірної оптимізації методом Хука-Дживса. Назви змінних та " +
                "координати початкової точки вводити через пробіл.";
            msg += Environment.NewLine;
            msg += "В налаштуваннях(знак шестерні) можна обрати: яку точку ми шукаємо (min, max), " +
                "крок, максимальну кількість ітерацій, точність, зменшення кроку.";
            msg += Environment.NewLine;
            msg += "Дробові числа вводити через крапку(3.14)";
            MessageBox.Show(msg, "Help");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string msg = String.Format(@"bezver  gonchar");
            msg += Environment.NewLine;
            msg += "See on Github: https://github.com/bezver/Hooke-Jeeves_algorithm";
            MessageBox.Show(msg, "Autors");
        }
    }
}
