using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject GameOver;
    public GameObject GameStart;
    public GameObject GameHelp;
    public GameObject GamePlay;
    public GameObject syy;
    public GameObject sySs;
    public GameObject musicc;
    public static GameObject sy;
    public static GameObject syS;
    public static GameObject music;
    
    public static GameObject[] con;
    // Start is called before the first frame update
    void Start()
    {
        con = new GameObject[4];
        con[0] = GameOver;
        con[1] = GameStart;
        con[2] = GameHelp;
        con[3] = GamePlay;
        sy=syy;
        syS=sySs;
        music=musicc;
        gameStart();
        playBGM();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void playBGM()
    {
        music.SetActive(true);
        sy.SetActive(false);
        syS.SetActive(true);
    }
    public static void stopBGM(){
        music.SetActive(false);
        sy.SetActive(true);
        syS.SetActive(false);
    }
    public static void gamePlay()
    {
        show(3);
    }
    public static void gameStart()
    {
        show(1);
    }
    public static void gameHelp()
    {
        show(2);
    }
    public static void gameOver()
    {
        show(0);
    }
    public static void gameExit()
    {
        Application.Quit();
    }
    public static void show(int o)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == o)
            {
                con[i].SetActive(true);
            }
            else
            {
                con[i].SetActive(false);
            }
        }
    }
}
