// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class WereBear : MartialUnit // A Big Chonky Tank, spell attacks bypass armor, apart from nature spells.
    {
        public WereBear() : base(new Dice(2,4,2), 30, Race.Monster, 5, 5,
            new Dice(2,12,0), new Dice(2,9,0), -1) { }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.Roll();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} defends the attack.");
                // Defend action - do nothing
            }
            else
            {
                if (attacker is CasterUnit)
                {
                    CasterUnit casterAttacker = (CasterUnit)attacker;
                    if (casterAttacker.Spell.Element != Element.Nature)
                        ApplyDamageBypassArmor(casterAttacker.Damage.Roll());
                }
                else
                {
                    ApplyDamage(attacker.Damage.Roll());
                }
            }
        }

    }

    sealed class PacifistGoblin : MartialUnit
    {
        public PacifistGoblin() : base(new Dice(1,4,1), 15, Race.Monster, 10, 7,
            new Dice(1,1,30), new Dice(1,10,0), 1) { }

        public override void Attack(Unit defender)
        {
            Console.WriteLine($"{this} is too busy in the auction house...");
        }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.Roll();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} thorny armor retailiates");
                // Defend action - thorn damage
                base.Attack(attacker);

            }
            else
            {
              ApplyDamage(attacker.Damage.Roll());
            }
            
        }

    }

    sealed class ShadowPriest : CasterUnit // Has 20% to chain attacks, forever...
    {
        public ShadowPriest() : base(10, Race.Monster, new MindSpike(), 2,
            new Dice(4,8,0), new Dice(2,12,0), 0) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);
            Console.WriteLine($"{this} attacks once more!");
            if (Random.Shared.Next(10) <= 1) this.Attack(defender);
        }

    }
}
