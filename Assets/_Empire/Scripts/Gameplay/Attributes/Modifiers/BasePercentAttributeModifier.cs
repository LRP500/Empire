namespace Empire.Attributes
{
    public class BasePercentAttributeModifier : AttributeModifier
    {
        public override int Priority => 1;

        public override float Apply(float attributeValue, float modifierValue)
        {
            return attributeValue * modifierValue;
        }

        public BasePercentAttributeModifier(float value) : base(value) {}
        public BasePercentAttributeModifier(float value, bool stacks) : base(value, stacks) {}
    }
}
