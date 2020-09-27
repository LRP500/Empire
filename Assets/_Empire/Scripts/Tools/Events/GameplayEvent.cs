namespace Empire
{
    public static class GameplayEvent
    {
        /// -------------------------------------
        /// Game Over
        /// -------------------------------------
        public static readonly string GameOver = "GameplayEvent_GameOver";
        public static readonly string PlayerVictory = "GameplayEvent_PlayerVictory";
        public static readonly string PlayerDefeat = "GameplayEvent_PlayerDefeat";

        /// -------------------------------------
        /// Map Interactions
        /// -------------------------------------
        public static readonly string TerritorySecondarySelect = "GameplayEvent_TerritorySecondarySelect";
        public static readonly string TerritoryPrimarySelect = "GameplayEvent_TerritoryPrimarySelect";
        public static readonly string CancelSecondaryMouseSelect = "GameplayEvent_CancelSecondaryMouseSelect";

        /// -------------------------------------
        /// Resources
        /// -------------------------------------
        public static readonly string CashSpent = "GameplayEvent_CashSpent";

        /// -------------------------------------
        /// Take Over
        /// -------------------------------------
        public static readonly string TakeOverDragStart = "GameplayEvent_TakeOverDragStart";
        public static readonly string TakeOverFailed = "GameplayEvent_TakeOverFailed";
        public static readonly string TakeOverSuccess = "GameplayEvent_TakeOverSuccess";
        public static readonly string TakeOver = "GameplayEvent_TakeOver";
    }
}