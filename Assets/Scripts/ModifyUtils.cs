using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {

    public class ModifyUtils : MonoBehaviour
    {
        [SerializeField]
        UtilsSettings _utilsSettings;

        void Awake()
        {
            Utils.UtilsSettings = _utilsSettings;
        }
    }

}
