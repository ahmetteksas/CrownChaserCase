using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {

    public class PlayerInventory : MonoBehaviour {

        private static bool isInventoryCreated = false;
        public static Inventory inventory;
        
        [SerializeField]
        private List<SlotValue> inventoryItems;

        void OnEnable() {
            if (!isInventoryCreated) {
                isInventoryCreated = true;
                inventory = new Inventory(inventoryItems);
                DontDestroyOnLoad(this.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
    
}
