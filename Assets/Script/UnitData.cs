using UnityEngine;

[System.Serializable]
public class UnitData 
{
    public int cost;
    public int hp;
    public int attackPower;
    public float blowPower;
    public float moveSpeed;
    public float weight;
    public float intervalTime;
    public bool isGround;
    public AttackRangeType attackRangeType;
    public UnitController UnitPrefab;

    public Material material;

    //public Sprite unitSprite;
    //public AnimationClip moveAnime;
    //public AnimationClip attackAnime;
    //public AnimationClip deadAnime;
}
