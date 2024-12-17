using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/TimeSettings")]
public class TimeSettings : ScriptableObject
{
    [SerializeField]
    public TimeHMS processTime;

    public TimeSpan processTimeSpan => new TimeSpan(processTime.hour, processTime.minute, processTime.second);
}

[Serializable]
public struct TimeHMS
{
    [Range(0, 24)]
    public int hour;
    [Range(0, 59)]
    public int minute;
    [Range(0, 59)]
    public int second;

    public TimeHMS(DateTime dateTime)
    {
        this.hour = dateTime.Hour;
        this.minute = dateTime.Minute;
        this.second = dateTime.Second;
    }
}