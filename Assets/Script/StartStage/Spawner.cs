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


    public IEnumerator AllySpawnLoop(GameManager gameManager)
    {
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
                randomAllyUnitIndex = Random.Range(0, 7);

                UnitController generateAllyUnit = Instantiate(gameManager.allyUnitDatas[randomAllyUnitIndex].UnitPrefab, navMeshHit.position, Quaternion.identity);
                gameManager.GenerateAllyList.Add(generateAllyUnit);
                generateAllyUnit.SetupUnitStateEnemy(gameManager.allyUnitDatas);
                generateAllyUnit.StartMoveUnit(gameManager, gameManager.GenerateEnemyList);
            }

            yield return new WaitForSeconds(8);
        }

    }

    public IEnumerator EnemySpawnLoop(GameManager gameManager)
    {
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

                UnitController generateEnemyUnit = Instantiate(gameManager.enemyUnitDatas[randomEnemyUnitIndex].UnitPrefab, navMeshHit.position, Quaternion.identity);
                gameManager.GenerateEnemyList.Add(generateEnemyUnit);
                generateEnemyUnit.SetupUnitStateEnemy(gameManager.enemyUnitDatas);
                generateEnemyUnit.StartMoveUnit(gameManager, gameManager.GenerateAllyList);

            }

            yield return new WaitForSeconds(4);
        }

    }

}
