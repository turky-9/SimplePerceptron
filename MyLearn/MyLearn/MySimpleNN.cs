using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLearn
{
    public class APerceptronLogic
    {
        public static double Output(double[] v, double[] w, EActivateFunc f)
        {
            double work = w[w.Length - 1];

            for (int i = 0; i < v.Length; i++)
            {
                work += v[i] * w[i];
            }

            switch (f)
            {
                case EActivateFunc.Step:
                    work = work >= 0 ? 1 : 0;
                    break;
                case EActivateFunc.Sigmoid:
                    work = 1 / (1 + Math.Exp(-work));
                    break;
                case EActivateFunc.Equal:
                    break;
            }

            return work;
        }

        public static double Loss(double[] v, double[] t)
        {
            if (v.Length != t.Length)
                throw new Exception("出力と教師の数が異なります");

            double ret = 0;
            for (int i = 0; i < v.Length; i++)
            {
                ret += Math.Pow(t[i] - v[i], 2);
            }
            return ret;
        }
    }


    public class MySimpleNN
    {

        public int Layers { get { return this.PerceptronPerLayer.Length; } }
        public int[] PerceptronPerLayer { get; set; }

        protected static Random RND = new Random();

        /// <summary>
        /// [layer][perceptron][weight]
        /// </summary>
        public double[][][] Weights;

        /// <summary>
        /// length : layer count
        /// value : perceptron count on layer
        /// </summary>
        /// <param name="incnt">入力数</param>
        /// <param name="pp"></param>
        public MySimpleNN(int incnt, int[] pp)
        {
            this.PerceptronPerLayer = pp;

            this.Weights = new double[this.Layers][][];
            for(int i = 0; i < this.Layers; i++)
            {
                this.Weights[i] = new double[this.PerceptronPerLayer[i]][];
                int weightcnt = i == 0
                    ? incnt + 1
                    : this.PerceptronPerLayer[i - 1] + 1;

                for (int j = 0; j < this.PerceptronPerLayer[i]; j++)
                {
                    this.Weights[i][j] = new double[weightcnt];

                    for (int k = 0; k < weightcnt; k++)
                    {
                        this.Weights[i][j][k] = (RND.NextDouble() * 2) - 1;
                    }
                }
            }
        }

        public double[] Output(double[] v, bool lasteq = false)
        {
            double[] in_val = v;
            double[] out_val = null;
            for (int i = 0; i < this.Weights.Length; i++)
            {
                out_val = new double[this.Weights[i].Length];
                for (int j = 0; j < this.Weights[i].Length; j++)
                {
                    if(i == this.Weights.Length - 1 && lasteq)
                        out_val[j] =  APerceptronLogic.Output(in_val, this.Weights[i][j], EActivateFunc.Equal);
                    else
                        out_val[j] =  APerceptronLogic.Output(in_val, this.Weights[i][j], EActivateFunc.Sigmoid);
                }
                in_val = out_val;
            }
            return out_val;
        }

        public void Learn(double[] v, double[] t, bool lasteq = false)
        {
            double[][][] learn = this.NumericalGradient(v, t, lasteq);

            for (int i = 0; i < this.Weights.Length; i++)
            {
                for (int j = 0; j < this.Weights[i].Length; j++)
                {
                    for (int k = 0; k < this.Weights[i][j].Length; k++)
                    {
                        this.Weights[i][j][k] += learn[i][j][k];
                    }
                }
            }
        }

        protected double[][][] NumericalGradient(double[] v, double[] t, bool lasteq)
        {
            const double ritsu = 0.01;
            const double delta = 0.005;

            double swap;

            double[][][] ret = new double[this.Layers][][];
            for(int i = 0; i < this.Weights.Length; i++)
            {
                ret[i] = new double[this.Weights[i].Length][];
                int weightcnt = i == 0
                    ? this.Weights[0].Length + 1
                    : this.Weights[i -1].Length + 1;

                for (int j = 0; j < this.Weights[i].Length; j++)
                {
                    ret[i][j] = new double[weightcnt];

                    for (int k = 0; k < this.Weights[i][j].Length; k++)
                    {
                        swap = this.Weights[i][j][k];

                        this.Weights[i][j][k] = swap + delta;
                        double h1 = APerceptronLogic.Loss(this.Output(v, lasteq), t);

                        this.Weights[i][j][k] = swap - delta;
                        double h2 = APerceptronLogic.Loss(this.Output(v, lasteq), t);

                        this.Weights[i][j][k] = swap;

                        double x = ritsu * ((h1 - h2) / (delta * 2)) * (-1);
                        ret[i][j][k] = x;
                    }
                }
            }

            return ret;
        }
    }
}
