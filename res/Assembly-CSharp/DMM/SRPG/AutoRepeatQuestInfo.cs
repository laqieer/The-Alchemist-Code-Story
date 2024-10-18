// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "表示更新", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "初期化完了", FlowNode.PinTypes.Output, 100)]
  public class AutoRepeatQuestInfo : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_REFRESH = 20;
    private const int PIN_OUTPUT_INIT = 100;
    [SerializeField]
    private Text mBoxCountText;
    [SerializeField]
    private Text mBoxCountMaxText;
    [SerializeField]
    private Text mBoxCountMaxText2;
    [SerializeField]
    private Text mRestTimeText;
    [SerializeField]
    private Text mBoxFullOnText;
    [SerializeField]
    private Text mCurrentLapText;
    [SerializeField]
    private GameObject mDropItemEmpty;
    [SerializeField]
    private GameObject mRunningNavi;
    [SerializeField]
    private GameObject mFinishedNavi;
    [SerializeField]
    private Button mBoxAddButton;

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        this.Refresh();
      }
      else
        this.Init();
    }

    private void Init()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Refresh()
    {
      List<Unit.DropItem> dropItem = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.GetDropItem();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBoxCountText, (UnityEngine.Object) null))
        this.mBoxCountText.text = dropItem.Count.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBoxCountMaxText, (UnityEngine.Object) null))
        this.mBoxCountMaxText.text = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.Size.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBoxCountMaxText2, (UnityEngine.Object) null))
        this.mBoxCountMaxText2.text = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.Size.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentLapText, (UnityEngine.Object) null))
        this.mCurrentLapText.text = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.CurrentLap.ToString();
      GameUtility.SetGameObjectActive(this.mRunningNavi, false);
      GameUtility.SetGameObjectActive(this.mFinishedNavi, false);
      GameUtility.SetGameObjectActive(this.mDropItemEmpty, dropItem.Count <= 0);
      DataSource.Bind<QuestParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.FindQuest(MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.QuestIname));
      GameUtility.SetGameObjectActive((Component) this.mBoxAddButton, !MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestBoxSizeLimit());
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Update() => this.Update_RestTime();

    private void Update_RestTime()
    {
      GameUtility.SetGameObjectActive((Component) this.mBoxFullOnText, false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRestTimeText, (UnityEngine.Object) null))
        return;
      if (!MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
      {
        GameUtility.SetGameObjectActive((Component) this.mRestTimeText, false);
      }
      else
      {
        TimeSpan timeSpan = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.EndTimeEx - TimeManager.ServerTime;
        if (timeSpan.TotalSeconds > 0.0 && MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.AUTO_REPEAT_NOW)
        {
          GameUtility.SetGameObjectActive(this.mRunningNavi, true);
          GameUtility.SetGameObjectActive(this.mFinishedNavi, false);
          if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsBoxFull)
          {
            GameUtility.SetGameObjectActive((Component) this.mBoxFullOnText, true);
            GameUtility.SetGameObjectActive((Component) this.mRestTimeText, false);
          }
          else
          {
            GameUtility.SetGameObjectActive((Component) this.mBoxFullOnText, false);
            GameUtility.SetGameObjectActive((Component) this.mRestTimeText, true);
            this.mRestTimeText.text = string.Format(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_RESTTIME_FORMAT"), (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
          }
        }
        else
        {
          GameUtility.SetGameObjectActive(this.mRunningNavi, false);
          GameUtility.SetGameObjectActive(this.mFinishedNavi, true);
          if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsBoxFull)
          {
            GameUtility.SetGameObjectActive((Component) this.mBoxFullOnText, true);
            GameUtility.SetGameObjectActive((Component) this.mRestTimeText, false);
          }
          else
          {
            GameUtility.SetGameObjectActive((Component) this.mBoxFullOnText, false);
            GameUtility.SetGameObjectActive((Component) this.mRestTimeText, true);
            this.mRestTimeText.text = string.Format(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_RESTTIME_FORMAT"), (object) 0, (object) 0, (object) 0);
          }
        }
      }
    }
  }
}
