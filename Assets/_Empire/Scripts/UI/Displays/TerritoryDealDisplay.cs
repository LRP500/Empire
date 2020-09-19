using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Empire.DealManager;

namespace Empire
{
    public class TerritoryDealDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        [SerializeField]
        private Image _image = null;

        public void Initialize(TerritoryDealInfo info)
        {
            SetText(info.activeDeal ?? info.currentOffer);
            _image.color = _text.color = (info.activeDeal != null) ? Color.white : Color.red;
        }

        private void SetText(Deal deal)
        {
            _text.text = string.Format("{0} ({1}k$)", deal.Quantity, deal.SellingPrice);
        }
    }
}
