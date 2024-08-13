using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpinWheel {

    [Serializable]
    public class ThresholdValue {
        public int threshold;
        public string notation;
    }
    [CreateAssetMenu(menuName = "SpinWheel/Utils/UtilsSettings")]
    public class UtilsSettings : ScriptableObject {

        [SerializeField]
        private List<ThresholdValue> _thresholdValues;

        public List<ThresholdValue> ThresholdValues {
            get {return _thresholdValues;}
            set {_thresholdValues = value;}
        }
        
    }

}

