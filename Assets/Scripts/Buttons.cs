using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{   
    public static Buttons instance;

    [SerializeField] GameObject menuInicio, gamePlay;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
   
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
        if(scene == 0)
        {
            gamePlay.SetActive(false);
            menuInicio.SetActive(true);
        }
        else
        {
            gamePlay.SetActive(true);
            menuInicio.SetActive(true);
        }
        SceneManager.LoadScene(scene);
    }
}
