using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitController : MonoBehaviour
{
    private UnitController targetUnit;
    public UnitController TargetUnit { get => targetUnit; }

    private GameManager gameManager;

    private int maxHp;

    private Tween tween;

    //ユニットステータス群
    [SerializeField, Header("ユニットNo.")]
    private int unitNo;
    [SerializeField, Header("コスト")]
    private int cost;
    [SerializeField, Header("HP")]
    private int hp;
    [SerializeField, Header("攻撃力")]
    private int attackPower;
    [SerializeField, Header("衝撃力")]
    private int blowPower;
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 0.01f;
    [SerializeField, Header("重量")]
    private float weight;
    [SerializeField, Header("攻撃間隔")]
    private float intervalTime;
    [SerializeField, Header("攻撃範囲")]
    private BoxCollider attackRangeSize;
    
    public Material material;

    /// <summary>
    /// ユニットステータスの設定
    /// </summary>
    /// <param name="uiManager"></param>
    /// <param name="unitGenerator"></param>
    /// <returns></returns>
    public IEnumerator SetupUnitState(UIManager uiManager, UnitGenerator unitGenerator)
    {
        unitNo = unitGenerator.unitDatas[uiManager.btnIndex].unitNo;
        cost = unitGenerator.unitDatas[uiManager.btnIndex].cost;
        hp = unitGenerator.unitDatas[uiManager.btnIndex].unitNo;
        attackPower = unitGenerator.unitDatas[uiManager.btnIndex].attackPower;
        blowPower = unitGenerator.unitDatas[uiManager.btnIndex].blowPower;
        moveSpeed = unitGenerator.unitDatas[uiManager.btnIndex].moveSpeed;
        weight = unitGenerator.unitDatas[uiManager.btnIndex].weight;
        material = unitGenerator.unitDatas[uiManager.btnIndex].material;
        this.GetComponent<Renderer>().material = material;
        attackRangeSize.size = DataBaseManager.instance.GetAttackRangeSize(unitGenerator.unitDatas[uiManager.btnIndex].attackRangeType);
        attackRangeSize.center = DataBaseManager.instance.GetAttackRangePos(unitGenerator.unitDatas[uiManager.btnIndex].attackRangeType);
        intervalTime = unitGenerator.unitDatas[uiManager.btnIndex].intervalTime;
        yield return maxHp = hp;
    }

    /// <summary>
    /// ユニットの移動
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator MoveUnit(GameManager gameManager,List<UnitController> unitList)
    {
        this.gameManager = gameManager;

        float standardDistanceValue = 1000;　//敵の距離を比較するための基準となる変数。適当な数値を代入

        //Debug.Log("監視開始");
        while (this.hp >= 0)
        {
            if (gameManager.gameMode == GameManager.GameMode.Play)
            {
                if (targetUnit == null)
                { 
                    //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する処理↓↓                 
                    foreach (UnitController target in unitList)
                    {
                        //EnemyUnitList内に登録してあるオブジェクトとの距離を測り変数に代入する
                        float nearTargetDistanceValue = Vector3.Distance(transform.position, target.transform.position);

                        //基準値より小さければその数値を基準値に代入していき一番小さい数値が変数に残る。その数値を持つオブジェクトが一番近い敵となる
                        if (standardDistanceValue > nearTargetDistanceValue)
                        {
                            standardDistanceValue = nearTargetDistanceValue;

                            targetUnit = target;
                        }
                    }
                }
                else standardDistanceValue = 1000;

                //Debug.Log("移動準備完了");
                if (targetUnit != null)
                transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, moveSpeed);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 一定間隔毎にAttack()メソッドを実行
    /// </summary>
    /// <returns></returns>
    public IEnumerator PreparateAttack()
    {
        Debug.Log("攻撃準備開始");

        int timer = 0;

        while (true)
        {
            if (gameManager.gameMode == GameManager.GameMode.Play)
            {
                timer++;

                if (timer > intervalTime)
                {
                    timer = 0;

                    Attack(attackPower);
                }
            }
            yield return null;
        }
    }

    public void Attack(int amount)
    {
        Debug.Log("Attack()開始");
        targetUnit.hp = Mathf.Clamp(targetUnit.hp -= amount, 0, maxHp);

        if (targetUnit.hp <= 0)
        {
            gameManager.EnemyList.Remove(targetUnit);
            Destroy(targetUnit.gameObject);
            targetUnit = null;
        }
        //ノックバック演出
        //StartCoroutine(KnockBack());
    }

    private IEnumerator KnockBack()
    {
        targetUnit.tween.timeScale = 0.05f;

        yield return new WaitForSeconds(0.5f);

        targetUnit.tween.timeScale = 1.0f;
    }
}
