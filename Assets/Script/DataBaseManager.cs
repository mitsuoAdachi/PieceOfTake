using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public UnitDataSO unitDataSO;

    public AttackRangeSizeSO attackRangeSizeSO;

    //シングルトンの作成
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 各ユニットのattckRangeTypeより攻撃コライダーのサイズと位置のVector3を取得
    /// </summary>
    /// <param name="attackRangeType"></param>
    /// <returns></returns>
    public (Vector3 size, Vector3 pos) GetAttackRange(AttackRangeType attackRangeType)
    {
        return (attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == attackRangeType).rangeSize,
                     attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == attackRangeType).colliderPos);
    }

    //タプル型を使用しない場合
    //public Vector3 GetAttackRangeSize(AttackRangeType selectAttackRangeType)
    //{
    //    return attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == selectAttackRangeType).rangeSize;
    //}
    //public Vector3 GetAttackRangePos(AttackRangeType selectAttackRangeType)
    //{
    //    return attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == selectAttackRangeType).colliderPos;
    //}

    //foreachで書いた場合
    //foreach (AttackRangeSize attackRangeSize in attackRangeSizeSO.attackRangeSizesList) {
    //  if(attackRangeSize.attackRangeType == attackRangeType) {
    //      return attackRangeType.size;
}
