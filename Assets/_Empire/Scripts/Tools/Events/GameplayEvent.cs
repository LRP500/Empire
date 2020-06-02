namespace Empire
{
    public static class GameplayEvent
    {
        // World Map
        public static readonly string TerritorySecondarySelect = "GameplayEvent_TerritorySecondarySelect";
        public static readonly string TerritoryPrimarySelect = "GameplayEvent_TerritoryPrimarySelect";

        // Threat
        public static readonly string CashSpent = "GameplayEvent_CashSpent";
        public static readonly string TakeOverFailed = "GameplayEvent_TakeOverFailed";
        public static readonly string TakeOverSuccess = "GameplayEvent_TakeOverSuccess";
    }
}