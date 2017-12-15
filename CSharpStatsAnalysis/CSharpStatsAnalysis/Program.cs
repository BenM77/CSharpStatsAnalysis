namespace CSharpStatsAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberVector nv = new NumberVector(1, 10, false);
            NumberVector nv2 = new NumberVector(2, 6, 2);
            Matrix m = new Matrix(nv, nv2, true);

            double[] arr = new double[] { 1, 5, 8, 1, 3, 4, 9, 10, 2, 4, 4, 5, 6, 7, 3, 4, 5, 10, 9, 6 };
            NumberVector nv3 = new NumberVector(arr);
            Matrix m2 = new Matrix(nv3, 2, 10);

            NumberVector nv4 = m.ConvertToVector();
            nv4.displayStats();
        }
    }
}
