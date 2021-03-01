namespace Empire.Stats
{
    public interface IAttribute : IStat
    {
        public float ModifiedValue { get; }
        
        public void AddModifier(AttributeModifier modifier);
        public void RemoveModifier(AttributeModifier modifier);
        public void ClearModifiers();
        public void UpdateModifiers();
    }
}