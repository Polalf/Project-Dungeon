using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;
    public virtual void TakeDamage(SpriteRenderer sr)
    {
        StartCoroutine(DamageAnim(sr));
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private IEnumerator DamageAnim (SpriteRenderer sr)
    {
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            sr.color += new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color += new Color(0, 0, 0, 1);
        }
       
    }
        
}
