using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    [SerializeField]
    private Tile tilePrefab;

    [SerializeField]
    private GameObject grid;

    private const int MAX_DISABLED_CELLS = 29;

    public void CreateCalendarPuzzle() {
        // Clear previous grid
        ResetGrid();

        // Generate new grid
        GenerateGrid();
    }

    public void GenerateGrid() {
        int currentDisabledCells = 0;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile newTile = Instantiate(tilePrefab);
                newTile.name = "Tile " + x + " " + y;

                float randValue = Random.value;
                int remainingTiles = (width * height) - (x * y + 1);

                // Randomly determine disabled tiles
                if (randValue < 0.5f || remainingTiles < MAX_DISABLED_CELLS) {
                    newTile.enabled = false;
                    newTile.gameObject.GetComponent<Image>().color = Color.grey;
                }
                
                newTile.transform.SetParent(grid.transform, false);
            }
        }
    }

    public void ResetGrid() {
        int cells = grid.transform.childCount;

        // Skip the first 5 elements, as those are the day column labels
        for (int i = 5; i < cells; i++) {
            GameObject.Destroy(grid.transform.GetChild(i).gameObject);
        }
    }
}
