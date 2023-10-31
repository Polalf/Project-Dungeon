using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text arrowUi;
    [SerializeField] private TMP_Text potionsUi;
    public int arrows;
    [SerializeField] private int potions = 0;
    [SerializeField] private PlayerController p_playerController;
    [SerializeField] private int cantOfCure = 15;

    private void Start()
    {
        if(arrowUi == null) arrowUi = GameObject.Find("ArrowText").GetComponent<TMP_Text>();

        if (potionsUi == null) potionsUi = GameObject.Find("PotionText").GetComponent<TMP_Text>();
       
    }
    private void Update()
    {
        arrowUi.text = "X " +  arrows.ToString();
        potionsUi.text = "X " + potions.ToString();
    }
    public void AddObject(int _cantidad, Loot.LootObject _typeLoot)
    {
        if (_typeLoot == Loot.LootObject.Arrow) arrows += _cantidad;
        else if (_typeLoot == Loot.LootObject.Potion) potions += _cantidad;
    }

    public void RemoveArrows()
    {
        arrows--;
    }
    public void UsePotion()
    {
        p_playerController.Cure(cantOfCure);
        potions--;
        if (potions <= 0) potions = 0;
    }
}
