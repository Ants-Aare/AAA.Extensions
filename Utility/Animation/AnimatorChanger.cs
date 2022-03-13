using UnityEngine;

public class AnimatorChanger : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private string propertyName;

    [Header("References")]
    [SerializeField]
    private Animator anim = null;

    public void SetBool(bool value)
    {
        anim.SetBool(propertyName, value);
    }
    public void SetBool(int value)
    {
        anim.SetBool(propertyName, value != 0);
    }
}