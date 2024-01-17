namespace InterC_ForGames
{
    class Program
    {
        public static void Main(String[] args)
        {

            Dice d = new Dice(3, 8, -12);
            int hash = d.GetHashCode();
            Console.WriteLine($"{d} Hashcode is : {hash}");


        }
    }
}   



