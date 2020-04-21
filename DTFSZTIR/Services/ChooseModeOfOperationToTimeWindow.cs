using DTFSZTIR.Models;

namespace DTFSZTIR.Services
{
    public class ChooseModeOfOperationToTimeWindow
    {
        public static int Choose(int startTime, int endTime, Resource[] resources, int resourceIndex, int cut_mode)
        {
            int return_value;
            if (cut_mode == 1)
            {
                return_value = SetOperationToTimeWindowWithCut.Set(startTime, endTime, resources, resourceIndex);
            }
            else
            {
                return_value = SetOperationToTimeWindow.Set(startTime, endTime, resources, resourceIndex);
            }
            return return_value;
        }
    }
}
