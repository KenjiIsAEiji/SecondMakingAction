using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ScoreManagementData",menuName = "ScoreManagementData")]
public class ScoreManagementData : ScriptableObject
{
    [Header("スコア基底ポイント設定")]
    public int BaseNomalPoint = 10;
    public int BaseLongRangePoint = 15;
    public int BaseLPRemaingPoint = 1000;
    public int BasePassingTimePoint = 10000;
}
