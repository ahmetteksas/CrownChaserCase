using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpinWheel {

    public class Inventory {

        private List<SlotValue> _inventory;
        
        public Inventory() {
            _inventory = new List<SlotValue>();
        }

        public Inventory(List<SlotValue> inventoryItems) {
            _inventory = inventoryItems;
        }

        //Returns the number of type of items in inventory
        public int InventoryLength {
            get {return _inventory.Count;}
        }

        //Returns the item at the specified index
        public SlotValue ItemAt(int index) {
            if(0 < index && index >= _inventory.Count) {
                //Negative index value or out of inventory bounds
                return null;
            }
            return _inventory[index];
        }

        public void AddItem(SlotValue slotValue) {
            for(int i = 0; i < _inventory.Count; i++) {
                if(slotValue.SlotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                    //Item already exists so increase the amount
                    _inventory[i].Amount += slotValue.Amount;
                    return;
                }
            }

            //Item doesn't exists so add the item to inventory
            _inventory.Add(slotValue);
        }

        //Returns false if delete item amount is higher than what is available
        public bool DeleteItem(SlotValue slotValue) {
            for(int i = 0; i < _inventory.Count; i++) {
                if(slotValue.SlotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                    if(_inventory[i].Amount >= slotValue.Amount) {
                        //There is enough amount of item to be deleted
                        _inventory[i].Amount -= slotValue.Amount;
                        return true;

                    }

                    else {
                        //There is not enough amount of item to be deleted
                        return false;
                    }
                }
            }
            //There is none of the item to be deleted
            return false;
        }

        //Returns the speficied item amount, if there is none return -1
        public int ItemAmount(SlotItem slotItem) {

            for(int i = 0; i < _inventory.Count; i++) {

                if(slotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                return _inventory[i].Amount;
                }
            }

            return -1;
        }

        //Checks if the given item is in inventory
        public bool IsInInventory(SlotValue slotValue) {

            for(int i = 0; i < _inventory.Count; i++) {

                if(slotValue.SlotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                    //Item exists in inventory
                    return true;
                }
            }
                return false;
        }

        //Adds the items in an inventory to this one
        public void AddInventory(Inventory addedInventory) {

            for(int i = 0; i < addedInventory.InventoryLength; i++) {
                this.AddItem(addedInventory.ItemAt(i));
            }

        }

        //Deletes all of the existing items
        public void EmptyInventory() {

            _inventory = new List<SlotValue>();

        }
    }
    
}

