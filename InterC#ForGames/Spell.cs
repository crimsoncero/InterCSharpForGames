// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    enum Element
    {
        Arcane,
        Fire,
        Frost,
        Nature,
        Chaos,
        Shadow,
        Holy,
    }
    abstract class Spell
    {
        public virtual string Name { get; protected set; }
        public virtual int Damage { get; set; }
        public virtual Element Element { get; protected set; }

        public Spell(string name,int damage, Element element)
        {
            Name = name;
            Damage = damage;
            Element = element;
        }
    }
    sealed class IceSpike : Spell
    {
        public IceSpike(int damage) : base("Ice Spike", damage , Element.Frost) { }
    }

    sealed class PhoenixFlames : Spell
    {
        public PhoenixFlames(int damage) : base("Phoenix Flames", damage, Element.Fire) { }
    }

    sealed class MindSpike : Spell
    {
        public MindSpike(int damage) : base("Mind Spike", damage, Element.Shadow) { }
    }
}
