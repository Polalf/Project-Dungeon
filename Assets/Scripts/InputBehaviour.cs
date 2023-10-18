using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController p_playerController;
    [SerializeField] private RaycastBehaviour p_raycastBehaviour;

    [Header("Settings")]
    [SerializeField] private float maxTapTime = 0.5f;

    private Vector2 starPos;
    private bool pressed;
    private float tapTime;

    public Vector2 playerPos;
    void Start()
    {
        playerPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = gameObject.transform.position;

        if(Input.touchCount>0)
        {
            Touch dedo = Input.GetTouch(0);
            if(dedo.phase == TouchPhase.Began)
            {
                tapTime = 0;
                pressed = true;
                starPos = dedo.position;
            }
            else if(dedo.phase==TouchPhase.Ended)
            {
                pressed = false;

                if(tapTime < maxTapTime)
                {
                    Vector2 deltaPos = dedo.position - starPos;
                    if(deltaPos.magnitude < 10)
                    {
                        Tap(dedo.position);
                    }
                    else
                    {
                        if(Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x > 0) SwipeRigth();
                            else SwipeLeft();
                        }
                        else
                        {
                            if (deltaPos.y > 0) SwipeUp();
                            else SwipeDown();
                        }
                    }
                }
            }

            //if (pressed)
            //{
            //    tapTime += Time.deltaTime;

            //    if (ta)
            //}
        }
    }

    #region Funciones 
    private void Tap(Vector2 position)
    {
        Vector2 topPos = Camera.main.ScreenToWorldPoint(position);
        //p_raycastBehaviour
    }

    private void SwipeRigth()
    {
        p_playerController.MoveTo(new Vector2(1, 0));
    }

    private void SwipeLeft()
    {
        p_playerController.MoveTo(new Vector2(-1, 0));
    }
    private void SwipeUp()
    {
        p_playerController.MoveTo(new Vector2(0, 1));
    }
    private void SwipeDown()
    {
        p_playerController.MoveTo(new Vector2(0, -1));
    }
    #endregion
}
