// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class SpellBreaker : MartialUnit // A tank that specializes against Casters, when attacking a caster unit performs a double attack.
    {
        private int  _casterDefense = 2;

        public SpellBreaker() : base(damage: new Dice(2, 4, 1), hp: 22, race: Race.Elf, armor: 4, carryingCapacity: 4,
            hitChance: new Dice(2, 10, 2), defenseRating: new Dice(2,10, 3), speed: 2) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);
            if (defender is CasterUnit)
            {
                Console.WriteLine($"A caster! {this} attacks once more!");
                base.Attack(defender);
            }
        }

        public override void Defend(Unit attacker, int hitRoll)
        {

            int defenseRoll = DefenseRating.GetNumber() + (attacker is CasterUnit ? _casterDefense : 0);
            Console.WriteLine($"{attacker} rolled {hitRoll} attack against {this} {defenseRoll} defense");
            if (hitRoll < defenseRoll)
            {
                Console.WriteLine($"{this} defends the attack.");
                // Do nothing;
            }
            else
            {
                ApplyDamage(attacker.Damage.GetNumber());
            }
        }
    }

    sealed class Pyromancer : CasterUnit // A hot caster that fires two spells at once.
    {
        public Pyromancer() : base(hp: 13, race: Race.Elf, spell: new PhoenixFlames(), carryingCapacity: 1, 
            hitChance: new Dice(3,8,0), defenseRating: new Dice(1,20, 0), 2) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);

            // Doesn't attack twice if weather is cold.
            if (CurrentWeather != Weather.Hail)
            {
                Console.WriteLine($"{this} is hot, and fires another spell!");
                base.Attack(defender);
            }
              

            // If the weather is hot, attacks once more.
            if (CurrentWeather == Weather.Sunny)
            {
                Console.WriteLine($"{this} is hotter, and blasts another spell!");
                base.Attack(defender);
            }
        }
    }

    sealed class Ranger : MartialUnit // An archer unit that has high damage, and low armor
    {
        public Ranger() : base(damage: new Dice(2, 10, 2), hp: 18, race: Race.Elf, armor: 1, carryingCapacity: 2, 
            hitChance: new Dice(2,10,4), defenseRating: new Dice(2,10,0), speed: 3) { }
    }
}
