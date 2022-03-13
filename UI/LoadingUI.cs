using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using AAA.Utility.Singleton;

namespace Contaquest.UI
{
    public class LoadingUI : PersistentGenericSingleton<LoadingUI>
    {
        [SerializeField] private TextMeshProUGUI textMessage;
        [SerializeField] private GameObject canvas;
        [SerializeField] private float dotWaitDuration = 0.4f;
        [SerializeField] private string loadingText = "Loading";

        private bool isEnabled = false;

        [Button][HideInEditorMode]
        public void Enable()
        {
            if (isEnabled)
                return;
            Debug.Log("Enabling Loading");
            canvas.SetActive(true);
            StartCoroutine(UpdateDots());
            isEnabled = true;
        }
        [Button][HideInEditorMode]
        public void Disable()
        {
            if (!isEnabled)
                return;
            Debug.Log("Disabling Loading");

            canvas.SetActive(false);
            StopAllCoroutines();
            isEnabled = false;
        }

        public void UpdateText(string newText)
        {
            loadingText = newText;
            StopAllCoroutines();
            StartCoroutine(UpdateDots());
        }

        private IEnumerator UpdateDots()
        {
            if (textMessage != null)
            {
                while (true)
                {
                    textMessage.text = loadingText;
                    yield return new WaitForSeconds(dotWaitDuration);
                    textMessage.text = loadingText + ".";
                    yield return new WaitForSeconds(dotWaitDuration);
                    textMessage.text = loadingText + "..";
                    yield return new WaitForSeconds(dotWaitDuration);
                    textMessage.text = loadingText + "...";
                    yield return new WaitForSeconds(dotWaitDuration);
                }
            }
        }
    }
}
