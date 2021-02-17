using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Empire.DealManager;

namespace Empire
{
    public class TerritoryDealDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Image _image;

        public void Initialize(TerritoryDealInfo info)
        {
            SetText(info.activeDeal ?? info.currentOffer);

            if (info.activeDeal != null)
            {
                SetColor(Color.white);
            }
            else
            {
                int methProduction = GameplayContext.Instance.productionManager.MethProduction;
                SetColor(methProduction >= info.currentOffer.Quantity ? Color.white : Color.red);
            }
        }

        private void SetText(Deal deal)
        {
            _text.text = $"{deal.Quantity}lbs ({deal.SellingPrice}k$)";
        }

        private void SetColor(Color color)
        {
            _image.color = color;
            _text.color = color;
        }
    }
}
