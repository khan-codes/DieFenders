using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> waypointList = new List<Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Waypoint> GetPath()
    {
        if (waypointList.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CalculatePath();
        }
        return waypointList;
    }

    void CalculatePath()
    {
        waypointList.Add(endWaypoint);
        endWaypoint.isPlaceable = false;

        Waypoint wp = waypointList[0];

        while(wp != null && wp.exploredFrom != null)
        {
            waypointList.Add(wp.exploredFrom);
            wp.isPlaceable = false;
            wp = wp.exploredFrom;
        }

        startWaypoint.isPlaceable = false;
        waypointList.Reverse();
    }

    void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            //print("Searching from: " + searchCenter.name);
            HaltIfEnd();
            if (!isRunning)
            {
                //print("Calling break");
                break;
            }
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEnd()
    {
        if (searchCenter == endWaypoint)
        {
            //print("The endpoint has been found. Execution stopped.");       // TODO remove log
            isRunning = false;
        }
    }

    void ExploreNeighbours()
    {
        foreach(Vector2Int direction in directions)
        {
            Vector2Int temp = searchCenter.GetGridPos() + direction;
            QueueNewNeigbhours(temp);
        }
    }

    void QueueNewNeigbhours(Vector2Int temp)
    {
        Waypoint wp;
        grid.TryGetValue(temp, out wp);     // result = grid[temp];   But would have used try and catch with it

        if (wp != null )
        {
            if (wp.isExplored || queue.Contains(wp))
            {
                //print("Not exploring as its already done.");
            }
            else
            {
                queue.Enqueue(wp);      // Adding the wp (which is a neighbour of from) into the dictionary
                wp.exploredFrom = searchCenter;
                //print("Queueing " + wp.name);
            }   
        }

        else
        {
            //print("No block found at " + temp);
        }
    }

    void LoadBlocks()       // Add waypoints in the dictionary
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();   // FindObjectsOfType is responisble for finding objects of a certain type
        // from all over the scene, and not only from the object to which this script (pathfinder) or belongs or its children belong.

        foreach (Waypoint wp_i in waypoints)
        {
            // check for overlapping blocks
            bool isOverlapping = grid.ContainsKey(wp_i.GetGridPos());

            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping at: " + wp_i.name);
            }

            else
            {
                // add to dictionary
                grid.Add(wp_i.GetGridPos(), wp_i);
            }
        }
    }
}
