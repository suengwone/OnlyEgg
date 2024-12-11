using System;
using System.Collections;
using UnityEngine;
using EnumTypes;

public class GameManager : BaseMonoBehaviour, IGame
{
    [SerializeField]
    private GameCurrentState gameCurrentState;

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private DataManager dataManager;

    private void Start()
    {
        (this as IManager).Initialize();
    }

    void IManager.Initialize()
    {
        gameCurrentState = GameCurrentState.Play;
        uiManager.Init(this);
        dataManager.Init(this);

        (uiManager as IManager).Initialize();
        (dataManager as IManager).Initialize();
    }

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

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            dataManager.SetPause(true);

            var pauseTimeStamp = dataManager.CurrentTime;

            Debug.Log($"Paues Time : {pauseTimeStamp.Hour:D2} : {pauseTimeStamp.Minute:D2} : {pauseTimeStamp.Second:D2}");
        }
        else
        {
            if (dataManager.IsPaused)
            {
                dataManager.SetPause(false);

                var gap = dataManager.GapTime;

                Debug.Log(string.Format($"Gap Time : {gap.Hours:D2} : {gap.Minutes:D2} : {gap.Seconds:D2}"));
            }
        }
    }
}
