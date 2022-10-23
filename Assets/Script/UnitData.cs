using UnityEngine;

[System.Serializable]
public class UnitData 
{
    public int id;
    public string name;
    public int cost;
    public int hp;
    public int attackPower;
    public float blowPower;
    public float moveSpeed;
    public float weight;
    public float intervalTime;
    public UnitController UnitPrefab;
    public Sprite unitImage;
    public AudioSource generateVoice;

    //public AttackRangeType attackRangeType;
    //public Sprite unitSprite;
    //public AnimationClip moveAnime;
    //public AnimationClip attackAnime;
    //public AnimationClip deadAnime;
}
