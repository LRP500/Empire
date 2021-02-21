using TMPro;
using UnityEngine;

namespace Empire
{
    public class HelpPanel : PanelUI
    {
        [SerializeField]
        private TextAsset _mechanicsTextFile;

        [SerializeField]
        private TextAsset _controlsTextFile;

        [SerializeField]
        private TextMeshProUGUI _mechanicsText;

        [SerializeField]
        private TextMeshProUGUI _controlsText;

        protected override void Awake()
        {
            InitializeHelpText();
        }

        private void InitializeHelpText()
        {
            _mechanicsText.text = _mechanicsTextFile.text;
            _controlsText.text = _controlsTextFile.text;
        }
    }
}
