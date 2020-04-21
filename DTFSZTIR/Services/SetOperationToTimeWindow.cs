using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Services
{
    public class SetOperationToTimeWindow
    {
        public static int Set(int startTime, int endTime, Resource[] resources, int resourceIndex)
        {//visszaadja melyik TW a megfelelő
            //azt a TimeWindow-t keresi, ahova az adott operációt el tudja helyezni megszakítás nélkül

            int modified_st = startTime;
            int modified_et = endTime;
            int execution_time = endTime - startTime; //végrehajtási idő

            int f = -1;
            int c = 0; //hanyadik TW

            while (c < resources[resourceIndex].IntervalNumber)
            {
                if (modified_st < resources[resourceIndex].Intervals[c].EndTime)// ez esetben tudjuk csak vizsgálni
                {
                    //hatarra illesztes ha kell
                    modified_st = Math.Max(modified_st, resources[resourceIndex].Intervals[c].StartTime);
                    modified_et = modified_st + execution_time;
                    if (modified_et <= resources[resourceIndex].Intervals[c].EndTime)
                    {//belefer
                        f = c;
                        break;
                    }
                    else
                    {//kilog
                        c++;
                        if (c >= resources[resourceIndex].IntervalNumber)
                        {
                            modified_st = resources[resourceIndex].Intervals[c - 1].EndTime;
                            modified_et = modified_st + execution_time;
                            break;
                        }
                        continue;
                    }

                }
                c++;
            }
            startTime = modified_st;
            endTime = modified_et;
            return f;
        }
    }
}
