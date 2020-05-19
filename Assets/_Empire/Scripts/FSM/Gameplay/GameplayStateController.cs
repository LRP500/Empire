using Tools.FSM;
using UnityEngine;

namespace Empire
{
    public class GameplayStateController : AStateController
    {
        [SerializeField]
        private GameplayContext _context = null;
    }
}