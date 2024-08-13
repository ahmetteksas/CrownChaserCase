using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpinWheel {


    public class InventoryScreenManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject backButton;
        // Start is called before the first frame update
        void Start()
        {
            backButton.GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenuScreen");
            });
        }
    }
    
}
