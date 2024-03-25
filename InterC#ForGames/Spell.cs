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
        public string Name { get; protected set; }
        public Bag Damage { get; protected set; } // Spells universally use Bag for damage.
        public Element Element { get; protected set; }

        public Spell(string name,Bag damage, Element element)
        {
            Name = name;
            Damage = damage;
            Element = element;
        }
    }
    sealed class GlacialSpike : Spell
    {
        public GlacialSpike() : base("Ice Spike", new Bag(new int[] { 4, 4, 5, 5, 6, 6 }) , Element.Frost) { }
    }

    sealed class PhoenixFlames : Spell
    {
        public PhoenixFlames() : base("Phoenix Flames", new Bag(new int[] { 8, 8, 10 }), Element.Fire) { }
    }

    sealed class MindSpike : Spell
    {
        public MindSpike() : base("Mind Spike", new Bag(new int[] { 5, 12, 5, 12, 5, 12 }), Element.Shadow) { }
    }
}
