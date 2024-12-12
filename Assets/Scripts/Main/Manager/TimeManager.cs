using System;
using System.Collections;
using UnityEngine;

public class TimeManager : BaseMonoBehaviour
{
    public IEnumerator FlowTimeInRealTime(Action onFinish)
    {
        WaitForSecondsRealtime secondRealTime = new WaitForSecondsRealtime(1);

        while (true)
        {
            // second -= 1;

            // if (second < 0)
            // {
            //     second += 60;
            //     minute -= 1;

            //     if (minute < 0)
            //     {
            //         minute += 60;
            //         hour -= 1;

            //         if (hour < 0)
            //         {
            //             hour = 0;
            //             minute = 0;
            //             second = 0;
            //             break;
            //         }
            //     }
            // }

            // uiManager.UpdateTime();

            yield return secondRealTime;
        }

        onFinish?.Invoke();
    }
}
