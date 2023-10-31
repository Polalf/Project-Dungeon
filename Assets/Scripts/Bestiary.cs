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

    [SerializeField] private List<int> counters = new List<int>(13);

    [Header("UI Base")]
    [SerializeField] private Sprite baseImage;
    [SerializeField] private string unknownInfo;
    

    [Header("UI")]
    [SerializeField] private Sprite frontPage, paperPage;
    [SerializeField] private GameObject infoEnemies;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI infoLvl1, infoLvl2, infoLvl3, infoLvl4;
    [Header("UI Indice")]
    [SerializeField] private GameObject indice;
    [SerializeField] private List<Image> imagesIndex = new List<Image>(8);

    [Header("Page Behaviour")]
    [SerializeField] private int pageIndex;

    private void Start()
    {
        enemyImage.sprite = baseImage;
        enemyName.text = unknownInfo;
        infoLvl1.text = unknownInfo;
        infoLvl2.text = unknownInfo;
        infoLvl3.text = unknownInfo;
        infoLvl4.text = unknownInfo;

        
    }

    public void SelectedFromIndex(int _pageIndex)
    {
        indice.SetActive(false);
        ShowInfo(_pageIndex);
        infoEnemies.SetActive(true);
    }
    public void backToIndex()
    {
        infoEnemies.SetActive(false);
        indice.SetActive(true);
    }

    public void ShowInfo(int _pageIndex)
    {
        if (counters[_pageIndex] >= 1)
            {
            enemyImage.sprite = enemiesSo[_pageIndex].e_idleSprite;
            enemyName.text = enemiesSo[_pageIndex].enemyName;
            if (counters[_pageIndex] >= enemiesSo[_pageIndex].killsTo1)
            {
                infoLvl1.text = enemiesSo[_pageIndex].e_infoLvl1.ToString();
            }
            if (counters[_pageIndex] >= enemiesSo[_pageIndex].killsTo2)
            {
                infoLvl2.text = enemiesSo[_pageIndex].e_infoLvl2;
            }
            if (counters[_pageIndex] >= enemiesSo[_pageIndex].killsTo3)
            {
                infoLvl3.text = enemiesSo[_pageIndex].e_infoLvl3;
            }
            if (counters[_pageIndex] >= enemiesSo[_pageIndex].killsTo4)
            {
                infoLvl4.text = enemiesSo[_pageIndex].e_infoLvl4;
            }
        }
            else
        {
            enemyImage.sprite = baseImage;
            enemyName.text = unknownInfo;
            infoLvl1.text = unknownInfo;
            infoLvl2.text = unknownInfo;
            infoLvl3.text = unknownInfo;
            infoLvl4.text = unknownInfo;
        }
    }
    public void AddEnemyCount(int listPos)
    {
        counters[listPos]++;
    }
    public void OpenBook()
    {
        for (int i = 0; i < counters.Count; i++)
        {
            if (counters[i] > 0) imagesIndex[i].sprite = enemiesSo[i].e_idleSprite;
        }
        indice.SetActive(true);

    }
    public void NextPage()
    {
        // Animación
        pageIndex++;
        ShowInfo(pageIndex);
        if (pageIndex > counters.Count) pageIndex = counters.Count;
    }

    public void PreviousPage()
    {
        pageIndex--;
        ShowInfo(pageIndex);
        if (pageIndex <= 0) pageIndex = 0;
    }


    public void CloseBook()
    {
        //ANIMACION
        gameObject.SetActive(false);
    }
}