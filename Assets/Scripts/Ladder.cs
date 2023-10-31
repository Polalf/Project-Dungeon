using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int sceneToLoad;
    
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,new Vector2(0.2f,0.2f),0,Vector2.zero,0.2f,playerMask);

        if(hit)
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
       
    }
}
