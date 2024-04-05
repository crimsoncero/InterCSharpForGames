

using System.Collections;

namespace InterC_ForGames
{
    public static class Extensions
    {

        public static void PrintCollection(this IEnumerable coll)
        {
            foreach (var item in coll)
            {
                Console.WriteLine(item);
            }
        }

        public static T GetHighest<T,TKey>(this IEnumerable<T> coll, Func<T,TKey> keySelector)
        {
            var orderedColl = coll.OrderByDescending(keySelector);
            return orderedColl.First();
        }


    }


}
