using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI")]


    public TMP_Text goldUI;

    public GameObject jailUI, jailUIText;

    public Animator jailAnim;

    public float jailUIMidTime, jailUIEndTime;

    public string jailAnimationName = "Fade";




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void JailUIStart()
    {
        jailUI.SetActive(true);
        jailUIText.SetActive(true);

        jailAnim.Play(jailAnimationName);

        Invoke("JailUIMid", jailUIMidTime);
        Invoke("JailUIEnd", jailUIEndTime);
    }

    public void JailUIMid()
    {
        jailUIText.SetActive(false);
    }

    public void JailUIEnd()
    {
        jailUI.SetActive(false);
    }

}
