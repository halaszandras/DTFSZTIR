using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Evaluators
{
    public class Evaluator
    {
        //F|perm|Cmax esetén a célfüggvény, késés, csuszas stb. értéke
        public static void Evaluate(Job[] jobs, int numberOfJobs, int numberOfResources, int[] solution, double[] GoalFunctions)
        {
            int completion; //bef.idopont
            int lateness; //keses
            int tardiness; //csuszas

            double Tmax = 0; //max csuszás
            double Tsum = 0; //csúszás összeg
            double Usum = 0; //késő munkak szama

            for (int i = 0; i < numberOfJobs; i++)
            {
                //adott munka, utolsó erőforráson a bef.ideje
                completion = jobs[i].EndTime[numberOfResources - 1];
                //bef.ido - határidő
                lateness = completion - jobs[i].DueDate;
                //maximum a 0, keses
                tardiness = Math.Max(0, lateness);
                if (i == 0)
                    Tmax = tardiness;
                else
                if (Tmax < tardiness)
                    Tmax = tardiness;

                Tsum += tardiness;

                if (tardiness > 0)
                    Usum++;

                //célfgvek
                GoalFunctions[0] = jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1];  //Cmax: F|perm|Cmax eseteben
                GoalFunctions[1] = Tmax;
                GoalFunctions[2] = Tsum;
                GoalFunctions[3] = Usum;
            }
        }

    }
}
