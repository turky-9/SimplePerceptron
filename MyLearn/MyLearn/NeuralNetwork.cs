using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MyLearn
{
    public enum EActivateFunc
    {
        Step = 1,
        Sigmoid = 2,
        Equal = 3
    }


    /// <summary>
    /// 乱数で重みとバイアスを初期化する
    /// </summary>
    public class LearnPerceptron : PoorPerceptron
    {
        protected static Random RND = new Random();

        /// <summary>
        /// 引数は入力の個数 -> 重みの個数（バイアス含む）
        /// </summary>
        /// <param name="cnt"></param>
        public LearnPerceptron(int cnt, EActivateFunc ef)
        {
            this.Weights = new double[cnt + 1];
            this.Init(ef);
        }

        protected virtual void Init(EActivateFunc ef)
        {
            for (int i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] = this.GetRndValue();
            }

            switch (ef)
            {
                case EActivateFunc.Sigmoid:
                    this.ActivateFunc = this.SigmoidActivateFunc;
                    break;

                case EActivateFunc.Equal:
                    this.ActivateFunc = this.EqualActivateFunc;
                    break;
            }
        }

        protected double GetRndValue()
        {
            return (RND.NextDouble() * 2) - 1;
        }
    }

    public class NNAnd : LearnPerceptron
    {
        public NNAnd()
            : base(2, EActivateFunc.Sigmoid)
        {
            //Debug.WriteLine("横にV0、縦にV1。V0 * W0 + V1 * W1 + B = 0を以下の形に直す。");
            //Debug.WriteLine("V1 = (-W0/W1)*V0 - B/W1");
        }

        #region 各々
        public void Learn(double[] val, double teach)
        {
            Func<double, double> f0 = (x) =>
            {
                return Math.Pow(teach - this.SigmoidActivateFunc(x * val[0]               + this.Weights[1] * val[1] + this.Weights[2]), 2);
            };
            Func<double, double> f1 = (x) =>
            {
                return Math.Pow(teach - this.SigmoidActivateFunc(this.Weights[0] * val[0] + x * val[1]               + this.Weights[2]), 2);
            };
            Func<double, double> fb = (x) =>
            {
                return Math.Pow(teach - this.SigmoidActivateFunc(this.Weights[0] * val[0] + this.Weights[1] * val[1] + x), 2);
            };

            double ritsu = 0.01;
            double d1 = ritsu * this.NumericalDifferential(f0, this.Weights[0]) * (-1);
            double d2 = ritsu * this.NumericalDifferential(f1, this.Weights[1]) * (-1);
            double d3 = ritsu * this.NumericalDifferential(fb, this.Weights[2]) * (-1);

            this.Weights[0] += d1;
            this.Weights[1] += d2;
            this.Weights[2] += d3;

//            Debug.WriteLine("[A] " + this.ToString() + " -> " + ret.ToString());
//            Debug.WriteLine("V1 = " + ((-1) * this.Weights[0] / this.Weights[1]).ToString() + " * V0 - " + (this.Bias / this.Weights[1]).ToString());
//            Debug.WriteLine(d1.ToString() + "," + d2.ToString() + "," + d3.ToString());
        }
        #endregion

        #region バッチ
        public void LearnedBatch(double[][] vals, double[] teach)
        {
//            Debug.WriteLine("[B] " + this.ToString());

            double ritsu = 0.05;

            double[] dd1 = new double[vals.Length];
            double[] dd2 = new double[vals.Length];
            double[] dd3 = new double[vals.Length];

            for(int i = 0; i < vals.Length; i++)
            {
                double[] val = vals[i];
                double ret = this.Get(val);

                Func<double, double> f0 = (x) =>
                {
                    return Math.Pow(teach[i] - this.SigmoidActivateFunc(x * val[0]               + this.Weights[1] * val[1] + this.Weights[2]), 2);
                };
                Func<double, double> f1 = (x) =>
                {
                    return Math.Pow(teach[i] - this.SigmoidActivateFunc(this.Weights[0] * val[0] + x * val[1]               + this.Weights[2]), 2);
                };
                Func<double, double> fb = (x) =>
                {
                    return Math.Pow(teach[i] - this.SigmoidActivateFunc(this.Weights[0] * val[0] + this.Weights[1] * val[1] + x), 2);
                };

                double d1 = ritsu * this.NumericalDifferential(f0, this.Weights[0]) * (-1);
                double d2 = ritsu * this.NumericalDifferential(f1, this.Weights[1]) * (-1);
                double d3 = ritsu * this.NumericalDifferential(fb, this.Weights[2]) * (-1);

                dd1[i] = d1;
                dd2[i] = d2;
                dd3[i] = d3;
            }


            double avgd1 = dd1.Average();
            double avgd2 = dd2.Average();
            double avgd3 = dd3.Average();
            this.Weights[0] += avgd1;
            this.Weights[1] += avgd2;
            this.Weights[2] += avgd3;

//            Debug.WriteLine("[A] " + this.ToString() + " -> " + ret.ToString());
//            Debug.WriteLine("V1 = " + ((-1) * this.Weights[0] / this.Weights[1]).ToString() + " * V0 - " + (this.Bias / this.Weights[1]).ToString());
//            Debug.WriteLine(dd1.Average().ToString() + "," + dd2.Average().ToString() + "," + dd3.Average().ToString());
        }
        #endregion

        /// <summary>
        /// 数値微分
        /// </summary>
        /// <param name="fnc"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private double NumericalDifferential(Func<double, double> fnc, double x)
        {
            const double delta = 0.01;

            double h1 = fnc(x + delta);
            double h2 = fnc(x - delta);

            return (h1 - h2) / (delta * 2);
        }

#region パラメータ化してラムダ式しないよう修正
        public void LearnedBatch2(double[][] vals, double[] teach)
        {

            double[] dd1 = new double[vals.Length];
            double[] dd2 = new double[vals.Length];
            double[] dd3 = new double[vals.Length];

            for (int i = 0; i < vals.Length; i++)
            {
                double[] val = vals[i];

                double[] now = new double[3];
                now[0] = this.Weights[0];
                now[1] = this.Weights[1];
                now[2] = this.Weights[2];

                double[] tmp = this.NumericalDifferential2(teach[i], val, now);

                dd1[i] = tmp[0];
                dd2[i] = tmp[1];
                dd3[i] = tmp[2];
            }

            double avgd1 = dd1.Average();
            double avgd2 = dd2.Average();
            double avgd3 = dd3.Average();
            this.Weights[0] += avgd1;
            this.Weights[1] += avgd2;
            this.Weights[2] += avgd3;
        }

        private double[] NumericalDifferential2(double t, double[] v, double[] w)
        {
            double ritsu = 0.05;
            const double delta = 0.01;

            double[] ret = new double[w.Length];

            for (int i = 0; i < w.Length; i++)
            {
                double org = w[i];
                w[i] = org + delta;

                double[] tmp1 = new double[] { w[0], w[1], w[2] };
                double h1 = Math.Pow(t - this.Calc(v, tmp1), 2);

                w[i] = org - delta;
                tmp1 = new double[] { w[0], w[1], w[2] };
                double h2 = Math.Pow(t - this.Calc(v, tmp1), 2);

                w[i] = org;

                ret[i] = ritsu * ((h1 - h2) / (delta * 2)) * (-1);
            }

            return ret;
        }
#endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Weights[1].ToString());
            sb.Append(" : ");
            sb.Append(this.Weights[2].ToString());
            sb.Append(" : ");
            sb.Append(this.Weights[0].ToString());

            return sb.ToString();
        }

        public string ToString2()
        {
            return "V1 = " + ((-1) * this.Weights[1] / this.Weights[2]).ToString() + " * V0 - " + (this.Weights[0]/ this.Weights[2]).ToString() + Environment.NewLine
                + this.Weights[1].ToString() + ", " + this.Weights[2].ToString() + ", " + this.Weights[0].ToString();
        }
    }

}
