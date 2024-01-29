// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    enum Race
    {
        Human,
        Elf,
        Monster,
    }
    enum Weather
    {
        Clear,
        Sunny,
        ManaStorm,
        Hail,
    }

    abstract class Unit
    {

        public virtual IRandomProvider Damage { get; protected set; }
        public int HP { get; protected set; }
        public Race Race { get; init; }
        public int Speed { get; protected set; }
        public int CarryingCapacity { get; protected set; } // How many resources the unit can take after defeating an opponent unit.
        protected IRandomProvider HitChance { get;  set; }
        protected IRandomProvider DefenseRating { get;  set; }
        protected Weather CurrentWeather { get;  set; }

        public bool IsDead => (HP <= 0);


        public Unit(IRandomProvider damage, int hp, Race race, int carryingCapacity, 
            IRandomProvider hitChance, IRandomProvider defenseRating, int speed)
        {
            Damage = damage;
            HP = hp;
            Race = race;
            CarryingCapacity = carryingCapacity;
            HitChance = hitChance;
            DefenseRating = defenseRating;
            Speed = speed;
            CurrentWeather = Weather.Clear;

        }

        public virtual void Attack(Unit defender)
        {
            int hitRoll = HitChance.GetNumber();
            defender.Defend(this, hitRoll);
        }

        public virtual void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.GetNumber();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} defends the attack.");
                // Defend action
            }
            else
            {
                // Get hit action
                ApplyDamage(attacker.Damage.GetNumber());
            }
        }

        public virtual void WeatherEffect(Weather weather)
        {
            CurrentWeather = weather;
        }

        protected virtual void ApplyDamage(int damage)
        {
            Console.WriteLine($"{this} took {damage} damage");
            HP -= damage;
            if (HP < 0) HP = 0;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{HP}HP]";
        }
    }

    abstract class CasterUnit : Unit
    {
        public virtual Spell Spell { get; protected set; }

        public CasterUnit (int hp, Race race, Spell spell,int carryingCapacity, 
            IRandomProvider hitChance, IRandomProvider defenseRating, int speed) : 
            base (spell.Damage,hp, race, carryingCapacity, hitChance, defenseRating, speed )
        {
            Spell = spell;
        }

        public override void Attack(Unit defender)
        {
            Console.WriteLine($"Casting {Spell.Name}!");
            base.Attack(defender);
        }

        public override void WeatherEffect(Weather weather)
        {
            base.WeatherEffect(weather);

            // Every turn in a manastorm, a caster spell gets a permanent +1 to its damage roll. 
            if(weather == Weather.ManaStorm)
            {
                //Damage = Damage.AddModifier(1);
            }
        }

    }


    abstract class MartialUnit : Unit
    {
        public virtual int Armor { get; protected set; }

        public MartialUnit(IRandomProvider damage, int hp, Race race, int armor, int carryingCapacity, 
            IRandomProvider hitChance, IRandomProvider defenseRating, int speed) :
            base(damage, hp, race, carryingCapacity, hitChance, defenseRating, speed)
        {
            Armor = armor;
        }

        protected override void ApplyDamage(int damage)
        {
            int finalDamage = damage - Armor; // Reduces the damage taken by Armor Value
            if (finalDamage < 0) finalDamage = 0; // Doesn't allow negetive damage values.
            base.ApplyDamage(damage);
        }


        protected void ApplyDamageBypassArmor(int damage)
        {
            base.ApplyDamage(damage);
        }
      
    }

}