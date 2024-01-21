// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class SpellBreaker : MartialUnit // A tank that specializes against Casters, when attacking a caster unit performs a double attack.
    {
        private int  _casterDefense = 2;

        public SpellBreaker() : base(new Dice(2, 4, 1), 22, Race.Elf, 4, 4,
            new Dice(2, 10, 2), new Dice(2,10, 3), 2) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);
            if (defender is CasterUnit)
                base.Attack(defender);
        }

        public override void Defend(Unit attacker, int hitRoll)
        {
            int casterDefense = attacker is CasterUnit ? _casterDefense : 0;
            if (hitRoll < HitChance.Roll() + casterDefense)
            {
                // Do nothing;
            }
            else
            {
                ApplyDamage(attacker.Damage.Roll());
            }
        }
    }

    sealed class Pyromancer : CasterUnit // A hot caster that fires two spells at once.
    {
        public Pyromancer() : base(13, Race.Elf, new PhoenixFlames(), 1, 
            new Dice(3,8,0), new Dice(1,20, 0), 2) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);

            // Doesn't attack twice if weather is cold.
            if (CurrentWeather != Weather.Hail)
                base.Attack(defender);

            // If the weather is hot, attacks once more.
            if (CurrentWeather == Weather.Sunny)
            {
                base.Attack(defender);
            }
        }

        protected override void WeatherEffect(Weather weather)
        {
            base.WeatherEffect(weather);
        }
    }

    sealed class Ranger : MartialUnit // An archer unit that has high damage, and low armor
    {
        public Ranger() : base(new Dice(2, 10, 2), 18, Race.Elf, 1, 2, 
            new Dice(2,10,4), new Dice(2,10,0), 3) { }
    }
}
