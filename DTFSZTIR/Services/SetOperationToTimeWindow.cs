using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Services
{
    public class SetOperationToTimeWindow
    {
        public static int Set(int startTime, int endTime, Resource[] resources, int resourceIndex)
        {//visszaadja melyik TW a megfelelő
            //azt a TimeWindow-t keresi, ahova az adott operációt el tudja helyezni megszakítás nélkül

            int modifiedStartTime = startTime;
            int modifiedEndTime = endTime;
            int executionTime = endTime - startTime;

            int f = -1;
            int c = 0; //hanyadik TW

            while (c < resources[resourceIndex].IntervalNumber)
            {
                if (modifiedStartTime < resources[resourceIndex].Intervals[c].EndTime)// ez esetben tudjuk csak vizsgálni
                {
                    //hatarra illesztes ha kell
                    modifiedStartTime = Math.Max(modifiedStartTime, resources[resourceIndex].Intervals[c].StartTime);
                    modifiedEndTime = modifiedStartTime + executionTime;
                    if (modifiedEndTime <= resources[resourceIndex].Intervals[c].EndTime)
                    {//belefer
                        f = c;
                        break;
                    }
                    else
                    {//kilog
                        c++;
                        if (c >= resources[resourceIndex].IntervalNumber)
                        {
                            modifiedStartTime = resources[resourceIndex].Intervals[c - 1].EndTime;
                            modifiedEndTime = modifiedStartTime + executionTime;
                            break;
                        }
                        continue;
                    }

                }
                c++;
            }
            startTime = modifiedStartTime;
            endTime = modifiedEndTime;
            return f;
        }
    }
}
