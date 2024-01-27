// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class Paladin : MartialUnit // A martial unit with high damage and average everything else.
    {
        public Paladin() : base(new Dice(2,6,3), 20, Race.Human, 2, 3,
            new Dice(2,10,0), new Dice(1,20,0), 0) { }

       
    }

    sealed class ShieldKnight : MartialUnit // A tank unit that takes half the damage from attacking martial units, but full damage from caster units.
    {
        public ShieldKnight() : base(new Dice(1,8,2), 25, Race.Human, 0, 4,
            new Dice(3,4,1), new Dice(1,20, 2), -2) { }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.Roll();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} defends the attack.");
                // Do Nothing;
            }
            else
            {
                if (attacker is CasterUnit)
                    ApplyDamage(attacker.Damage.Roll());
                else
                    ApplyDamage(attacker.Damage.Roll() / 2);
            }
        }
    }


    sealed class Mage : CasterUnit // Just a really powerful old wizard. When it is hailing, will always hit with glacial spike.
    {
        public Mage() : base(14, Race.Human, new GlacialSpike(), 1,
            new Dice(1,20,0), new Dice(1, 20, -3), 1)  { }

        public override void Attack(Unit defender)
        {
            int hitRoll = CurrentWeather == Weather.Hail? HitChance.MaxRoll() : HitChance.Roll();
            defender.Defend(this, hitRoll);
        }
    }

}