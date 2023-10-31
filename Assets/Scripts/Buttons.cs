using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{   
    public void TimeControl(float scale)
    {
        Time.timeScale = scale;
    }
    public void CurarPlayer()
    {
        FindObjectOfType<Inventory>().UsePotion();
    }

}
