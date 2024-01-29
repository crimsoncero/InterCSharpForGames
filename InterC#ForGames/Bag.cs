// ---- C# II (Dor Ben Dor) ----
//         Amit Breiman
// -----------------------------

namespace InterC_ForGames
{
    struct Bag : IRandomProvider
    {
        private int[] Base { get; init; }
        private List<int> BagList { get; set; }


        public Bag(IEnumerable<int> IntegerEnumrable)
        {
            Base = IntegerEnumrable.ToArray();
            BagList = new List<int>();
            InitBag();
        }

        public int GetNumber()
        {
            if (BagList.Count == 0) InitBag(); // Refills the list if empty.

            int randomIndex = Random.Shared.Next(0, BagList.Count);
            
            int randomNumber = BagList[randomIndex];
            BagList.RemoveAt(randomIndex); 
            
            return randomNumber;
        }

        private void InitBag()
        {
            BagList = Base.ToList();
        }
    }
}
