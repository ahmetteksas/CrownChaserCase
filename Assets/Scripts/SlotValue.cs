using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpinWheel {

    [Serializable]
    public class SlotValue {
        
        [SerializeField]
        private SlotItem _slotItem;

        public SlotItem SlotItem {
            get {return _slotItem;}
        }

        [SerializeField]
        private int _amount;

        public int Amount {
            get {return _amount;}
            set {_amount = value;}
        }
            
        public SlotValue(SlotItem slotItem, int amount) {
        this._slotItem = slotItem;
        this._amount = amount;
        }
        
        public SlotValue(SlotValue other) {
            this._slotItem = other.SlotItem;
            this._amount = other.Amount;
        }
    }

}
