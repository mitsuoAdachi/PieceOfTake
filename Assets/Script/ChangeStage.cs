using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    [SerializeField]
    private StageInfo stage;

    [SerializeField]
    private GameManager gameManager;

    int stageLv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StageChange());
    }

    private IEnumerator StageChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(stage.gameObject);
            stageLv++;
            yield return new WaitForSeconds(1);
            stage = Instantiate(gameManager.stageDatas[stageLv].stagPrefab);
        }
    }
}
