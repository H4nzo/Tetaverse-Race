// using UnityEngine;

// namespace Hanzo
// {
//     public class ItemSpawner : MonoBehaviour
//     {
//         public GameObject itemPrefab;
//         public float instantiateTime = 7f;
//         public float spawnRadius = 10f; // Maximum distance from the spawner to spawn the item

//         private float itemTimer;


//         private void Start()
//         {
//             itemTimer = instantiateTime;
//         }

//         private void Update()
//         {
//             if (itemPrefab != null)
//             {
//                 itemTimer -= Time.deltaTime;

//                 if (itemTimer <= 0f)
//                 {
//                     SpawnItem();
//                     itemTimer = instantiateTime;
//                 }
//             }
//         }

//         private void SpawnItem()
//         {
//             Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
//             randomDirection += transform.position;

//             Vector3 spawnPosition = FindSpawnPosition(randomDirection);
//             if (spawnPosition != Vector3.zero)
//             {
//                 Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
//             }
//         }

//         private Vector3 FindSpawnPosition(Vector3 randomPosition)
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(randomPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity))
//             {
//                 return hit.point;
//             }
//             return Vector3.zero; // Unable to find a valid spawn position
//         }
//     }
// }

using UnityEngine;
using UnityEngine.AI;

namespace Hanzo
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemPrefab;
        public float instantiateTime = 2.0f;
        public float spawnRadius = 10f; // Maximum distance from the spawner to spawn the item

        private NavMeshSurface navMeshSurface;
        [SerializeField] private float itemTimer;

        private void Start()
        {
            navMeshSurface = GetComponent<NavMeshSurface>();
            itemTimer = instantiateTime;
        }

        private void Update()
        {
            if (itemPrefab != null)
            {
                itemTimer -= Time.deltaTime;

                if (itemTimer <= 0f)
                {
                    SpawnItem();
                    itemTimer = instantiateTime;
                }
            }
        }

        private void SpawnItem()
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 1.0f, NavMesh.AllAreas))
            {
                Instantiate(itemPrefab, hit.position, Quaternion.identity);
                // OnItemSpawned();
               
            }
        }

        // Optional: Regenerate the navmesh when an item is spawned (if needed).
        private void OnItemSpawned()
        {
            navMeshSurface.BuildNavMesh();
#if UNITY_EDITOR
            Debug.Log($"Mesh is rebuilt");
#endif
        }
    }
}

