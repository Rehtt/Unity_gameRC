using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FK : MonoBehaviour
{
    public GameObject k;
    public GameObject head;
    public GameObject fenUi;
    public GameObject[] num;


    int w = 10;
    int h = 16;

    //🍗
    public GameObject food;
    bool isT = false;

    //🐍
    GameObject[,] gameObjects;
    GameObject[] s;
    bool down = false;

    int xx = 1;
    int yy = 0;
    float lastFall = 0;
    int fen;

    GameObject[] fenshu;
    void Start()
    {

    }
    void OnEnable()
    {
        fen = 0;
        fenUi.SetActive(true);
        gameObjects = new GameObject[w, h];
        s = new GameObject[4];
        CreS();
        Food();
    }
    void Update()
    {

        // 👈
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (xx != 1)
            {
                xx = -1; yy = 0;
                runS();
                lastFall = Time.time;
            }
        }
        // 👉
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (xx != -1)
            {

                xx = 1; yy = 0;
                runS();
                lastFall = Time.time;
            }
        }
        // 👆
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (yy != -1)
            {
                xx = 0; yy = 1;
                runS();
                lastFall = Time.time;
            }
        }
        // 👇
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (yy != 1)
            {
                xx = 0; yy = -1;
                runS();
                lastFall = Time.time;
            }


        }
        headR();
        if (Time.time - lastFall >= 0.7)
        {
            runS();
            lastFall = Time.time;
        }
        if (down && Time.time - lastFall >= 0.05)
        {
            Down();
            lastFall = Time.time;
        }
        fenShow();
    }
    void headR()
    {
        float r = s[0].transform.localEulerAngles.z;
        if (xx == 1)
        {
            s[0].transform.Rotate(new Vector3(0, 0, 270 - r));
        }
        else if (xx == -1)
        {
            s[0].transform.Rotate(new Vector3(0, 0, 90 - r));
        }
        else if (yy == 1)
        {
            s[0].transform.Rotate(new Vector3(0, 0, 0 - r));
        }
        else if (yy == -1)
        {
            s[0].transform.Rotate(new Vector3(0, 0, 180 - r));
        }
    }

    //🦿🐍
    void runS()
    {
        Vector2 last = new Vector2(0, 0);
        if (!down)
            for (int i = 0; i < 4; i++)
            {
                Vector2 ss = s[i].transform.position;
                if (i == 0)
                {
                    Vector2 tr = new Vector2(ss.x + xx, ss.y + yy);
                    if ((Vector2)food.transform.position == tr)
                    {
                        // Down();
                        fen += 10;
                        down = true;
                        food.SetActive(false);
                        break;
                    }
                    if (tr.x < 0 || tr.x > w - 1 || tr.y > h - 1 || gameObjects[(int)tr.x, (int)tr.y])
                    {
                        SDie();
                        break;
                    }
                    else
                    {

                        s[i].transform.position = tr;
                    }

                }
                else
                {

                    s[i].transform.position = last;
                }
                last = ss;
            }
    }

    //🐍👇
    void Down()
    {
        down = true;
        for (int i = 0; i < 4; i++)
        {
            Vector2 ss = s[i].transform.position;
            if (ss.y - 1 < 0 || gameObjects[(int)ss.x, (int)ss.y - 1])
            {
                down = false;
                continue;
            }
        }
        if (down)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 ss = s[i].transform.position;
                s[i].transform.position = new Vector2(ss.x, ss.y - 1);
            }
        }
        else
        {
            SToG();
            Food();
            CreS();
        }
    }
    //✨🐍
    void CreS()
    {
        for (int i = 0, j = 3; i < 4; i++, j--)
        {

            xx = 1;
            yy = 0;
            GameObject game = k;
            if (i == 0)
                game = head;
            s[i] = GameObject.Instantiate(game, new Vector2(j, h - 1), Quaternion.identity) as GameObject;
        }
    }
    //🐍->🎮
    void SToG()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 xy = s[i].transform.position;
            gameObjects[(int)xy.x, (int)xy.y] = s[i];
        }
        GridDown();
        s = new GameObject[4];
    }

    //✨🍗
    void Food()
    {

        int x, y;
        x = Random.Range(0, w - 1);
        y = Random.Range(0, h - 1);
        if (gameObjects[x, y])
        {
            Food();
        }
        else
        {
            food.SetActive(true);
            if (!isT)
            {
                food = Instantiate(food, new Vector2(x, y), Quaternion.identity) as GameObject;
                isT = true;
            }
            else
            {
                food.transform.position = new Vector2(x, y);

            }
        }

    }

    //😢🐍
    void SDie()
    {
        Game.gameOver();
    }
    private void OnDisable()
    {
        for (int i = 0; i < s.Length; i++)
        {
            Destroy(s[i]);
        }
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Destroy(gameObjects[i, j]);
            }
        }
        isT = true;
        food.SetActive(false);
    }

    //消
    void GridDown()
    {
        bool delH = true;
        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                if (!gameObjects[i, j])
                {
                    delH = false;
                }
            }
            if (delH)
            {
                fen += 100;
                DelH(j);
                --j;
            }
            delH = true;
        }

    }
    void DelH(int y)
    {
        for (; y < h - 1; y++)
            for (int i = 0; i < w; i++)
            {
                if (gameObjects[i, y + 1])
                {
                    if (!gameObjects[i, y])
                        gameObjects[i, y] = Instantiate(k, new Vector2(i, y), Quaternion.identity) as GameObject;

                    Destroy(gameObjects[i, y + 1]);
                    gameObjects[i, y + 1] = null;
                }
                else if (gameObjects[i, y])
                {
                    Destroy(gameObjects[i, y]);
                    gameObjects[i, y] = null;

                }
            }
    }

    GameObject[] numF;
    void fenShow()
    {
        if (numF != null)
            for (int q = 0; q < numF.Length; q++)
            {
                Destroy(numF[q]);
                numF[q] = null;
            }

        Vector2 xy = fenUi.transform.Find("num").gameObject.transform.position;
        float fenX = xy.x;
        float fenY = xy.y;
        char[] i = fen.ToString().ToCharArray();
        numF = new GameObject[i.Length];

        for (int j = 0; j < i.Length; j++)
        {
            numF[j] = Instantiate(num[i[j] - '0'], new Vector2(fenX, fenY), Quaternion.identity) as GameObject;
            fenX = (float)(fenX + 0.5);
        }
    }
    //todo Rehtt
}
