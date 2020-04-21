using DTFSZTIR.Models;

namespace DTFSZTIR.Schedulers
{
    public class CDS_Scheduler
    {
        //Fm|perm|Cmax
        public static void Schedule(Job[] jobs, int numberOfJobs, Resource[] resources, int numberOfResources, int[] solution, int cut_mode)
        {
            int[] actualSolution = new int[numberOfJobs]; //aktuális
            int[] bestSolution = new int[numberOfJobs]; //legjobb

            int actualCompletionTime; //completion time
            int bestCompletionTime = 0;

            for (int j = 0; j < numberOfJobs; j++)
            {
                //virtuális kétgépes feladat Johnsonnal
                Johnson_Scheduler.Schedule(jobs, numberOfJobs, j, actualSolution);
                //szimu
                Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, actualSolution, 0, cut_mode);
                //kiertekeles
                actualCompletionTime = jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1];

                if (j == 0)
                {
                    bestCompletionTime = actualCompletionTime;
                    Services.CopySolutionToNewInstance.Copy(bestSolution, actualSolution, numberOfJobs);
                }
                else
                {
                    if (bestCompletionTime > actualCompletionTime)
                    {
                        bestCompletionTime = actualCompletionTime;
                        Services.CopySolutionToNewInstance.Copy(bestSolution, actualSolution, numberOfJobs);
                    }
                }
            }
        }        
    }
}
