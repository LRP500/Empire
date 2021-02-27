namespace Empire.Attributes
{
    public abstract class AttributeModifier
    {
        /// <summary>
        /// The modifier's value.
        /// </summary>
        public float Value { get; private set; }

        /// <summary>
        /// Order by which modifiers will be calculated.
        /// </summary>
        public abstract int Priority { get; }

        /// <summary>
        /// Set to true to make modifier additive, set to false to keep the highest only.
        /// </summary>
        public bool Stacks { get; }

        /// <summary>
        /// Apply the modifier to the given value.
        /// </summary>
        /// <param name="attributeValue">The value to apply the modifier to.</param>
        /// <param name="modifierValue">The value of the modifier.</param>
        /// <returns></returns>
        public abstract float Apply(float attributeValue, float modifierValue);
        
        /// <summary>
        /// Sets modifier's value.
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(float value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructor with no parameter.
        /// </summary>
        protected AttributeModifier() : this(0) { }

        /// <summary>
        /// Constructor taking value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stacks"></param>
        protected AttributeModifier(float value, bool stacks = true)
        {
            SetValue(value);
            Stacks = stacks;
        }
    }
}
