using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace SpinWheel {

    public class UIManager : MonoBehaviour {
        [SerializeField]
        private List<LevelSettings> levelSettings;
        [SerializeField]
        private int silverPeriod;
        [SerializeField]
        private int goldPeriod;
        [SerializeField]
        private GameObject rewardsFrameExitButton;
        [SerializeField]
        private GameObject rewardsFrameDisplayContainer;
        [SerializeField]
        private GameObject levelsFrameLevelLabelsContainer;
        [SerializeField]
        private GameObject slotValuePrefab;
        [SerializeField]
        private GameObject bronzePrefab;
        [SerializeField]
        private GameObject silverPrefab;
        [SerializeField]
        private GameObject goldPrefab;
        [SerializeField]
        private GameObject exitScreenPopUp;
        [SerializeField]
        private GameObject deathScreenPopUp;
        [SerializeField]
        private GameObject levelLabelPrefab;
        [SerializeField]
        private Sprite bronzeLevelLabelSprite;
        [SerializeField]
        private Sprite silverLevelLabelSprite;
        [SerializeField]
        private Sprite goldLevelLabelSprite;

        private const int TOTAL_SLOTS = 8;

        
        private GameObject currentLevelInstance;

        //Level number that increases each level starting from level 1
        private int currentLevelNo = 1;

        private Inventory rewardsInventory = new Inventory();
        void Start() {
            GenerateLevelLabels();
            rewardsFrameExitButton.GetComponent<Button>().onClick.AddListener(ExitButtonCallback);
            InitLevel(currentLevelNo);
        }
        void OnValidate() {
            if(silverPeriod < 2) {
                silverPeriod = 2;
            }
            if(goldPeriod <= silverPeriod) {
                goldPeriod = silverPeriod + 1;
            }
        }

        private void InitLevel(int levelNo) {

            int levelNoMinusOne = levelNo - 1;

            levelSettings[levelNoMinusOne].ShuffleRewards();

            if(levelNo % goldPeriod == 0) currentLevelInstance = Instantiate(goldPrefab, transform);
            else if(levelNo % silverPeriod == 0) currentLevelInstance = Instantiate(silverPrefab, transform);
            else currentLevelInstance = Instantiate(bronzePrefab, transform);
            
            //Can be changed to take parameters with a factory but does it make sense?

            currentLevelInstance.GetComponent<SpinWheelPrefab>().InitSpinWheel(SpinWheel, levelSettings[levelNoMinusOne]);
        }

        private void DisplayInventory() {

            GameObject container = rewardsFrameDisplayContainer;

            //Delete all of the children
            foreach (Transform child in container.transform) {
                GameObject.Destroy(child.gameObject);
            }


            for(int i = 0; i < rewardsInventory.InventoryLength; i++) {
                Sprite sprite = rewardsInventory.ItemAt(i).SlotItem.ItemIcon;
                GameObject slotValueObject = Instantiate(slotValuePrefab, container.transform);
                slotValueObject.GetComponentInChildren<Image>().sprite = rewardsInventory.ItemAt(i).SlotItem.ItemIcon;
                slotValueObject.GetComponentInChildren<TextMeshProUGUI>().text = rewardsInventory.ItemAt(i).Amount.ToString();
                float height = slotValueObject.GetComponentInChildren<RectTransform>().sizeDelta.y;
                slotValueObject.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(0, - height * i / 2);
                if(i >= rewardsInventory.InventoryLength - 1) {
                    //Last child
                    //Expand container according to the children
                    container.GetComponent<RectTransform>().sizeDelta = new Vector2(container.GetComponent<RectTransform>().sizeDelta.x, height * (i + 1) / 2);
                }
            }
        }

        private void SpinWheel() {
            //Exit button is inactive since game started
            rewardsFrameExitButton.GetComponent<Button>().interactable = false;

            int randomInt = UnityEngine.Random.Range(levelSettings[currentLevelNo - 1].MinimumRoll, levelSettings[currentLevelNo - 1].MaximumRoll);
            currentLevelInstance.GetComponent<SpinWheelPrefab>().RotateWheel(() => {
                
                //Spin animation is over
                if(levelSettings[currentLevelNo - 1].Rewards[randomInt % TOTAL_SLOTS].SlotItem.ItemType == ItemType.Lethal) {
                    //Item is a deathly bomb
                    Destroy(currentLevelInstance);

                    if(currentLevelNo <= levelSettings.Count) {
                        currentLevelNo++;
                        if(currentLevelNo % silverPeriod == 0 || currentLevelNo % goldPeriod == 0) {
                            //Exit button is active
                            rewardsFrameExitButton.GetComponent<Button>().interactable = true;
                        }
                        GenerateLevelLabels();
                        InitLevel(currentLevelNo);
                    }
                    deathScreenPopUp.transform.SetAsLastSibling();
                    deathScreenPopUp.SetActive(true);
                }

                else {
                    //Item is an inventory item
                    rewardsInventory.AddItem(levelSettings[currentLevelNo - 1].Rewards[randomInt % TOTAL_SLOTS]);

                    DisplayInventory();

                    Destroy(currentLevelInstance);

                    if(currentLevelNo <= levelSettings.Count) {
                        currentLevelNo++;
                        if(currentLevelNo % silverPeriod == 0 || currentLevelNo % goldPeriod == 0) {
                            //Exit button is active
                            rewardsFrameExitButton.GetComponent<Button>().interactable = true;
                        }
                        GenerateLevelLabels();
                        InitLevel(currentLevelNo);
                    }

                    else {
                        //There are no more levels
                        PlayerInventory.inventory.AddInventory(rewardsInventory);
                        SceneManager.LoadScene("MainMenuScreen");
                    }
                }
            },
            randomInt); 

            currentLevelInstance.GetComponentInChildren<Button>().interactable = false;
        }

        private void ExitButtonCallback() {
            
            exitScreenPopUp.transform.SetAsLastSibling();
            exitScreenPopUp.SetActive(true);
            exitScreenPopUp.GetComponent<ExitScreenPopUp>().earnedItems = rewardsInventory;
            
        }

        private void GenerateLevelLabels() {
            float offsetX = 20.0f;
            float labelWidth = levelLabelPrefab.GetComponent<RectTransform>().sizeDelta.x + offsetX;

            GameObject container = levelsFrameLevelLabelsContainer;

            //Delete existing childs
            foreach (Transform transform in container.transform) {
                Destroy(transform.gameObject);
            }

            for(int i = 0; i < levelSettings.Count; i++) {
                int levelIndex = i + 1;
                GameObject levelLabel = Instantiate(levelLabelPrefab, container.transform);
                levelLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(labelWidth * (i - (currentLevelNo - 1)), levelLabel.GetComponent<RectTransform>().anchoredPosition.y);
                levelLabel.GetComponentInChildren<TextMeshProUGUI>().text = levelIndex.ToString();
                if(levelIndex % goldPeriod == 0) {
                    levelLabel.GetComponent<Image>().sprite = goldLevelLabelSprite;
                }
                else if(levelIndex % silverPeriod == 0) {
                    levelLabel.GetComponent<Image>().sprite = silverLevelLabelSprite;
                }
                else {
                    levelLabel.GetComponent<Image>().sprite = bronzeLevelLabelSprite;
                }
            }
        }
    }

}

