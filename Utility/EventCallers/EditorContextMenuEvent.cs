using UnityEngine;
using UnityEngine.Events;

public class EditorContextMenuEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onContextMenu = null;

    [ContextMenu("Invoke Event")]
    private void InvokeEvent()
    {
        onContextMenu.Invoke();
    }
}