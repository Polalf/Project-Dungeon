using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bestiary : MonoBehaviour
{
    [Header("Monsters")]
    [SerializeField] List<SOEnemies> enemiesSO;

    [Header("UI")]
    [SerializeField] private Image frontPage, paperPage;
    [SerializeField] private GameObject infoEnemies;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private TMP_Text infoLvl1, infoLvl2, infoLvl3, infoLvl4;
    

    [Header("Page Behaviour")]
    [SerializeField] private int pageIndex;
    

    private void Update()
    {


        if (pageIndex > -1)
        {
            //foreach (SOEnemies enemy in )
            //{
                

            
            gameObject.GetComponent<Image>().sprite = paperPage.sprite;
            switch (enemiesSO[pageIndex].huntedCount)
            {
                case 1:
                    enemyImage.sprite = enemiesSO[pageIndex].e_idleSprite;
                    enemyName.text = enemiesSO[pageIndex].enemyName;
                    break;
                case 2:
                    infoLvl1.text = enemiesSO[pageIndex].e_infoLvl1;
                    break;
                case 3:
                    infoLvl2.text = enemiesSO[pageIndex].e_infoLvl2;
                    break;
                case 4:
                    infoLvl3.text = enemiesSO[pageIndex].e_infoLvl3;
                    break;
                case 5:
                    infoLvl4.text = enemiesSO[pageIndex].e_infoLvl4;
                    break;
                default:
                    break;
            }

        }
        else
        {
            gameObject.GetComponent<Image>().sprite = frontPage.sprite;
            infoEnemies.SetActive(false);
            
        }
    }
    public void OpenBook()
    {
        gameObject.SetActive(true);
        //animation

       
    }
    public void NextPage()
    {
        // Animación
        pageIndex++;
        if (pageIndex >= enemiesSO.Count)
        {
            pageIndex = enemiesSO.Count;
            CloseBook();
        }
    }

    public void PreviousPage()
    {
        pageIndex--;

        if (pageIndex < 0)
        {
            pageIndex = -1;
            CloseBook();
        }
    }
    
    public void CloseBook()
    {
        //ANIMACION
        gameObject.SetActive(false);
    }
}
