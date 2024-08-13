using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpinWheel {
    
    public class ExitScreenPopUp : MonoBehaviour {
        public Inventory earnedItems;

        [SerializeField]
        private GameObject exitScreenExitButton;
        [SerializeField]
        private GameObject exitScreenGoBackButton;

        void Start()
        {
            exitScreenExitButton.GetComponent<Button>().onClick.AddListener(() => {
                PlayerInventory.inventory.AddInventory(earnedItems);
                SceneManager.LoadScene("MainMenuScreen");
            });
            exitScreenGoBackButton.GetComponent<Button>().onClick.AddListener(() => {
                gameObject.SetActive(false);
            });
        }
    }

}