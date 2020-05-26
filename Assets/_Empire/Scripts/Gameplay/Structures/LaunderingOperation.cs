namespace Empire
{
    public class LaunderingOperation
    {
        public int LaunderingRate { get; private set; } = 0;

        public LaunderingOperation(LaunderingOperationSettings settings)
        {
            LaunderingRate = settings.InitialLaunderingRate;
        }
    }
}
