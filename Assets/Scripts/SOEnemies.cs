using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game/Enemies")]
public class SOEnemies : ScriptableObject
{
    public string enemyName;
    [Header("Settings")]
    public int e_maxLife;
    public int e_damage;

    [Header("Movement Settings")]
    public float e_movementTime;
    public AnimationCurve e_movementCurve;

    [Header("Visuals")]
    public Sprite e_idleSprite;
    //public List<Sprite> e_atkSprite = new List<Sprite>(3);
    public List<Sprite> e_sideWalkSprite = new List<Sprite>(4);
    public List<Sprite> e_backWalkSprite = new List<Sprite>(4);
    public List<Sprite> e_frontWalkSprite = new List<Sprite>(4);

}
