
using System.Net.Security;

namespace InterC_ForGames
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Distinct Ascending: ");
            foreach (var t in DistinctAscending(new int[] { 1, 45, 2, 1, 2, 2, 41, -1 }))
            {
                Console.WriteLine(t);
            }

            Console.WriteLine("\nAverage Word Length:");
            Console.WriteLine(AverageWordLength("Bla bla wooohoo, wdalwalf"));

            Console.WriteLine("\nStarts And Ends With:");
            foreach (var t in StartsAndEndsWith(new string[] { "Oy Hellos", "There", "General", "Helloooofo", "OnlyHans" }, 'O', 's'))
            {
                Console.WriteLine(t);
            }


            List<Enemy> es = new();
            for (int i = 0; i < 150; i++)
            {
                es.Add(new Enemy() { Damage = Random.Shared.Next(1, 150) });
            }
            // would print the enemy with the higest damage
            Console.WriteLine(es.GetHighest(e => e.Damage));
            Console.WriteLine();

        }

        public static IEnumerable<T> DistinctAscending<T>(IEnumerable<T> collection)
        {
            return collection.Distinct().OrderBy(x => x);
        }

        public static int AverageWordLength(string str)
        {
            string[] words = str.Split(' ', ',', '.', ':', ';', '?', '!');
            int sum = 0;

            foreach (string word in words)
            {
                sum += word.Length;
            }

            return sum / words.Count();
        }

        public static IEnumerable<string> StartsAndEndsWith(IEnumerable<string> strings, char start, char end)
        {
            return strings.Where((s) => s.StartsWith(start) && s.EndsWith(end));
        }



    }

    public class Enemy
    {
        public int Damage = 10;
        public override string ToString()
        {
            return Damage.ToString();
        }

    }
}



