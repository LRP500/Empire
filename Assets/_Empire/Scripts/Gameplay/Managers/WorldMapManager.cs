using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/World Map Manager")]
    public class WorldMapManager : ScriptableObject
    {
        [SerializeField]
        private TerritoryListVariable _territories = null;
        public List<Territory> Territories => _territories.Items;

        [Header("States")]

        [SerializeField]
        private TerritoryState _territoryStateUnreachable = null;

        [SerializeField]
        private TerritoryState _territoryStateUndisputed = null;

        [SerializeField]
        private TerritoryState _territoryStateControlled = null;

        [SerializeField]
        private TerritoryState _territoryStateInDeal = null;

        [SerializeField]
        private TerritoryState _territoryStateRival = null;

        public void Initialize()
        {
            foreach (Territory territory in _territories.Items)
            {
                SetUnreachable(territory);
            }
        }

        public void SetStartingTerritory()
        {
            SetControlled(_territories.Random());
        }

        #region Territory State

        public void SetControlled(Territory territory)
        {
            TransitionTo(territory, _territoryStateControlled);
        }

        public void SetUndisputed(Territory territory)
        {
            TransitionTo(territory, _territoryStateUndisputed);
        }

        public void SetInDeal(Territory territory)
        {
            TransitionTo(territory, _territoryStateInDeal);
        }

        public void SetRival(Territory territory)
        {
            TransitionTo(territory, _territoryStateRival);
        }

        public void SetUnreachable(Territory territory)
        {
            TransitionTo(territory, _territoryStateUnreachable);
        }

        public void TransitionTo(Territory territory, TerritoryState state)
        {
            TerritoryState instance = Instantiate(state);
            instance.SetTerritory(territory);
            territory.SetState(instance);
            instance.OnEnterState();

            Debug.Log($"[{name}] Transition to {territory.State.GetType().Name}");
        }

        #endregion Territory State
    }
}
