using System.Collections.Generic;
using Empire.Attributes;

namespace Empire
{
    public sealed class Associate
    {
        private string Name { get; }

        private List<AttributeModifier> PositiveTraits { get; }
        private List<AttributeModifier> NegativeTraits { get; }

        public Associate(string name, List<AttributeModifier> positiveTraits, List<AttributeModifier> negativeTraits)
        {
            Name = name;
            PositiveTraits = positiveTraits;
            NegativeTraits = negativeTraits;
        }
    }
}
