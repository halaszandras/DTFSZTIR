namespace DTFSZTIR.Services
{
    public class CopyGoalFunctionsToNewInstance
    {
        public static void Copy(double[] f1, double[] f2, int k)
        {
            for (int i = 0; i < k; i++)
            {
                f1[i] = f2[i];
            }
        }
    }
}
