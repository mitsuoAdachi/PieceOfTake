using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnCenterPoint;

    private int randomAllyUnitIndex;
    private int randomEnemyUnitIndex;

    private List<UnitController> allyUnitList = new List<UnitController>();
    private List<UnitController> enemyUnitList = new List<UnitController>();

    private GameManager gameManager;

    private void Start()
    {
        StartCoroutine(EnemySpawnLoop());
        StartCoroutine(AllySpawnLoop());
    }

    public IEnumerator AllySpawnLoop()
    {
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            //ユニットを生成する範囲
            var radius = new Vector3(10, 0);
            var spawnCircle = Quaternion.Euler(0, Random.Range(0, 360), 0) * radius;
            var spawnPosition = spawnCenterPoint.transform.position + spawnCircle;

            //Navmesh上のランダムな位置にユニットを指定秒数毎に生成し続ける
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                randomAllyUnitIndex = Random.Range(0, 7);

                UnitController generateAllyUnit = Instantiate(DataBase.instance.allyUnitDatas[randomAllyUnitIndex].UnitPrefab, navMeshHit.position, Quaternion.identity);
                DataBase.instance.GenerateAllyList.Add(generateAllyUnit);
                generateAllyUnit.SetupUnitStateEnemy(DataBase.instance.allyUnitDatas);
                generateAllyUnit.StartMoveUnit(gameManager, DataBase.instance.GenerateEnemyList);
            }

            yield return new WaitForSeconds(3);
        }

    }

    public IEnumerator EnemySpawnLoop()
    {
        if(TryGetComponent(out gameManager) == true)
        {
            Debug.Log(gameManager);
        }

        yield return new WaitForSeconds(1);

        while (true)
        {
            //ユニットを生成する範囲
            var radius = new Vector3(10, 0);
            var spawnCircle = Quaternion.Euler(0, Random.Range(0, 360), 0) * radius;
            var spawnPosition = spawnCenterPoint.transform.position + spawnCircle;

            //Navmesh上のランダムな位置にユニットを指定秒数毎に生成し続ける
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                randomEnemyUnitIndex = Random.Range(0, 3);

                UnitController generateEnemyUnit = Instantiate(DataBase.instance.enemyUnitDatas[randomEnemyUnitIndex].UnitPrefab, navMeshHit.position, Quaternion.identity);
                DataBase.instance.GenerateEnemyList.Add(generateEnemyUnit);
                generateEnemyUnit.SetupUnitStateEnemy(DataBase.instance.enemyUnitDatas);
                generateEnemyUnit.StartMoveUnit(gameManager, DataBase.instance.GenerateAllyList);

            }

            yield return new WaitForSeconds(2);
        }

    }

}
