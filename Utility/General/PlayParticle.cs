using UnityEngine;
using Sirenix.OdinInspector;
using AAA.Utility.GlobalVariables;

public class PlayParticle : MonoBehaviour
{
    [TabGroup("Properties")][SerializeField] private bool instantiateAsChild = true;
    [TabGroup("Properties")][SerializeField] private Transform instantiatePosition;

    [TabGroup("References")][SerializeField][InlineEditor] private PrefabPool randomizedParticlePrefabs, multipleParticles, particlePrefabs;

    public void PlaySpecificParticle(int prefabIndex)
    {
        GameObject go = particlePrefabs.GetInstanceFromPool(prefabIndex);

        CreateParticlesystem(go);
    }

    public void PlayRandomizedParticle()
    {
        GameObject go = particlePrefabs.GetRandomInstanceFromPool();

        CreateParticlesystem(go);
    }
    public void PlayRandomizedParticleAtPosition(Vector3 position)
    {
        GameObject go = particlePrefabs.GetRandomInstanceFromPool();

        CreateParticlesystem(go, position);
    }

    public void PlayMultipleParticles()
    {
        GameObject[] gameObjects = multipleParticles.GetAllInstances();
        foreach (var go in gameObjects)
        {
            CreateParticlesystem(go);
        }
    }
    public void PlayMultipleParticlesAtPosition(Vector3 position)
    {
        GameObject[] gameObjects = multipleParticles.GetAllInstances();
        foreach (var go in gameObjects)
        {
            CreateParticlesystem(go, position);
        }
    }

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