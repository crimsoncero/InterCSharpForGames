// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class WereBear : MartialUnit // A Big Chonky Tank, spell attacks bypass armor, apart from nature spells.
    {
        public WereBear() : base(damage: new Dice(2,4,2), hp: 30, race: Race.Monster, armor: 4, carryingCapacity: 5,
            hitChance: new Dice(2,12,0), defenseRating: new Dice(2,10,0), speed: -1) { }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.GetNumber();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll <= defenseRoll)
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
                        ApplyDamageBypassArmor(casterAttacker.Damage.GetNumber());
                }
                else
                {
                    ApplyDamage(attacker.Damage.GetNumber());
                }
            }
        }

    }

    sealed class PacifistGoblin : MartialUnit
    {

        public PacifistGoblin() : base(damage: new Bag(new int[] {2,3,5,7,11,13,17}), hp: 18, race: Race.Monster, armor: 3, carryingCapacity: 7,
            hitChance: new Dice(1,10,3), defenseRating: new Dice(1,10,0), speed: 1) { }

        public override void Attack(Unit defender)
        {
            Console.WriteLine($"{this} has bought a bunch of prime weaponery...");
            base.Attack(defender);
        }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int defenseRoll = DefenseRating.GetNumber();
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");

            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} thorny armor retailiates, is that what he bought from the AH?");
                // Defend action - thorn damage
                base.Attack(attacker);

            }
            else
            {
              ApplyDamage(attacker.Damage.GetNumber());
            }
            
        }

    }

    sealed class ShadowPriest : CasterUnit // Has 20% to chain attacks, forever...
    {
        public ShadowPriest() : base(hp: 10, race: Race.Monster, spell: new MindSpike(), carryingCapacity: 2,
            hitChance: new Dice(4,8,0), defenseRating: new Dice(2,12,0), speed: 1) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);

            if (Random.Shared.Next(10) <= 1)
            {
                Console.WriteLine($"{this} attacks once more!");
                this.Attack(defender);
            }
        }

    }
}
