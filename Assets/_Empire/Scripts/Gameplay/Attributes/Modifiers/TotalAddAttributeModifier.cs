namespace Empire.Attributes
{
    public class TotalAddAttributeModifier : AttributeModifier
    {
        public override int Priority => 4;

        public override float Apply(float attributeValue, float modifierValue)
        {
            return modifierValue;
        }

        public TotalAddAttributeModifier(float value) : base(value) {}
        public TotalAddAttributeModifier(float value, bool stacks) : base(value, stacks) {}
    }
}