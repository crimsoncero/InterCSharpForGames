// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

    
namespace InterC_ForGames
{
    static class Combat
    {
        private const int _weatherBaseDuration = 3; // How long the weather duration is. 
        private const int _weatherChangeChance = 25; // out of 100.
        public static int Duel(Actor a1, Actor a2, bool isAuto)
        {
            Weather weather = Weather.Clear;
            int weatherDuration = -1;
            Console.WriteLine("-------Actor 1 vs Actor 2-------");
            int turnNumber = 1;

            while (true)
            {
                // Actors Info Section.
                Console.WriteLine($"{a1}");
                Console.WriteLine($"{a2}");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine($"------ Turn {turnNumber} ------\n");


                // Weather handling.
                if (HasWeatherChanged())
                {
                    weather = RandomWeather();
                    weatherDuration = _weatherBaseDuration;
                    Console.WriteLine($"The weather changed to {weather}!");
                }
                else if (weatherDuration == 0)
                {
                    Console.WriteLine($"The {weather} weather calmed down.");
                    weather = Weather.Clear; // returns to clear weather.
                    weatherDuration = -1; //Infinite duration, until weather changes.
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

                Console.WriteLine("------------------------------------------------------------------------------------------------\n");
                
                // Choose a random living unit from each actor.
                int u1Index = a1.ChooseRandomUnitIndex();
                int u2Index = a2.ChooseRandomUnitIndex();
                Unit u1 = a1.Units[u1Index];
                Unit u2 = a2.Units[u2Index];


                // Make the two units fight, order of attacks is determined by speed.
                if (u1.Speed > u2.Speed)
                {
                    Fight((a1,u1Index), (a2,u2Index));
                    
                }
                else
                {
                    Fight((a2, u2Index), (a1, u1Index));
                }

                // Check if an actor won the fight.
                bool a1Dead = a1.AreAllDead();
                bool a2Dead = a2.AreAllDead();
                
                if (a1Dead && a2Dead)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"\nIt's a tie!");
                    return 0;
                }
                if (a2Dead)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"\n{a1.Name} Won the fight with {a1.Resources} resources!");
                    return 1;
                }
                if (a1Dead)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"\n{a2.Name} Won the fight with {a2.Resources} resources!");
                    return -1;
                }

                // Advance to next turn.
                turnNumber++;

                if (!isAuto)
                {
                    Console.WriteLine("\n Press any button to continue...\n");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine();
                }
               
            }
           


        }

        public static int DuelFast(Actor a1, Actor a2, bool isAuto)
        {
            Weather weather = Weather.Clear;
            int weatherDuration = -1;
            int turnNumber = 1;

            while (true)
            {
                // Actors Info Section.
               

                // Weather handling.
                if (HasWeatherChanged())
                {
                    weather = RandomWeather();
                    weatherDuration = _weatherBaseDuration;
                }
                else if (weatherDuration == 0)
                {
                    weather = Weather.Clear; // returns to clear weather.
                    weatherDuration = -1; //Infinite duration, until weather changes.
                }
                else
                {
                    weatherDuration--;
                }

                foreach (Unit u in Enumerable.Concat(a1.Units, a2.Units))
                {
                    u.WeatherEffect(weather);
                }


                // Choose a random living unit from each actor.
                int u1Index = a1.ChooseRandomUnitIndex();
                int u2Index = a2.ChooseRandomUnitIndex();
                Unit u1 = a1.Units[u1Index];
                Unit u2 = a2.Units[u2Index];


                // Make the two units fight, order of attacks is determined by speed.
                if (u1.Speed > u2.Speed)
                {
                    Fight((a1, u1Index), (a2, u2Index));

                }
                else
                {
                    Fight((a2, u2Index), (a1, u1Index));
                }

                // Check if an actor won the fight.
                bool a1Dead = a1.AreAllDead();
                bool a2Dead = a2.AreAllDead();

                if (a1Dead && a2Dead)
                {
                    return 0;
                }
                if (a2Dead)
                {
                    return 1;
                }
                if (a1Dead)
                {
                    return -1;
                }

                // Advance to next turn.
                turnNumber++;

            }



        }

        private static int TakeResources(Actor winner, Actor loser, int amount)
        {
            int amountTaken = 0;

            // Can't take resources that dont exist.
            if(loser.Resources - amount < 0)
            {
                winner.Resources += loser.Resources;
                amountTaken = loser.Resources;
                loser.Resources = 0;
            }
            else
            {
                winner.Resources += amount;
                amountTaken = amount;
                loser.Resources -= amount; 
            }

            return amountTaken;
        }

        /// <summary>
        /// Makes two units fight each other, the first unit attacks first and the second unit attacks second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        private static void Fight((Actor,int) first,  (Actor,int) second)
        {
            Unit firstU = first.Item1.Units[first.Item2];
            Unit secondU = second.Item1.Units[second.Item2];
            Console.WriteLine($"{firstU} is faster than {secondU}\n");
            Console.WriteLine($"{firstU} attacks {secondU}");
            firstU.Attack(secondU);

            // Handle the second unit death.
            if (secondU.IsDead)
            {
                Console.WriteLine($"{secondU} has died!");
                Console.WriteLine($"{first.Item1.Name} took {TakeResources(first.Item1, second.Item1, firstU.CarryingCapacity)} resources from {second.Item1.Name}");
                second.Item1.UnitDied(second.Item2);
                return;
            }
            
            Console.WriteLine($"\n{secondU} attacks {firstU}");
            secondU.Attack(firstU);

            // Handle the first unit death.
            if (firstU.IsDead)
            {
                Console.WriteLine($"{firstU} has died!");
                Console.WriteLine($"{second.Item1.Name} took {TakeResources(second.Item1, first.Item1, secondU.CarryingCapacity)} resources from {first.Item1.Name}!");
                first.Item1.UnitDied(first.Item2);
                return;
            }
        }
    

        private static bool HasWeatherChanged()
        {
            if (Random.Shared.Next(100) < _weatherChangeChance)
                return true;
            else 
                return false;
        }
        private static Weather RandomWeather()
        {
            Array values = Enum.GetValues(typeof(Weather));
            return (Weather)values.GetValue(Random.Shared.Next(1,values.Length)); // Get a random weather, excluding clear weather.
        }
    }



    internal class Actor
    {
        public string Name { get; protected set; }
        public List<Unit> Units { get; set; }
        public List<int> LiveUnitsIndex { get; set; }
        public int Resources { get; set; }
        public Race Race { get; protected set; }


        public Actor(string name, Race race, int numOfUnits, int resources)
        {
            Name = name;
            Race = race;
            Resources = resources;
            LiveUnitsIndex = new List<int>();
            Units = AssignRandomUnits(numOfUnits);

        }

        public Actor(string name, Race race, int numOfUnits, (int,int) resourcesRange)
        {
            Name = name;
            Race = race;
            Resources = Random.Shared.Next(resourcesRange.Item1, resourcesRange.Item2);
            LiveUnitsIndex = new List<int>();
            Units = AssignRandomUnits(numOfUnits);

        }


#region Combat Methods

        public void UnitDied(int unitIndex)
        {
            LiveUnitsIndex.Remove(unitIndex);
        }

        public int ChooseRandomUnitIndex()
        {
            if (AreAllDead()) return -1; // return a -1 index if all units are dead, as there will be no units to return.
            return LiveUnitsIndex[Random.Shared.Next(0, LiveUnitsIndex.Count)]; 
        }

        public bool AreAllDead()
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
            return $"{Name}'s Units: {ToStringUnitList()}Resources: {Resources}";
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
