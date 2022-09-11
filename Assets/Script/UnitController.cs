using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private UnitController targetUnit;
    public UnitController TargetUnit { get => targetUnit; }

    [SerializeField]
    private ParticleSystem isAttackParticle;

    //攻撃間隔用タイマー
    [SerializeField]
    private int timer;

    //敵の距離を比較するための基準となる変数。適当な数値を代入
    public float standerdDistanceValue = 1000;

    private GameManager gameManager;
    private UIManager uiManager;

    private NavMeshAgent agent;

    private int maxHp;

    public int unitNumber;

    public bool isAttack = false;

    private Animator anime;
    private int attackAnime;
    private int knockBackAnime;
    private int walkAnime;
    private int deadAnime;

    //ユニットステータス群
    [SerializeField, Header("コスト")]
    private int cost;
    public int Cost { get => cost; }
    [SerializeField, Header("HP")]
    private int hp;
    [Header("攻撃力")]
    public int attackPower;
    [Header("衝撃力")]
    public float blowPower;
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("重量")]
    private float weight;
    [SerializeField, Header("攻撃間隔")]
    private float intervalTime;
    [SerializeField, Header("攻撃範囲")]
    private BoxCollider attackRangeSize;   
    public Material material;

    private void Start()
    {
        //コンポーネントのキャッシュ
        anime = GetComponent<Animator>();
        attackAnime = Animator.StringToHash("attack");
        knockBackAnime = Animator.StringToHash("knockBack");
        walkAnime = Animator.StringToHash("walk");
        deadAnime = Animator.StringToHash("dead");
    }
    /// <summary>
    /// ユニットステータスの設定
    /// </summary>
    /// <param name="uiManager"></param>
    /// <param name="unitGenerator"></param>
    /// <returns></returns>
    public void SetupUnitStateAlly(List<UnitData> unitDatas,UIManager uiManager)
    {
        agent = GetComponent<NavMeshAgent>();

        this.uiManager = uiManager;

        cost = unitDatas[uiManager.btnIndex].cost;
        hp = unitDatas[uiManager.btnIndex].hp;
        attackPower = unitDatas[uiManager.btnIndex].attackPower;
        blowPower = unitDatas[uiManager.btnIndex].blowPower;
        agent.speed = unitDatas[uiManager.btnIndex].moveSpeed;
        weight = unitDatas[uiManager.btnIndex].weight;
        (attackRangeSize.size, attackRangeSize.center) = DataBaseManager.instance.GetAttackRange(unitDatas[uiManager.btnIndex].attackRangeType);
        intervalTime = unitDatas[uiManager.btnIndex].intervalTime;
        maxHp = hp;

        material = unitDatas[uiManager.btnIndex].material;
        this.GetComponent<Renderer>().material = material;
    }

    public void SetupUnitStateEnemy(List<UnitData> unitDatas)
    {
        agent = GetComponent<NavMeshAgent>();

        cost = unitDatas[unitNumber].cost;
        hp = unitDatas[unitNumber].hp;
        attackPower = unitDatas[unitNumber].attackPower;
        blowPower = unitDatas[unitNumber].blowPower;
        agent.speed = unitDatas[unitNumber].moveSpeed;
        weight = unitDatas[unitNumber].weight;
        (attackRangeSize.size, attackRangeSize.center) = DataBaseManager.instance.GetAttackRange(unitDatas[unitNumber].attackRangeType);
        intervalTime = unitDatas[unitNumber].intervalTime;
        maxHp = hp;

        material = unitDatas[unitNumber].material;
        this.GetComponent<Renderer>().material = material;
    }

    /// <summary>
    /// ユニットの移動
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator OnMoveUnit(GameManager gameManager,List<UnitController> unitList)
    {
        this.gameManager = gameManager;

        //Debug.Log("監視開始");
        while (this.hp > 0)
        {
            //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する処理↓↓                 
            foreach (UnitController target in unitList)
            {
                if (gameManager.gameMode == GameManager.GameMode.Play && target != null)
                {
                    //攻撃間隔タイマーはMoveUnitメソッド内で換算
                    timer++;

                    //EnemyUnitList内に登録してあるオブジェクトとの距離を測り変数に代入する
                    float nearTargetDistanceValue = Vector3.SqrMagnitude(target.transform.position - transform.position);

                    //基準値より小さければその数値を基準値に代入していき一番小さい数値が変数に残る。その数値を持つオブジェクトが一番近い敵となる
                    if (standerdDistanceValue > nearTargetDistanceValue)
                    {
                        standerdDistanceValue = nearTargetDistanceValue;

                        targetUnit = target;
                    }

                    if (targetUnit != null)
                    {
                        //ナビメッシュを使用した移動
                        agent.destination = targetUnit.transform.position;

                        anime.SetFloat(walkAnime, agent.velocity.sqrMagnitude);

                        //進行方向を向く
                        transform.LookAt(targetUnit.transform);
                    }
                    else
                    {
                        standerdDistanceValue = 1000;
                        anime.SetFloat(walkAnime, 0);
                    }
                }
            }
            yield return null;
        }
        Debug.Log(this.name + "MoveUnit終了");
    }

    /// <summary>
    /// MoveUnitをUnitController上でCoroutineで動かすためのメソッド
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="unitList"></param>
    public void StartMoveUnit(GameManager gameManager, List<UnitController> unitList)
    {
        StartCoroutine(OnMoveUnit(gameManager, unitList));
    }

    /// <summary>
    /// 一定間隔毎に攻撃アニメーションを実行
    /// </summary>
    /// <returns></returns>
    public void PreparateAttack()
    {
        if (this.hp > 0 && gameManager.gameMode == GameManager.GameMode.Play)
        {
            if (targetUnit.hp > 0)
            {
                if (timer > intervalTime)
                {
                    timer = 0;

                    //Attack(attackPower);
                    anime.SetTrigger(attackAnime);
                }
            }
        }
    }

    /// <summary>
    /// ダメージを与える処理
    /// </summary>
    /// <param name="amount"></param>
    public void OnDamage(int amount)
    {
        this.hp = Mathf.Clamp(this.hp -= amount, 0, maxHp);

        if(this.hp <= 0)
        {
            agent.isStopped = true;
            targetUnit = null;
            anime.SetTrigger(deadAnime);

            gameManager.GenerateEnemyList.Remove(this);
            gameManager.GenerateAllyList.Remove(this);

            Destroy(this.gameObject, 3);
        }
    }

    /// <summary>
    /// ノックバック演出
    /// </summary>
    /// <param name="blowPower"></param>
    public void OnKnockBack(float blowPower)
    {
        //agent.velocity -= transform.forward * blowPower;
        //anime.SetTrigger(knockBackAnime);

        transform.DOMove(transform.forward * -blowPower,1);
    }

    /// <summary>
    /// 弓矢・魔法を発射するエフェクト
    /// </summary>
    public void OnAttackPartical()
    {
        isAttackParticle.Play();
    }

    /// <summary>
    /// 攻撃アニメーションに組み込むメソッド
    /// </summary>
    public void AnimationEventDamage()
    {
        if (targetUnit != null)
        {
            targetUnit.OnDamage(this.attackPower);

            //ノックバック演出
            targetUnit.OnKnockBack(this.blowPower);
        }
    }
}
