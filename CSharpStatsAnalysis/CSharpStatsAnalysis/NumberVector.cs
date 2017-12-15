using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    public class NumberVector : Vector<double>
    {
        // standard double array
        public NumberVector(double[] arr)
        {
            vecArr = arr;
        }

        // standard int array
        public NumberVector(int[] arr)
        {
            vecArr = arr.Select(d => (double)d).ToArray();
        }

        // repeat or sequence
        public NumberVector(int start, int endOrLength, bool repeat)
        {
            // repeat
            if (repeat == true)
            {
                if(endOrLength < 0)
                    throw new Exception("Cannot create vector: length is negative.");

                vecArr = new double[endOrLength];
                for (int i = 0; i < vecArr.Length; i++)
                    vecArr[i] = start;
            }
            // sequence
            else
            {
                if(start > endOrLength)
                {
                    int temp = start;
                    start = endOrLength;
                    endOrLength = temp;
                }

                vecArr = new double[(endOrLength - start)+1];
                for(int i = 0; i < vecArr.Length; i++)
                    vecArr[i] = start++;
            }
        }

        // repeat with end parameter
        public NumberVector(int start, int end, int numRepeats)
        {
            if(start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            int counter = start;

            vecArr = new double[(end-start+1)*numRepeats];
            for (int i = 0; i < vecArr.Length; i++)
            {
                vecArr[i] = counter;
                if (counter == end)
                    counter = start;
                else
                    counter++;
            }
        }

        // repeat with array variation
        public NumberVector(int start, int end, int[] c)
        {
            int diff = end - start;
            if(diff != c.Length-1)
                throw new Exception("Cannot create vector: given array is not the correct length to match with elements");
            else
            {
                // checking to see if any integer within the array is less than 0
                testArrayInput(c);

                int numVecElements = 0;
                for(int i = 0; i < c.Length; i++)
                {
                    numVecElements += c[i];
                }
                vecArr = new double[numVecElements];

                // number of times an element should be repeated, as determined by its respective value in the c array
                int vecCounter;
                // index of current vecArr element
                int vecIndex = 0;
                // index of the c array
                int cIndex = 0;
                for(int i = start; i <= end; i++)
                {
                    vecCounter = 0;
                    // getting the number of times the current element should be repeated
                    vecCounter = c[cIndex];

                    // initial index before the current element is added to the vecArr
                    int vecInitialIndex = vecIndex;
                    while(vecIndex < vecInitialIndex + vecCounter)
                        vecArr[vecIndex++] = i;

                    cIndex++;
                }
            }
        }

        // sequence
        public NumberVector(double start, double end, bool byParameter, double byOrLength)
        {
            if(end < start)
            {
                // swapping start and end
                double temp = start;
                start = end;
                end = temp;
            }

            if (byParameter == true)
            {
                // creating a sequence using by
                int numArrElements = (int)((end - start) / byOrLength);
                vecArr = new double[numArrElements + 1];

                int index = 0;
                for (double i = start; i <= end; i += (int)byOrLength)
                {
                    vecArr[index] = i;
                    index++;
                }
            }
            else
            {
                // creating a sequence using length
                vecArr = new double[(int)byOrLength];

                double diff = end - start;
                byOrLength--;

                double increment = diff / byOrLength;

                int index = 0;
                for (double i = start; i <= end; i += increment)
                {
                    vecArr[index] = i;
                    index++;
                }
            }
        }

        // testing array input for negative numbers
        private void testArrayInput(int[] cArr)
        {
            foreach(int c in cArr)
            {
                if (c < 0)
                    throw new Exception("Cannot create vector: input array contains negative number");
            }
        }

        // tests to see if values exist within the vector's array
        private void testArray()
        {
            if (vecArr.Length == 0)
                throw new Exception("Cannot calculate statistics on vector: the vector's array contains no numbers");
        }

        public double getMean()
        {
            testArray();
            double total = 0;
            foreach(double x in vecArr)
            {
                total += x;
            }
            return total / vecArr.Length;
        }

        public double getMedian()
        {
            testArray();
            double[] vecArrCopy = vecArr;
            Array.Sort(vecArrCopy);

            // odd number of elements
            if (vecArr.Length % 2 == 1)
                return vecArrCopy[vecArrCopy.Length / 2];

            double result = vecArrCopy[vecArrCopy.Length / 2] + vecArrCopy[(vecArrCopy.Length / 2) - 1];
            // even number of elements
            return result / 2.0;
        }

        public double getMin()
        {
            testArray();

            double min = vecArr[0];
            for(int i = 1; i < vecArr.Length; i++)
            {
                if (vecArr[i] < min)
                    min = vecArr[i];
            }
            return min;
        }

        public double getMax()
        {
            testArray();

            double max = vecArr[0];
            for(int i = 1; i < vecArr.Length; i++)
            {
                if (vecArr[i] > max)
                    max = vecArr[i];
            }
            return max;
        }

        public double getLowerQuartile()
        {
            testArray();
            double[] vecArrCopy = vecArr;
            Array.Sort(vecArrCopy);

            double[] lowerHalf = vecArrCopy.Take(vecArrCopy.Length / 2).ToArray();

            // odd number of elements
            if (lowerHalf.Length % 2 != 0)
                return lowerHalf[lowerHalf.Length / 2];

            double test = lowerHalf[lowerHalf.Length / 2];
            // even number of elements
            return (lowerHalf[lowerHalf.Length / 2] + lowerHalf[(lowerHalf.Length / 2) - 1]) / 2.0;
        }

        public double getUpperQuartile()
        {
            testArray();
            double[] vecArrCopy = vecArr;
            Array.Sort(vecArrCopy);

            int skipFactor;
            skipFactor = vecArrCopy.Length / 2;

            if (vecArrCopy.Length % 2 == 1)
                skipFactor++;
            double[] upperHalf = vecArrCopy.Skip(skipFactor).Take(vecArrCopy.Length / 2).ToArray();

            // odd number of elements
            if (upperHalf.Length % 2 != 0)
                return upperHalf[upperHalf.Length / 2];

            // even number of elements
            double result = (upperHalf[upperHalf.Length / 2] + upperHalf[(upperHalf.Length / 2) - 1]);
            return  result / 2.0;
        }

        public void displayStats()
        {
            Console.WriteLine("Minimum: " + getMin());
            Console.WriteLine("Maximum: " + getMax());
            Console.WriteLine("Mean: " + getMean());
            Console.WriteLine("Median: " + getMedian());

            Console.Write("Mode: ");
            double[] modeArr = getMode();
            foreach(double x in modeArr)
            {
                Console.Write(x);
                if(x != modeArr[modeArr.Length - 1])
                    Console.Write(", ");
            }
            Console.WriteLine();

            Console.WriteLine("Lower Quartile (25%): " + getLowerQuartile());
            Console.WriteLine("Upper Quartile (75%): " + getUpperQuartile());
            Console.WriteLine("\n");
        }
    }
}
