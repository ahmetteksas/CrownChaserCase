using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpinWheel {

    [CreateAssetMenu(menuName = "SpinWheel/LevelSettings")]
    public class LevelSettings : ScriptableObject {
        public void ShuffleRewards() {
            int len = _rewards.Length;
            for (int i = 0; i < len; i++) {
                int randomIndex = i + UnityEngine.Random.Range(0, len - i);
                SlotValue temp = _rewards[randomIndex];
                _rewards[randomIndex] = _rewards[i];
                _rewards[i] = temp;
            }
        }

        const int size = 8;
        
        //Minimum number of elements between the base element and the randomly chosen index.
        [SerializeField] private int _minimumRoll;

        public int MinimumRoll {
            get {return _minimumRoll;}
        }

        //Maximum number of elements between the base element and the randomly chosen index.
        [SerializeField] private int _maximumRoll;

        public int MaximumRoll {
            get {return _maximumRoll;}
        }

        [SerializeField] private SlotValue[] _rewards = new SlotValue[size];

        public SlotValue[] Rewards { get 
            {return _rewards;}
        }

        

        void OnValidate() {
            if (_rewards.Length != size) {
                Debug.LogWarning("Don't change the rewards array size!");
                Array.Resize(ref _rewards, size);
            }

            if(_minimumRoll < 1 ){
                Debug.LogWarning("Minimum roll must be positive!");
                _minimumRoll = 1;
            }

            if(_maximumRoll <= _minimumRoll) {
                Debug.LogWarning("Maximum roll must be higher than minimum roll!");
                _maximumRoll = _minimumRoll + 1;
            }

            for(int i = 0; i < _rewards.Length; i++) {
                if(_rewards[i].Amount < 1) {
                    _rewards[i].Amount = 1;
                }
            }
        }
    }

}
