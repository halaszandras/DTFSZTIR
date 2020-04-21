using DTFSZTIR.Models;

namespace DTFSZTIR.Schedulers
{
    public class Dannenbring
    {
        //Fm|perm|Cmax
        public static void Scheduler(Job[] jobs, int numberOfJobs, int numberOfResources, int[] solution)
        {
            Job[] virtualJobTable = new Job[numberOfJobs];
            for (int i = 0; i < numberOfJobs; i++)
            {
                virtualJobTable[i] = new Job
                {
                    ProcessTime = new int[2]
                };
                virtualJobTable[i].ProcessTime[0] = 0;
                virtualJobTable[i].ProcessTime[1] = 0;

                for (int j = 0; j < numberOfResources; j++)
                {
                    virtualJobTable[i].ProcessTime[0] += jobs[i].ProcessTime[j] * (numberOfResources - (j + 1) + 1);
                    virtualJobTable[i].ProcessTime[1] += jobs[i].ProcessTime[j] * (j + 1);
                }
            }
            Johnson_Scheduler.Schedule(virtualJobTable, numberOfJobs, 0, solution);
        }

    }
}
