using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/World Map Manager")]
    public class WorldMapManager : ScriptableManager<WorldMapManager>
    {
        [SerializeField]
        private TerritoryListVariable _territories = null;
        public List<Territory> Territories => _territories.Items;

        [SerializeField]
        private TerritoryListVariable _controlledTerritories = null;
        public List<Territory> ControlledTerritories => _controlledTerritories.Items;

        [Header("Territory States")]

        [SerializeField]
        [LabelText("Unreachable")]
        private TerritoryState _territoryStateUnreachable = null;

        [SerializeField]
        [LabelText("Undiscputed")]
        private TerritoryState _territoryStateUndisputed = null;

        [SerializeField]
        [LabelText("Controlled")]
        private TerritoryState _territoryStateControlled = null;

        [SerializeField]
        [LabelText("In Deal")]
        private TerritoryState _territoryStateInDeal = null;

        [SerializeField]
        [LabelText("Rival")]
        private TerritoryState _territoryStateRival = null;

        public override void Initialize()
        {
            _controlledTerritories.Clear();

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
            _controlledTerritories.Add(territory);
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
            _controlledTerritories.Remove(territory);
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
