using UnityEngine;
using System;

[System.Serializable]
public class UnitData 
{
    public int unitNo;
    public int cost;
    public int hp;
    public int attackPower;
    public float moveSpeed;
    public float intervalTime;

    public Sprite unitSprite;
    public AnimationClip anime;
}
