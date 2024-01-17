
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace InterC_ForGames
{
    readonly struct Dice
    {
        public readonly uint Scalar;
        public readonly uint BaseDie;
        public readonly int Modifier;

        public Dice(uint scalar, uint baseDie, int modifier)
        {
            Scalar = scalar;
            BaseDie = baseDie;
            Modifier = modifier;
        }

        public int Roll()
        {
            int sum = Modifier;

            for(int i = 0; i < Scalar; i++)
            {
                sum += Random.Shared.Next((int)BaseDie) + 1;
            }

            return sum;
        }

        public override string ToString()
        {
            return $"{Scalar}d{BaseDie}{(Modifier < 0 ? "" : "+")}{Modifier}"; // Prints the Dice in a format like 3d6+0 (Scalar = 3, BaseDie = 6, Modifier = 0).
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            // return false if obj is either null or not a Dice.
            if (obj == null || ! this.GetType().Equals(obj.GetType())) return false;

            Dice other = (Dice)obj;

            return
                  other.Scalar == this.Scalar &&
                  other.BaseDie == this.BaseDie &&
                  other.Modifier == this.Modifier;
        }
        
        public static bool operator ==(Dice x, Dice y)
        {
            return x.Equals(y);
        }
        public static bool operator !=(Dice x, Dice y)
        {
            return !x.Equals(y);
        }
        public override int GetHashCode()
        {
            // Assuming reasonable range of Base Die to be between 1 and 100, and reasonable amount of dice thrown at once to be up to 100.


            int modBaseDie = ((int)BaseDie % 100);
            int modScalar = ((int)Scalar % 100) * 1_000;
            int modModifier = Math.Abs(Modifier % 1_000) * 10_000_000;
            return (modBaseDie + modScalar + modModifier) * (Modifier < 0 ? -1 : 1);
        }
    }
}
