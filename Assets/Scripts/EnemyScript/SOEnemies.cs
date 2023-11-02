using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game/Enemies")]
public class SOEnemies : ScriptableObject
{
    public int listPos = 0;
    public string enemyName;
    [Header("Settings")]
    public int e_maxLife;
    public int e_damage;

    [Header("Movement Settings")]
    public float e_movementTime;
    public AnimationCurve e_movementCurve;

    [Header("Visuals")]
    public Sprite e_idleSprite;
    public List<Sprite> e_sideWalkSprite = new List<Sprite>(4);
    public List<Sprite> e_backWalkSprite = new List<Sprite>(4);
    public List<Sprite> e_frontWalkSprite = new List<Sprite>(4);
    //public AnimationClip e_animSide,e_animBack,e_animFront;

    [Header("Bestiary")]
    public int killsTo1;
    public int killsTo2, killsTo3, killsTo4;
    public string e_infoLvl1, e_infoLvl2, e_infoLvl3, e_infoLvl4;
}
