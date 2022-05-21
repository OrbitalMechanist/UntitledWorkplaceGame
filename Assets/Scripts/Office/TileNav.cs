using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNav : MonoBehaviour
{
    //The tile system to avoid collisions with. Walkable spaces must not contain any tiles.
    public GameObject navigableTileSystemInstance;

    //The size of a tile in Unity units.
    public float tileSize = 0.2f;

    //How many seconds it takes to move one tile
    public float secondsPerStep = 0.05f;

    //A mask to only check collision on the relevant layer that contains the obstructions.
    private LayerMask obstacleMask;

    //List of steps to take.
    private List<Vector3> directions;

    //Needs to be set to true to start moving.
    private bool needsToMove = false;

    //Internal variables that have to be preserved between frames
    private int stepIndex = 0;
    private float stepElapsedTime = 0;

    //for editor testing.
    public bool drawPath = false;

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

        RaycastHit hit = new RaycastHit();

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
                float newCost = costToReach[currentLoc] + 0.1f;
                //if (navigableTileSystemInstance.GetComponent<Tilemap>()
                //    .GetTile(navigableTileSystemInstance.GetComponent<Grid>().WorldToCell(next)) == null
                if (Physics2D.OverlapCircle(next, 0.1f, obstacleMask.value) == null
                    && (!costToReach.ContainsKey(next) || newCost < costToReach[next] ))
                {
                    costToReach[next] = newCost;
                    float priority = newCost + Mathf.Abs(targetLoc.x - next.x) + Mathf.Abs(targetLoc.y - next.y);//(targetLoc - next).magnitude;
                    frontier.Add(next, priority);
                    prevLoc[next] = currentLoc;
                }
                else if (drawPath)
                {
                    Debug.DrawLine(currentLoc, next, Color.red, 5, false);
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
            if (drawPath)
            {
                Debug.DrawLine(currentLoc, prevLoc[currentLoc], Color.green, 5, false);
            }
            instructions.Add(currentLoc - prevLoc[currentLoc]);
            currentLoc = prevLoc[currentLoc];
        }

        instructions.Reverse();
        return instructions;
    }

    //START moving to position. Execution will continue without waiting for it to be done.
    public void MoveTo(Vector3 targetLoc)
    {
        directions = AStarToPos(targetLoc);
        needsToMove = true;
    }

    //I may implement this properly but for now I'm giving up on it.
    /*
    //Once you start executing this function, the next instruction will not fire until it's done.
    public IEnumerator BlockingMoveTo(Vector3 targetLoc)
    {
        directions = AStarToPos(targetLoc);
        needsToMove = true;
        int i = 0;
        while (needsToMove && i < 255)
        {
            singleFrameWait();
            i++;
        }
        yield return ;
    }
    */
    public bool moving => needsToMove;

    private void Awake()
    {
        obstacleMask = LayerMask.GetMask("Obstacle");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(navigableTileSystemInstance == null)
        {
            navigableTileSystemInstance = GameObject.FindWithTag("Nav Tile System");
        }
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
