using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndex : MonoBehaviour
{
    //ユニット選択ボタンのナンバリング
    [SerializeField]
    private int btnIndex;
    public int BtnIndex { get => btnIndex; }
}