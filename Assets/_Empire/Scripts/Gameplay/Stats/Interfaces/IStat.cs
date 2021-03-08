namespace Empire.Stats
{
    public interface IStat
    {
        public float BaseValue { get; }
        public float MinValue { get; }
        public float MaxValue { get; }
        
        public void SetBaseValue(float value);
    }
}
