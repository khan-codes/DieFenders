using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]     // The obkect to which the script is attached becones the selection base, 
// the whole object will be selected next time you click on it instead on a child of its.
[RequireComponent (typeof(Waypoint))]
public class CubeEditor: MonoBehaviour
{
    Waypoint waypoint;
    int gridSize;           // will come from waypoint.cs script
    Vector2Int gridPos;     // will come from waypoint.cs script
    string labelText;       // will come from waypoint.cs script
    TextMesh text;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();
    }

    void Start()
    {
        // Wow, such empty!
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        gridPos = waypoint.GetGridPos();
        transform.position = new Vector3(gridPos.x * 10, 0f, gridPos.y * 10);
    }
    
    private void UpdateLabel()
    {
        text = GetComponentInChildren<TextMesh>();
        labelText = waypoint.GetWaypointLabel();
        text.text = labelText;
        this.gameObject.name = labelText;
    }
}
