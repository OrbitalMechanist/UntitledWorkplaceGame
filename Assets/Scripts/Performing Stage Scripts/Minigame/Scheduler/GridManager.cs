using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class GridManager : MonoBehaviour
{
    public GameObject rowPrefab;

    /** Camera of the UI. */
    public Camera uiCamera;

    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private GameObject resultPopUp;

    [SerializeField]
    private GameObject meetingBlockHolder;

    private GameObject[] meetingTilePrefabs;

    private bool gameIsActive = false;

    private bool gameFinished = false;

    /** The total maximum height of the blocks that can be displayed in the meeting tile holder. Used to determine max meeting tiles to generate. */
    private const int MAX_GENERATED_BLOCK_HEIGHT = 8;

    /** The total maximum width of the blocks that can be displayed in the meeting tile holder. Used to determine max meeting tiles to generate. */
    private const int MAX_GENERATED_BLOCK_WIDTH = 2;

    private const int STAT_INCREASE = 5;

    private const string RESULT_HEADER_TEXT = "Hooray! Meetings Scheduled!";

    private const string BLURB_TEXT = "You were able to masterfully fit all the required meetings into everyone’s schedules, boosting the organization of the team.";

    public void Start() {
        // Load all the meeting tiles from the Assets/Resources/MeetingTiles folder
        meetingTilePrefabs = Resources.LoadAll<GameObject>("MeetingTiles");
    }

    public void Update() {
        if (gameIsActive) {
            bool tilesFilled = true;
            int childCount = grid.transform.childCount;

            for (int i = 5; i < childCount; i++) {
                Tile tile = grid.transform.GetChild(i).GetComponent<Tile>();
                if (tile.isEnabled() == true) {
                    tilesFilled &= tile.getFillStatus();
                }
            }
            if (tilesFilled && !gameFinished) {
                gameFinished = true;
                resultPopUp.SetActive(true);
                resultPopUp.GetComponent<DisplayMinigameResults>().BoostEmployeeWorkEthic(STAT_INCREASE, RESULT_HEADER_TEXT, BLURB_TEXT);
            }
        }
    }

    public void CreateCalendarPuzzle() {
        // Clear previous grid and blocks
        ResetGridAndBlocks();
        
        // Generate meeting blocks
        List<int[,]> tileLayoutList = GenerateMeetingBlocks();

        // Generate new grid
        GenerateGridUI(GenerateGridLayout(tileLayoutList));

        gameIsActive = true;
    }

    public List<int[,]> GenerateMeetingBlocks() {
        List<int[,]> tileLayoutList = new List<int[,]>();
        
        int blockCount = 1;
        int rowTotalBlockHeight = 0;
        int usedBlockHeight = 0;
        int usedBlockWidth = 0;

        // Instantiate initial row
        GameObject currentRow = Instantiate(rowPrefab);
        currentRow.transform.SetParent(meetingBlockHolder.transform, false);

        // Generate blocks
        while (usedBlockHeight < MAX_GENERATED_BLOCK_HEIGHT) {
            // Get a random meeting tile from the meeting tile prefabs folder
            int randIndex = Random.Range(0, meetingTilePrefabs.Length - 1);
            var newMeetingBlock = Instantiate(meetingTilePrefabs[randIndex]);

            // Create new row if block width is filled
            if (usedBlockWidth >= MAX_GENERATED_BLOCK_WIDTH || usedBlockWidth + newMeetingBlock.GetComponent<MeetingBlock>().tileWidth > MAX_GENERATED_BLOCK_WIDTH) {
                currentRow = Instantiate(rowPrefab);
                currentRow.transform.SetParent(meetingBlockHolder.transform, false);
                usedBlockWidth = 0;

                usedBlockHeight += Mathf.RoundToInt((float)rowTotalBlockHeight / (float)MAX_GENERATED_BLOCK_WIDTH);
            }
            newMeetingBlock.name = "Block" + blockCount;
            newMeetingBlock.GetComponent<MeetingBlock>().uiCamera = uiCamera;
            
            // Assign block row to the meeting block holder
            newMeetingBlock.transform.SetParent(currentRow.transform, false);

            // Add tile layout 2D array representation to the list
            tileLayoutList.Add(newMeetingBlock.GetComponent<MeetingBlock>().getTileLayout());
            
            // Increment counters
            blockCount++;
            rowTotalBlockHeight += newMeetingBlock.GetComponent<MeetingBlock>().tileHeight;
            usedBlockWidth += newMeetingBlock.GetComponent<MeetingBlock>().tileWidth;
        }

        return tileLayoutList;
    }

    private int[,] GenerateGridLayout(List<int[,]> tileLayoutList) {
        int[,] gridLayout = new int[height, width];

        // Fill grid with default values
        for (int y = 0; y < gridLayout.GetLength(0); y++) {
            for (int x = 0; x < gridLayout.GetLength(1); x++) {
                gridLayout[y, x] = 0;
            }
        }

        for (int i = 0; i < tileLayoutList.Count; i++) {
            // Get random (x,y) coords to place tile onto grid
            int randXIndex = Random.Range(0, width - 1);
            int randYIndex = Random.Range(0, height - 1);

            // Verify tile can be placed onto grid and is not already occupied
            while (!VerifyOpenTilePlacement(gridLayout, tileLayoutList[i], ref randXIndex, ref randYIndex)) {
                // Roll again if spot is already taken
                randXIndex = Random.Range(0, width - 1);
                randYIndex = Random.Range(0, height - 1);
            }

            // Place tile onto 2D array grid
            for (int y = 0; y < tileLayoutList[i].GetLength(0); y++) {
                for (int x = 0; x < tileLayoutList[i].GetLength(1); x++) {
                    gridLayout[randYIndex + y, randXIndex + x] = tileLayoutList[i][y, x];
                }
            }
        }
        return gridLayout;
    }

    public bool VerifyOpenTilePlacement(int[,] gridLayout, int[,] tile, ref int xIndex, ref int yIndex) {
        bool isOpen = true;

        // Adjust starting coordinates to make sure the tile can fit onto the grid
        while (xIndex + tile.GetLength(1) > width) {
            xIndex--;
        }
        while (yIndex + tile.GetLength(0) > height) {
            yIndex--;
        }

        // Check if the area covered by the tile to insert is open
        for (int y = 0; y < tile.GetLength(0); y++) {
            for (int x = 0; x < tile.GetLength(1); x++) {
                if (gridLayout[yIndex + y, xIndex + x] == tile[y, x]) {
                    isOpen = false;

                    return isOpen;
                }
            }
        }

        return isOpen;
    }

    public void GenerateGridUI(int[,] gridLayout) {
        // Generate cells for the grid, starting enabled by default
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                Tile newTile = Instantiate(tilePrefab);
                newTile.name = "Tile " + x + " " + y;
                newTile.x = x;
                newTile.y = y;

                // Disable tile if the grid layout specifies it
                if (gridLayout[y, x] == 0) {
                    newTile.disableTile();
                } 
                
                // Attach tile to the grid
                newTile.transform.SetParent(grid.transform, false);
            }
        }
    }

    public void ResetGridAndBlocks() {
        // Reset calendar grid
        int cells = grid.transform.childCount;

        // Skip the first 5 elements, as those are the day column labels
        for (int i = 5; i < cells; i++) {
            GameObject.Destroy(grid.transform.GetChild(i).gameObject);
        }

        // Reset blocks
        int blockRows = meetingBlockHolder.transform.childCount;
        for (int i = 0; i < blockRows; i++) {
            GameObject.Destroy(meetingBlockHolder.transform.GetChild(i).gameObject);
        }

        gameIsActive = false;
        gameFinished = false;
    }
}
