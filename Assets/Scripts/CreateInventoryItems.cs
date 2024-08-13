using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpinWheel {
    public class CreateInventoryItems : MonoBehaviour
    {
        [SerializeField]
        private GameObject itemPrefab;
        private Inventory inventory;

        void Awake() {
            inventory = PlayerInventory.inventory;
            for(int i = 0; i < inventory.InventoryLength; i++) {
                GameObject item = Instantiate(itemPrefab, this.transform);
                item.GetComponentInChildren<Image>().sprite = inventory.ItemAt(i).SlotItem.ItemIcon;
                item.GetComponentInChildren<TextMeshProUGUI>().text = Utils.AddThousandSeperator(inventory.ItemAt(i).Amount, '.');
                float width = item.GetComponent<RectTransform>().sizeDelta.x;
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(width * (i - (float)(inventory.InventoryLength - 1) / 2), item.GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }
}