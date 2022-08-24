using UnityEngine;

public class StageLevelIndex : MonoBehaviour
{
    //ユニット選択ボタンのナンバリング
    [SerializeField]
    private int stageLvIndex;
    public int StageLvIndex { get => StageLvIndex; }

    [SerializeField]
    public Transform[] prefabTran;
}