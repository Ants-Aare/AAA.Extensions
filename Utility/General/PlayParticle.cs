using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private bool instantiateAsChild = true;
    [SerializeField]
    private Transform instantiatePosition;

    [Header("References")]
    [SerializeField]
    private GameObject[] randomizedParticlePrefabs;
    [SerializeField]
    private GameObject[] multipleParticles;
    [SerializeField]
    private GameObject[] particleList;

    public void PlaySpecificParticle(int particlePrefab)
    {
        if (particlePrefab < particleList.Length && particlePrefab >= 0 && particleList[particlePrefab] != null)
        {
            GameObject go = CreateParticlesystem(particleList[particlePrefab]);
            Destroy(go, go.GetComponent<ParticleSystem>().main.duration + 0.5f);
        }
    }
    public void PlayRandomizedParticle()
    {
        if (randomizedParticlePrefabs.Length > 0)
        {
            GameObject go = CreateParticlesystem(randomizedParticlePrefabs[Random.Range(0, randomizedParticlePrefabs.Length)]);
            Destroy(go, go.GetComponent<ParticleSystem>().main.duration + 0.5f);
        }
    }
    public void PlayRandomizedParticleAtPosition(Vector3 position)
    {
        if (randomizedParticlePrefabs.Length > 0)
        {
            GameObject go = CreateParticlesystem(randomizedParticlePrefabs[Random.Range(0, randomizedParticlePrefabs.Length)]);
            Destroy(go, go.GetComponent<ParticleSystem>().main.duration + 0.5f);
        }
    }

    public void PlayMultipleParticles()
    {
        for (int i = 0; i < multipleParticles.Length; i++)
        {
            GameObject go = CreateParticlesystem(multipleParticles[i]);
            Destroy(go, go.GetComponent<ParticleSystem>().main.duration + 0.5f);
        }

    }
    public void PlayMultipleParticlesAtPosition(Vector3 position)
    {
        for (int i = 0; i < multipleParticles.Length; i++)
        {
            GameObject go = CreateParticlesystem(multipleParticles[i], position);
            Destroy(go, go.GetComponent<ParticleSystem>().main.duration + 0.5f);
        }
    }

    private GameObject CreateParticlesystem(GameObject prefab)
    {
        GameObject go = null;
        if (instantiateAsChild)
            go = Instantiate(prefab, this.transform);
        else
            go = Instantiate(prefab);

        if (instantiatePosition != null)
        {
            go.transform.position = instantiatePosition.position;
            go.transform.rotation = instantiatePosition.rotation;
        }
        else
        {
            go.transform.position = transform.position;
        }
        return go;
    }
    private GameObject CreateParticlesystem(GameObject prefab, Vector3 position)
    {
        GameObject go = CreateParticlesystem(prefab);
        go.transform.position = position;
        return go;
    }
}