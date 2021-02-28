using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Associate Manager")]
    public class AssociateManager : ScriptableManager<AssociateManager>
    {
        [ReadOnly]
        private List<Associate> _associates;

        public override void Initialize()
        {
        }
    }
}