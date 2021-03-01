namespace Empire.Stats
{
    public class BaseAddAttributeModifier : AttributeModifier
    {
        public override int Priority => 2;

        public override float Apply(float attributeValue, float modifierValue)
        {
            return modifierValue;
        }

        public BaseAddAttributeModifier(float value) : base(value) {}
        public BaseAddAttributeModifier(float value, bool stacks) : base(value, stacks) {}
    }
}