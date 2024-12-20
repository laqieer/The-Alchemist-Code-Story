﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "Set ranking quest", FlowNode.PinTypes.Input, 0)]
  public class RankingQuestButton : MonoBehaviour, IGameParameter, IFlowInterface
  {
    private const int PERIOD_STATE_ICON_INDEX_OPEN = 0;
    private const int PERIOD_STATE_ICON_INDEX_WAIT = 1;
    private const int PERIOD_STATE_ICON_INDEX_VISIBLE = 2;
    [SerializeField]
    private ImageArray m_PeriodStateIcon;
    [SerializeField]
    private Text m_Time;

    private void Start()
    {
      this.UpdateValue();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      RankingQuestParam dataOfClass = DataSource.FindDataOfClass<RankingQuestParam>(this.gameObject, (RankingQuestParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedQuestID = dataOfClass.iname;
      GlobalVars.SelectedRankingQuestParam = dataOfClass;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void UpdateValue()
    {
      RankingQuestParam dataOfClass = DataSource.FindDataOfClass<RankingQuestParam>(this.gameObject, (RankingQuestParam) null);
      if (dataOfClass == null)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        DateTime serverTime = TimeManager.ServerTime;
        this.gameObject.SetActive(true);
        if (dataOfClass.scheduleParam.IsAvailablePeriod(serverTime))
        {
          if ((UnityEngine.Object) this.m_PeriodStateIcon != (UnityEngine.Object) null)
            this.m_PeriodStateIcon.ImageIndex = 0;
          TimeSpan timeSpan = dataOfClass.scheduleParam.endAt - serverTime;
          if (!((UnityEngine.Object) this.m_Time != (UnityEngine.Object) null))
            return;
          if (timeSpan.TotalDays >= 1.0)
            this.m_Time.text = LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_DAY", new object[1]
            {
              (object) timeSpan.Days
            });
          else if (timeSpan.TotalHours >= 1.0)
            this.m_Time.text = LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_HOUR", new object[1]
            {
              (object) timeSpan.Hours
            });
          else
            this.m_Time.text = LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_MINUTE", new object[1]
            {
              (object) Mathf.Max(timeSpan.Minutes, 0)
            });
          this.m_Time.gameObject.SetActive(true);
        }
        else if (dataOfClass.scheduleParam.IsAvailableVisiblePeriod(serverTime))
        {
          if ((UnityEngine.Object) this.m_PeriodStateIcon != (UnityEngine.Object) null)
            this.m_PeriodStateIcon.ImageIndex = 2;
          if (!((UnityEngine.Object) this.m_Time != (UnityEngine.Object) null))
            return;
          this.m_Time.gameObject.SetActive(false);
        }
        else
          this.gameObject.SetActive(false);
      }
    }
  }
}
