using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    [RequireComponent(typeof(Button))]
    public class ButtonAction : MonoBehaviour
    {
        [SerializeField]
        private ScriptableAction _action;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(_action.Execute);
        }
    }
}
