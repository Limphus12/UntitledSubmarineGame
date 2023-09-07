using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { PLAY, PAUSE }

    private static GameState currentState = GameState.PLAY;

    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

    public class OnGameStateChangedEventArgs : EventArgs { public GameState i; }

    public void SwitchState(GameState newState)
    {
        if (newState == currentState) return;

        currentState = newState;

        //firing off our event here
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs { i = currentState });
    }

    public GameState GetGameState() => currentState;

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case GameState.PLAY:

                    SwitchState(GameState.PAUSE);

                    break;

                case GameState.PAUSE:

                    SwitchState(GameState.PLAY);

                    break;
            }
        }
    }
}
