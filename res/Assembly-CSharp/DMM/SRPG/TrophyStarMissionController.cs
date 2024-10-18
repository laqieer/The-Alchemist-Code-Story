// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyStarMissionController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Daily宝箱を選択", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Weekly宝箱を選択", FlowNode.PinTypes.Output, 102)]
  public class TrophyStarMissionController : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private Text DailyStarNum;
    [SerializeField]
    private GameObject DailyGoSelParent;
    [SerializeField]
    private TrophyStarMissionDailyItem DailySelItemTemplate;
    [SerializeField]
    private Slider DailyStarPointGauge;
    [Space(5f)]
    [SerializeField]
    private Text WeeklyStarNum;
    [SerializeField]
    private TrophyStarMissionWeeklyItem[] WeeklyItems;
    [SerializeField]
    private Text WeeklyEndDate;
    private const int PIN_IN_INIT = 1;
    private const int PIN_IN_REFRESH = 2;
    private const int PIN_OUT_DAILY_TREASURE_SELECTED = 101;
    private const int PIN_OUT_WEEKLY_TREASURE_SELECTED = 102;
    private TrophyWindow mTrophyWindowComponent;

    private TrophyWindow TrophyWindowComponent
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrophyWindowComponent, (UnityEngine.Object) null))
          this.mTrophyWindowComponent = ((Component) this).GetComponentInParent<TrophyWindow>();
        return this.mTrophyWindowComponent;
      }
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      this.Refresh(true);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void Refresh(bool is_init = false)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.Player == null || instance.Player.TrophyStarMissionInfo == null)
        return;
      PlayerData.TrophyStarMission trophyStarMissionInfo = instance.Player.TrophyStarMissionInfo;
      if (trophyStarMissionInfo.Daily != null)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.DailyStarNum))
          this.DailyStarNum.text = trophyStarMissionInfo.Daily.StarNum.ToString();
        int num = 0;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TrophyWindowComponent))
          num = this.TrophyWindowComponent.GetTrophyStarMissionDailyStarCount();
        if (num > 0)
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.DailyStarPointGauge))
            this.DailyStarPointGauge.value = (float) trophyStarMissionInfo.Daily.StarNum / (float) num;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.DailyGoSelParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.DailySelItemTemplate))
          {
            ((Component) this.DailySelItemTemplate).gameObject.SetActive(false);
            GameUtility.DestroyChildGameObjects(this.DailyGoSelParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
            {
              ((Component) this.DailySelItemTemplate).gameObject
            }));
            if (trophyStarMissionInfo.Daily.TsmParam != null)
            {
              List<TrophyStarMissionParam.StarSetParam> starSetList = trophyStarMissionInfo.Daily.TsmParam.StarSetList;
              for (int index = 0; index < starSetList.Count; ++index)
              {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                TrophyStarMissionController.\u003CRefresh\u003Ec__AnonStorey0 refreshCAnonStorey0 = new TrophyStarMissionController.\u003CRefresh\u003Ec__AnonStorey0();
                // ISSUE: reference to a compiler-generated field
                refreshCAnonStorey0.\u0024this = this;
                // ISSUE: reference to a compiler-generated field
                refreshCAnonStorey0.tsmdi = UnityEngine.Object.Instantiate<TrophyStarMissionDailyItem>(this.DailySelItemTemplate, this.DailyGoSelParent.transform, false);
                // ISSUE: reference to a compiler-generated field
                Slider component = ((Component) refreshCAnonStorey0.tsmdi).GetComponent<Slider>();
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
                {
                  // ISSUE: reference to a compiler-generated field
                  UnityEngine.Object.Destroy((UnityEngine.Object) refreshCAnonStorey0.tsmdi);
                }
                else
                {
                  component.value = Mathf.Min((float) (int) starSetList[index].RequireStar / (float) num, 1f);
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: method pointer
                  refreshCAnonStorey0.tsmdi.SetItem(index, new UnityAction((object) refreshCAnonStorey0, __methodptr(\u003C\u003Em__0)));
                  // ISSUE: reference to a compiler-generated field
                  ((Component) refreshCAnonStorey0.tsmdi).gameObject.SetActive(true);
                }
              }
            }
          }
        }
      }
      if (trophyStarMissionInfo.Weekly != null)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WeeklyStarNum))
          this.WeeklyStarNum.text = trophyStarMissionInfo.Weekly.StarNum.ToString();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WeeklyEndDate))
        {
          int yyMmDd = trophyStarMissionInfo.Weekly.YyMmDd;
          DateTime dateTime = new DateTime(2000 + yyMmDd / 10000, yyMmDd % 10000 / 100, yyMmDd % 100).AddDays(6.0);
          this.WeeklyEndDate.text = string.Format(LocalizedText.Get("sys.TROPHY_LIMITED_TIME"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day);
        }
      }
      if (this.WeeklyItems == null)
        return;
      for (int index = 0; index < this.WeeklyItems.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TrophyStarMissionController.\u003CRefresh\u003Ec__AnonStorey1 refreshCAnonStorey1 = new TrophyStarMissionController.\u003CRefresh\u003Ec__AnonStorey1();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey1.\u0024this = this;
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey1.tsmwi = this.WeeklyItems[index];
        // ISSUE: reference to a compiler-generated field
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) refreshCAnonStorey1.tsmwi))
        {
          if (is_init)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            refreshCAnonStorey1.tsmwi.SetItem(index, new UnityAction((object) refreshCAnonStorey1, __methodptr(\u003C\u003Em__0)));
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey1.tsmwi.Refresh();
          }
        }
      }
    }

    public void OnDailySelectItem(TrophyStarMissionDailyItem item)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) item))
        return;
      TrophyStarMissionParam.SelectDailyTreasureIndex = item.Index;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void OnWeeklySelectItem(TrophyStarMissionWeeklyItem item)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) item))
        return;
      TrophyStarMissionParam.SelectWeeklyTreasureIndex = item.Index;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
      {
        if (pinID != 2)
          return;
        this.Refresh();
      }
      else
        this.Init();
    }
  }
}
