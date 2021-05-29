using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] PathFinder pathfinder;
    List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 0.2f;
    [SerializeField] ParticleSystem destruction;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<PathFinder>();
        path = pathfinder.GetPath();
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
        DestinationDestruction();
    }

    private void DestinationDestruction()
    {
        ParticleSystem vfx = Instantiate(destruction, new Vector3(transform.position.x + 10, transform.position.y + 10, transform.position.z), Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, 2f);
        Destroy(gameObject);
    }
}