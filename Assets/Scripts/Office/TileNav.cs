using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNav : MonoBehaviour
{
    //The tile system to avoid collisions with. Walkable spaces must not contain any tiles.
    public GameObject navigableTileSystemInstance;

    //An object we want to go to.
    public GameObject navTargetInstance;

    //The size of a tile in Unity units.
    public float tileSize = 1;

    //How many seconds it takes to move one tile
    public float secondsPerStep = 0.5f;

    //List of steps to take.
    private List<Vector3> directions;

    //Needs to be set to true to start moving.
    private bool needsToMove = false;

    //Internal variables that have to be preserved between frames
    private int stepIndex = 0;
    private float stepElapsedTime = 0;

    //This is a somewhat crappy version of the A* algorithm designed to
    //avoid making an actual graph representation. It's a little slower.
    //WARNING: START AND TARGET MUST BOTH BE AT THE EXACT CENTER OF THEIR RESPECTIVE TILE!!!
    public List<Vector3> AStarToPos(Vector3 targetLoc)
    {
        PriorityQueue<Vector3> frontier = new PriorityQueue<Vector3>();
        
        Vector3 currentLoc = this.transform.position;

        frontier.Add(currentLoc, 0);

        Dictionary<Vector3, Vector3> prevLoc = new Dictionary<Vector3, Vector3>();
        prevLoc[currentLoc] = default;
        Dictionary<Vector3, float> costToReach = new Dictionary<Vector3, float>();
        costToReach[currentLoc] = 0;

        while(frontier.Count != 0)
        {
            currentLoc = frontier.Pop();

            if(currentLoc == targetLoc)
            {
                break;
            }

            //A list of neighbouring tile positions.
            Vector3[] neighbours = {currentLoc + Vector3.right * tileSize,
                currentLoc + Vector3.down * tileSize,
                currentLoc + Vector3.left * tileSize,
                currentLoc + Vector3.up * tileSize};

            foreach(Vector3 next in neighbours)
            {
                float newCost = costToReach[currentLoc] + 1;
                if (navigableTileSystemInstance.GetComponent<Tilemap>()
                    .GetTile(navigableTileSystemInstance.GetComponent<Grid>().WorldToCell(next)) == null
                    && (!costToReach.ContainsKey(next) || newCost < costToReach[next] ))
                {
                    costToReach[next] = newCost;
                    float priority = costToReach[next] + (targetLoc - next).magnitude;
                    frontier.Add(next, priority);
                    prevLoc[next] = currentLoc;
                }
            }
        }

        List<Vector3> instructions = new List<Vector3>();
        //Trace back the path to find a list of position changes.
        while (true)
        {
            if(prevLoc[currentLoc] == default)
            {
                break;
            }
            instructions.Add(currentLoc - prevLoc[currentLoc]);
            currentLoc = prevLoc[currentLoc];
        }

        instructions.Reverse();
        return instructions;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetLoc = navTargetInstance.transform.position;
        directions = AStarToPos(targetLoc);
        needsToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (needsToMove) // Smooth movement
        {
            if(stepElapsedTime >= secondsPerStep) //step completion
            {
                stepElapsedTime = 0;
                stepIndex++;

                //we are doing a lot of division and floating point errors as well as timing issues
                //do add up. By forcibly compensating every step, the snapping can be almost invisible.
                transform.position = navigableTileSystemInstance.GetComponent<Grid>()
                    .CellToWorld(navigableTileSystemInstance.GetComponent<Grid>().WorldToCell(transform.position)) 
                    + new Vector3(tileSize / 2, tileSize / 2); 
            }
            if (stepIndex >= directions.Count) //movement completion
            {
                stepIndex = 0;
                needsToMove = false;
            }
            else
            {
                transform.position += directions[stepIndex] * (Time.deltaTime / secondsPerStep);
                stepElapsedTime += Time.deltaTime;
            }
        }
    }
}
