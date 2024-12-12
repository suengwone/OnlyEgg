using EnumTypes;
using Globals;
using System;
using UnityEngine;
using Time = Utils.Time;

public class DataManager : DataProcess
{
#region Variable
    private IGame gameManager;

    [SerializeField]
    private GameCurrentState gameCurrentState;

    [SerializeField]
    private EggTypes eggType;
    public EggTypes EggType => eggType;

    [SerializeField]
    private ActionData actionDataValue;

    public DateTime CurrentTime => Time.GetCurrent();
    private DateTime processEndTime;
    public TimeSpan GapTime => Time.GetGapTime(processEndTime);

    private bool isPaused;
    public bool IsPaused => isPaused;

#endregion

#region Monster Value
    public TextData<float> fatigue;

    public TextData<float> energy;

    public TextData<float> experience;
#endregion

#region Method
    public async override void Initialize()
    {
        gameCurrentState = GameCurrentState.Play;

        // Load Data
        var initDataPath = Constants.CacheDataFilePath + "init" + Constants.CacheFileExtesion;
        var initData = await Laod<LoadData>(initDataPath);

        initData ??= Resources.Load<LoadData>("TextValue/InitialValue");

        this.eggType = initData.eggType;

        actionDataValue = Resources.Load<ActionData>("TextValue/ActionValue");
    }

    // public void Init(IGame gameManager)
    // {
    //     gameCurrentState = GameCurrentState.Play;
    //     this.gameManager = gameManager;
    // }

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
#endregion
}

public class TextData<T>
{
    private Action<string> callback;
    public void SetUpCallback(Action<string> callback)
    {
        this.callback = callback;
    }

    private T text;
    public string Text
    {
        get => text.ToString();
        set
        {
            text = (T)Convert.ChangeType(value, typeof(T));
            callback.Invoke(value);
        }
    }
}