using System.Collections;
using Tools.FSM;

namespace Empire
{
    public abstract class GameplayState : AState
    {
        protected GameplayStateController _controller = null;

        public override IEnumerator Run(AStateController controller)
        {
            _controller = controller as GameplayStateController;
            yield return controller.StartCoroutine(RunExtend());
        }
    }
}
