using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
    public List<GameObject> mapas;
    [SerializeField] private int iterations;
    [SerializeField] private float genDist;
    Vector3 genPos;
    void Start()
    {
        genPos = transform.position;
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Generate()
    {
        int i = 0;
        while (i < 5)
        {
            Instantiate(mapas[Random.Range(0,mapas.Count)], genPos, Quaternion.identity);
            Debug.Log(genPos);
            yield return null;
            genPos += new Vector3(genDist, 0);
            i++;
        }
        Debug.Log("termino la generacion");
    }
}

