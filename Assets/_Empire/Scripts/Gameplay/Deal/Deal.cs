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
        protected Territory _territory;
        public Territory Territory => _territory;

        [ReadOnly]
        [SerializeField]
        protected int _quantity;

        [ReadOnly]
        [SerializeField]
        protected int _sellingPrice; 

        public int Quantity => _quantity;
        public int SellingPrice => _sellingPrice;
    }
}