using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private RaycastBehaviour m_raycastBehaviour;

    public EnemyManager nosewn;


    // Methods
    /// <summary>
    /// Manda una señal con la posicion en pixeles del dedo
    /// </summary>
    private void Tap(Vector2 position)
    {
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(position);
        //m_raycastBehaviour.DoRay(tapPos);
        m_raycastBehaviour.TryDoDamage(tapPos);
        //foreach (var item in nosewn.instantiateEnemy)
        //{
        //    item.GetComponent<EnemyController>().TakeDamage(5);
        //}
        //TurnManager.EndTurn();
    }

    /// <summary>
    /// Devuelve true si la pantalla esta siendo presionada y false si se dejo de presionar.
    /// Tambien devuelve la posicion del dedo al presionar y la posicion del dedo al soltar.
    /// </summary>
   

    /// <summary>
    /// Swipe hacia la izquierda
    /// </summary>
    private void SwipeLeft()
    {

        m_playerController.MoveTo(new Vector2(-1, 0));



    }

    /// <summary>
    /// Swipe hacia la derecha
    /// </summary>
    private void SwipeRight()
    {

        m_playerController.MoveTo(new Vector2(1, 0));



    }

    /// <summary>
    /// Swipe hacia arriba
    /// </summary>
    private void SwipeUp()
    {

        m_playerController.MoveTo(new Vector2(0, 1));



    }

    /// <summary>
    /// Swipe hacia abajo
    /// </summary>
    private void SwipeDown()
    {

        m_playerController.MoveTo(new Vector2(0, -1));



    }













    // Variables
    [Header("Settings")]
    [SerializeField] private float maxTapTime = 0.5f;

    private Vector2 startPos;
    private bool pressed;
    private bool pressWasTriggered;
    private float tapTime;


    public Vector2 playerPos;
    private void Start()
    {
        playerPos = gameObject.transform.position;
    }
    // Methods 
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

        playerPos = gameObject.transform.position;

        if (Input.touchCount > 0)
        {
            Touch dedo = Input.GetTouch(0);

            if (dedo.phase == TouchPhase.Began)
            {
                tapTime = 0;
                pressed = true;
                startPos = dedo.position;
                pressWasTriggered = false;
            }

            else if (dedo.phase == TouchPhase.Ended)
            {
                pressed = false;

                if (pressWasTriggered)
                {
                
                }

                if (tapTime < maxTapTime)
                {
                    Vector2 deltaPos = dedo.position - startPos;

                    if (deltaPos.magnitude < 10)
                    {
                        Tap(dedo.position);
                    }
                    else
                    {
                        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
                        {
                            if (deltaPos.x > 0) SwipeRight();
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

            if (pressed)
            {
                tapTime += Time.deltaTime;

                if (tapTime >= maxTapTime && !pressWasTriggered)
                {
                    pressWasTriggered = true;
                    
                }
            }
        }
    }
}
