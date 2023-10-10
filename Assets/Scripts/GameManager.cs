using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Environment Stuff")]
    [SerializeField] private TitleStruct[] titleStructs;
    [SerializeField] private Gradient fogGradient;

    [Header("UI Stuff")]
    [SerializeField] private bool Ingame;
    [SerializeField] private TextMeshProUGUI Depthinfo, Depthinfo1, titleChange;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreCounterSubmarine, scoreCounterPlayer;

    [Header("Player Stuff")]
    [SerializeField] private GameObject submarine;
    [SerializeField] private GameObject subhud, subcamerarig, player;
    [Space, SerializeField] private GameObject playerSpawnpoint;
    [Space, SerializeField] private SubmarineController SubController;

    [Header("Player - Submarine Switching")]
    [SerializeField] private float switchingDistance = 25f;


    private float depths;
    private float depthp;


    


    public static GameObject Submarine;

    public static int Points;

    public static void ResetPoints() => Points = 0;

    public static void ModifyPoints(int amount) => Points += amount;




    void Start()
    {
        Submarine = submarine;
    }

    void Update()
    {
        if (Ingame) InGame();

        else
        {

        }
    }

    void InGame()
    {
        scoreCounterPlayer.text = "Score: " + Points;
        scoreCounterSubmarine.text = "Score: " + Points;

        depths = Mathf.Abs(submarine.transform.position.y);

        int depthInt = Mathf.RoundToInt(depths);
        Depthinfo.text = "Depth: " + depthInt;

        depthp = Mathf.Abs(player.transform.position.y);
        int depthInt1 = Mathf.RoundToInt(depthp);
        Depthinfo1.text = "Depth: " + depthInt1;

        if (Input.GetKeyDown(KeyCode.F)) Switch();

        //if (depths < 10) LoadIntoHangar();

        float y;

        if (inSubmarine) y = depths;

        else y = depthp;

        float normalizedY = Mathf.InverseLerp(0, 10000, y);

        Color targetColor = fogGradient.Evaluate(normalizedY);

        RenderSettings.fogColor = targetColor;
        //RenderSettings.skybox.SetColor("_Tint", targetColor);
        foreach (TitleStruct titleStruct in titleStructs)
        {
            if (y >= titleStruct.depth)
            {
                titleChange.text = titleStruct.text;
            }
        }
    }

    public static void LoadIntoHangar()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public static void LoadIntoGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    bool inSubmarine = true;

    private void Switch()
    {
        //add a distance check here beforehand
        float distance = Vector3.Distance(player.transform.position, submarine.transform.position);

        if (distance < switchingDistance && inSubmarine)
        {
            
        }

        inSubmarine = !inSubmarine;
        subhud.SetActive(inSubmarine); subcamerarig.SetActive(inSubmarine); player.SetActive(!inSubmarine);

        if (!inSubmarine) player.transform.position = playerSpawnpoint.transform.position;

        SubController.enabled = inSubmarine;
    }
}

[System.Serializable]
struct TitleStruct
{
    public float depth;
    public string text;
}
