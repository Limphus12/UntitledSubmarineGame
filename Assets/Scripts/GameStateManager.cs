using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { PLAY, PAUSE }

    private GameState currentState;

    public void SwitchState(GameState newState)
    {
        if (newState == currentState) return;

        currentState = newState;
    }

    public GameState GetGameState() => currentState;
}
