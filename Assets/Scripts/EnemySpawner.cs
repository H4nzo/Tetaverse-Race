using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Hanzo.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject[] spawnPoint;
        public Transform enemyContainer;

        // public int maxEnemies = 5;
        // private int numEnemiesSpawned = 0;

        [SerializeField]
        private float timer, timeToSpawn;
       



        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeToSpawn)
            {
                timer = 0;
                GameObject myEnemy = PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity) as GameObject;
                StartCoroutine(WaitForEnemySync(enemyPrefab));

            }
        }


        IEnumerator WaitForEnemySync(GameObject enemy)
        {
            enemy.transform.parent = enemyContainer;
            yield return new WaitForSeconds(0.1f); // Wait for a short period of time
            while (!enemy.GetComponent<PhotonView>().IsMine)
            {
                yield return null; // Continue waiting
            }
            enemy.SetActive(true); // Enable the enemy GameObject
        }
    }

}