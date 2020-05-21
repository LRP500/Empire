using UnityEngine;

namespace Empire
{
    public class WorldMap : MonoBehaviour
    {
        [SerializeField]
        private TerritoryListVariable _runtimeTerritories = null;

        public void Initialize()
        {
            foreach (Territory territory in _runtimeTerritories.Items)
            {
                territory.SetRival();
            }
        }

        public void SetStartingTerritory()
        {
            _runtimeTerritories.Random().SetControlled();
        }
    }
}
