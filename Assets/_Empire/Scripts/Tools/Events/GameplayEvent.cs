namespace Empire
{
    public static class GameplayEvent
    {
        /// -------------------------------------
        /// Game Over
        /// -------------------------------------
        public const string GameOver = "GameplayEvent_GameOver";
        public const string PlayerVictory = "GameplayEvent_PlayerVictory";
        public const string PlayerDefeat = "GameplayEvent_PlayerDefeat";

        /// -------------------------------------
        /// Map Interactions
        /// -------------------------------------
        public const string TerritorySecondarySelect = "GameplayEvent_TerritorySecondarySelect";
        public const string TerritoryPrimarySelect = "GameplayEvent_TerritoryPrimarySelect";
        public const string CancelSecondaryMouseSelect = "GameplayEvent_CancelSecondaryMouseSelect";

        /// -------------------------------------
        /// Resources
        /// -------------------------------------
        public const string CashSpent = "GameplayEvent_CashSpent";

        /// -------------------------------------
        /// Take Over
        /// -------------------------------------
        public const string TakeOverDragStart = "GameplayEvent_TakeOverDragStart";
        public const string TakeOverFailed = "GameplayEvent_TakeOverFailed";
        public const string TakeOverSuccess = "GameplayEvent_TakeOverSuccess";
        public const string TakeOver = "GameplayEvent_TakeOver";
    }
}