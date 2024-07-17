using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLearn
{
    /// <summary>
    /// パーセプトロン
    /// </summary>
    public abstract class PoorPerceptron
    {
        /// <summary>
        /// バイアスはWeightの最後にする
        /// </summary>
        //public double Bias { get; internal set; }

        /// <summary>
        /// 重み
        /// </summary>
        public double[] Weights { get; internal set; }

        /// <summary>
        /// 活性化関数
        /// </summary>
        public Func<double, double> ActivateFunc { get; protected set; }


        public PoorPerceptron()
        {
            this.ActivateFunc = this.DefActivateFunc;
        }

        public virtual double Get(double[] val)
        {
            return this.Calc(val, this.Weights);
        }

        protected virtual double Calc(double[] v, double[] w)
        {
            double work = w[w.Length - 1];

            for (int i = 0; i < v.Length; i++)
            {
                work += v[i] * w[i];
            }

            return this.ActivateFunc(work);
        }

        protected double DefActivateFunc(double v)
        {
            if (v >= 0)
                return 1;
            else
                return 0;
        }

        protected double SigmoidActivateFunc(double v)
        {
            return 1 / (1 + Math.Exp(-v));
        }

        protected double EqualActivateFunc(double v)
        {
            return v;
        }
    }

    #region primitives
    public class AndPerceptron : PoorPerceptron
    {
        public AndPerceptron()
        {
            this.Weights = new double[3];
            this.Weights[0] = 0.3;
            this.Weights[1] = 0.3;
            this.Weights[2] = -0.5;
        }
    }

    public class OrPerceptron : PoorPerceptron
    {
        public OrPerceptron()
        {
            this.Weights = new double[3];
            this.Weights[0] = 0.3;
            this.Weights[1] = 0.3;
            this.Weights[2] = -0.1;
        }
    }

    public class NandPerceptron : PoorPerceptron
    {
        public NandPerceptron()
        {
            this.Weights = new double[3];
            this.Weights[0] = -0.3;
            this.Weights[1] = -0.3;
            this.Weights[2] = 0.5;
        }
    }

    public class XorPerceptron : PoorPerceptron
    {
        public AndPerceptron _and { get; protected set; }
        public OrPerceptron _or { get; protected set; }
        public NandPerceptron _nand { get; protected set; }

        public XorPerceptron()
        {
            this._and = new AndPerceptron();
            this._or = new OrPerceptron();
            this._nand = new NandPerceptron();
        }

        public override double Get(double[] val)
        {
            double or_result = this._or.Get(val);

            double nand_result = this._nand.Get(val);

            double[] in_and = new double[] { or_result, nand_result };

            return this._and.Get(in_and);
        }
    }
    #endregion

}
