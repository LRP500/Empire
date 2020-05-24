using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Empire
{
    [Serializable]
    public class Deal
    {
        protected DealOfferSettings _settings = null;

        [ReadOnly]
        [SerializeField]
        protected Territory _territory = null;
        public Territory Territory => _territory;

        [ReadOnly]
        [SerializeField]
        protected int _quantity = 0;

        [ReadOnly]
        [SerializeField]
        protected int _sellingPrice = 0; 

        public int Quantity => _quantity;
        public int SellingPrice => _sellingPrice;
    }
}