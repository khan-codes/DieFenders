using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float thresholdDistance = 25f;
    [SerializeField] GameObject shootingVFX;

    // states
    [SerializeField] public List<GameObject> enemies;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceEnemy();
        FireAtEnemy();
        SetTargetEnemy();
    }

    void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;         // assume the first one is the smallest

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy);
        }

        targetEnemy = closestEnemy;
    }

    Transform GetClosest(Transform closestEnemy, EnemyDamage testEnemy)
    {
        float distanceFromClosestEnemy = Vector3.Distance(closestEnemy.position, gameObject.transform.position);
        float distanceFromTestEnemy = Vector3.Distance(testEnemy.transform.position, gameObject.transform.position);

        if(distanceFromClosestEnemy < distanceFromTestEnemy)
        {
            return closestEnemy;
        }

        else
        {
            return testEnemy.transform;
        }
    }

    void FaceEnemy()
    {
        objectToPan.LookAt(targetEnemy);
    }

    void FireAtEnemy()
    {
        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
            if (distance <= thresholdDistance)
            {
                Shoot(true);
            }

            else
            {
                Shoot(false);
            }
        }
        else
        {
            Shoot(false);
        }
    }

    void Shoot(bool isActive)
    {
        shootingVFX = transform.Find("Tower_A_Top").Find("bullets").gameObject;
        ParticleSystem.EmissionModule emissionModule = shootingVFX.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
