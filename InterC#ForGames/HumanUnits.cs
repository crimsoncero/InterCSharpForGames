// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class Paladin : MartialUnit // A martial unit with high damage and average everything else.
    {
        public Paladin() : base(damage: new Dice(2,8,3), hp: 20, race: Race.Human, armor: 2, carryingCapacity: 3,
            hitChance: new Dice(2,10,0), defenseRating: new Dice(1,20,0), speed: 0) { }

       
    }

    sealed class ShieldKnight : MartialUnit // A tank unit that takes half the damage from attacking martial units, but full damage from caster units.
    {
        public ShieldKnight() : base(damage: new Dice(1,8,2), hp: 30, race: Race.Human, armor: 2, carryingCapacity: 4,
            hitChance: new Dice(3,4,1), defenseRating: new Dice(1,20, 4), speed: -1) { }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.GetNumber();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll <= defenseRoll)
            {
                Console.WriteLine($"{this} defends the attack.");
                // Do Nothing;
            }
            else
            {
                if (attacker is CasterUnit)
                    ApplyDamage(attacker.Damage.GetNumber());
                else
                    ApplyDamage(attacker.Damage.GetNumber() / 2);
            }
        }
    }


    sealed class Mage : CasterUnit // Just a really powerful old wizard. When it is hailing, will always hit with glacial spike.
    {
        public Mage() : base(hp: 14, race: Race.Human, spell: new GlacialSpike(), carryingCapacity: 1,
            hitChance: new Dice(1,20,2), defenseRating: new Dice(1, 20, -1), speed: 1)  { }

        public override void Attack(Unit defender)
        {
            int hitRoll = CurrentWeather == Weather.Hail? ((Dice)HitChance).MaxRoll() : HitChance.GetNumber();
            defender.Defend(this, hitRoll);
        }
    }

}