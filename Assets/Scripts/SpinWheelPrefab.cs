using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace SpinWheel
{

    public class SpinWheelPrefab : MonoBehaviour
    {
        [SerializeField]
        GameObject objectToSpin;

        const float rotateAngle = 45.0f;
        const float timerBase = 1.0f;
        const float timerMultiplier = 0.2f;

        private bool canRotate = true;
        private UIManager manager;
        public Button spinButton;
        public delegate void RotationOverCallback();

        public delegate void SpinButtonCallback();

        public void InitSpinWheel(SpinButtonCallback spinButtonCallback, LevelSettings levelSettings)
        {
            for (int i = 0; i < objectToSpin.transform.childCount; i++)
            {
                Transform childTransform = objectToSpin.transform.GetChild(i);
                childTransform.GetComponentInChildren<Image>().sprite = levelSettings.Rewards[i].SlotItem.ItemIcon;
                int amount = levelSettings.Rewards[i].Amount;
                childTransform.GetComponentInChildren<TextMeshProUGUI>().text = Utils.ShortenInteger("x", amount);
            }
            spinButton = GetComponentInChildren<Button>();
            spinButton.onClick.AddListener(() => spinButtonCallback());
            spinButton.onClick.AddListener(() => SendRotateInfo());
            manager = FindObjectOfType<UIManager>();
            Debug.Log(spinButton.gameObject.name);
            //GetComponentInChildren<Button>().onClick.AddListener(() => spinButtonCallback());
            //GetComponentInChildren<Button>().onClick.AddListener(() => SendRotateInfo());

        }
        public void SendRotateInfo()
        {
            manager.rotateCount--;
        }

        public void RotateWheel(RotationOverCallback callback, int randomInt)
        {
            if (canRotate)
            {
                objectToSpin.transform.DORotate(new Vector3(0, 0, randomInt * rotateAngle), timerBase + randomInt * timerMultiplier, RotateMode.LocalAxisAdd).OnComplete(() =>
                {
                    callback();
                });
            }
        }
        private void Update()
        {
            if (manager.rotateCount <= 0)
            {
                if (manager.rewardsInventory.ItemAt(0).Amount >= 100)
                {
                    manager.getExtraSpin.SetActive(true);
                }
                else
                {
                    //manager.deathScreenPopUp.SetActive(true);
                }
                Debug.Log(spinButton.gameObject.name);
                spinButton.interactable = false;
            }
            else
            {
                Debug.Log(spinButton.gameObject.name);
                manager.getExtraSpin.SetActive(false);

                spinButton.interactable = true;
            }
        }
    }

}
