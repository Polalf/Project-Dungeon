using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bestiary : MonoBehaviour
{
    [Header("Monsters")]
    [SerializeField] private List<SOEnemies> enemiesSo;

    [SerializeField] private List<int> counters = new List<int>(12);

    [Header("UI Base")]
    [SerializeField] private Sprite baseImage;
    [SerializeField] private string unknownInfo;
    

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
            switch (counters[pageIndex])
            {
                case 1:
                    enemyImage.sprite = enemiesSo[pageIndex].e_idleSprite;
                    enemyName.text = enemiesSo[pageIndex].enemyName;
                    break;
                case 2:
                    infoLvl1.text = enemiesSo[pageIndex].e_infoLvl1;
                    break;
                case 3:
                    infoLvl2.text = enemiesSo[pageIndex].e_infoLvl2;
                    break;
                case 4:
                    infoLvl3.text = enemiesSo[pageIndex].e_infoLvl3;
                    break;
                case 5:
                    infoLvl4.text = enemiesSo[pageIndex].e_infoLvl4;
                    break;
                default:
                    enemyImage.sprite = baseImage;
                    enemyName.text = unknownInfo;
                    infoLvl1.text = unknownInfo;
                    infoLvl2.text = unknownInfo;
                    infoLvl3.text = unknownInfo;
                    infoLvl4.text = unknownInfo;
                    break;
            }

        }
        else
        {
            gameObject.GetComponent<Image>().sprite = frontPage.sprite;
            infoEnemies.SetActive(false);

        }
    }

    public void AddEnemyCount(int listPos)
    {
        counters[listPos]++;
    }
    public void OpenBook()
    {
        gameObject.SetActive(true);
        //animation


    }
    public void NextPage()
    {
        // Animación
        
    }

    public void PreviousPage()
    {
        
    }

    public void CloseBook()
    {
        //ANIMACION
        gameObject.SetActive(false);
    }
}