using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour{
    [SerializeField] private Color deadColor = new Color(0.2f, 0.2f, 0.2f);
    [SerializeField] private Color aliveColor = new Color(1, 1, 0);
    private SpriteRenderer cellRenderer;
    private bool wasAlive = false;
    private bool isAlive = false;

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

    // Sets the number neighbors
    public void SetNeighbors(int neighbors){
        // The cell should only be alive if:
        // The cell has 3 neighbors (it doesn't matter whether the cell is alive)
        // The cell is alive and has 2 neighbors
        IsAlive = neighbors == 3 || (isAlive && neighbors == 2);
    }

    void UpdateColor(){
        // Set the correct color for whether it is alive or dead
        if(!wasAlive && isAlive){
            cellRenderer.color = aliveColor;
        }else if(wasAlive && !isAlive){
            cellRenderer.color = deadColor;
        }
    }

    public void ToggleAlive(){
        // Toggle the cell between dead and alive
        IsAlive = !IsAlive;
    }

    // Start is called before the first frame update
    void Start(){
        // Get the renderer, for setting the color
        cellRenderer = GetComponent<SpriteRenderer>();

        // Set the color to the dead color, as the cell is dead
        cellRenderer.color = deadColor;
    }
}
