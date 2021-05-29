using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawn = 1f;
    [SerializeField] Text playerScore;
    Vector3Int spawnPosition = new Vector3Int (10, 0, 30);
    int arrayPosition = 0;
    int numberOfEnemySpawned = 0;
    
    [SerializeField ]GameObject enemy;
    [SerializeField] AudioClip spawnSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnEnemies());
        playerScore.text = numberOfEnemySpawned.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            InstantiateEnemy();
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    }

    void InstantiateEnemy()
    {
        GameObject fx = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        fx.name = "spawnedEnemy";
        fx.transform.parent = this.transform;       // when setting parent make sure that the transform of the parent is set to 0, 0, 0. Because if not the new childern will be placed on a wrong position
        numberOfEnemySpawned++;
        playerScore.text = numberOfEnemySpawned.ToString();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}
