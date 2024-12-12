using UnityEngine;
using EnumTypes;

public class GameManager : BaseMonoBehaviour, IGame
{
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
        // uiManager.Init(this);
        // dataManager.Init(this);

        (uiManager as IManager).Initialize();
        (dataManager as IManager).Initialize();
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

    public void Notify(NotifyType notifyType, string message)
    {
        switch (notifyType)
        {
            case NotifyType.Data:
            {
                var datas = message.Split(':');
                break;
            }
            case NotifyType.UI:
            {
                break;
            }
        }
    }
}
