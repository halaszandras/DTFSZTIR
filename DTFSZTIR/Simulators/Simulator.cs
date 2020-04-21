using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Simulators
{
    public class Simulator
    {
        public static void Simulate(Job[] job, int numberOfJobs, Resource[] resources, int numberOfResources, int[] solution, int baseTime, int cut_mode)
        {
            for (int i = 0; i < numberOfJobs; i++)
            {
                for (int r = 0; r < numberOfResources; r++)
                {
                    if (i == 0) //legelso munka
                    {
                        if (r == 0)//legelso gepen
                        {
                            job[solution[i]].StartTime[r] = baseTime;
                        }
                        else //nem a legelso gepen
                        {
                            job[solution[i]].StartTime[r] = job[solution[i]].EndTime[r - 1] + resources[r].TransportTime[r - 1]; //előző befejézési ideje plusz a mozgatas
                        }
                        //megállapítjuk a befejezési időt: indul+dolgozik+átáll
                        job[solution[i]].EndTime[r] = job[solution[i]].StartTime[r] + resources[r].SetupTime[0, solution[i]] + job[solution[i]].ProcessTime[r];
                    }
                    else//nem a legelso munka
                    {
                        if (r == 0)//legelso gepen
                        {
                            job[solution[i]].StartTime[r] = job[solution[i - 1]].EndTime[r];
                        }
                        else//nem a legelso gepen
                        {
                            job[solution[i]].StartTime[r] = Math.Max(job[solution[i]].EndTime[r - 1] + resources[r].TransportTime[r - 1], job[solution[i - 1]].EndTime[r]); //előző gépről mikor indul és elozo munka mikor fejeződik be
                        }
                        //megállapítjuk a befejezési időt: indul+dolgozik+átáll
                        job[solution[i]].EndTime[r] = job[solution[i]].StartTime[r] + resources[r].SetupTime[solution[i - 1], solution[i]] + job[solution[i]].ProcessTime[r];
                    }
                    //eroforrashoz illesztes
                    //!!!
                    int vmi = cut_mode;
                }

            }
        }       
    }
}
