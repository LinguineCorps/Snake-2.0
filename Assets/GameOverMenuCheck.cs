using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverMenuCheck : MonoBehaviour
{
    public GameObject GameOverMenu;

    // Update is called once per frame
    void Update()
    {
        GameObject myObject = GameObject.FindWithTag("Player");
        if (myObject == null)
        {
            GameOverMenu.SetActive(true);
        }
        else
        {

        }
    }
}
