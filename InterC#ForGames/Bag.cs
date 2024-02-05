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

        /// <summary>
        /// Loads a number into the current bag, does not persist after being used.
        /// </summary>
        /// <param name="loadedNumber"></param>
        public void Load(int loadedNumber)
        {
            BagList.Add(loadedNumber);
        }

        /// <summary>
        /// Finds the maximum value that can be pulled from the bag. 
        /// </summary>
        /// <returns></returns>
        public int FindMaxValue()
        {
            int currentMax = Base[0];

            for(int i = 1; i < Base.Length; i++)
            {
                if(currentMax < Base[i])
                    currentMax = Base[i];
            }

            return currentMax;
        }
        private void InitBag()
        {
            BagList = Base.ToList();
        }


    }
}
