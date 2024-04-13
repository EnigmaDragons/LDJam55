using System;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{

    [SerializeField]
    private float defaultTimeInSeconds = default;

    [SerializeField]
    private TextMeshProUGUI clockText;


    private void Start(){
        //Set clock to default time
        CurrentGameState.UpdateState(state => state.currentGameTime = defaultTimeInSeconds);
    }

    private void Update(){
        //decrement clock
        CurrentGameState.UpdateState(state => state.currentGameTime -= Time.deltaTime);
        clockText.text = string.Format("{0}:{1:00}", (int)CurrentGameState.GameState.currentGameTime / 60, (int)CurrentGameState.GameState.currentGameTime % 60);
        
        //check if 0, lose
        if(CurrentGameState.GameState.currentGameTime <= 0){
            Message.Publish(new GameOver());
        }
    }
}