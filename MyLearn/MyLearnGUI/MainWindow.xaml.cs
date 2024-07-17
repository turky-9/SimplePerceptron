using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyLearn;
using System.Diagnostics;

namespace MyLearnGUI
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double[] val = null;
            AndPerceptron p = new AndPerceptron();
            StringBuilder sb = new StringBuilder();

            val = new double[] { 0, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 0, 1 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 1 };
            sb.Append(p.Get(val).ToString());

            MessageBox.Show(sb.ToString());
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            double[] val = null;
            OrPerceptron p = new OrPerceptron();
            StringBuilder sb = new StringBuilder();

            val = new double[] { 0, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 0, 1 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 1 };
            sb.Append(p.Get(val).ToString());

            MessageBox.Show(sb.ToString());
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            double[] val = null;
            NandPerceptron p = new NandPerceptron();
            StringBuilder sb = new StringBuilder();

            val = new double[] { 0, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 0, 1 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 1 };
            sb.Append(p.Get(val).ToString());

            MessageBox.Show(sb.ToString());
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            double[] val = null;
            XorPerceptron p = new XorPerceptron();
            StringBuilder sb = new StringBuilder();

            val = new double[] { 0, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 0 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 0, 1 };
            sb.Append(p.Get(val).ToString() + " , ");

            val = new double[] { 1, 1 };
            sb.Append(p.Get(val).ToString());

            MessageBox.Show(sb.ToString());
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            PoorLayers layers = new LayerdXor();

            double[] val = null;
            StringBuilder sb = new StringBuilder();

            val = new double[] { 0, 0 };
            sb.Append(layers.Get(val)[0].ToString() + " , ");

            val = new double[] { 1, 0 };
            sb.Append(layers.Get(val)[0].ToString() + " , ");

            val = new double[] { 0, 1 };
            sb.Append(layers.Get(val)[0].ToString() + " , ");

            val = new double[] { 1, 1 };
            sb.Append(layers.Get(val)[0].ToString());

            MessageBox.Show(sb.ToString());
        }


        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //Func<double, double> fnc = x => x * x;
            //for (double i = -10; i <= 10; i++)
            //{
            //    Debug.WriteLine(i.ToString() + ":" + this.Bibun(fnc, i) + ":" + (2 * i).ToString());
            //}

            this.Bibun(null, 0);
        }

        private double Bibun(Func<double, double> fnc, double x)
        {
            /*
            const double delta = 0.0001;

            double h = fnc(x);
            double h1 = fnc(x + delta);

            return (h1 - h) / delta;
            */

            int[] hoge = new int[] { 1, 2 };
            Debug.WriteLine(hoge[0].ToString());
            this.foo(hoge);
            Debug.WriteLine(hoge[0].ToString());


            string[][] ary2 = new string[3][];
            ary2[0] = new string[] { "item1" };
            ary2[1] = new string[] { "item1", "item2" };
            ary2[2] = new string[] { "item1", "item2", "item3" };

            Debug.WriteLine(ary2[1][1]);

            return 0;
        }
        private void foo(int[] h)
        {
            h[0] = 100;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            NNAnd nn = new NNAnd();

            double[] d1 = new double[] { 0, 0 };
            double[] d2 = new double[] { 1, 0 };
            double[] d3 = new double[] { 0, 1 };
            double[] d4 = new double[] { 1, 1 };

            double[][] dd = new double[][] { d1, d2, d3, d4 };

            //
            //AND
            //
            StringBuilder sb = new StringBuilder();
            sb.Append(nn.Get(d1).ToString() + " , ");
            sb.Append(nn.Get(d2).ToString() + " , ");
            sb.Append(nn.Get(d3).ToString() + " , ");
            sb.Append(nn.Get(d4).ToString());
            Debug.WriteLine(sb.ToString());
            //Debug.WriteLine(nn.ToString2()); //中身

            for (int i = 0; i < 10000; i++)
            {
                //各々
                //nn.Learn(d1, 0);
                //nn.Learn(d2, 0);
                //nn.Learn(d3, 0);
                //nn.Learn(d4, 1);

                //バッチ
                //nn.LearnedBatch(dd, new double[] { 0, 0, 0, 1 });

                //パラメータ
                nn.LearnedBatch2(dd, new double[] { 0, 0, 0, 1 });
            }

            sb = new StringBuilder();
            sb.Append(nn.Get(d1).ToString() + " , ");
            sb.Append(nn.Get(d2).ToString() + " , ");
            sb.Append(nn.Get(d3).ToString() + " , ");
            sb.Append(nn.Get(d4).ToString());
            Debug.WriteLine(sb.ToString());
            //Debug.WriteLine(nn.ToString2()); //中身

            //
            //OR
            //
            nn = new NNAnd();
            sb = new StringBuilder();
            sb.Append(nn.Get(d1).ToString() + " , ");
            sb.Append(nn.Get(d2).ToString() + " , ");
            sb.Append(nn.Get(d3).ToString() + " , ");
            sb.Append(nn.Get(d4).ToString());
            Debug.WriteLine(sb.ToString());

            for (int i = 0; i < 10000; i++)
            {
                nn.Learn(d1, 0);
                nn.Learn(d2, 1);
                nn.Learn(d3, 1);
                nn.Learn(d4, 1);
            }

            sb = new StringBuilder();
            sb.Append(nn.Get(d1).ToString() + " , ");
            sb.Append(nn.Get(d2).ToString() + " , ");
            sb.Append(nn.Get(d3).ToString() + " , ");
            sb.Append(nn.Get(d4).ToString());
            Debug.WriteLine(sb.ToString());
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MySimpleNN nn = new MySimpleNN(2, new int[] { 2, 2 });

            double[] d1 = new double[] { 0, 0 };
            double[] d2 = new double[] { 1, 0 };
            double[] d3 = new double[] { 0, 1 };
            double[] d4 = new double[] { 1, 1 };
            double[][] dd = new double[][] { d1, d2, d3, d4 };

            double[] t1 = new double[] { 0, 0 };
            double[] t2 = new double[] { 1, 0 };
            double[] t3 = new double[] { 1, 0 };
            double[] t4 = new double[] { 0, 1 };
            double[][] tt = new double[][] { t1, t2, t3, t4 };

            StringBuilder sb = null;
            for (int i = 0; i < 30000; i++)
            {
                nn.Learn(d1, t1);
                nn.Learn(d2, t2);
                nn.Learn(d3, t3);
                nn.Learn(d4, t4);
                if ((i % 1000) == 0)
                {
                    sb = new StringBuilder();
                    sb.Append(i.ToString() + ":" + nn.Output(d1)[0].ToString() + " , ");
                    sb.Append(nn.Output(d2)[0].ToString() + " , ");
                    sb.Append(nn.Output(d3)[0].ToString() + " , ");
                    sb.Append(nn.Output(d4)[0].ToString() + Environment.NewLine);
                    sb.Append(i.ToString() + ":" + nn.Output(d1)[1].ToString() + " , ");
                    sb.Append(nn.Output(d2)[1].ToString() + " , ");
                    sb.Append(nn.Output(d3)[1].ToString() + " , ");
                    sb.Append(nn.Output(d4)[1].ToString());
                    Debug.WriteLine(sb.ToString());
                }
            }

            sb = new StringBuilder();
            sb.Append(nn.Output(d1)[0].ToString() + " , ");
            sb.Append(nn.Output(d2)[0].ToString() + " , ");
            sb.Append(nn.Output(d3)[0].ToString() + " , ");
            sb.Append(nn.Output(d4)[0].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d1)[1].ToString() + " , ");
            sb.Append(nn.Output(d2)[1].ToString() + " , ");
            sb.Append(nn.Output(d3)[1].ToString() + " , ");
            sb.Append(nn.Output(d4)[1].ToString());
            Debug.WriteLine(sb.ToString());
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            MySimpleNN nn = new MySimpleNN(1, new int[] { 30, 1 });


            Debug.WriteLine("learning...");
            StringBuilder sb = null;
            for (int x = 0; x < 1000; x++)
            {
                for (double i = -5; i <= 5; i += 1)
                {
                    nn.Learn(new double[] { i }, new double[] { i * i }, true);
                }
                //Debug.WriteLine("-1:1:" + nn.Output(new double[] { -1 }, true)[0].ToString());
                //Debug.WriteLine("0:0:" + nn.Output(new double[] { 0 }, true)[0].ToString());
                //Debug.WriteLine("1:1:" + nn.Output(new double[] { 1 }, true)[0].ToString());
                //Debug.WriteLine("");
            }

            Debug.WriteLine("learned");
            for (double i = -5; i <= 5; i += 0.1 )
            {
                sb = new StringBuilder();
                sb.Append(i.ToString() + ":" + (i * i).ToString() + ":" + nn.Output(new double[] { i }, true)[0].ToString());
                Debug.WriteLine(sb.ToString());
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            //BS	PL	取引先	口座	bsにしろ	取引先にしろ
            //0	    0	0	    0	    1	        1
            //0    	0	0	    1	    1	        1
            //0    	0	1	    0	    1	        0
            //0    	0	1	    1	    1        	0
            //0    	1	0	    0	    1	        1
            //0    	1	0	    1	    1	        1
            //0    	1	1	    0	    1	        0
            //0    	1	1	    1	    1	        0
            //1    	0	0	    0	    0	        1
            //1    	0	0	    1	    0	        1
            //1    	0	1	    0	    0	        0
            //1    	0	1	    1	    0	        0
            //1    	1	0	    0	    0	        1
            //1    	1	0	    1	    0	        1
            //1    	1	1	    0	    0	        0
            //1    	1	1	    1	    0	        0

            MySimpleNN nn = new MySimpleNN(16, new int[] { 16,2 });

            double[] d1 = new double[] { 0, 0, 0, 0 };
            double[] d2 = new double[] { 0, 0, 0, 1 };
            double[] d3 = new double[] { 0, 0, 1, 0 };
            double[] d4 = new double[] { 0, 0, 1, 1 };
            double[] d5 = new double[] { 0, 1, 0, 0 };
            double[] d6 = new double[] { 0, 1, 0, 1 };
            double[] d7 = new double[] { 0, 1, 1, 0 };
            double[] d8 = new double[] { 0, 1, 1, 1 };
            double[] d9 = new double[] { 1, 0, 0, 0 };
            double[] d10 = new double[] { 1, 0, 0, 1 };
            double[] d11 = new double[] { 1, 0, 1, 0 };
            double[] d12 = new double[] { 1, 0, 1, 1 };
            double[] d13 = new double[] { 1, 1, 0, 0 };
            double[] d14 = new double[] { 1, 1, 0, 1 };
            double[] d15 = new double[] { 1, 1, 1, 0 };
            double[] d16 = new double[] { 1, 1, 1, 1 };

            double[][] dd = new double[][] { d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16 };


            double[] t1 = new double[] { 1, 0 }; //例外を想定
            //double[] t1 = new double[] { 1, 1 }; //通常
            double[] t2 = new double[] { 1, 1 };
            double[] t3 = new double[] { 1, 0 };
            double[] t4 = new double[] { 1, 0 };
            double[] t5 = new double[] { 1, 1 };
            double[] t6 = new double[] { 1, 1 };
            double[] t7 = new double[] { 1, 0 };
            double[] t8 = new double[] { 1, 0 };
            double[] t9 = new double[] { 0, 1 };
            double[] t10 = new double[] { 0, 1 };
            double[] t11 = new double[] { 0, 0 };
            double[] t12 = new double[] { 0, 0 };
            double[] t13 = new double[] { 0, 1 };
            double[] t14 = new double[] { 0, 1 };
            double[] t15 = new double[] { 0, 0 };
            double[] t16 = new double[] { 0, 0 };

            double[][] tt = new double[][] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16 };

            StringBuilder sb = null;
            for (int i = 0; i < 5000; i++)
            {
                nn.Learn(d1, t1);
                nn.Learn(d2, t2);
                nn.Learn(d3, t3);
                nn.Learn(d4, t4);
                nn.Learn(d5, t5);
                nn.Learn(d6, t6);
                nn.Learn(d7, t7);
                nn.Learn(d8, t8);
                nn.Learn(d9, t9);
                nn.Learn(d10, t10);
                nn.Learn(d11, t11);
                nn.Learn(d12, t12);
                nn.Learn(d13, t13);
                nn.Learn(d14, t14);
                nn.Learn(d15, t15);
                nn.Learn(d16, t16);
            }

            sb = new StringBuilder();
            sb.Append(nn.Output(d1)[0].ToString() + " , " + nn.Output(d1)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d2)[0].ToString() + " , " + nn.Output(d2)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d3)[0].ToString() + " , " + nn.Output(d3)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d4)[0].ToString() + " , " + nn.Output(d4)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d5)[0].ToString() + " , " + nn.Output(d5)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d6)[0].ToString() + " , " + nn.Output(d6)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d7)[0].ToString() + " , " + nn.Output(d7)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d8)[0].ToString() + " , " + nn.Output(d8)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d9)[0].ToString() + " , " + nn.Output(d9)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d10)[0].ToString() + " , " + nn.Output(d10)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d11)[0].ToString() + " , " + nn.Output(d11)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d12)[0].ToString() + " , " + nn.Output(d12)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d13)[0].ToString() + " , " + nn.Output(d13)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d14)[0].ToString() + " , " + nn.Output(d14)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d15)[0].ToString() + " , " + nn.Output(d15)[1].ToString() + Environment.NewLine);
            sb.Append(nn.Output(d16)[0].ToString() + " , " + nn.Output(d16)[1].ToString() + Environment.NewLine);
            Debug.WriteLine(sb.ToString());
        }

    }
}
