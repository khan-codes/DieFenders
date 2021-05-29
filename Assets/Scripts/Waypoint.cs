using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //[SerializeField] [Range(1f, 20f)] const float gridSize = 10f;
    const int gridSize = 10;
    Vector2Int gridPos;
    string label;
    public bool isExplored = false;
    public Waypoint exploredFrom;
    [SerializeField] Color color = Color.black;
    [SerializeField] public bool isPlaceable = true;
    [SerializeField] public bool isTower = false;
    [SerializeField] TowerFactory TF_script;


    // Start is called before the first frame update
    void Start()
    {
        TF_script = FindObjectOfType<TowerFactory>();   
    }

    // Update is called once per frame
    void Update()
    {
        //OnMouseOver();
    }

    public Vector2Int GetGridPos()
    {
        gridPos.x = Mathf.RoundToInt(transform.position.x / gridSize);
        gridPos.y = Mathf.RoundToInt(transform.position.z / gridSize);

        return gridPos;
    }

    public string GetWaypointLabel()
    {
        label = (gridPos.x).ToString() + "," + (gridPos.y).ToString(); // dividing by ten to keep the coordinates 
        // from being just the multiples of 10.
        return label;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.isPlaceable && !isTower)
            {
                //Debug.Log(gameObject.name);
                TF_script.AddTower(this);
                isTower = true;
            }

            else
            {
                print("Not placeable/Tower already present at " + this.name);
            }
        }
    }
}
