using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject rowPrefab;

    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private GameObject meetingBlockHolder;

    private GameObject[] meetingTilePrefabs;

    private List<int[,]> tileLayoutList;

    private const int MAX_DISABLED_CELLS = 29;

    private const int MAX_GENERATED_BLOCK_HEIGHT = 8;

    private const int MAX_GENERATED_BLOCK_WIDTH = 2;

    public void Start() {
        meetingTilePrefabs = Resources.LoadAll<GameObject>("MeetingTiles");

        tileLayoutList = new List<int[,]>();
    }

    public void CreateCalendarPuzzle() {
        // Clear previous grid and blocks
        ResetGridAndBlocks();
        
        // Generate meeting blocks
        GenerateMeetingBlocks();

        // Generate new grid
        GenerateGridUI(GenerateGridLayout());
    }

    public void GenerateMeetingBlocks() {
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
            
            // Assign block row to the meeting block holder
            newMeetingBlock.transform.SetParent(currentRow.transform, false);

            // Add tile layout 2D array representation to the list
            tileLayoutList.Add(newMeetingBlock.GetComponent<MeetingBlock>().getTileLayout());

            blockCount++;
            rowTotalBlockHeight += newMeetingBlock.GetComponent<MeetingBlock>().tileHeight;
            usedBlockWidth += newMeetingBlock.GetComponent<MeetingBlock>().tileWidth;
        }
    }

    private int[,] GenerateGridLayout() {
        int[,] gridLayout = new int[width, height];

        for (int i = 0; i < tileLayoutList.Count; i++) {
            // Get random (x,y) coords to place tile onto grid
            int randXIndex = Random.Range(0, width);
            int randYIndex = Random.Range(0, height);

            // Make sure the tile can fit onto the grid
            while (randXIndex + tileLayoutList[i].GetLength(0) > width) {
                randXIndex--;
            }
            while (randYIndex + tileLayoutList[i].GetLength(1) > height) {
                randYIndex--;
            }

            // Place tile onto 2D array grid
            for (int x = 0; x < tileLayoutList[i].GetLength(0); x++) {
                for (int y = 0; y < tileLayoutList[i].GetLength(1); y++) {
                    gridLayout[randXIndex + x, randYIndex + y] = tileLayoutList[i][x, y];
                }
            }
        }

        return gridLayout;
    }

    public void GenerateGridUI(int[,] gridLayout) {
        int currentDisabledCells = 0;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile newTile = Instantiate(tilePrefab);
                newTile.name = "Tile " + x + " " + y;
                newTile.x = x;
                newTile.y = y;

                // Disable tile if the grid layout specifies it
                if (gridLayout[x, y] == 0) {
                    newTile.enabled = false;
                    newTile.gameObject.GetComponent<Image>().color = Color.grey;
                } 
                
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
    }
}
