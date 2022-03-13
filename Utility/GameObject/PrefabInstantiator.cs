using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private bool InstantiateOnStart = false;

    public void Start()
    {
        if (InstantiateOnStart)
            InstantiateAllPrefabs();
    }

    public void InstantiateAllPrefabs()
    {
        foreach (GameObject prefab in prefabs)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
    public void InstantiateSpecificPrefab(int index)
    {
        Instantiate(prefabs[index], Vector3.zero, Quaternion.identity);
    }
}