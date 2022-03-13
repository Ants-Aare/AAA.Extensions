using UnityEngine;

public class ShowError : MonoBehaviour, IErrorSource
{
    [SerializeField] private string errorMessage;

    public Error GetError()
    {
        return new Error(errorMessage, this);
    }

    public void ShowErrorMessage()
    {
        ShowErrorMessage(errorMessage);
    }

    public void ShowErrorMessage(string message)
    {
        ErrorManager.Instance.ShowError(new Error(errorMessage, this));
    }
}
