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
        Heatwave,
        Rain,
        ManaStorm,
        Hail,
    }


    abstract class Unit
    {

        public Dice Damage { get; protected set; }
        public int HP { get; protected set; }
        public int CarryingCapacity { get; protected set; }
        public Dice HitChance { get; protected set; }
        public Dice DefenseRating { get; protected set; }
        public Race Race { get; protected set; }
        public Weather CurrentWeather { get; protected set; }

        
        public Unit(Dice damage, int hp, Race race)
        {
            Damage = damage;
            HP = hp;
            Race = race;
            CurrentWeather = Weather.Clear;
        }

        public virtual void Attack(Unit defender)
        {
            defender.Defend(this);
        }
        public virtual void Defend(Unit attacker)
        {
            ApplyDamage(attacker.Damage.Roll());
        }
        protected virtual void WeatherEffect(Weather weather)
        {
            CurrentWeather = weather;
        }

        protected void ApplyDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }


    }

    abstract class CasterUnit : Unit
    {
        public virtual Spell Spell { get; protected set; }

        public CasterUnit (int hp, Race race, Spell spell) : base (spell.Damage,hp, race)
        {
            Spell = spell;
        }

        public override void Attack(Unit defender)
        {
            Console.WriteLine($"Casting {Spell.Name}!");
            base.Attack(defender);
        }
    }


    abstract class MartialUnit : Unit
    {
        public virtual int Armor { get; protected set; }

        public MartialUnit(Dice damage, int hp, Race race, int armor) : base(damage, hp, race)
        {
            Armor = armor;
        }

        public override void Defend(Unit attacker)
        {
            int finalDamage = attacker.Damage.Roll() - Armor; // Reduces the damage taken by Armor Value
            if(finalDamage < 0) finalDamage = 0; // Doesn't allow negetive damage values.
            ApplyDamage (finalDamage);
        }

    }

}