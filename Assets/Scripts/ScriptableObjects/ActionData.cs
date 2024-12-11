using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    // 액션별 수치 변화 데이터
    
    // 밥 먹기
    [Header("Eat")]
    public MonsterValue eatValue;

    [Header("Sleep")]
    public MonsterValue sleepValue;

    [Header("Exercise")]
    public MonsterValue exerciseValue;

    [Header("Toilet")]
    public MonsterValue toiletValue;
    
}

[Serializable]
public struct MonsterValue
{
    public float fatigue;
    public float energy;
    public float experience;
}