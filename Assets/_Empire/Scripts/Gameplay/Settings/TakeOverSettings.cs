﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Settings/Take Over")]
    public class TakeOverSettings : ScriptableObject
    {
        [SerializeField]
        [SuffixLabel("(%)", Overlay = true)]
        private int _successChance = 50;
        public int SuccessChance => _successChance;

        [SerializeField]
        [SuffixLabel("(%)", Overlay = true)]
        private int _resourceGainChance = 50;
        public int ResourceGainChance => _resourceGainChance;

        [SerializeField]
        [SuffixLabel("($)", Overlay = true)]
        private int _cashQuantityMin = 10000;
        public int CashQuantityMin => _cashQuantityMin;

        [SerializeField]
        [SuffixLabel("($)", Overlay = true)]
        private int _cashQuantityMax = 1000000;
        public int CashQuantityMax => _cashQuantityMax;

        [SerializeField]
        [SuffixLabel("(lbs)", Overlay = true)]
        private int _methQuantityMin = 100;
        public int MethQuantityMin => _methQuantityMin;

        [SerializeField]
        [SuffixLabel("(lbs)", Overlay = true)]
        private int _methQuantityMax = 1000;
        public int MethQuantityMax => _methQuantityMax;

        public int RandomMethAmount()
        {
            return Random.Range(MethQuantityMin, MethQuantityMax + 1);
        }

        public int RandomCashAmount()
        {
            return Random.Range(CashQuantityMin, CashQuantityMax + 1);
        }
    }
}