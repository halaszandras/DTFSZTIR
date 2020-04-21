using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Schedulers
{
    public class ExpandedJohnson_Scheduler
    {
        public static void Schedule(Job[] jobs, int numberOfJobs, int resourceIndex, int[] solution)
        {
            Job[] virtualJobTable = new Job[numberOfJobs];
            int min_0, max_1, min_2;

            for (int i = 0; i < numberOfJobs; i++)
            {
                virtualJobTable[i].ProcessTime = new int[2];
                virtualJobTable[i].ProcessTime[0] = jobs[i].ProcessTime[resourceIndex] + jobs[i].ProcessTime[resourceIndex + 1];
                virtualJobTable[i].ProcessTime[1] = jobs[i].ProcessTime[resourceIndex + 1] + jobs[i].ProcessTime[resourceIndex + 2];
            }

            Johnson_Scheduler.Schedule(virtualJobTable, numberOfJobs, 0, solution);

            //optimalitás vizsgálat
            min_0 = jobs[0].ProcessTime[0];
            max_1 = jobs[0].ProcessTime[1];
            min_2 = jobs[0].ProcessTime[2];

            for (int i = 0; i < numberOfJobs; i++)
            {
                if (min_0 > jobs[i].ProcessTime[0])
                    min_0 = jobs[i].ProcessTime[0];

                if (max_1 < jobs[i].ProcessTime[1])
                    max_1 = jobs[i].ProcessTime[1];

                if (min_2 > jobs[i].ProcessTime[2])
                    min_2 = jobs[i].ProcessTime[2];
            }
            if (min_0 <= max_1 || min_2 <= max_1)
            {
                Console.WriteLine("Optimális.");
            }
            else
            {
                Console.WriteLine("Nem biztos, hogy optimális.");
            }
        }
    }
}
