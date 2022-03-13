using UnityEngine;

public class Reparent : MonoBehaviour
{
    [SerializeField]
    private Transform[] objectsToReparent;

    [SerializeField]
    private Transform targetParent;
    [SerializeField]
    private Transform[] targetParents;

    public void ReparentObjectsToTargetParent()
    {
        ReparentObjectsTo(targetParent);
    }
    public void ReparentObjectsToTargetParents()
    {
        for (int i = 0; i < objectsToReparent.Length; i++)
        {
            if(objectsToReparent[i] != null && targetParents[i] != null)
                objectsToReparent[i].parent = targetParents[i];
        }
    }

    public void ReparentObjectsTo(Transform targetParent)
    {
        if(targetParent == null)
            return;
        for (int i = 0; i < objectsToReparent.Length; i++)
        {
            if(objectsToReparent[i] != null)
                objectsToReparent[i].parent = targetParent;
        }
    }
    public void ReparentObjectToTargetParent(Transform sourceObject)
    {
        sourceObject.parent = targetParent;
    }
}