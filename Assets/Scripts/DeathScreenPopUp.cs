using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SpinWheel {

    public class DeathScreenPopUp : MonoBehaviour {
        [SerializeField]
        private SlotValue slotValue;
        [SerializeField]
        private GameObject exitButton;
        [SerializeField]
        private GameObject reviveButton;
        [SerializeField]
        private GameObject reviveButtonAmountText;
        [SerializeField]
        private GameObject reviveButtonItemIcon;
        void OnEnable()
        {
            exitButton.GetComponent<Button>().interactable = true;
            reviveButtonAmountText.GetComponent<TextMeshProUGUI>().text = slotValue.Amount.ToString();
            reviveButtonItemIcon.GetComponent<Image>().sprite = slotValue.SlotItem.ItemIcon;

            exitButton.GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenuScreen");
            });        

            Inventory playerInventory = PlayerInventory.inventory;
            if(playerInventory.ItemAmount(slotValue.SlotItem) >= slotValue.Amount) {
                Debug.Log(slotValue.Amount);
                //There are enough currency to spend
                reviveButton.GetComponent<Button>().onClick.AddListener(() => {
                    playerInventory.DeleteItem(slotValue);
                    reviveButton.GetComponent<Button>().interactable = true;
                    gameObject.SetActive(false);
                });
            }

            else {
                //Not enough currency
                reviveButton.GetComponent<Button>().interactable = false;
                reviveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough Currency";
            }
        }
    }

}
