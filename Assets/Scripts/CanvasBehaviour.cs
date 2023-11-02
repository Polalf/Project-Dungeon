using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBehaviour : MonoBehaviour
{
    public static CanvasBehaviour instance;

    public void TimeControl(float scale)
    {
        Time.timeScale = scale;
    }
    public void CurarPlayer()
    {
        FindObjectOfType<Inventory>().UsePotion();
    }
    public void ChangeScene(int scene)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(scene);
    }
    public void PlayerAct(bool actPlayer)
    {
        PlayerController.instance.gameObject.SetActive(actPlayer);
    }

    public void Salir()
    {
        Application.Quit();
    }
}

