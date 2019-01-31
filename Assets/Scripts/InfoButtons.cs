using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtons : MonoBehaviour
{

    public GameObject panel;
    public GameObject fightInfo;
    public GameObject updateInfo;
    public GameObject fightButton;
    public GameObject updateButton;
    public float bD = 35;

    private void FixedUpdate()
    {
        if(InfoFightingTime() && InfoUpdateTime())
        {
            fightInfo.SetActive(false);
            updateInfo.SetActive(false);
            panel.SetActive(false);
        }
    }

    public bool InfoUpdateTime()
    {

        if (Input.touches.Length>0 && CheckOverlapTouch(Input.GetTouch(0).position,updateButton.transform.position))
        {
            panel.SetActive(true);
            updateInfo.SetActive(true);
            return false;
        }
        return true;
    }
    public bool InfoFightingTime()
    {
        if (Input.touches.Length > 0 && CheckOverlapTouch(Input.GetTouch(0).position, fightButton.transform.position))
        {
            panel.SetActive(true);
            fightInfo.SetActive(true);
            return false;
        }
        return true;
    }

    private bool CheckOverlapTouch(Vector2 touch,Vector2 button)
    {
        if (touch.x < button.x + bD && touch.x > button.x-bD && touch.y < button.y +bD && touch.y > button.y- bD)
            return true;
        return false;
    }
}
