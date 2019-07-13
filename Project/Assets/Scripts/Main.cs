using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    
    public Animator stateMachine;

    public GameObject Sequence;
    public GameObject Game;

    public GameObject Title;
    public GameObject Ending;
    public GameObject About;
    public Camera camera;

    // Buttons
    [Header("Buttons")]
    public Button StartBtn;

    public Button AboutBtn;
    public Button BackBtn;

    public static Main main = null;

    void Awake()
    {
        if (main == null)
            main = this;

        StartBtn.onClick.AddListener(StartGame);
        AboutBtn.onClick.AddListener(ToAbout);
        BackBtn.onClick.AddListener(BackTitle);
    }
    void StartGame()
    {
        Main.main.stateMachine.SetTrigger("StartGame");
    }

    void ToAbout()
    {
        Main.main.stateMachine.SetTrigger("About");
    }

    void BackTitle()
    {
        Main.main.stateMachine.SetTrigger("BackToTittle");
    }

}
