namespace Empire
{
    public class Laboratory
    {
        public int ProductionRate { get; private set; } = 0;

        public Laboratory(LaboratorySettings settings)
        {
            ProductionRate = settings.InitialProductionRate;
        }
    }
}
