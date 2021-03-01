namespace Empire.Stats
{
    public class TotalPercentAttributeModifier : AttributeModifier
    {
        public override int Priority => 3;

        public override float Apply(float attributeValue, float modifierValue)
        {
            return attributeValue * modifierValue / 100;
        }

        public TotalPercentAttributeModifier(float value) : base(value) {}
        public TotalPercentAttributeModifier(float value, bool stacks) : base(value, stacks) {}
    }
}