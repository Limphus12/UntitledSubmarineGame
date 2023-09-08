using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameStateManager gameStateManager;

    [SerializeField] private GameObject menuButton;

    private void Start()
    {
        //make sure we have our game state manager
        if (!gameStateManager) gameStateManager = FindObjectOfType<GameStateManager>();

        //and subscribe to the events!
        gameStateManager.OnGameStateChanged += CheckGameState;
    }

    private void CheckGameState(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (!menuButton) return;

        switch (e.i)
        {
            case GameStateManager.GameState.PLAY:

                menuButton.SetActive(false);

                break;
            case GameStateManager.GameState.PAUSE:

                menuButton.SetActive(true);

                break;

            default:
                break;
        }
    }
}
