using EnumTypes;
using Globals;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Time = Utils.Time;

// TODO
// Data Binding to UI
public class DataManager : DataProcess
{
#region Variable
    private IGame gameManager;

    [SerializeField]
    private GameCurrentState gameCurrentState;

    [SerializeField]
    private ActionData actionDataValue;
    public ActionData ActionDataValue { get => actionDataValue; }

    private DateTime processEndTime;
    public TimeSpan GapTime => Time.GetGapTime(processEndTime);
#endregion

#region Monster Value
    public BindableProperty<float> Fatigue { get; } = new BindableProperty<float>();
    
    public BindableProperty<float> Energy { get; } = new BindableProperty<float>();

    public BindableProperty<float> Experience { get; } = new BindableProperty<float>();

    public BindableProperty<EggTypes> EggType { get; } = new BindableProperty<EggTypes>();

    public BindableProperty<bool> IsPause { get; } = new BindableProperty<bool>();
#endregion

#region Method
    public async override void Initialize()
    {
        gameCurrentState = GameCurrentState.Play;

        // Load Data
        var initDataPath = Constants.CacheDataFilePath + "init" + Constants.CacheFileExtesion;
        var initData = await Laod<LoadData>(initDataPath);

        initData ??= Resources.Load<LoadData>("TextValue/InitialValue");

        EggType.Value = initData.eggType;

        if (EggType.Value == EggTypes.Hatching)
        {
            var hatchingTime = Resources.Load<TimeSettings>("TextValue/HatchingTime");
            processEndTime = Time.GetCurrent() + hatchingTime.processTimeSpan;

            StartCoroutine(FlowTime());
        }

        Fatigue.Value = initData.fatigue;
        Energy.Value = initData.energy;
        Experience.Value = initData.experience;
    }

    public void SetupUIBindings(UIManager uiManagerRef)
    {
        Fatigue.BindTo(value => uiManagerRef.FatigueText.text = value.ToString());
        Energy.BindTo(value => uiManagerRef.EnergyText.text = value.ToString());
        Experience.BindTo(value => uiManagerRef.ExperienceText.text = value.ToString());

        EggType.BindTo(value => 
        {
            uiManagerRef.SetState(value, null);
        });

        uiManagerRef.SetEventOnClickEgg(() => 
        {
            if (EggType.Value == EggTypes.Hatching)
            {
                processEndTime -= Time.OneSecond;

                TimeUpdate?.Invoke(processEndTime - Time.GetCurrent());
            }
        });

        actionDataValue = Resources.Load<ActionData>("TextValue/ActionValue");

        uiManagerRef.FoodButton.onClick.AddListener(() => 
        {
            Fatigue.Value += actionDataValue.foodValue.fatigue;
            Energy.Value += actionDataValue.foodValue.energy;
            Experience.Value += actionDataValue.foodValue.experience;
        });

        uiManagerRef.ExerciseButton.onClick.AddListener(() => 
        {
            Fatigue.Value += actionDataValue.exerciseValue.fatigue;
            Energy.Value += actionDataValue.exerciseValue.energy;
            Experience.Value += actionDataValue.exerciseValue.experience;
        });

        uiManagerRef.PoopButton.onClick.AddListener(() => 
        {
            Fatigue.Value += actionDataValue.poopValue.fatigue;
            Energy.Value += actionDataValue.poopValue.energy;
            Experience.Value += actionDataValue.poopValue.experience;   
        });

        uiManagerRef.SleepButton.onClick.AddListener(() => 
        {
            Fatigue.Value += actionDataValue.sleepValue.fatigue;
            Energy.Value += actionDataValue.sleepValue.energy;
            Experience.Value += actionDataValue.sleepValue.experience;

            EggType.Value = EggTypes.Sleep;
        });

        TimeUpdate = (timeSpan) => uiManagerRef.UpdateTime(timeSpan);
    }

    private Action<TimeSpan> TimeUpdate;
    private IEnumerator FlowTime()
    {
        WaitForSecondsRealtime waitForSecondRealtime = new WaitForSecondsRealtime(1);

        while (true)
        {
            yield return waitForSecondRealtime;
            
            var remainTime = processEndTime - Time.GetCurrent();

            if (remainTime <= TimeSpan.Zero)
            {
                EggType.Value = EggTypes.None;
                
                // Switch Egg Image to Monster Image

                yield break;
            }
            else
            {
                TimeUpdate?.Invoke(remainTime);
            }
        }
    }

    // public void SetPause(bool isPaused)
    // {
    //     this.isPaused = isPaused;

    //     if (this.isPaused)
    //     {
    //         gameCurrentState = GameCurrentState.Pause;
    //     }
    //     else
    //     {
    //         gameCurrentState = GameCurrentState.Play;
    //     }
    // }
#endregion
}