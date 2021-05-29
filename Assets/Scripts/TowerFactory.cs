using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] public GameObject towerPrefab;
    [SerializeField] int towerLimit = 7;
    [SerializeField] Queue<Waypoint> towerCircularQueue = new Queue<Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTower(Waypoint waypoint)
    {
        if (towerCircularQueue.Count == towerLimit)
        {
            // Enqueue
            InstantiateTower(waypoint);
            towerCircularQueue.Enqueue(waypoint);       // add waypoint as the last item in the queue

            // Dequeue
            Waypoint DequeuePosition = towerCircularQueue.Dequeue();     // get the first item in the queue
            GameObject towerToDestroy = transform.Find("spawned_tower_" + DequeuePosition.transform.position.x + DequeuePosition.transform.position.z).gameObject;      // find that particular tower by name
            Destroy(towerToDestroy);    // destory the first tower in the queue
            DequeuePosition.isPlaceable = true;     // make the waypoint that had the tower "placeable"
            DequeuePosition.isTower = false;        // make the wapoint that had the tower "towerable"
        }
        else
        {
            // Enqueue
            InstantiateTower(waypoint);
            towerCircularQueue.Enqueue(waypoint);
        }
    }

    public void InstantiateTower(Waypoint waypoint)
    {
        float heightAdjustmentForTower = 5f;
        GameObject fx = Instantiate(towerPrefab, new Vector3(waypoint.transform.position.x, waypoint.transform.position.y + heightAdjustmentForTower, waypoint.transform.position.z), Quaternion.identity);
        fx.name = "spawned_tower_" + waypoint.transform.position.x.ToString() + waypoint.transform.position.z.ToString();
        fx.transform.parent = gameObject.transform;
        //print(fx.name);
    }
}
