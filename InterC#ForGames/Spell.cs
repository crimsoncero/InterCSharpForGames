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
        public Dice Damage { get; protected set; }
        public Element Element { get; protected set; }

        public Spell(string name,Dice damage, Element element)
        {
            Name = name;
            Damage = damage;
            Element = element;
        }
    }
    sealed class GlacialSpike : Spell
    {
        public GlacialSpike() : base("Ice Spike", new Dice(3,10,0) , Element.Frost) { }
    }

    sealed class PhoenixFlames : Spell
    {
        public PhoenixFlames() : base("Phoenix Flames", new Dice(2,8,3), Element.Fire) { }
    }

    sealed class MindSpike : Spell
    {
        public MindSpike() : base("Mind Spike", new Dice(2,4,0), Element.Shadow) { }
    }
}
