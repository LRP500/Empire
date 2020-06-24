namespace Empire
{
    public static class GameplayEvent
    {
        //public static readonly string MaxThreatReached = "GameplayEvent_MaxThreatReached";
        //public static readonly string AllTerritoriesControlled = "GameplayEvent_AllTerritoriesControlled";

        public static readonly string GameOver = "GameplayEvent_GameOver";
        public static readonly string PlayerVictory = "GameplayEvent_PlayerVictory";
        public static readonly string PlayerDefeat = "GameplayEvent_PlayerDefeat";

        public static readonly string TerritorySecondarySelect = "GameplayEvent_TerritorySecondarySelect";
        public static readonly string TerritoryPrimarySelect = "GameplayEvent_TerritoryPrimarySelect";
        public static readonly string CancelSecondaryMouseSelect = "GameplayEvent_CancelSecondaryMouseSelect";

        public static readonly string CashSpent = "GameplayEvent_CashSpent";
        public static readonly string TakeOverFailed = "GameplayEvent_TakeOverFailed";
        public static readonly string TakeOverSuccess = "GameplayEvent_TakeOverSuccess";
    }
}