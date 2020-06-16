using System.Collections.Generic;
using Tools.Time;
using Tools.UI;
using UnityEngine;

namespace Empire
{
    public class TimeControlUI : PanelUI
    {
        public struct SpeedMultiplierInfo
        {
            public float speed;
            public KeyCode input;

            public SpeedMultiplierInfo(float speed, KeyCode input)
            {
                this.speed = speed;
                this.input = input;
            }
        }

        [SerializeField]
        private Toggle _pauseToggle = null;

        [SerializeField]
        private Toggle _resumeToggle = null;

        [SerializeField]
        private Toggle _forwardToggle = null;

        [SerializeField]
        private Toggle _fastForwawrdToggle = null;

        [Space]
        [SerializeField]
        private float _forwardSpeed = 5;

        [SerializeField]
        private float _fastForwardSpeed = 10;

        [Space]
        [SerializeField]
        private TimeController _timeController = null;

        private Toggle _lastActiveToggle = null;

        private Dictionary<Toggle, SpeedMultiplierInfo> _speedMultipliers = null;

        private void Awake()
        {
            _speedMultipliers = new Dictionary<Toggle, SpeedMultiplierInfo>
            {
                { _pauseToggle, new SpeedMultiplierInfo(0, KeyCode.Q) },
                { _resumeToggle, new SpeedMultiplierInfo(1, KeyCode.W) },
                { _forwardToggle, new SpeedMultiplierInfo(_forwardSpeed, KeyCode.E) },
                { _fastForwawrdToggle, new SpeedMultiplierInfo(_fastForwardSpeed, KeyCode.R) }
            };

            _pauseToggle.OnSelect.AddListener(delegate { OnToggleSelected(_pauseToggle); });
            _resumeToggle.OnSelect.AddListener(delegate { OnToggleSelected(_resumeToggle); });
            _forwardToggle.OnSelect.AddListener(delegate { OnToggleSelected(_forwardToggle); });
            _fastForwawrdToggle.OnSelect.AddListener(delegate { OnToggleSelected(_fastForwawrdToggle); });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SelectToggle(_pauseToggle.isOn ? (_lastActiveToggle ?? _resumeToggle) : _pauseToggle);
            }

            foreach (var info in _speedMultipliers)
            {
                if (Input.GetKeyDown(info.Value.input))
                {
                    OnToggleSelected(info.Key);
                    info.Key.SetSelected(true);
                }
            }
        }

        private void OnToggleSelected(Toggle toggle)
        {
            _timeController.SetSpeedMultiplier(_speedMultipliers[toggle].speed);

            if (toggle != _pauseToggle)
            {
                _lastActiveToggle = toggle;
            }
        }
    }
}
