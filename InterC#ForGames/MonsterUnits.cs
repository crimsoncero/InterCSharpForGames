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
            if (hitRoll < DefenseRating.Roll())
            {
                // Defend action - do nothing
            }
            else
            {
                if (attacker is CasterUnit)
                {
                    CasterUnit casterAttacker = (CasterUnit)attacker;
                    if (casterAttacker.Spell.Element != Element.Nature)
                        ApplyDamage(casterAttacker.Damage.Roll());
                }
                else
                {
                    
                }
            }
           
        }

    }

    sealed class PacifistGoblin : MartialUnit
    {
        public PacifistGoblin() : base(0, 10, Race.Monster, 8) { }

        public override void Attack(Unit defender)
        {
            Console.WriteLine("He is too busy in the auction house...");
        }

        public override void Defend(Unit attacker)
        {
            Console.WriteLine("For some reason he did spent a lot of gold on armor...");
            base.Defend(attacker);
        }

    }

    sealed class ShadowPriest : CasterUnit // Has 10% to chain attacks, forever...
    {
        public ShadowPriest() : base(10, Race.Monster, new MindSpike()) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);

            if (Random.Shared.Next(2) == 1) this.Attack(defender);
        }

    }
}
