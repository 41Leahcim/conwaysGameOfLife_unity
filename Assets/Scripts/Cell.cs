using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour{
    [SerializeField] private Color deadColor = new Color(0.5f, 0.5f, 0.5f);
    [SerializeField] private Color aliveColor = new Color(1, 1, 0);
    [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f);
    private SpriteRenderer cellRenderer;
    private bool[] history = new bool[10];
    private bool wasAlive = false;
    private bool isAlive = false;
    private int generation = 0;

    public bool IsAlive{
        // Returns whether the cell is alive
        get{ return isAlive; }

        // The cell should only be killed or revive by itself, using toggle, or by changing the number of neighbors
        private set{
            // Store whether the cell was alive in the previous iteration
            wasAlive = isAlive;

            // Set isAlive to the new value
            isAlive = value;

            // update the color of the cell
            UpdateColor();
        }
    }

    // Start is called before the first frame update
    void Start(){
        // Get the renderer, for setting the color
        cellRenderer = GetComponent<SpriteRenderer>();

        // Set the color to the dead color, as the cell is dead
        cellRenderer.color = deadColor;
    }

    void UpdateColor(){
        // Set the correct color for whether it is alive or dead
        if(!wasAlive && isAlive){
            cellRenderer.color = aliveColor;
        }else if(wasAlive && !isAlive){
            cellRenderer.color = deadColor;
        }
    }

    void UpdateHistory(){
        // If we had more generations than fit in the history:
        if(generation >= history.Length){
            // Shift the history back
            for(int i = 0;i < history.Length - 1;i++){
                history[i] = history[i + 1];
            }

            // Put the current value at the end
            history[history.Length - 1] = isAlive;
        }else{
            // Otherwise, add the current value at the current point of history
            history[generation++] = isAlive;
        }
    }

    // Sets the number neighbors
    public void SetNeighbors(int neighbors){
        // Add the current value to the history
        UpdateHistory();

        // The cell should only be alive if:
        // The cell has 3 neighbors (it doesn't matter whether the cell was alive)
        // The cell is alive and has 2 neighbors
        IsAlive = neighbors == 3 || (isAlive && neighbors == 2);
    }
    
    // Step back one generation
    public void StepBack(){
        // Revert to the previous generation, if there is any
        if(generation > 0){
            IsAlive = history[--generation];
        }
    }

    // Clear the history
    public void ClearHistory() => generation = 0;

    // Toggle the cell between dead and alive, when the cell is clicked
    void OnMouseDown() => IsAlive = !IsAlive;

    // Set the color to the hover color
    void OnMouseEnter() => cellRenderer.color = hoverColor;

    // Set the correct color for whether it is alive or dead
    void OnMouseExit() => cellRenderer.color = isAlive? aliveColor : deadColor;
}
