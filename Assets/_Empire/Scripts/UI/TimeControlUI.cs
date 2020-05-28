using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Time;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TimeControlUI : MonoBehaviour
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
                { _forwardToggle, new SpeedMultiplierInfo(5, KeyCode.E) },
                { _fastForwawrdToggle, new SpeedMultiplierInfo(10, KeyCode.R) }
            };

            _pauseToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(_pauseToggle); });
            _resumeToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(_resumeToggle); });
            _forwardToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(_forwardToggle); });
            _fastForwawrdToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(_fastForwawrdToggle); });

            SelectToggle(_resumeToggle);
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
                    SelectToggle(info.Key);
                }
            }
        }

        private void ToggleValueChanged(Toggle toggle)
        {
            if (toggle.isOn)
            {
                toggle.Select();

                _timeController.SetSpeedMultiplier(_speedMultipliers[toggle].speed);

                if (toggle != _pauseToggle)
                {
                    _lastActiveToggle = toggle;
                }
            }
        }

        private void SelectToggle(Toggle toggle)
        {
            toggle.isOn = true;
        }
    }
}
