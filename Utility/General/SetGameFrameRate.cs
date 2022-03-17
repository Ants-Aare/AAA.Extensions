using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAA.Utility.General
{
    public class SetGameFrameRate : MonoBehaviour
    {
        public int maxFramesPerSecond = 30;

        private void Awake()
        {
            SetGameFrameRateTo(maxFramesPerSecond);
        }

        //this limits the FPS and processor usage and therefore battery lifetime, 30FPS is mobile standard
        public void SetGameFrameRateTo(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
    }
}