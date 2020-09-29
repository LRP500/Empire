using UnityEngine;

namespace Empire
{
    /// <summary>
    /// OBSOLETE
    /// </summary>
    public class TerritoryActionInfoTakeOverUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private TerritoryTakeOverOddDisplay _oddsDisplay = null;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            //TerritoryActionTakeOver takeOver = action as TerritoryActionTakeOver;
            //_oddsDisplay.Initialize(territory);
        }
    }
}