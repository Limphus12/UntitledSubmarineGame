using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Color fogColor;
    public Gradient fogGradient;
    public bool Ingame;
    public TextMeshProUGUI Depthinfo;
    public TextMeshProUGUI Depthinfo1;
    public TextMeshProUGUI titleChange;
    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.fogColor = fogColor;
        Submarine = submarine;
    }

    public GameObject submarine;
    public GameObject subhud;
    public GameObject subcamerarig;
    public GameObject player;
    public static GameObject Submarine;
    public GameObject playerSpawnpoint;
    public SubmarineController SubController;
    float depths;
    float depthp;
    [Header("Titles")]
        [SerializeField] private TitleStruct[] titleStructs;
    // Update is called once per frame
    void Update()
    {

        if (Ingame)
        {
            InGame();
        }
        else
        {

        }
    }

    void InGame()
    {
        depths = Mathf.Abs(submarine.transform.position.y);

        int depthInt = Mathf.RoundToInt(depths);
        Depthinfo.text = "" + depthInt;

        depthp = Mathf.Abs(player.transform.position.y);
        int depthInt1 = Mathf.RoundToInt(depthp);
        Depthinfo1.text = "" + depthInt1;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Switch();
        }

        if (depths < 10)
        {
            SceneManager.LoadSceneAsync(0);
        }

        float y;

        if (inSubmarine)
        {
            y = depths;
        }

        else
        {
            y = depthp;
        }

        float normalizedY = Mathf.InverseLerp(0, 10000, y);

        Color targetColor = fogGradient.Evaluate(normalizedY);

        RenderSettings.fogColor = targetColor;
        RenderSettings.skybox.SetColor("_Tint", targetColor);
        foreach (TitleStruct titleStruct in titleStructs)
        {
            if (y >= titleStruct.depth)
            {
                titleChange.text = titleStruct.text;
            }
        }
    }

    public static void loadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    bool inSubmarine = true;
    private void Switch()
    {
        inSubmarine = !inSubmarine;
        //submarine.SetActive(inSubmarine);
        subhud.SetActive(inSubmarine);
        subcamerarig.SetActive(inSubmarine);
        player.SetActive(!inSubmarine);
        if (!inSubmarine)
        {
            player.transform.position = playerSpawnpoint.transform.position;
        }
        SubController.enabled = inSubmarine;
    }
}

[System.Serializable]

struct TitleStruct
{
    public float depth;
    public string text;
}
