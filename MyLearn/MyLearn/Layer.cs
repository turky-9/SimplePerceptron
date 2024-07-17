using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLearn
{
    public class PoorLayer
    {
        public List<PoorPerceptron> PerceptronList { get; protected set; }

        public PoorLayer()
        {
            this.PerceptronList = new List<PoorPerceptron>();
        }

        public PoorLayer(IEnumerable<PoorPerceptron> lst)
        {
            this.PerceptronList = new List<PoorPerceptron>(lst);
        }

        public virtual double[] Get(double[] val)
        {
            List<double> ret = new List<double>();
            foreach (var p in this.PerceptronList)
            {
                ret.Add(p.Get(val));
            }

            return ret.ToArray();
        }
    }

    public class PoorLayers
    {
        public List<PoorLayer> LayerList { get; protected set; }

        public PoorLayers()
        {
            this.LayerList = new List<PoorLayer>();
        }

        public PoorLayers(IEnumerable<PoorLayer> lst)
        {
            this.LayerList = new List<PoorLayer>(lst);
        }

        public virtual double[] Get(double[] val)
        {
            List<double> in_val = new List<double>(val);
            List<Double> out_val = null;
            foreach (var l in this.LayerList)
            {
                out_val = new List<double>(l.Get(in_val.ToArray()));
                in_val = out_val;
            }

            return out_val.ToArray();
        }
    }

    public class LayerdXor : PoorLayers
    {
        public LayerdXor()
        {
            PoorLayer first = new PoorLayer();
            first.PerceptronList.Add(new OrPerceptron());
            first.PerceptronList.Add(new NandPerceptron());

            PoorLayer second = new PoorLayer();
            second.PerceptronList.Add(new AndPerceptron());

            this.LayerList.Add(first);
            this.LayerList.Add(second);
        }
    }
}
