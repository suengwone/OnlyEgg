using UnityEngine;
using UnityEngine.UI;
using EnumTypes;
using TMPro;
using System;
using System.Collections;

public class UIManager : BaseMonoBehaviour, IUI
{
    private IGame gameManager;

    public IGame GameManager
    {
        get => gameManager;
    }

    [SerializeField]
    private TMP_Text stateTitle;

    [SerializeField]
    private TMP_Text stateValue;

    [SerializeField]
    private TMP_Text fatigueText;
    public TMP_Text FatigueText => fatigueText;

    [SerializeField]
    private TMP_Text energyText;
    public TMP_Text EnergyText => energyText;

    [SerializeField]
    private TMP_Text experienceText;
    public TMP_Text ExperienceText => experienceText;

#region User Action Button
    [SerializeField]
    private Button eggButton;

    [SerializeField]
    private Button foodButton;
    public Button FoodButton => foodButton;

    [SerializeField]
    private Button exerciseButton;
    public Button ExerciseButton => exerciseButton;

    [SerializeField]
    private Button poopButton;
    public Button PoopButton => poopButton;

    [SerializeField]
    private Button sleepButton;
    public Button SleepButton => sleepButton;
#endregion


    void IManager.Initialize()
    {
        
    }

    public void SetEventOnClickEgg(Action OnClick)
    {
        eggButton.onClick.AddListener(() => OnClick?.Invoke() );
    }

    public void SetState(EggTypes state, Action onChangeState)
    {
        switch(state)
        {
            case EggTypes.None:
                stateTitle.text = "Nothing";
                stateValue.text = "to do";
                break;
            case EggTypes.Hatching:
                stateTitle.text = "Time : ";
                UpdateTime(6, 0, 0);
                onChangeState?.Invoke();
                break;
            case EggTypes.Sleep:
                stateTitle.text = "Sleep";
                stateValue.text = "Z Z Z";
                break;
            default:
                break;
        }
    }

    public void UpdateTime(TimeSpan? timeSpan)
    {
        stateValue.text = timeSpan == TimeSpan.Zero ? 
            "" :
            $"{timeSpan?.Hours:D2} : {timeSpan?.Minutes:D2} : {timeSpan?.Seconds:D2}";
    }
    private void UpdateTime(int hour, int minute, int second)
    {
        stateValue.text = $"{hour:D2} : {minute:D2} : {second:D2}";
    }
}