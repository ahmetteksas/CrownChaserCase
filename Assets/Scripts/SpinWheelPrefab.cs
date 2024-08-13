using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace SpinWheel {

    public class SpinWheelPrefab : MonoBehaviour
    {
        [SerializeField]
        GameObject objectToSpin;

        const float rotateAngle = 45.0f;
        const float timerBase = 1.0f;
        const float timerMultiplier =  0.2f;

        public delegate void RotationOverCallback();

        public delegate void SpinButtonCallback();

        public void InitSpinWheel(SpinButtonCallback spinButtonCallback, LevelSettings levelSettings) {
            for(int i = 0; i < objectToSpin.transform.childCount; i++) {
                Transform childTransform = objectToSpin.transform.GetChild(i);
                childTransform.GetComponentInChildren<Image>().sprite = levelSettings.Rewards[i].SlotItem.ItemIcon;
                int amount = levelSettings.Rewards[i].Amount;
                childTransform.GetComponentInChildren<TextMeshProUGUI>().text = Utils.ShortenInteger("x", amount);
            }
            
            //Add spin button callback
            GetComponentInChildren<Button>().onClick.AddListener(() => spinButtonCallback());
        }

        public void RotateWheel(RotationOverCallback callback, int randomInt) {
            objectToSpin.transform.DORotate(new Vector3(0, 0, randomInt * rotateAngle), timerBase + randomInt * timerMultiplier, RotateMode.LocalAxisAdd).OnComplete(() => {
                callback();
            });
        }
    }

}
