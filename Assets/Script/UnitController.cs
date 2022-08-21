using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitController : MonoBehaviour
{

    [SerializeField]
    private UnitController targetUnit;
    public UnitController TargetUnit { get => targetUnit; }

    private GameManager gameManager;
    private UIManager uiManager;

    private int maxHp;

    private int timer;

    private Vector3 latestPos;

    public bool isAttack = false;

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
    private float blowPower;
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
    public void SetupUnitState(UIManager uiManager)
    {
        this.uiManager = uiManager;

        unitNo = gameManager.unitDatas[uiManager.btnIndex].unitNo;
        cost = gameManager.unitDatas[uiManager.btnIndex].cost;
        hp = gameManager.unitDatas[uiManager.btnIndex].hp;
        attackPower = gameManager.unitDatas[uiManager.btnIndex].attackPower;
        blowPower = gameManager.unitDatas[uiManager.btnIndex].blowPower;
        moveSpeed = gameManager.unitDatas[uiManager.btnIndex].moveSpeed;
        weight = gameManager.unitDatas[uiManager.btnIndex].weight;
        material = gameManager.unitDatas[uiManager.btnIndex].material;
        this.GetComponent<Renderer>().material = material;
        (attackRangeSize.size, attackRangeSize.center) = DataBaseManager.instance.GetAttackRange(gameManager.unitDatas[uiManager.btnIndex].attackRangeType);
        intervalTime = gameManager.unitDatas[uiManager.btnIndex].intervalTime;
        maxHp = hp;
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
            if (gameManager.gameMode == GameManager.GameMode.Play && targetUnit != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, moveSpeed);

                //進行方向を向く
                Vector3 diff = transform.position - latestPos;
                latestPos = transform.position;

                if (diff.magnitude > 0.01f)
                    transform.rotation = Quaternion.LookRotation(-diff);
            }
            yield return null;
        }
    }

    /// <summary>
    /// MoveUnitをUnitController上でCoroutineで動かすためのメソッド
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="unitList"></param>
    public void StartMoveUnit(GameManager gameManager, List<UnitController> unitList)
    {
        StartCoroutine(MoveUnit(gameManager, unitList));
    }

    /// <summary>
    /// 一定間隔毎にAttack()メソッドを実行
    /// </summary>
    /// <returns></returns>
    public IEnumerator PreparateAttack()
    {
        Debug.Log("攻撃準備開始");

        while (isAttack)
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
        yield break;
    }

    /// <summary>
    /// 対象にダメージを与える、倒した時の処理、ノックバック演出
    /// </summary>
    /// <param name="amount"></param>
    public void Attack(int amount)
    {
        Debug.Log("Attack()開始");
        targetUnit.hp = Mathf.Clamp(targetUnit.hp -= amount, 0, maxHp);

        if (targetUnit.hp <= 0)
        {
            isAttack = false;
            targetUnit.moveSpeed = 0;
            gameManager.EnemyList.Remove(targetUnit);
            Destroy(targetUnit.gameObject,3);
            targetUnit = null;
        }
        else
        //ノックバック演出
        KnockBack(this.blowPower);
    }

    /// <summary>
    /// ノックバック演出
    /// </summary>
    /// <param name="blowPower"></param>
    private void KnockBack(float blowPower)
    {
        Rigidbody targetRb = targetUnit.GetComponent<Rigidbody>();
        targetRb.velocity = transform.forward * blowPower;
        blowPower *= 0.98f;
        //targetUnit.transform.DOMove(transform.forward * blowPower,1);
    }
}
