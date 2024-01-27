namespace InterC_ForGames
{
    class Program
    {
        public static void Main(String[] args)
        {


            Actor actor1 = new Actor("Bob",Race.Elf, 4, 30);
            Actor actor2 = new Actor("Mark",Race.Human, 4, 30);
            Combat.Duel(actor1, actor2);


        }
    }
}   



