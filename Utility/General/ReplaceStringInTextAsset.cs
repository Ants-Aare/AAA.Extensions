using System;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

namespace AAA.Utility.General
{
    public class ReplaceStringInTextAsset : MonoBehaviour
    {
        #if UNITY_EDITOR
        public string outputFolder;
        public string fileType;

        public List<TextAsset> textAssets = new List<TextAsset>();

        public string oldString;
        public string newString;

        [Button]
        public void ReplaceString()
        {
            foreach (var textAsset in textAssets)
            {
                string textString = textAsset.text;

                textString = textString.Replace(oldString, newString);

                File.WriteAllText(Application.dataPath + outputFolder + textAsset.name + fileType, textString);
            }
        }
        #endif
    }
}