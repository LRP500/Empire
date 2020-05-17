using Cinemachine;
using UnityEngine;

namespace Empire
{
    public class CameraRigController : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera = null;

        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = null;

        [SerializeField]
        private float _initialCameraDistance = 10f;

        [SerializeField]
        private float _zoomSensitivity = 1f;

        private CinemachineFramingTransposer _vcamTransposer = null;

        private void Awake()
        {
            InitializeVirtualCamera();
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                _vcamTransposer.m_CameraDistance -= _zoomSensitivity * 10 * Time.deltaTime ;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                _vcamTransposer.m_CameraDistance += _zoomSensitivity * 10 * Time.deltaTime;
            }
        }

        private void InitializeVirtualCamera()
        {
            _vcamTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _vcamTransposer.m_CameraDistance = _initialCameraDistance;
        }
    }
}