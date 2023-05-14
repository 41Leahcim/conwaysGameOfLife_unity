using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CellManager : MonoBehaviour{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Vector3 margin = new Vector3(0.02f, 0.02f, 0.0f);
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float leftX;
    [SerializeField] private float topY;
    private Cell[,] cells;

    // Start is called before the first frame update
    void Start(){
        // Create a new array for the cells
        cells = new Cell[height, width];

        // Create the cells
        for(int y = 0;y < height;y++){
            for(int x = 0;x < width;x++){
                Vector2 blockScale = cellPrefab.transform.localScale + margin;
                Cell cell = Instantiate(cellPrefab, new Vector3(leftX + x * blockScale.x, topY - y * blockScale.y, 0), cellPrefab.transform.rotation);
                cell.transform.SetParent(transform);
                cells[y,x] = cell.GetComponent<Cell>();
            }
        }
    }

    int countNeighbors(int x, int y){
        // Determine the limits for the loops
        int minX = x > 0? x - 1 : 0;
        int maxX = x < width - 1? x + 1 : x;
        int minY = y > 0? y - 1 : 0;
        int maxY = y < height - 1? y + 1 : y;

        // Count the number of neighbors
        int neighbors = 0;
        for(int y2 = minY;y2 <= maxY;y2++){
            for(int x2 = minX;x2 <= maxX;x2++){
                if((x2 != x || y2 != y) && cells[y2,x2].IsAlive){
                    neighbors++;
                }
            }
        }
        return neighbors;
    }

    public void UpdateCells(){
        // Create an array for the number of neighbors
        int[,] neighbors = new int[height, width];

        // Count the number of neighbors for every cell
        for(int y = 0;y < height;y++){
            for(int x = 0;x < width;x++){
                neighbors[y,x] = countNeighbors(x, y);
            }
        }

        // update the cells
        for(int y = 0;y < height;y++){
            for(int x = 0;x < width;x++){
                cells[y,x].SetNeighbors(neighbors[y,x]);
            }
        }
    }

    public void StepBack(){
        // Step back all cells on the board
        foreach(Cell cell in cells){
            cell.StepBack();
        }
    }

    public void ClearBoard(){
        // Kill every cell and clear the history of every cell
        foreach(Cell cell in cells){
            cell.SetNeighbors(0);
            cell.ClearHistory();
        }
    }

    public void ClearHistory(){
        // Clear the history of every cell
        foreach(Cell cell in cells){
            cell.ClearHistory();
        }
    }

    // Close the application
    void OnExit(InputValue value) => Application.Quit(0);
}
