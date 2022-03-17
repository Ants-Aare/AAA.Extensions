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
        CreateParticlesystem(particlePrefabs.GetPrefab(prefabIndex));
    }

    public void PlayRandomizedParticle()
    {
        CreateParticlesystem(particlePrefabs.GetRandomPrefabFromPool());
    }
    public void PlayRandomizedParticleAtPosition(Vector3 position)
    {
        CreateParticlesystem(randomizedParticlePrefabs.GetRandomPrefabFromPool(), position);
    }

    public void PlayMultipleParticles()
    {
        foreach (var prefab in multipleParticles.GetAllPrefabs())
        {
            CreateParticlesystem(prefab);
        }
    }
    public void PlayMultipleParticlesAtPosition(Vector3 position)
    {
        foreach (var prefab in multipleParticles.GetAllPrefabs())
        {
            CreateParticlesystem(prefab, position);
        }
    }

    private GameObject CreateParticlesystem(GameObject prefab)
    {
        GameObject go = instantiateAsChild ? Instantiate(prefab, transform) : Instantiate(prefab);

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
        if(particleSystem != null)
            Destroy(go, particleSystem.main.duration + 0.5f);

        return go;
    }
    private GameObject CreateParticlesystem(GameObject prefab, Vector3 position)
    {
        GameObject go = CreateParticlesystem(prefab);
        go.transform.position = position;
        return go;
    }
}