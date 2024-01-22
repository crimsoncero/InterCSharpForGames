// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    class Actor
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


        #region Fight Algorithm Methods
        public void Fight(Actor other)
        {
            Console.WriteLine("-------Actor 1 vs Actor 2-------");
            Console.WriteLine($"Actor 1 Units are: {this.ToStringUnitList()}and they have {this.Resources} Resources.");
            Console.WriteLine($"Actor 2 Units are: {other.ToStringUnitList()}and they have {other.Resources} Resources.");
            Console.WriteLine("------------------------------------------------------------------------------------------------");


            Console.WriteLine("-----Turn 1------");

            int u1Index = this.ChooseRandomUnitIndex();
            int u2Index = other.ChooseRandomUnitIndex();
            Unit u1 = this.Units[u1Index];
            Unit u2 = other.Units[u2Index];

            if(u1.Speed > u2.Speed)
            {
                this.Units[u1Index].Attack(other.Units[u2Index]);
                other.Units[u2Index].Attack(this.Units[u1Index]);
            }
            else
            {
                other.Units[u2Index].Attack(this.Units[u1Index]);
                this.Units[u1Index].Attack(other.Units[u2Index]);
            }



            Console.WriteLine($"Actor 1 Units are: {this.ToStringUnitList()}and they have {this.Resources} Resources.");
            Console.WriteLine($"Actor 2 Units are: {other.ToStringUnitList()}and they have {other.Resources} Resources.");




        }


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
