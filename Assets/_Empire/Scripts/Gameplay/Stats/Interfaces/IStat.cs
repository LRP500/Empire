namespace Empire.Stats
{
    public interface IStat
    {
        public float BaseValue { get; }
        public float MinValue { get; }
        public float MaxValue { get; }
        public bool Clamped { get; set; }
        
        public void SetBaseValue(float value);
    }
}
