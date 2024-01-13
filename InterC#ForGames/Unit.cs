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
    abstract class Unit
    {

        public virtual int Damage { get; protected set; }
        public virtual int HP { get; protected set; }
        public virtual Race Race { get; protected set; }

        
        public Unit(int damage, int hp, Race race)
        {
            Damage = damage;
            HP = hp;
            Race = race;
        }

        public virtual void Attack(Unit defender)
        {
            defender.Defend(this);
        }
        public virtual void Defend(Unit attacker)
        {
            ApplyDamage(attacker.Damage);
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

        public MartialUnit(int damage, int hp, Race race, int armor) : base(damage, hp, race)
        {
            Armor = armor;
        }

        public override void Defend(Unit attacker)
        {
            int finalDamage = attacker.Damage - Armor; // Reduces the damage taken by Armor Value
            if(finalDamage < 0) finalDamage = 0; // Doesn't allow negetive damage values.
            ApplyDamage (finalDamage);
        }

    }

}