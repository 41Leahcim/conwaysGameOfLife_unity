using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour{
    [SerializeField] private TMPro.TMP_Text playText;
    [SerializeField] private TMPro.TMP_Text valueText;
    [SerializeField] private CellManager manager;
    private static bool playing = false;
    private Coroutine updater;
    private float delay = 1.0f;
    
    IEnumerator PlayMode(){
        while(true){
            // Updathe the cells every delay seconds
            manager.UpdateCells();
            yield return new WaitForSeconds(delay);
        }
    }

    public void TogglePlayMode(){
        if(playing){
            // Stop updating the cells
            StopCoroutine(updater);

            // Turn the Pause button into a Play button
            playText.SetText("Play");
        }else{
            // Start updating the cells
            updater = StartCoroutine("PlayMode");

            // Turn the Play button into a pause button
            playText.SetText("Pause");
        }

        // Toggle the play mode
        playing = !playing;
    }

    public void SetDelay(float newDelay){
        // Set the delay to the new delay value
        delay = newDelay;

        // Set the delay text to the new delay
        valueText.SetText(delay.ToString());
    }
}
