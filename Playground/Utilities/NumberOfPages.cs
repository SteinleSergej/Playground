namespace Playground.Utilities
{
    public class NumberOfPages
    {
        public static int CountPages(int totalEntries, int recordsToTake)
        {
            int result = 0;

            if ((totalEntries % recordsToTake) != 0)
            {
                result = (totalEntries / recordsToTake) + 1;
            }
            else
            {
                result = totalEntries / recordsToTake;
            }
            return result;

        }
    }
}
