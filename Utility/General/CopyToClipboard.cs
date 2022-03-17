using UnityEngine;
using TMPro;

namespace AAA.Utility.General
{
    public class CopyToClipboard : MonoBehaviour
    {
        [SerializeField] private string clipboardString;
        [SerializeField] private TextMeshProUGUI clipBoardText;

        public void CopyTextToClipboard()
        {
            Clipboard.Copy(clipBoardText.text);
        }
        public void CopyStringToClipboard()
        {
            Clipboard.Copy(clipboardString);
        }
        public void CopyStringToClipboard(string clipboardString)
        {
            Clipboard.Copy(clipboardString);
        }
    }

    public static class Clipboard
    {
        public static void Copy(this string s)
        {
            TextEditor te = new TextEditor();
            te.text = s;
            te.SelectAll();
            te.Copy();
        }
    }
}