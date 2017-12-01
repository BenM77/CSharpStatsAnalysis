using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    class Program
    {
        static double[] seqLength(int start, int end, int length)
        {
            double[] seq = new double[length];

            int diff = end - start;
            length--;

            double increment = diff / (double)length;

            int index = 0;
            for(double i = start; i <= end; i += increment)
            {
                seq[index] = i;
                index++;
            }

            return seq;
        }

        static int[] seqBy(int start, int end, int by)
        {
            int numArrElements = (end - start) / by;
            int[] seq = new int[numArrElements+1];

            int index = 0;
            for(int i = start; i <= end; i += by)
            {
                seq[index] = i;
                index++;
            }

            return seq;
        }

        static void Main(string[] args)
        {
            int[] test = seqBy(1, -10, 5);
            for(int i = 0; i < test.Length; i++)
                Console.WriteLine(test[i]);
        }
    }
}
