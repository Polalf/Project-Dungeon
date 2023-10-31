using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Inventory inventory;

    [Header("Follow")]
    [SerializeField] private float minTapTime = 0.3f;
    [SerializeField] private float maxTapTime = 0.7f;
    
    [Header ("Shoot")]
    [SerializeField] private GameObject arrow;

    [Header("Visuals")]
    private SpriteRenderer sr;
    [SerializeField] private Sprite bowIdle, bowCharge;

    private float tapTime = 0;
    private bool isPress;
    private bool canShoot = false;
    private bool canFollow = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch dedo = Input.GetTouch(0);
            //float angle = Mathf.Atan2(dedo.position.y - transform.position.y, dedo.position.x - transform.position.x);
            Vector3 dedoPos = Camera.main.ScreenToWorldPoint(dedo.position);
            dedoPos.z = 0;
            Vector3 lookAtDir = dedoPos - transform.position;
            if(dedo.phase == TouchPhase.Began)
            {
                tapTime = 0;
                isPress = true;
            }
            else if(dedo.phase == TouchPhase.Ended)
            {
                isPress = false;
                if(canShoot) StartCoroutine(ShootArrow());
                
                    
                
            }
            
            if (canFollow)
            {
                transform.up = lookAtDir;
            }
            
        }
        sr.sprite = null;
        if (isPress)
        {
            tapTime += Time.deltaTime;
            if(tapTime>= minTapTime)
            {
                sr.sprite = bowIdle;
                canFollow = true;
                if(tapTime >= maxTapTime)
                {
                    if (inventory.arrows > 0) sr.sprite = bowCharge;
                    else sr.sprite = bowIdle;
                    canShoot = true;
                }
            }
        }

        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ShootArrow(mousePos);
        //}
    }

    public IEnumerator ShootArrow()//Vector2 target)
    {
        if (inventory.arrows <= 0) yield break;
        //float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
        if (inventory.arrows > 0)
        {
            Debug.Log("Disparando Flecha");
            Instantiate(arrow, transform.position, transform.rotation);
            inventory.arrows--;
            sr.sprite = bowIdle;
            yield return new WaitForSeconds(0.2f);
            sr.sprite = null;
            canShoot = false;
            canFollow = false;
        }
        
    }
}
