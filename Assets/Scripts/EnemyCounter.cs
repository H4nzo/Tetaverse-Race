using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public GameObject enemySpawner;
    [SerializeField] int EnemyIndex;
    GameObject[] enemies;
    public Transform EnemyContainer;

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length >= EnemyIndex)
        {
            enemySpawner.SetActive(false);
            foreach (var e in enemies)
            {
                e.transform.SetParent(EnemyContainer);
            }
        }


    }
}
