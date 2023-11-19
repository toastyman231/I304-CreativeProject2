using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;

    [SerializeField] private float spawnTimeMin;

    [SerializeField] private float spawnTimeMax;

    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        StartCoroutine(Spawn());
        GameplayEvents.DeathEvent += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        GameplayEvents.DeathEvent -= OnPlayerDeath;
    }

    private IEnumerator Spawn()
    {
        float randomTime = Random.Range(spawnTimeMin, spawnTimeMax);

        yield return new WaitForSeconds(randomTime);

        Instantiate(spawnPrefab, RandomPointInBounds(_collider.bounds), Quaternion.identity);

        StartCoroutine(Spawn());
    }

    private void OnPlayerDeath()
    {
        StopAllCoroutines();
        foreach (var enemy in FindObjectsByType<EnemyController>(FindObjectsInactive.Exclude,
                     FindObjectsSortMode.None))
        {
            Destroy(enemy.gameObject);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
