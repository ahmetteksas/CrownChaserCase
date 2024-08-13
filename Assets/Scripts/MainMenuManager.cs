using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpinWheel {

    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playButton;
        [SerializeField]
        private GameObject inventoryButton;
        [SerializeField]
        private GameObject exitButton;

        void Start()
        {
            playButton.GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("SpinWheelScreen");
            });
            inventoryButton.GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("InventoryScreen");
            });
            exitButton.GetComponent<Button>().onClick.AddListener(() => {
                Application.Quit();
            });
        }
    }

}