using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/TimeSettings")]
public class TimeSettings : ScriptableObject
{
    public TimeHMS processTime;

    public DateTime standardDateTime = DateTime.Now;
    public TimeHMS standardTimeHMS = new TimeHMS(DateTime.Now);
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