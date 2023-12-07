using UnityEngine;
using AAA.Utility;
using NaughtyAttributes;

public class PlayParticle : MonoBehaviour
{
    [SerializeField] private bool instantiateAsChild = true;
    [SerializeField] private Transform instantiatePosition;

    [SerializeField][Expandable] private PrefabPool randomizedParticlePrefabs, multipleParticles, particlePrefabs;

    private GameObject CreateParticlesystem(GameObject go)
    {
        if(instantiateAsChild)
            go.transform.parent = transform;

        if (instantiatePosition != null)
        {
            go.transform.position = instantiatePosition.position;
            go.transform.rotation = instantiatePosition.rotation;
        }
        else
        {
            go.transform.position = transform.position;
        }

        ParticleSystem particleSystem = go.GetComponent<ParticleSystem>();
        // if(particleSystem != null)
        //     Destroy(go, particleSystem.main.duration + 0.5f);

        return go;
    }
    private GameObject CreateParticlesystem(GameObject prefab, Vector3 position)
    {
        GameObject go = CreateParticlesystem(prefab);
        go.transform.position = position;
        return go;
    }
}