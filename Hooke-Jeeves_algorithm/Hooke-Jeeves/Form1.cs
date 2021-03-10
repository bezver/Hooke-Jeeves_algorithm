using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                List<string> paramNames =  this.textBox1.Text.Split(' ').ToList<string>();
                string formula =           this.textBox2.Text;
                List<double> paramValues = this.textBox3.Text.Split(' ').Select(Convert.ToDouble).ToList<double>();
                bool findMin =             radioButton1.Checked;
                double h =                 Convert.ToDouble(this.textBox4.Text);
                double epsilon =           Convert.ToDouble(this.textBox5.Text);
                double a =                 Convert.ToDouble(this.textBox6.Text);
                int iterationsCount =      Convert.ToInt32(this.textBox7.Text);

                double result = Solver.Function(paramNames, paramValues, formula);
                this.textBox8.Text = result.ToString();
                //throw new InvalidProgramException("HI");
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
    }
}
