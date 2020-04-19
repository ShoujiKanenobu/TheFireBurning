using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menuObj;
    public FirePitController fp;

    public bool paused;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && fp.fireHP > 0)
        {
            menuObj.SetActive(!menuObj.activeSelf);
            paused = true;
        }
        else if (menuObj.activeInHierarchy == false)
        {
            paused = false;
        }
    }
}
