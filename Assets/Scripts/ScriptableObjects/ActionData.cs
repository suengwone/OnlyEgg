using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    // 액션별 수치 변화 데이터
    
    // 밥 먹기
    [Header("Food")]
    public MonsterValue foodValue;

    [Header("Sleep")]
    public MonsterValue sleepValue;

    [Header("Exercise")]
    public MonsterValue exerciseValue;

    [Header("Poop")]
    public MonsterValue poopValue;
    
}

[Serializable]
public struct MonsterValue
{
    public float fatigue;
    public float energy;
    public float experience;
}