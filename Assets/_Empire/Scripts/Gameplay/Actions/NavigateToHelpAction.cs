using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Actions/Navigate To Help")]
    public class NavigateToHelpAction : ScriptableAction
    {
        [SerializeField]
        private PauseMenuVariable _pauseMenu;
        
        public override void Execute()
        {
            _pauseMenu.Value.Open();
            _pauseMenu.Value.NavigateToHelp();
        }
    }
}
