namespace Empire
{
    public class LaunderingOperation
    {
        public LaunderingOperation(LaunderingOperationSettings settings)
        {
            LaunderingRate = settings.InitialLaunderingRate;
        }

        public int LaunderingRate { get; private set; } = 0;
    }
}
