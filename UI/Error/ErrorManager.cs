using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using AAA.Utility.Singleton;

public class ErrorManager : PersistentGenericSingleton<ErrorManager>
{
    [SerializeField] private float defaultErrorShowTime = 1f;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject errorIcon;
    [SerializeField] private UnityEvent onShowMessage, onHideMessage;

    //private Error currentError = null;

    #region Showing Errors
    public void ShowError(string message)
    {
        Debug.LogWarning("Showing Error " + message);
        errorIcon.SetActive(true);
        text.text = message;
        onShowMessage?.Invoke();

        HideError(defaultErrorShowTime);
    }
    public void ShowSuccess(string message)
    {
        errorIcon.SetActive(false);
        text.text = message;
        onShowMessage?.Invoke();

        HideError(defaultErrorShowTime);
    }
    public void ShowError(Error error)
    {
        ShowError(error.errorMessage);
    }
    public void ShowErrorPermanent(Error error)
    {
        Debug.LogWarning("Showing Error " + error.errorMessage);

        if (error != null)
        {
            errorIcon.SetActive(true);
            text.text = error.errorMessage;
            onShowMessage?.Invoke();
        }
        else
        {
            HideError(0);
        }
    }

    #endregion

    public void HideError(float errorShowTime)
    {
        if(errorShowTime == 0f)
        {
            onHideMessage?.Invoke();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(StartHideError(defaultErrorShowTime));
    }
    public IEnumerator StartHideError(float errorShowTime)
    {
        yield return new WaitForSeconds(errorShowTime);
        onHideMessage?.Invoke();
    }
}
