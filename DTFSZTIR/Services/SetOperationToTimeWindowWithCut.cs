using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Services
{
    public class SetOperationToTimeWindowWithCut
    {
        public static int Set(int startTime, int endTime, Resource[] resources, int resourceIndex)
        {//visszaadja melyik TW a megfelelő
            //azt a TimeWindow-t keresi, ahova az adott operációt el tudja helyezni megszakítással
            int modifiedStartTime = startTime;
            int modifiedEndTime = endTime;
            int executionTime = endTime - startTime; //végrehajtási idő

            int f = -1;
            int c = 0; //hanyadik TW
            int fps = -1; //elso resz kezdete

            while (c < resources[resourceIndex].IntervalNumber)
            {
                if (modifiedStartTime < resources[resourceIndex].Intervals[c].EndTime)// ez esetben tudjuk csak vizsgálni
                {
                    //hatarra illesztes ha kell
                    modifiedStartTime = Math.Max(modifiedStartTime, resources[resourceIndex].Intervals[c].StartTime);
                    modifiedEndTime = modifiedStartTime + executionTime;

                    if (fps == -1)
                        fps = modifiedStartTime;  //elso darab inditasa

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
                            modifiedEndTime = modifiedStartTime + executionTime;
                            break;
                        }
                        executionTime -= resources[resourceIndex].Intervals[c - 1].EndTime - modifiedStartTime;
                        continue;
                    }

                }
                c++;
            }
            if (fps != -1)
                startTime = fps;
            else
                startTime = modifiedStartTime;

            endTime = modifiedEndTime;
            return f;
        }
    }
}
