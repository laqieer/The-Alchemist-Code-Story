// Decompiled with JetBrains decompiler
// Type: SRPG.EventScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventScript : ScriptableObject
  {
    public const string ScenePreviewName = "@EventScenePreview";
    public const string EventScriptDir = "Events/";
    public const int MAX_UNMANAGED_FILE = 10;
    private static Canvas mCanvas;
    private static RectTransform mEventRootTransform;
    private static Canvas mBackLogCanvas;
    private static GameObject mFrame;
    private static float mCurrMessageAutoTimer = 0.0f;
    private static bool mMessageAuto = false;
    public static EventScript.AutoFlagChange OnAutoFlagChanged;
    public static EventScript.OnBackLogButtonClick OnBackLogButtonClicked;
    private static EventBackLog mBackLog = (EventBackLog) null;
    public static string[] StrCompTypeRestHP = new string[7]
    {
      "==",
      "!=",
      ">",
      ">=",
      "<",
      "<=",
      string.Empty
    };
    public static string[] StrCalcTypeRestHP = new string[3]
    {
      "％",
      "　",
      string.Empty
    };
    public static string[] StrSkillTiming = new string[3]
    {
      "スキル使用前",
      "スキル使用後",
      string.Empty
    };
    public static string[] StrShortSkillTiming = new string[3]
    {
      "前",
      "後",
      string.Empty
    };
    public string QuestID;
    public EventScript.ScriptSequence[] mSequences = new EventScript.ScriptSequence[1];
    private static GameObject mButtonLog = (GameObject) null;
    private static GameObject mButtonAuto = (GameObject) null;

    public static Canvas Canvas => EventScript.mCanvas;

    public static RectTransform EventRootTransform => EventScript.mEventRootTransform;

    public static Canvas BackLogCanvas => EventScript.mBackLogCanvas;

    public static bool IsMessageAuto
    {
      get => EventScript.mMessageAuto;
      set
      {
        if (EventScript.mMessageAuto == value)
          return;
        if (value)
          GameUtility.SetNeverSleep();
        else
          GameUtility.SetDefaultSleepSetting();
        EventScript.mMessageAuto = value;
        EventScript.mCurrMessageAutoTimer = 0.0f;
        if (EventScript.OnAutoFlagChanged == null)
          return;
        EventScript.OnAutoFlagChanged(value);
      }
    }

    public static bool MessageAutoForward(float deltaTime)
    {
      if (!EventScript.IsMessageAuto)
        return false;
      EventScript.mCurrMessageAutoTimer += Time.deltaTime;
      if ((double) EventScript.mCurrMessageAutoTimer < (double) GameSettings.Instance.Message_AutoForwardSec)
        return false;
      EventScript.mCurrMessageAutoTimer = 0.0f;
      return true;
    }

    public static EventBackLog BackLog
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) EventScript.mBackLog, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLogCanvas, (UnityEngine.Object) null))
        {
          EventScript.mBackLog = UnityEngine.Object.Instantiate<EventBackLog>(AssetManager.Load<EventBackLog>("UI/BackLog"), ((Component) EventScript.BackLogCanvas).transform);
          ((Component) EventScript.mBackLog).gameObject.SetActive(false);
          EventScript.BackLogCanOpen = false;
        }
        return EventScript.mBackLog;
      }
    }

    public static bool BackLogCanOpen
    {
      set
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLog, (UnityEngine.Object) null))
          return;
        EventScript.BackLog.CanOpen = value;
      }
      get
      {
        return !UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLog, (UnityEngine.Object) null) && EventScript.BackLog.CanOpen;
      }
    }

    public static void AddBackLog(string name, string text)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLog, (UnityEngine.Object) null))
        return;
      EventScript.BackLog.Add(name, text);
    }

    public static void OpenBackLog()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLog, (UnityEngine.Object) null))
        return;
      EventScript.BackLog.Open();
    }

    public static void CloseBackLog()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLog, (UnityEngine.Object) null))
        return;
      EventScript.BackLog.Close();
    }

    public static IntVector2 ConvToIntVector2Grid(string str_grid)
    {
      IntVector2 intVector2Grid = new IntVector2(0, 0);
      if (!string.IsNullOrEmpty(str_grid))
      {
        string[] strArray = str_grid.Split(',');
        if (strArray != null)
        {
          if (strArray.Length > 0)
            int.TryParse(strArray[0], out intVector2Grid.x);
          if (strArray.Length > 1)
            int.TryParse(strArray[1], out intVector2Grid.y);
        }
      }
      return intVector2Grid;
    }

    public static string ConvToStringGrid(IntVector2 iv_grid)
    {
      return string.Format("{0},{1}", (object) iv_grid.x, (object) iv_grid.y);
    }

    public static EventScript.cRestHP ConvToObjectRestHP(string str_rest_hp)
    {
      EventScript.cRestHP objectRestHp = new EventScript.cRestHP();
      if (!string.IsNullOrEmpty(str_rest_hp))
      {
        string[] strArray1 = str_rest_hp.Split(',');
        if (strArray1 != null)
        {
          foreach (string str in strArray1)
          {
            char[] chArray = new char[1]{ '-' };
            string[] strArray2 = str.Split(chArray);
            if (strArray2 != null && strArray2.Length >= 3)
            {
              int result1 = 0;
              int result2 = 0;
              int result3 = 0;
              int.TryParse(strArray2[0], out result1);
              int.TryParse(strArray2[1], out result2);
              int.TryParse(strArray2[2], out result3);
              EventScript.cRestHP.Cond cond = new EventScript.cRestHP.Cond(result1, result2, result3);
              objectRestHp.mCondLists.Add(cond);
            }
          }
        }
      }
      return objectRestHp;
    }

    public static string ConvToStringRestHP(EventScript.cRestHP rest_hp)
    {
      string stringRestHp = string.Empty;
      if (rest_hp != null)
      {
        for (int index = 0; index < rest_hp.mCondLists.Count; ++index)
        {
          EventScript.cRestHP.Cond mCondList = rest_hp.mCondLists[index];
          if (index != 0)
            stringRestHp += ",";
          stringRestHp = stringRestHp + (object) (int) mCondList.mComp + "-" + (object) mCondList.mVal + "-" + (object) (int) mCondList.mCalc;
        }
      }
      return stringRestHp;
    }

    public void ResetTriggers()
    {
      for (int index = 0; index < this.mSequences.Length; ++index)
        this.mSequences[index].Triggered = false;
    }

    public static void DestroyCanvas()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) EventScript.mCanvas).gameObject);
      EventScript.IsMessageAuto = false;
      EventScript.mCanvas = (Canvas) null;
      EventScript.mEventRootTransform = (RectTransform) null;
      EventScript.mButtonLog = (GameObject) null;
      EventScript.mButtonAuto = (GameObject) null;
    }

    public static void DestroyBackLogCanvas()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) EventScript.mBackLogCanvas).gameObject);
      EventScript.mBackLog = (EventBackLog) null;
      EventScript.mBackLogCanvas = (Canvas) null;
    }

    private void CreateCanvas()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mCanvas, (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = new GameObject("EventCanvas", new System.Type[7]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (SRPG_CanvasScaler),
        typeof (CanvasStack),
        typeof (Button),
        typeof (NullGraphic),
        typeof (SetCanvasBounds)
      });
      EventScript.mCanvas = gameObject1.GetComponent<Canvas>();
      EventScript.mCanvas.renderMode = (RenderMode) 0;
      CanvasStack component = gameObject1.GetComponent<CanvasStack>();
      component.Priority = -1;
      component.Modal = true;
      if (SetCanvasBounds.IsWideScreen)
      {
        EventScript.mEventRootTransform = ((Component) EventScript.mCanvas).GetComponent<RectTransform>();
      }
      else
      {
        GameObject gameObject2 = new GameObject("CanvasBoundsPanel", new System.Type[1]
        {
          typeof (RectTransform)
        });
        gameObject2.transform.SetParent(gameObject1.transform, false);
        EventScript.mEventRootTransform = gameObject2.GetComponent<RectTransform>();
        gameObject1.GetComponent<SetCanvasBounds>().panel = EventScript.mEventRootTransform;
      }
      EventScript.IsMessageAuto = false;
    }

    private static GameObject ButtonLog
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) EventScript.mButtonLog, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mEventRootTransform, (UnityEngine.Object) null))
        {
          EventScript.mButtonLog = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/BtnLog"), (Transform) EventScript.mEventRootTransform);
          EventScript.mButtonLog.transform.SetAsLastSibling();
          EventScript.mButtonLog.gameObject.SetActive(false);
        }
        return EventScript.mButtonLog;
      }
    }

    private static GameObject ButtonAuto
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) EventScript.mButtonAuto, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mEventRootTransform, (UnityEngine.Object) null))
        {
          EventScript.mButtonAuto = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/BtnLogAuto"), (Transform) EventScript.mEventRootTransform);
          EventScript.mButtonAuto.transform.SetAsLastSibling();
          EventScript.mButtonAuto.gameObject.SetActive(false);
        }
        return EventScript.mButtonAuto;
      }
    }

    public static void ActiveButtons(bool enable)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.ButtonLog, (UnityEngine.Object) null))
        EventScript.ButtonLog.gameObject.SetActive(enable);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.ButtonAuto, (UnityEngine.Object) null))
        EventScript.ButtonAuto.gameObject.SetActive(enable);
      EventScript.SetButtonFront();
    }

    public static void SetButtonFront()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.ButtonLog, (UnityEngine.Object) null) && EventScript.ButtonLog.gameObject.activeSelf)
        EventScript.ButtonLog.transform.SetAsLastSibling();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.ButtonAuto, (UnityEngine.Object) null) || !EventScript.ButtonAuto.gameObject.activeSelf)
        return;
      EventScript.ButtonAuto.transform.SetAsLastSibling();
    }

    private void CreateBackLogCanvas()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mBackLogCanvas, (UnityEngine.Object) null))
        return;
      EventScript.mBackLogCanvas = new GameObject("EventBackLogCanvas", new System.Type[3]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (SRPG_CanvasScaler)
      }).GetComponent<Canvas>();
      EventScript.mBackLogCanvas.renderMode = (RenderMode) 0;
      EventScript.mBackLogCanvas.sortingOrder = (int) short.MaxValue;
      ((Component) EventScript.mBackLogCanvas).gameObject.SetActive(false);
    }

    public static void DestroyFrame()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.mFrame, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) EventScript.mFrame.gameObject);
      EventScript.mFrame = (GameObject) null;
    }

    public void PrepareAssets() => AssetManager.PrepareAssets("Events/" + ((UnityEngine.Object) this).name);

    public void PrepareUnmanagedAssets()
    {
      foreach (EventScript.ScriptSequence mSequence in this.mSequences)
      {
        foreach (EventAction action in mSequence.Actions)
        {
          string[] managedAssetListData = action.GetUnManagedAssetListData();
          if (managedAssetListData != null)
          {
            foreach (string name in managedAssetListData)
              AssetDownloader.AddUnManagedData(name);
          }
        }
      }
    }

    private EventScript.Sequence StartSequence(
      EventScript.TestCondition test,
      bool is_auto_forward = true,
      int startOffset = 0)
    {
      for (int index1 = 0; index1 < this.mSequences.Length; ++index1)
      {
        if (!this.mSequences[index1].Triggered && test(this.mSequences[index1]))
        {
          this.CreateCanvas();
          this.CreateBackLogCanvas();
          GameObject gameObject = new GameObject(((UnityEngine.Object) this).name);
          this.mSequences[index1].Triggered = true;
          EventScript.Sequence sequence = gameObject.AddComponent<EventScript.Sequence>();
          sequence.Actions = new EventAction[this.mSequences[index1].Actions.Count - startOffset];
          sequence.IsAutoForward = is_auto_forward;
          sequence.ParentSequence = this.mSequences[index1];
          for (int index2 = startOffset; index2 < this.mSequences[index1].Actions.Count; ++index2)
          {
            int index3 = index2 - startOffset;
            sequence.Actions[index3] = UnityEngine.Object.Instantiate<EventAction>(this.mSequences[index1].Actions[index2]);
            sequence.Actions[index3].Sequence = sequence;
            if (index2 > startOffset)
              sequence.Actions[index3 - 1].NextAction = sequence.Actions[index3];
          }
          return sequence;
        }
      }
      return (EventScript.Sequence) null;
    }

    public EventScript.Sequence OnPostMapLoad()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.PostMapLoad));
    }

    public EventScript.Sequence OnStart(int startOffset = 0, bool is_auto_forward = false)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Auto), is_auto_forward, startOffset);
    }

    public EventScript.Sequence OnQuestWin()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Win));
    }

    public EventScript.Sequence OnTurnStart(int turnCount)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.TurnStart && trigger.Turn == turnCount));
    }

    public EventScript.Sequence OnUnitStart(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitStart && controller.IsA(trigger.UnitName) && trigger.Turn == controller.Unit.TurnCount));
    }

    public EventScript.Sequence OnUnitHPChange(TacticsUnitController controller)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.HPBelowPercent && controller.IsA(trigger.UnitName) && controller.HPPercentage <= trigger.Percentage));
    }

    public EventScript.Sequence OnUnitTurnStart(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitTurnStart && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName) && trigger.Turn == controller.Unit.TurnCount));
    }

    public EventScript.Sequence OnUnitAppear(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitAppear && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName)));
    }

    public EventScript.Sequence OnUnitDead(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitDead && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName)));
    }

    public EventScript.Sequence OnStandbyGrid(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        IntVector2 intVector2Grid = EventScript.ConvToIntVector2Grid(trigger.GridXY);
        return trigger.Trigger == EventScript.ScriptSequence.StartConditions.StandbyGrid && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller) && controller.Unit.x == intVector2Grid.x && controller.Unit.y == intVector2Grid.y;
      }));
    }

    public EventScript.Sequence OnUnitRestHP(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        EventScript.cRestHP objectRestHp = EventScript.ConvToObjectRestHP(trigger.RestHP);
        bool flag = true;
        foreach (EventScript.cRestHP.Cond mCondList in objectRestHp.mCondLists)
        {
          int num = 0;
          switch (mCondList.mCalc)
          {
            case EventScript.cRestHP.Cond.CalcType.SCALE:
              num = controller.HPPercentage;
              break;
            case EventScript.cRestHP.Cond.CalcType.FIXED:
              num = (int) controller.Unit.CurrentStatus.param.hp;
              break;
          }
          switch (mCondList.mComp)
          {
            case EventScript.cRestHP.Cond.CompType.EQUAL:
              if (num != mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
            case EventScript.cRestHP.Cond.CompType.NOT_EQUAL:
              if (num == mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
            case EventScript.cRestHP.Cond.CompType.GREATER:
              if (num <= mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
            case EventScript.cRestHP.Cond.CompType.GREATER_EQUAL:
              if (num < mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
            case EventScript.cRestHP.Cond.CompType.LESS:
              if (num >= mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
            case EventScript.cRestHP.Cond.CompType.LESS_EQUAL:
              if (num > mCondList.mVal)
              {
                flag = false;
                break;
              }
              break;
          }
          if (!flag)
            break;
        }
        return trigger.Trigger == EventScript.ScriptSequence.StartConditions.RestHP && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName) && flag;
      }));
    }

    public EventScript.Sequence OnUseSkill(
      EventScript.SkillTiming timing,
      TacticsUnitController controller,
      SkillData skill,
      List<TacticsUnitController> TargetLists,
      bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger =>
      {
        bool flag = false;
        if (TargetLists != null)
        {
          foreach (TacticsUnitController targetList in TargetLists)
          {
            if (this.IsContainsUnit(trigger.TargetUnit, targetList, controller))
            {
              flag = true;
              break;
            }
          }
        }
        return trigger.Trigger == EventScript.ScriptSequence.StartConditions.UseSkill && (EventScript.SkillTiming) trigger.SkillTiming == timing && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller) && flag && this.IsContainsSkill(trigger.TargetSkill, skill);
      }));
    }

    public EventScript.Sequence OnUnitWithdraw(TacticsUnitController controller, bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.UnitWithdraw && (!trigger.IsFirstOnly || isFirstPlay) && controller.IsA(trigger.UnitName)));
    }

    public EventScript.Sequence OnQuestLose()
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.Lose));
    }

    public EventScript.Sequence OnRecvSkillElem(
      TacticsUnitController controller,
      EElement elem,
      bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.RecvSkillElem && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller) && elem == (EElement) trigger.SkillElem));
    }

    public EventScript.Sequence OnRecvSkillCond(
      TacticsUnitController controller,
      EUnitCondition cond,
      bool isFirstPlay)
    {
      return this.StartSequence((EventScript.TestCondition) (trigger => trigger.Trigger == EventScript.ScriptSequence.StartConditions.RecvSkillCond && (!trigger.IsFirstOnly || isFirstPlay) && this.IsContainsUnit(trigger.UnitName, controller) && (cond & (EUnitCondition) trigger.SkillCond) != (EUnitCondition) 0));
    }

    private bool IsContainsUnit(
      string unit_name,
      TacticsUnitController self,
      TacticsUnitController opp = null)
    {
      if (string.IsNullOrEmpty(unit_name) || UnityEngine.Object.op_Equality((UnityEngine.Object) self, (UnityEngine.Object) null))
        return false;
      bool flag = false;
      string[] strArray = unit_name.Split(',');
      if (strArray != null)
      {
        foreach (string id in strArray)
        {
          switch (id)
          {
            case "pall":
              if (self.Unit.Side == EUnitSide.Player)
              {
                flag = true;
                break;
              }
              break;
            case "eall":
              if (self.Unit.Side == EUnitSide.Enemy)
              {
                flag = true;
                break;
              }
              break;
            default:
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) opp, (UnityEngine.Object) null) && id == "other")
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self, (UnityEngine.Object) opp))
                {
                  flag = true;
                  break;
                }
                break;
              }
              if (self.IsA(id))
              {
                flag = true;
                break;
              }
              break;
          }
          if (flag)
            break;
        }
      }
      return flag;
    }

    private bool IsContainsSkill(string skill_name, SkillData skill)
    {
      if (string.IsNullOrEmpty(skill_name) || skill == null)
        return false;
      if (skill_name.IndexOf("sall") >= 0)
        return true;
      bool flag = false;
      string[] strArray = skill_name.Split(',');
      if (strArray != null)
      {
        foreach (string str in strArray)
        {
          if (str == skill.SkillParam.iname)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    public delegate void AutoFlagChange(bool enable);

    public delegate void OnBackLogButtonClick();

    [Serializable]
    public class ScriptSequence
    {
      [StringIsActorID]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.HPBelowPercent, EventScript.ScriptSequence.StartConditions.UnitTurnStart, EventScript.ScriptSequence.StartConditions.UnitAppear, EventScript.ScriptSequence.StartConditions.UnitDead, EventScript.ScriptSequence.StartConditions.StandbyGrid, EventScript.ScriptSequence.StartConditions.RestHP, EventScript.ScriptSequence.StartConditions.UseSkill, EventScript.ScriptSequence.StartConditions.UnitWithdraw, EventScript.ScriptSequence.StartConditions.RecvSkillElem, EventScript.ScriptSequence.StartConditions.RecvSkillCond})]
      public string UnitName;
      [Range(0.0f, 99f)]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.HPBelowPercent})]
      public int Percentage;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitStart, EventScript.ScriptSequence.StartConditions.TurnStart, EventScript.ScriptSequence.StartConditions.UnitTurnStart})]
      public int Turn;
      [StringIsGrid]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.StandbyGrid})]
      public string GridXY;
      [StringIsRestHP]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RestHP})]
      public string RestHP;
      [IntIsSkillTiming]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      public int SkillTiming;
      [StringIsTargetSkill]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      public string TargetSkill;
      [StringIsTargetUnit]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UseSkill})]
      public string TargetUnit;
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.UnitTurnStart, EventScript.ScriptSequence.StartConditions.UnitAppear, EventScript.ScriptSequence.StartConditions.UnitDead, EventScript.ScriptSequence.StartConditions.StandbyGrid, EventScript.ScriptSequence.StartConditions.RestHP, EventScript.ScriptSequence.StartConditions.UseSkill, EventScript.ScriptSequence.StartConditions.UnitWithdraw})]
      public bool IsFirstOnly;
      [IntIsSkillElem]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RecvSkillElem})]
      public int SkillElem;
      [IntIsSkillCond]
      [EventScript.ScriptSequence.ConditionAttr(new EventScript.ScriptSequence.StartConditions[] {EventScript.ScriptSequence.StartConditions.RecvSkillCond})]
      public int SkillCond = 1;
      public EventScript.ScriptSequence.StartConditions Trigger;
      public List<EventAction> Actions;
      [NonSerialized]
      public bool Triggered;

      public bool IsSavePlayBgmID
      {
        get
        {
          EventScript.ScriptSequence.StartConditions trigger = this.Trigger;
          switch (trigger)
          {
            case EventScript.ScriptSequence.StartConditions.Auto:
            case EventScript.ScriptSequence.StartConditions.Win:
            case EventScript.ScriptSequence.StartConditions.PostMapLoad:
              return false;
            default:
              if (trigger != EventScript.ScriptSequence.StartConditions.Lose)
                return true;
              goto case EventScript.ScriptSequence.StartConditions.Auto;
          }
        }
      }

      public class ConditionAttr : Attribute
      {
        public EventScript.ScriptSequence.StartConditions[] Conditions;

        public ConditionAttr(
          params EventScript.ScriptSequence.StartConditions[] conditions)
        {
          this.Conditions = conditions;
        }

        public bool Contains(
          EventScript.ScriptSequence.StartConditions condition)
        {
          return Array.IndexOf<EventScript.ScriptSequence.StartConditions>(this.Conditions, condition) >= 0;
        }
      }

      public enum StartConditions
      {
        Auto,
        UnitStart,
        HPBelowPercent,
        Win,
        TurnStart,
        PostMapLoad,
        UnitTurnStart,
        UnitAppear,
        UnitDead,
        StandbyGrid,
        RestHP,
        UseSkill,
        UnitWithdraw,
        Lose,
        RecvSkillElem,
        RecvSkillCond,
      }
    }

    public class cRestHP
    {
      public List<EventScript.cRestHP.Cond> mCondLists = new List<EventScript.cRestHP.Cond>();

      public class Cond
      {
        public EventScript.cRestHP.Cond.CompType mComp;
        public int mVal;
        public EventScript.cRestHP.Cond.CalcType mCalc;

        public Cond()
        {
        }

        public Cond(int comp, int val, int calc)
        {
          if (comp < 0 || comp >= 6)
            comp = 0;
          if (calc < 0 || calc >= 2)
            calc = 0;
          this.mComp = (EventScript.cRestHP.Cond.CompType) comp;
          this.mVal = val;
          this.mCalc = (EventScript.cRestHP.Cond.CalcType) calc;
        }

        public enum CompType
        {
          EQUAL,
          NOT_EQUAL,
          GREATER,
          GREATER_EQUAL,
          LESS,
          LESS_EQUAL,
          MAX,
        }

        public enum CalcType
        {
          SCALE,
          FIXED,
          MAX,
        }
      }
    }

    public enum SkillTiming
    {
      BEFORE,
      AFTER,
      MAX,
    }

    private delegate bool TestCondition(EventScript.ScriptSequence trigger);

    public class Sequence : MonoBehaviour
    {
      public EventScript Script;
      public EventAction[] Actions;
      private bool mReady;
      private bool mForceSkip;
      private int mLastActionIndex = -1;
      private List<int> mForceSkipIndex = new List<int>();
      private UnityAction mClickAction;
      public bool IsAutoForward;
      private float mTimerAutoForward;
      private int mCurIdxAutoForward;
      public EventScript.ScriptSequence ParentSequence;
      private GameObject mScene;
      public List<GameObject> SpawnedObjects = new List<GameObject>();

      public GameObject Scene
      {
        set
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScene, (UnityEngine.Object) null))
            UnityEngine.Object.Destroy((UnityEngine.Object) this.mScene);
          this.mScene = value;
        }
        get => this.mScene;
      }

      private void Start()
      {
        DebugUtility.LogWarning(((object) this).ToString());
        // ISSUE: method pointer
        this.mClickAction = new UnityAction((object) this, __methodptr(OnClick));
        ((UnityEvent) ((Component) EventScript.Canvas).GetComponent<Button>().onClick).AddListener(this.mClickAction);
        this.mTimerAutoForward = 0.0f;
        this.mCurIdxAutoForward = -1;
        this.StartCoroutine(this.PreloadAssetsAsync());
      }

      private void OnDestroy()
      {
        EventScript.IsMessageAuto = false;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mScene);
        Event2dAction_FlipStandChara.DiscardAll();
        Event2dAction_OperateStandChara.DiscardAll();
        EventDialogBubble.DiscardAll();
        EventDialogBubbleCustom.DiscardAll();
        EventStandCharaController2.DiscardAll();
        for (int index = 0; index < this.Actions.Length; ++index)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.Actions[index]);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.Canvas, (UnityEngine.Object) null))
        {
          ((UnityEvent) ((Component) EventScript.Canvas).GetComponent<Button>().onClick).RemoveListener(this.mClickAction);
          EventScript.DestroyCanvas();
        }
        EventScript.DestroyFrame();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.BackLogCanvas, (UnityEngine.Object) null))
          return;
        EventScript.DestroyBackLogCanvas();
      }

      [DebuggerHidden]
      private IEnumerator PreloadAssetsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new EventScript.Sequence.\u003CPreloadAssetsAsync\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      private void StartActions()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (!this.Actions[index].Skip)
            this.Actions[index].PreStart();
        }
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (!this.Actions[index].Skip)
          {
            this.Actions[index].enabled = true;
            break;
          }
        }
      }

      private void Update()
      {
        if (!this.mReady)
          return;
        if (this.mForceSkip)
        {
          for (int index1 = 0; index1 < this.mForceSkipIndex.Count; ++index1)
          {
            int index2 = this.mForceSkipIndex[index1];
            if (index2 < this.Actions.Length && this.Actions[index2].enabled)
            {
              EventAction action = this.Actions[index2];
              switch (action)
              {
                case EventAction_WaitTap _:
                case EventAction_Dialog _:
                case EventAction_Dialog2 _:
                case EventAction_FadeCanvas _:
                case EventAction_FadeScreen _:
                case EventAction_WaitSeconds _:
                  action.GoToEndState();
                  action.enabled = false;
                  this.mForceSkipIndex.RemoveAt(index1);
                  --index1;
                  continue;
                default:
                  action.Update();
                  if (!action.enabled)
                  {
                    this.mForceSkipIndex.RemoveAt(index1);
                    --index1;
                    continue;
                  }
                  continue;
              }
            }
            else
            {
              this.mForceSkipIndex.RemoveAt(index1);
              --index1;
            }
          }
          if (this.mForceSkipIndex.Any<int>())
            return;
          if (this.mLastActionIndex >= 0)
            this.StartForceSkip();
          this.mForceSkip = false;
          this.mForceSkipIndex.Clear();
          this.mLastActionIndex = -1;
        }
        else
        {
          int num = -1;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled)
            {
              this.Actions[index].Update();
              num = index;
            }
          }
          bool flag1 = UnityEngine.Object.op_Inequality((UnityEngine.Object) EventScript.ButtonAuto, (UnityEngine.Object) null) && EventScript.ButtonAuto.gameObject.activeInHierarchy;
          if (!this.IsAutoForward || flag1)
            return;
          SceneBattle instance = SceneBattle.Instance;
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || !instance.Battle.RequestAutoBattle && !instance.Battle.IsMultiPlay)
            return;
          if (this.mCurIdxAutoForward != num)
          {
            this.mCurIdxAutoForward = num;
            this.mTimerAutoForward = GameSettings.Instance.Quest.WaitTimeScriptEventForward;
          }
          if ((double) this.mTimerAutoForward <= 0.0)
            return;
          this.mTimerAutoForward -= Time.deltaTime;
          if ((double) this.mTimerAutoForward > 0.0)
            return;
          bool flag2 = false;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled && this.Actions[index].Forward())
              flag2 = true;
          }
          if (flag2)
            return;
          this.mTimerAutoForward = 1f;
        }
      }

      private void StartForceSkip()
      {
        int index1 = this.mLastActionIndex + 1;
        for (int index2 = index1; index2 < this.Actions.Length; ++index2)
        {
          this.Actions[index2].GoToEndState();
          this.Actions[index2].Skip = true;
        }
        if (index1 >= this.Actions.Length)
          return;
        this.Actions[index1].Forward();
      }

      public bool IsPlaying
      {
        get
        {
          if (!this.mReady)
            return true;
          for (int index = 0; index < this.Actions.Length; ++index)
          {
            if (this.Actions[index].enabled)
              return true;
          }
          return false;
        }
      }

      private void OnClick()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled)
            this.Actions[index].Forward();
        }
      }

      public void GoToEndState()
      {
        this.mForceSkip = true;
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled)
            this.mForceSkipIndex.Add(index);
        }
        if (this.mForceSkipIndex.Any<int>())
          this.mLastActionIndex = this.mForceSkipIndex[this.mForceSkipIndex.Count - 1];
        else
          this.mLastActionIndex = -1;
      }

      public void OnQuit()
      {
        EventScript.ActiveButtons(false);
        EventQuit eventQuit = EventQuit.Find();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) eventQuit, (UnityEngine.Object) null))
          ((Component) eventQuit).gameObject.SetActive(false);
        int index1 = -1;
        for (int index2 = 0; index2 < this.Actions.Length; ++index2)
        {
          if (this.Actions[index2].enabled)
            index1 = index2;
          else if (index1 != -1)
          {
            if (!(this.Actions[index2] is Event2dAction_QuitDisable) && !(this.Actions[index2] is Event2dAction_Scene))
              this.Actions[index2].Skip = true;
            else
              break;
          }
        }
        if (index1 == -1)
          return;
        this.Actions[index1].OnEventSkip();
      }

      public void OnQuitImmediate()
      {
        int index1 = -1;
        for (int index2 = 0; index2 < this.Actions.Length; ++index2)
        {
          if (this.Actions[index2].enabled)
          {
            this.Actions[index2].SkipImmediate();
            this.Actions[index2].Skip = true;
            index1 = index2;
          }
          else
            this.Actions[index2].Skip = true;
        }
        if (index1 == -1)
          return;
        this.Actions[index1].OnEventSkip();
      }

      public bool ReplaySkipButtonEnable()
      {
        for (int index = 0; index < this.Actions.Length; ++index)
        {
          if (this.Actions[index].enabled && !this.Actions[index].ReplaySkipButtonEnable())
            return false;
        }
        return true;
      }
    }
  }
}
