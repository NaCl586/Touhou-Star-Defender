using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FairyData", menuName = "Scriptable Objects/Fairy Data")]
public class FairyData : ScriptableObject
{
    public string _name;
    public int HP;
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    
    [Header("Death Effect")]
    public AudioClip deathSound;
    public GameObject deathEffect;

    [Header("Bullet Effect")]
    public Sprite bullet;
    public AudioClip bulletShootSound;

    [Header("Item Drops")]
    public GameObject itemDrops;

    [Header("Shot Type")]
    public int shotType;
    public int shotCount;
}
