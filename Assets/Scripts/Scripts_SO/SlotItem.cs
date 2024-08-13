using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {

    public enum ItemType 
    {
        Collectible,
        Lethal
    }

    [CreateAssetMenu(menuName = "SpinWheel/ItemType")]
    public class SlotItem : ScriptableObject {
        [SerializeField]
        private string _itemName;
        
        public string ItemName {
            get {return _itemName;}
        }

        [SerializeField]
        private Sprite _itemIcon;
        
        public Sprite ItemIcon {
            get {return _itemIcon;}
        }

        [SerializeField]
        private ItemType _itemType;

        public ItemType ItemType {
            get {return _itemType;}
        }
    }

}
