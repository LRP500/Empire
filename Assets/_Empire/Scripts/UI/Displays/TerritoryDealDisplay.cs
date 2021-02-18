using TMPro;
using Tools.Time;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Empire.DealManager;

namespace Empire
{
    public class TerritoryDealDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        [FormerlySerializedAs("_text")]
        private TextMeshProUGUI _info;

        public void Initialize(TerritoryDealInfo info)
        {
            if (info.activeDeal != null)
            {
                SetColor(Color.white);
                SetText(info.activeDeal);
            }
            else
            {
                int methProduction = GameplayContext.Instance.productionManager.MethProduction;
                SetColor(methProduction >= info.currentOffer.Quantity ? Color.white : Color.red);
                SetText(info.currentOffer);
            }
        }

        private void SetText(Deal deal)
        {
            _info.text = $"{deal.Quantity}lbs ({deal.SellingPrice}k$)";
        }

        private void SetText(DealOffer offer)
        {
            string time = TimeUtility.Format(offer.RemainingTime);
            _info.text = $"{offer.Quantity}lbs ({offer.SellingPrice}k$) ({time})";
        }

        private void SetColor(Color color)
        {
            _image.color = color;
            _info.color = color;
        }
    }
}
