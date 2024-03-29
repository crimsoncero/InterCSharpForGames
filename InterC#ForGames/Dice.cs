﻿

using System.Diagnostics.CodeAnalysis;

namespace InterC_ForGames
{
    readonly struct Dice : IRandomProvider
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

        public int GetNumber()
        {
            int sum = Modifier;

            for(int i = 0; i < Scalar; i++)
            {
                sum += Random.Shared.Next((int)BaseDie) + 1;
            }

            return sum;
        }

        /// <summary>
        /// Returns a Dice that has x more to its modifier.
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
       public Dice AddModifier(int x)
        {
            return new Dice(Scalar, BaseDie, Modifier + x);
        }

        /// <summary>
        /// Returns the maximum roll result.
        /// </summary>
        /// <returns></returns>
        public int MaxRoll()
        {
            return (int)Scalar * (int)BaseDie + Modifier;
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
            return (int)((Scalar << 16) ^ (BaseDie << 8) ^ Modifier);
        }
    }
}
