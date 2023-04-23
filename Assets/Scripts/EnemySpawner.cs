using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanzo.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemy;
        public GameObject[] spawnPoint;
        public Transform enemyContainer;

        [SerializeField]
        private float timer, timeToSpawn;

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeToSpawn)
            {
                timer = 0;
                GameObject myEnemy = Instantiate(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity) as GameObject;
                myEnemy.transform.parent = enemyContainer;
            }
        }
    }

}