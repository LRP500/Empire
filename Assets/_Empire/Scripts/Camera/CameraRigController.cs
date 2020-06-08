using Cinemachine;
using UnityEngine;

namespace Empire
{
    public class CameraRigController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = null;

        [SerializeField]
        private float _initialCameraDistance = 10f;

        [SerializeField]
        private float _zoomSensitivity = 1f;

        [SerializeField]
        private MousePointer _mousePointer = null;

        private CinemachineFramingTransposer _vcamTransposer = null;

        private void Awake()
        {
            InitializeVirtualCamera();
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                float delta = Input.mouseScrollDelta.y > 0 ? -_zoomSensitivity : _zoomSensitivity;
                _vcamTransposer.m_CameraDistance += delta * 10 * Time.unscaledDeltaTime;
            }
        }

        private void InitializeVirtualCamera()
        {
            _vcamTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _vcamTransposer.m_CameraDistance = _initialCameraDistance;
        }

        public void SetLocked(bool state)
        {
            _mousePointer.enabled = !state;
        }
    }
}