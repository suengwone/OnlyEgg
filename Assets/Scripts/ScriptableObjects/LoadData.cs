using System;
using EnumTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/LoadingSettings")]
public class LoadData : ScriptableObject
{
    // 플레이 초기화를 위한 데이터
    // 로드해야될 데이터
    
    public EggTypes eggType;
    
    // Sleep, Exercise
    [Range(0, 100)]
    public float fatigue;
    public float recoveryTimePerFatigue;
    
    // Eat
    public int maxEnergy;
    public float energy;

    public int level;
    public float exp;
}