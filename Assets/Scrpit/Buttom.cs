using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    //按钮公共脚本
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.collider.gameObject;
                
                switch (go.name)
                {
                    case "game":
                        Game.gamePlay();
                        break;
                    case "help":
                        Game.gameHelp();
                        break;
                    case "exit":
                        Game.gameExit();
                        break;
                    case "syS":
                        Game.stopBGM();
                        break;
                    case "sy":
                        Game.playBGM();
                        break;

                }
            }
        }
    }
}
