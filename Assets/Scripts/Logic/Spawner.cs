using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    [SerializeField] private PrefabListData spawnerData;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform spawnRotation;
    
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private bool infinity = true;
    [SerializeField] Vector2Int numberOfSpawns = Vector2Int.one;
    [SerializeField] Vector2 startDelayВetwenSpawns = Vector2.zero;
    [SerializeField] Vector2 delayВetwenSpawns = Vector2.one;

    public UnityEvent OnStartSpawnEvent;
    public UnityEvent OnStopSpawnEvent;
    public UnityEvent<GameObject> OnSpawnPrefabEvent;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (playOnAwake && spawnerData != null) StartSpawn();
    }

    private IEnumerator StartSpawnRoutine()
    {
        yield return new WaitForSeconds(Random.Range(startDelayВetwenSpawns.x, startDelayВetwenSpawns.y));

        while (infinity)
        {
            SpawnRandomPrefab();
            yield return new WaitForSeconds(Random.Range(delayВetwenSpawns.x, delayВetwenSpawns.y));
        }

        for (int i = 0; i < Random.Range(numberOfSpawns.x, numberOfSpawns.y); i++)
        {
            SpawnRandomPrefab();
            yield return new WaitForSeconds(Random.Range(delayВetwenSpawns.x, delayВetwenSpawns.y));
        }

        OnStopSpawnEvent?.Invoke();
        yield break;
    }

    public void StartSpawn()
    {
        OnStartSpawnEvent?.Invoke();
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(StartSpawnRoutine());
    }

    public void StopSpawn()
    {
        OnStopSpawnEvent?.Invoke();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    public void SpawnRandomPrefab()
    {
        GameObject prefab = Instantiate(spawnerData.Prefabs[Random.Range(0, spawnerData.Prefabs.Length)], spawnParent);
        if(spawnPosition != null)prefab.transform.position = spawnPosition.position;
        if(spawnRotation != null)prefab.transform.rotation = spawnRotation.rotation;
        OnSpawnPrefabEvent?.Invoke(prefab);
    }

}
