
namespace InterC_ForGames.WIC
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
    }

    public interface IStanceable
    {
        public void ChangeStance();
    }

    public abstract class Unit : IDamageable
    {
        public virtual int Damage { get; protected set; }
        public int Defense { get; protected set; }
        public int HP { get; protected set; }
        public bool IsMelee { get; protected set; }
        public virtual int Range { get; protected set; }

        protected Unit(int damage, int defense, int hP, bool isMelee, int range)
        {
            Damage = damage;
            Defense = defense;
            HP = hP;
            IsMelee = isMelee;
            Range = range;
        }

        public virtual void TakeDamage(int damage)
        {
            HP -= damage;
            if(HP < 0) HP = 0;
        }

    }

    class Mage : Unit
    {
        public override int Damage { get => base.Damage * Range; protected set => base.Damage = value; }
        public override int Range
        {
            get => base.Range;
            protected set
            {
                base.Range = value;
                if(Range < MIN_RANGE) Range = MIN_RANGE; 
            }
        }

        private const int MIN_RANGE = 3;
        public Mage(int damage, int defense, int hP, bool isMelee, int range) : base(damage, defense, hP, isMelee, range) { }
    }

    class Warrior : Unit, IStanceable
    {
        public override int Damage
        {
            get
            {
                if (IsTankStance)
                {
                    return base.Damage / 2;
                }
                else
                {
                    return base.Damage;
                }
            }
            protected set => base.Damage = value;
        }
        public bool IsTankStance { get; protected set; }
        public Warrior(int damage, int defense, int hP, bool isMelee, int range) : base(damage, defense, hP, isMelee, range)
        {
            IsTankStance = false;
        }

        public void ChangeStance()
        {
            IsTankStance = !IsTankStance;
        }
    }


    class TreasureChest : IDamageable
    {
        public int Sturdiness { get; protected set; }
        public void TakeDamage(int damage)
        {
            Sturdiness -= damage;
            if(Sturdiness < 0) Sturdiness = 0;
        }
    }

}
