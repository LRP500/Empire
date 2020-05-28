﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public abstract class TerritoryAction : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Basic Info")]
        private string _title = string.Empty;
        public string Title => _title;

        [TextArea]
        [SerializeField]
        [BoxGroup("Basic Info")]
        private string _description = string.Empty;
        public string Description => _description;

        [Header("UI")]

        [SerializeField]
        private TerritoryActionInfoUI _infoPanelPrefab = null;
        public TerritoryActionInfoUI InfoPanelPrefab => _infoPanelPrefab;

        [Space]
        [SerializeField]
        protected GameplayContext _context = null;
        public GameplayContext Context => _context;

        public abstract void Execute(Territory territory);
        public abstract bool CanExecute(Territory territory);
    }
}