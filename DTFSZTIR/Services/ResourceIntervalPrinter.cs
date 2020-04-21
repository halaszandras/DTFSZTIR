using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Services
{
    public class ResourceIntervalPrinter
    {
        public static void Print(Resource[] resource, int numberOfResources)
        {
            Console.WriteLine("\n\n Eroforrasok rendelkezesre allasi idointervallumai");
            for (int r = 0; r < numberOfResources; r++)
            {
                Console.WriteLine("\n %d. eroforras [%d]", r, resource[r].IntervalNumber);
                Console.WriteLine("\n # \t Kezdet\tVege");
                for (int c = 0; c < resource[r].IntervalNumber; c++)
                {
                    Console.WriteLine("\n %d \t %ld \t %ld", c, resource[r].Intervals[c].StartTime, resource[r].Intervals[c].EndTime);
                }

            }
        }

    }
}
