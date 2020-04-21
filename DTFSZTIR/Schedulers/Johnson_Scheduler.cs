using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Schedulers
{
    public class Johnson_Scheduler
    {
        //F2|perm|Cmax 
        //U={1,2,3,4}
        //S={?,?,?,?}
        public static void Schedule(Job[] jobs, int numberOfJobs, int resourceIndex, int[] solution)
        {
            int index, temp;
            int value, val_of_j;
            int[] u = new int[numberOfJobs]; //előrendezés
            int first, last; //szabad helyek indexe

            for (int i = 0; i < numberOfJobs; i++)
            {
                u[i] = i; //kiindulás, ütemezni kívánt munkák id-je
            }

            //előrendezés
            for (int i = 0; i < numberOfJobs - 1; i++) // így megy végig az U tömbön
            {
                index = i;
                value = Math.Min(jobs[u[i]].ProcessTime[resourceIndex], jobs[u[i]].ProcessTime[resourceIndex + 1]);
                for (int j = i + 1; j < numberOfJobs; j++) //1 sorban mi a minimum
                {
                    val_of_j = Math.Min(jobs[u[j]].ProcessTime[resourceIndex], jobs[u[j]].ProcessTime[resourceIndex + 1]);
                    if (val_of_j < value)
                    {
                        index = j;
                        value = val_of_j;
                    }
                }
                if (index != i) //van jobb jelölt
                {  //csere
                    temp = u[index];
                    u[index] = u[i];
                    u[i] = temp;
                }
            }

            //ütemezés
            first = 0;    //eleje
            last = numberOfJobs - 1;  //vege

            for (int i = 0; i < numberOfJobs-1; i++)
            {
                if (jobs[u[i]].ProcessTime[resourceIndex] <= jobs[u[i]].ProcessTime[resourceIndex + 1])
                { //elölről nézve az elso szabad helyre
                    solution[first] = u[i];
                    first++;
                }
                else
                { //hátulsó az első szabad helyre
                    solution[last] = u[i];
                    last--;
                }
            }
        }
    }
}
