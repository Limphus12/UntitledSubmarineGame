using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using com.limphus.utilities;
using System;

public class UIManager : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private InteractionScript interactionScript;

    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject interactionUI;

    [Header("Controls UI")]
    [SerializeField] private GameObject fKeyUI;
    [SerializeField] private GameObject mouseUI;


    private void Start()
    {
        //make sure we have our script references
        if (!gameStateManager) gameStateManager = FindObjectOfType<GameStateManager>();
        if (!interactionScript) interactionScript = FindObjectOfType<InteractionScript>();

        //and subscribe to the events!
        gameStateManager.OnGameStateChanged += CheckGameState;
        interactionScript.OnInteractionCheck += CheckInteractionUI;
    }

    private void OnEnable()
    {
        //make sure we have our script references
        if (!gameStateManager) gameStateManager = FindObjectOfType<GameStateManager>();
        if (!interactionScript) interactionScript = FindObjectOfType<InteractionScript>();

        //and subscribe to the events!
        gameStateManager.OnGameStateChanged += CheckGameState;
        interactionScript.OnInteractionCheck += CheckInteractionUI;
    }

    private void OnDisable()
    {
        //make sure we have our script references
        if (!gameStateManager) gameStateManager = FindObjectOfType<GameStateManager>();
        if (!interactionScript) interactionScript = FindObjectOfType<InteractionScript>();

        //unsubscribe from the events!
        gameStateManager.OnGameStateChanged -= CheckGameState;
        interactionScript.OnInteractionCheck -= CheckInteractionUI;
    }

    private void OnDestroy()
    {
        //make sure we have our script references
        if (!gameStateManager) gameStateManager = FindObjectOfType<GameStateManager>();
        if (!interactionScript) interactionScript = FindObjectOfType<InteractionScript>();

        //unsubscribe from the events!
        gameStateManager.OnGameStateChanged -= CheckGameState;
        interactionScript.OnInteractionCheck -= CheckInteractionUI;
    }

    private void CheckInteractionUI(object sender, InteractionScript.OnInteractEventArgs e)
    {
        if (!interactionUI) return;

        interactionUI.SetActive(e.i);

        if (e.j == null)
        {
            //if we have no interactable, turn off this ui
            fKeyUI.SetActive(e.i);
            mouseUI.SetActive(e.i);

            return;
        }

        if (!fKeyUI || !mouseUI) return;

        Door door = e.j.GetComponent<Door>();

        if (door)
        {
            fKeyUI.SetActive(e.i);
        }

        Button button = e.j.GetComponent<Button>();

        if (button)
        {
            fKeyUI.SetActive(e.i);
        }

        PickupObject pickupObject = e.j.GetComponent<PickupObject>();

        if (pickupObject)
        {
            mouseUI.SetActive(e.i);
        }
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
