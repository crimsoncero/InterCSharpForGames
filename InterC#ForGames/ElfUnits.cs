
namespace InterC_ForGames
{
    sealed class SpellBreaker : MartialUnit // A tank that specializes against Casters, when attacking a caster unit performs a double attack.
    {
        public SpellBreaker() : base(3, 15, Race.Elf, 4) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);
            if(defender is CasterUnit) 
                base.Attack(defender);

        }
    }

    sealed class Pyromancer : CasterUnit // A hot caster that fires two spells at once.
    {
        public Pyromancer() : base(8, Race.Elf, new PhoenixFlames(4)) { }

        public override void Attack(Unit defender)
        {
            base.Attack(defender);
            base.Attack(defender);
        }
    }

    sealed class Ranger : MartialUnit // An archer unit that has high damage, and low armor
    {
        public Ranger() : base(6, 12, Race.Elf, 0) { }
    }
}
