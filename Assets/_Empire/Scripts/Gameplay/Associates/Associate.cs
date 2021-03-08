using System.Collections.Generic;

namespace Empire
{
    public class Associate
    {
        private string Name { get; }

        private List<Perk> PositivePerks { get; }
        private List<Perk> NegativePerks { get; }

        public Associate(string name, List <Perk> positivePerks, List<Perk> negativePerks)
        {
            Name = name;
            PositivePerks = positivePerks;
            NegativePerks = negativePerks;
        }
    }
}