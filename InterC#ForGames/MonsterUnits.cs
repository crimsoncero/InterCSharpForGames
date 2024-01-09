
namespace InterC_ForGames
{
    sealed class WereBear : MartialUnit // A Big Chonky Tank, spell attacks bypass armor, apart from nature spells.
    {
        public WereBear() : base(4,20, Race.Monster, 4) { }

        public override void Defend(Unit attacker)
        {
            if(attacker is CasterUnit)
            {
                CasterUnit casterAttacker = (CasterUnit)attacker;
                if (casterAttacker.Spell.Element != Element.Nature)
                    ApplyDamage(casterAttacker.Damage);
            }
            else
            {
                base.Defend(attacker);
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
        public ShadowPriest() : base(10,Race.Monster, new MindSpike(3)) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);

            if (Random.Shared.Next(2) == 1) this.Attack(defender);
        }

    }
}
