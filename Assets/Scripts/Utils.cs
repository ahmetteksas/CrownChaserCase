using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SpinWheel {
    public static class Utils {

        static UtilsSettings _utilsSettings;

        static public UtilsSettings UtilsSettings {
            get {return _utilsSettings;}
            set {_utilsSettings = value;}
        }
        //Takes integer value and shortens it with string values instead of zeroes
        public static string ShortenInteger(int value) {
            List<ThresholdValue> thresholdValues = _utilsSettings.ThresholdValues;
            thresholdValues = thresholdValues.OrderByDescending(obj => obj.threshold).ToList();
            for(int i = 0; i < thresholdValues.Count; i++) {
                int threshold = thresholdValues[i].threshold;
                if(value >= threshold) {
                    return (value /= threshold).ToString() + thresholdValues[i].notation;
                }
            }
            return value.ToString();
        }

        public static string ShortenInteger(string prefix, int value) {
            return prefix + ShortenInteger(value);
        }

        public static string AddThousandSeperator(int value, char seperator) {
            string strvalue = value.ToString();
            Debug.Log(strvalue.Length);
            string result = "";
            for(int i = 0; i < strvalue.Length; i++) {
                if((strvalue.Length - i) % 3 == 0) {
                    result += seperator;
                }
                result += strvalue[i];
            }
            return result;
        }
    }
}
