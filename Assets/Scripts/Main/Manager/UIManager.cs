using UnityEngine;
using EnumTypes;
using System.Text;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : BaseMonoBehaviour, IUI
{
    [SerializeField]
    private TMP_Text stateTitle;
    private StringBuilder stateTitleText;

    [SerializeField]
    private TMP_Text stateValue;
    private StringBuilder stateValueText;

    [SerializeField]
    private Button eggButton;

    private IGame gameManager;

    public IGame GameManager
    {
        get => gameManager;
    }


    void IManager.Initialize()
    {
        throw new NotImplementedException();
    }

    public void Init(IGame gameManager)
    {
        this.gameManager = gameManager;

        stateTitleText = new ("");
        stateValueText = new ("");
    }

    public void SetEventOnClickEgg(Action onClick)
    {
        eggButton.onClick.RemoveAllListeners();
        eggButton.onClick.AddListener(() => 
        {
            onClick?.Invoke();
        });

    }

    public void SetState(EggTypes state, Action onChangeState)
    {
        stateTitleText?.Clear();
        stateValueText?.Clear();

        switch(state)
        {
            case EggTypes.None:
                stateTitleText.Insert(0, "Nothing");
                stateValueText.Insert(0, "to do");
                break;
            case EggTypes.Hatching:
                stateTitleText.Insert(0, "Time :");
                stateValueText.Insert(0, "04 : 00 : 00");
                UpdateTime(4, 0, 0);
                onChangeState?.Invoke();
                break;
            case EggTypes.Sleep:
                stateTitleText.Insert(0, "Sleep :");
                stateValueText.Insert(0, "06 : 00 : 00");
                UpdateTime(6, 0, 0);
                onChangeState?.Invoke();
                break;
            default:
                break;
        }

        stateTitle.text = stateTitleText.ToString();
        stateValue.text = stateValueText.ToString();
    }

    void IUI.Hide()
    {
        throw new NotImplementedException();
    }

    void IUI.Show()
    {
        throw new NotImplementedException();
    }

    private void UpdateTime(int hour, int minute, int second)
    {
        stateValueText.Clear();
        stateValueText.Insert(0, string.Format("{0:D2} : {1:D2} : {2:D2}", hour, minute, second));
        stateValue.text = stateValueText.ToString();
    }
}