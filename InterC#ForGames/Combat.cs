﻿// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    static class Combat
    {
        private const int _weatherBaseDuration = 3; // How long the weather duration is. 

        public static void Duel(Actor a1, Actor a2)
        {
            Weather weather = Weather.Clear;
            int weatherDuration = 0;
            Console.WriteLine("-------Actor 1 vs Actor 2-------");
           

            while (true)
            {
                
                Console.WriteLine($"Actor 1 - {a1}");
                Console.WriteLine($"Actor 2 - {a2}");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("-----Turn 1------");

                if (weatherDuration == 0)
                {
                    weather = RandomWeather();
                    weatherDuration = _weatherBaseDuration;
                    Console.WriteLine($"The weather changed to {weather}!");
                }
                else
                {
                    Console.WriteLine($"The weather is {weather}.");
                    weatherDuration--;
                }
                
                foreach(Unit u in Enumerable.Concat(a1.Units, a2.Units))
                {
                    u.WeatherEffect(weather);
                }

                int u1Index = a1.ChooseRandomUnitIndex();
                int u2Index = a2.ChooseRandomUnitIndex();
                Unit u1 = a1.Units[u1Index];
                Unit u2 = a2.Units[u2Index];

                if (u1.Speed > u2.Speed)
                {
                    Console.WriteLine($"{u1} is faster than {u2}\n");
                    Console.WriteLine($"{u1} attacks {u2}");
                    a1.Units[u1Index].Attack(a2.Units[u2Index]);
                    Console.WriteLine($"\n{u2} attacks {u1}");
                    a2.Units[u2Index].Attack(a1.Units[u1Index]);
                }
                else
                {
                    Console.WriteLine($"{u2} is faster than {u1}\n");
                    Console.WriteLine($"{u2} attacks {u1}");
                    a2.Units[u2Index].Attack(a1.Units[u1Index]);
                    Console.WriteLine($"\n{u1} attacks {u2}");
                    a1.Units[u1Index].Attack(a2.Units[u2Index]);
                }

                Console.WriteLine("\n Press any button for next turn...\n");
                Console.ReadKey(true);
            }
           


        }

        private static Weather RandomWeather()
        {
            Array values = Enum.GetValues(typeof(Weather));
            return (Weather)values.GetValue(Random.Shared.Next(values.Length));
        }
    }



    internal class Actor
    {
        public List<Unit> Units { get; set; }
        public List<int> LiveUnitsIndex { get; set; }
        public int Resources { get; set; }
        public Race Race { get; protected set; }


        public Actor(Race race, int numOfUnits, int resources)
        {
            Race = race;
            Resources = resources;
            LiveUnitsIndex = new List<int>();
            Units = AssignRandomUnits(numOfUnits);

        }


        #region Combat Methods

        public int ChooseRandomUnitIndex()
        {
            if (IsAllDead()) return -1; // return a -1 index if all units are dead, as there will be no units to return.
            return LiveUnitsIndex[Random.Shared.Next(0, LiveUnitsIndex.Count)]; 
        }

        public bool IsAllDead()
        {
            return (LiveUnitsIndex.Count == 0);
        }
        public string ToStringUnitList()
        {
            string str = string.Empty;
            foreach (Unit unit in Units)
            {
                str += $"{unit.ToString()}, ";
            }
            return str;
        }

        public override string ToString()
        {
            return $"Units: {ToStringUnitList()}Resources: {Resources}";
        }
        #endregion

        #region Unit List Creation Methods
        private List<Unit> AssignRandomUnits(int numOfUnits)
        {
            List<Unit> units = new List<Unit>();
            for(int i = 0; i < numOfUnits; i++)
            {
                switch (Race)
                {
                    case Race.Human:
                        units.Add(RandomHuman());
                        break;
                    case Race.Elf:
                        units.Add(RandomElf());
                        break;
                    case Race.Monster:
                        units.Add(RandomMonster());
                        break;
                }
                LiveUnitsIndex.Add(i); // Adds the index of the added unit to the index list.
            }
            return units;
        }

        private Unit RandomHuman()
        {
            switch(Random.Shared.Next(0, 3))
            {
                case 0:
                    return new Paladin();
                case 1:
                    return new ShieldKnight();
                case 2:
                    return new Mage();
                default:
                    return new Paladin();
            }
        }

        private Unit RandomElf()
        {
            switch (Random.Shared.Next(0, 3))
            {
                case 0:
                    return new SpellBreaker();
                case 1:
                    return new Pyromancer();
                case 2:
                    return new Ranger();
                default:
                    return new SpellBreaker();
            }
        }

        private Unit RandomMonster()
        {
            switch (Random.Shared.Next(0, 3))
            {
                case 0:
                    return new WereBear();
                case 1:
                    return new PacifistGoblin();
                case 2:
                    return new ShadowPriest();
                default:
                    return new WereBear();
            }
        }
        #endregion
    }
}
