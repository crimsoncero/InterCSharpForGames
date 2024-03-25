
namespace InterC_ForGames
{
    class Program
    {
        public static void Main(String[] args)
        {
            

            int ElvesWinsAgainstHumans = 0;
            int ElvesWinsAgainstMonsters = 0;
            int HumanWinsAgainstElves = 0;
            int HumanWinsAgainstMonsters = 0;
            int MonstersWinsAgainstElves = 0;
            int MonstersWinsAgainstHumans = 0;

            int numOfIterations = 10_000;
            int numOfUnits = 4;


            // Humans V Elves

            for (int i = 0; i < numOfIterations; i++)
            {
                Actor actor1 = new Actor("Bob", Race.Human, numOfUnits, (10, 15));
                Actor actor2 = new Actor("Mark", Race.Elf, numOfUnits, (10, 15));
                switch(Combat.DuelFast(actor1, actor2, isAuto: true))
                {
                    case 1: HumanWinsAgainstElves++; break;
                    case -1: ElvesWinsAgainstHumans++; break;
                }
            }
            
            // Humans V Monstres

            for (int i = 0; i < numOfIterations; i++)
            {
                Actor actor1 = new Actor("Bob", Race.Human, numOfUnits, (10, 15));
                Actor actor2 = new Actor("Mark", Race.Monster, numOfUnits, (10, 15));
                switch (Combat.DuelFast(actor1, actor2, isAuto: true))
                {
                    case 1: HumanWinsAgainstMonsters++; break;
                    case -1: MonstersWinsAgainstHumans++; break;
                }
            }

            // Elves V Monstres

            for (int i = 0; i < numOfIterations; i++)
            {
                Actor actor1 = new Actor("Bob", Race.Elf, numOfUnits, (10, 15));
                Actor actor2 = new Actor("Mark", Race.Monster, numOfUnits, (10, 15));
                switch (Combat.DuelFast(actor1, actor2, isAuto: true))
                {
                    case 1: ElvesWinsAgainstMonsters++; break;
                    case -1: MonstersWinsAgainstElves++; break;
                }
            }

            Console.Clear();
            Console.WriteLine("Battle Simulation");
            Console.WriteLine($"Number of Iterations : {numOfIterations}");
            Console.WriteLine($"Number of Units : {numOfUnits}\n");
            Console.WriteLine($"Humans VS Elves : {HumanWinsAgainstElves} : {ElvesWinsAgainstHumans}\n");
            Console.WriteLine($"Humans VS Monsters : {HumanWinsAgainstMonsters} : {MonstersWinsAgainstHumans}\n");
            Console.WriteLine($"Monsters VS Elves : {MonstersWinsAgainstElves} : {ElvesWinsAgainstMonsters}\n");
        }
    }
}   



