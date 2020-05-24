using TMPro;
using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoTakeOverUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private KeyValueItemUI _successFailure = null;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            TerritoryActionTakeOver takeOver = action as TerritoryActionTakeOver;

            _successFailure.SetValue(takeOver.CalculateOdds(territory).ToString());
        }
    }
}