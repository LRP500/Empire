using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Empire.UI
{
    public class ResourceInfoPopupUI : PanelUI
    {
        [SerializeField]
        [FormerlySerializedAs("_resourceName")]
        private TextMeshProUGUI _title;

        [SerializeField]
        [FormerlySerializedAs("_resourceDescription")]
        private TextMeshProUGUI _description;

        [SerializeField]
        private ResourceVariable _hoveredResource;

        [SerializeField]
        private ResourceProductionVariable _hoveredProduction;

        protected override void Awake()
        {
            _hoveredResource.Subscribe(Refresh);
            _hoveredProduction.Subscribe(Refresh);
            Close();
        }

        private void Refresh()
        {
            if (_hoveredProduction.Value)
            {
                DisplayProductionInfo(_hoveredProduction);
                Open();
            }
            else if (_hoveredResource.Value)
            {
                DisplayResourceInfo(_hoveredResource);
                Open();
            }
            else
            {
                Close();
            }
        }

        private void DisplayResourceInfo(Resource resource)
        {
            _title.text = resource.Type.Name;
            _description.text = resource.Type.Description;
        }

        private void DisplayProductionInfo(ResourceProduction production)
        {
            _title.text = production.Name;
            _description.text = production.Description;
        }
    }
}
