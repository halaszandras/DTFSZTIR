using DTFSZTIR.Models;
using System;
using System.Collections.Generic;

namespace DTFSZTIR.Schedulers
{
    public class Palmer_Scheduler
    {
        public static void Schedule(Job[] jobs, int numberOfJobs, int numberOfResources, int[] solution)
        {
            List<PalmerJob> palmerPriority = new List<PalmerJob>(); //prioritáslista

            foreach (var j in jobs)
            {
                PalmerJob palmerJob = new PalmerJob(j.Id, 0);
                palmerPriority.Add(palmerJob);
            }


            for (int i = 0; i < numberOfJobs; i++)
            {
                //I[i] = 0;
                for (int j = 0; j < numberOfResources; j++)
                {
                    palmerPriority[i].Priority += -1 * jobs[i].ProcessTime[j] * (numberOfResources - (2 * (j + 1) - 1)) / 2;

                }
            }
            for (int i = 0; i < numberOfJobs; i++)
            {
                solution[i] = i;
            }            
            //Ütemezés prioritás alapján
            palmerPriority.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            palmerPriority.Reverse();
            
            for (int i = 0; i < palmerPriority.Count; i++)
            {
                solution[i] = palmerPriority[i].Id;
            }
        }
    }
}
