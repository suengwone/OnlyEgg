using UnityEngine;
using Time = Utils.Time;

public class GameManager : BaseMonoBehaviour, IGame
{
    [SerializeField]
    private UIManager uiManager;
    
    [SerializeField]
    private DataManager dataManager;

    [SerializeField]
    private TimeManager timeManager;

    private void Start()
    {
        (this as IManager).Initialize();
    }

    void IManager.Initialize()
    {
        dataManager.SetupUIBindings(uiManager);

        (uiManager as IManager).Initialize();
        (dataManager as IManager).Initialize();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        dataManager.IsPause.Value = pauseStatus;

        // if (pauseStatus)
        // {
        //     dataManager.SetPause(true);

        //     var pauseTimeStamp = Time.GetCurrent();

        //     Debug.Log($"Paues Time : {pauseTimeStamp.Hour:D2} : {pauseTimeStamp.Minute:D2} : {pauseTimeStamp.Second:D2}");

        //     timeManager.PauseGame();
        // }
        // else
        // {
        //     if (dataManager.IsPaused)
        //     {
        //         dataManager.SetPause(false);

        //         var gap = dataManager.GapTime;

        //         Debug.Log(string.Format($"Gap Time : {gap.Hours:D2} : {gap.Minutes:D2} : {gap.Seconds:D2}"));

        //         timeManager.ResumeGame();
        //     }
        // }
    }
}
