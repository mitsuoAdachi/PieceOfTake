using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class UnitController : MonoBehaviour
{
    private List<UnitController> unitList = new List<UnitController>();

    [SerializeField]
    private UnitController targetUnit;
    public UnitController TargetUnit { get => targetUnit; }

    [SerializeField]
    private ParticleSystem attackParticle; //インスペクター上で攻撃エフェクトをアタッチ

    [SerializeField]
    LayerMask stageLayer;     //地上判定用のレイヤー(stage)をインスペクター上で設定

    public int intervalTimer;     //攻撃間隔用タイマー

    private int maxHp;

    public int unitNumber;　　　//ユニットの整理番号

    private float standerdDistanceValue = 1000;    //敵の距離を比較するための基準となる変数。適当な数値を代入

    public GameManager gameManager;
    private UIManager uiManager;

    private Rigidbody rigid;
    private NavMeshAgent agent;

    public bool isAttack = false;
    private bool isKnockBack = false;

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

    private void Awake()
    {
        //コンポーネントのキャッシュ
        rigid = GetComponent<Rigidbody>();
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
        intervalTime = unitDatas[uiManager.btnIndex].intervalTime;
        maxHp = hp;

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
        intervalTime = unitDatas[unitNumber].intervalTime;
        maxHp = hp;
    }

    /// <summary>
    /// MoveUnitをUnitController上でCoroutineで動かすためのメソッド
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="unitList"></param>
    public void StartMoveUnit(GameManager gameManager, List<UnitController> unitList)
    {
        this.unitList = unitList;
        this.gameManager = gameManager;

        StartCoroutine("OnMoveUnit");
    }

    /// <summary>
    /// ユニットの移動
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator OnMoveUnit()
    {
        //Debug.Log("監視開始");
        while (this.hp > 0)
        {
            //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する処理↓↓                 
            foreach (UnitController target in unitList)
            {
                if (gameManager.gameMode == GameManager.GameMode.Play && target != null )
                {
                    //攻撃間隔タイマーをMoveUnitメソッド内で加算
                    intervalTimer++;
                    
                    //UnitList内に登録してあるオブジェクトとの距離を測り変数に代入する
                    float nearTargetDistanceValue = Vector3.SqrMagnitude(target.transform.position - transform.position);

                    //基準値より小さければその数値を基準値に代入していき一番小さい数値が変数に残る。その数値を持つオブジェクトが一番近い敵となる
                    if (standerdDistanceValue > nearTargetDistanceValue)
                    {
                        standerdDistanceValue = nearTargetDistanceValue;

                        targetUnit = target;
                    }

                    if (targetUnit != null)
                    {
                        //Debug.Log(this.gameObject.name + targetUnit);
                        //Debug.Log(this.gameObject.name + agent);

                        //ナビメッシュを使用した移動
                        agent.SetDestination(targetUnit.transform.position);

                        //Debug.Log(anime);

                        anime.SetFloat(walkAnime, agent.velocity.sqrMagnitude);
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
    /// 一定間隔毎に攻撃アニメーションを実行
    /// </summary>
    /// <returns></returns>
    public void PreparateAttack()
    {
        if (this.hp > 0 && gameManager.gameMode == GameManager.GameMode.Play)
        {
            if (targetUnit.hp > 0)
            {
                if (intervalTimer > intervalTime)
                {
                    intervalTimer = 0;

                    anime.SetTrigger(attackAnime);
                }
            }
        }
    }

    /// <summary>
    /// アニメーションイベントでダメージを与えるメソッドとノックバックさせるメソッドを呼び出す
    /// </summary>
    public void AnimationEventDamage()
    {
        if (targetUnit != null)
        {
            targetUnit.OnDamage(this.attackPower);

            targetUnit.OnKnockBack(this.blowPower);
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
            gameManager.GenerateEnemyList.Remove(this);
            gameManager.GenerateAllyList.Remove(this);

            agent.enabled = true;
            agent.isStopped = true;
            targetUnit = null;
            isAttack = false;
            anime.SetTrigger(deadAnime);

            Destroy(this.gameObject, 3);
        }
    }

    /// <summary>
    /// ノックバック演出
    /// </summary>
    /// <param name="blowPower"></param>
    public void OnKnockBack(float blowPower)
    {
        anime.SetTrigger(knockBackAnime);

        //ステージ(Navmesh)外に出るように障害機能を一旦切る
        rigid.isKinematic = false;
        StopCoroutine("OnMoveUnit");
        agent.enabled = false;

        //ノックバックはFixedUpdateで動かす
        isKnockBack = true;
        StartCoroutine(RetrunOnMoveUnit());
    }

    /// <summary>
    /// ノックバックをFixedUpdateで動かす
    /// </summary>
    private void FixedUpdate()
    {
        if (isKnockBack)
        {
            rigid.AddForce(-transform.forward * blowPower, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// ノックバック後、ステージ上にいるかどうかで処理を変更
    /// </summary>
    /// <returns></returns>
    private IEnumerator RetrunOnMoveUnit()
    {
        yield return new WaitForSeconds(0.5f);

        if (JudgeGround() == true)
        {
            isKnockBack = false;
            rigid.isKinematic = true;
            agent.enabled = true;
            StartCoroutine("OnMoveUnit");
        }
        else
        {
            Debug.Log("空中です");
            isKnockBack = false;
        }
    }
    /// <summary>
    /// ノックバック後ユニットから下方向へRayを飛ばしstageLayerのオブジェクトに接触した場合はtrueを返す。
    /// </summary>
    /// <returns></returns>
    private bool JudgeGround()
    {
        Ray ray = new Ray(transform.position + transform.forward*0.2f + Vector3.up * 0.2f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up * 0.2f, Vector3.down,Color.red,1);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 4.0f, stageLayer))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 弓矢・魔法を発射する
    /// </summary>
    public void OnAttackPartical()
    {
        attackParticle.Play();
    }

}
