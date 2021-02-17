using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public class CameraRigController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField]
        private CameraTargetController _targetController;

        [SerializeField]
        [BoxGroup("Zoom")]
        private float _initialCameraDistance = 10f;

        [SerializeField]
        [BoxGroup("Zoom")]
        private float _zoomSensitivity = 1f;

        [SerializeField]
        [BoxGroup("Zoom")]
        private float _cameraDistanceMin = 10f;

        [SerializeField]
        [BoxGroup("Zoom")]
        private float _cameraDistanceMax = 100f;

        private CinemachineFramingTransposer _vcamTransposer;

        private void Awake()
        {
            InitializeVirtualCamera();
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                float delta = Input.mouseScrollDelta.y > 0 ? -_zoomSensitivity : _zoomSensitivity;
                float distance = _vcamTransposer.m_CameraDistance + delta * 10 * Time.unscaledDeltaTime;
                _vcamTransposer.m_CameraDistance = Mathf.Clamp(distance, _cameraDistanceMin, _cameraDistanceMax);
            }
        }

        private void InitializeVirtualCamera()
        {
            _vcamTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _vcamTransposer.m_CameraDistance = _initialCameraDistance;
        }

        public void SetLocked(bool state)
        {
            _targetController.enabled = !state;
        }
    }
}