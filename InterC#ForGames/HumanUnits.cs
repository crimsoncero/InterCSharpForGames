// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    sealed class Paladin : MartialUnit // A martial unit with high damage
    {
        public Paladin() : base(5, 14, Race.Human, 2) { }

    }

    sealed class ShieldKnight : MartialUnit // A tank unit that takes half the damage from attacking martial units, but full damage from caster units.
    {
        public ShieldKnight() : base(2, 18, Race.Human, 0) { }

        public override void Defend(Unit attacker)
        {
            if(attacker is CasterUnit)
                base.Defend(attacker);
            else
                ApplyDamage(attacker.Damage / 2);
        }
    }

    sealed class Mage : CasterUnit // Just a really powerful old wizard.
    {
        public Mage() : base(6, Race.Human, new IceSpike(7)) { }

    }

}