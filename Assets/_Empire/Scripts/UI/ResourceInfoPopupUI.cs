using TMPro;
using UnityEngine;

namespace Empire.UI
{
    public class ResourceInfoPopupUI : PanelUI
    {
        [SerializeField]
        private TextMeshProUGUI _resourceName;

        [SerializeField]
        private TextMeshProUGUI _resourceDescription;

        [SerializeField]
        private ResourceVariable _hoveredResource;

        protected override void Awake()
        {
            _hoveredResource.Subscribe(Refresh);
            Close();
        }

        private void Refresh()
        {
            if (_hoveredResource.Value)
            {
                _resourceName.text = _hoveredResource.Value.Type.Name;
                _resourceDescription.text = _hoveredResource.Value.Type.Description;
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}
