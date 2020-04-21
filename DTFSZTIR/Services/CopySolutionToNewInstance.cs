namespace DTFSZTIR.Services
{
    public class CopySolutionToNewInstance
    {
        public static void Copy(int[] solution1, int[] solution2, int numberOfJobs)
        {
            for (int j = 0; j < numberOfJobs; j++)
            {
                solution1[j] = solution2[j];
            }
        }
    }
}
