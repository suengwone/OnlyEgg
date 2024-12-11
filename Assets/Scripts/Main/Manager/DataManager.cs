using EnumTypes;
using Globals;
using System;
using Time = Utils.Time;

public class DataManager : DataProcess, IData
{
    private IGame gameManager;

    public IGame GameManager => gameManager;

    private GameCurrentState gameCurrentState;

    public DateTime CurrentTime => Time.GetCurrent();
    private DateTime processEndTime;
    public TimeSpan GapTime => Time.GetGapTime(processEndTime);

    private EggTypes eggType;
    public EggTypes EggType => eggType;
    public void SetEggType(EggTypes eggType, Action onChange)
    {
        if (this.eggType != eggType)
        {
            this.eggType = eggType;
            onChange?.Invoke();
        }
    }

    public void SetProcessEndTime(int hour, int minute, int second)
    {
        processEndTime = Time.GetCurrent();

        processEndTime.AddHours(hour);
        processEndTime.AddMinutes(minute);
        processEndTime.AddSeconds(second);
    }

    private bool isPaused;
    public bool IsPaused => isPaused;

    public void SetPause(bool isPaused)
    {
        this.isPaused = isPaused;

        if (this.isPaused)
        {
            gameCurrentState = GameCurrentState.Pause;
        }
        else
        {
            gameCurrentState = GameCurrentState.Play;
        }
    }

    public void Init(IGame gameManager)
    {
        gameCurrentState = GameCurrentState.Play;
        this.gameManager = gameManager;
    }

    async void IManager.Initialize()
    {
        var initDataPath = Constants.CacheDataFilePath + "" + Constants.CacheFileExtesion;

        var initData = await Laod<LoadData>(initDataPath);
    }
}
