using Cinemachine;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Empire
{
    /// <summary>
    /// Generate camera impulses on events received.
    /// </summary>
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraImpulseManager : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource = null;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();

            EventManager.Instance.Subscribe(GameplayEvent.TakeOverFailed, OnEventReceived);
            EventManager.Instance.Subscribe(GameplayEvent.CashSpent, OnEventReceived);
        }

        [Button]
        private void GenerateImpulse()
        {
#if UNITY_EDITOR
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
#else
            _impulseSource.GenerateImpulse();
#endif
        }

        private void OnEventReceived(object parameter)
        {
            GenerateImpulse();
        }
    }
}
