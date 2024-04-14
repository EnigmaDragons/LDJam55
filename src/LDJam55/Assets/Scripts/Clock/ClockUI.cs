using System;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] 
    private float defaultTimeInSeconds = default;

    [SerializeField] 
    private TextMeshProUGUI clockText;

    private bool _hasLost;

    private void Start()
    {
        _hasLost = false;
        //Set clock to default time
        CurrentGameState.UpdateState(state => state.CurrentGameTime = defaultTimeInSeconds);
    }

    private void Update(){
        //decrement clock
        CurrentGameState.UpdateState(state => state.CurrentGameTime -= Time.deltaTime);
        clockText.text = string.Format("{0}:{1:00}", (int)CurrentGameState.GameState.CurrentGameTime / 60, (int)CurrentGameState.GameState.CurrentGameTime % 60);
        
        //check if 0, lose
        if(!_hasLost && CurrentGameState.GameState.CurrentGameTime <= 0)
        {
            _hasLost = true;
            Message.Publish(new GameOver());
        }
    }
}