// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceQuestEventMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Refresh", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(41, "難易度変更", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(131, "OpenStarReward", FlowNode.PinTypes.Output, 131)]
  [FlowNode.Pin(141, "クエスト選択された", FlowNode.PinTypes.Output, 141)]
  [FlowNode.Pin(142, "クエスト開催期間外", FlowNode.PinTypes.Output, 142)]
  [FlowNode.Pin(143, "キークエスト終了", FlowNode.PinTypes.Output, 143)]
  public class AdvanceQuestEventMap : MonoBehaviour, IFlowInterface
  {
    public const int PIN_IN_INIT = 1;
    public const int PIN_IN_REFRESH = 11;
    public const int PIN_IN_CHANGE_DIFFICULTY = 41;
    public const int PIN_OUT_INIT = 101;
    public const int PIN_OUT_OPEN_STAR_REWARD = 131;
    public const int PIN_OUT_SELECT_QUEST = 141;
    public const int PIN_OUT_QUEST_OUT_OF_PERIOD = 142;
    public const int PIN_OUT_KEY_QUEST_OUT_OF_PERIOD = 143;
    public const string ADVANCE_STAR_REWARD_SVB_KEY = "ADVANCE_STAR_REWARD";
    public const string ADVANCE_STAR_POINT_SVB_KEY = "ADVANCE_STAR_POINT";
    public const string ADVANCE_QUEST_PARAM_KEY = "ADVANCE_QUEST_PARAM";
    private Dictionary<QuestDifficulties, List<QuestParam>> mQuestList = new Dictionary<QuestDifficulties, List<QuestParam>>();
    private Dictionary<QuestDifficulties, List<AdvanceQuestMapSymbol>> mQuestSymbols = new Dictionary<QuestDifficulties, List<AdvanceQuestMapSymbol>>();
    private Dictionary<QuestDifficulties, List<QuestParam>> mQuestAvailable = new Dictionary<QuestDifficulties, List<QuestParam>>();
    [SerializeField]
    private AnimationCurve mScrollCurve;
    [SerializeField]
    private Transform mBGParent;
    [SerializeField]
    private Transform mPreviewParent;
    [SerializeField]
    private AdvanceQuestMapSymbol mRefQuestSymbol;
    [SerializeField]
    private List<GameObject> mRefQuestCursor;
    [SerializeField]
    private AdvanceQuestEventMapStarIcon StarRewardIconTemplate;
    [SerializeField]
    private Slider StarPointGauge;
    [SerializeField]
    private Text StarPointCurrent;
    [SerializeField]
    private Text StarPointMax;
    [SerializeField]
    private Text ChapterTitle;
    [SerializeField]
    private Text ChapterDifficulty;
    [SerializeField]
    private ImageArray TitleBack;
    [SerializeField]
    private List<ImageArray> ButtonDifficulty;
    [SerializeField]
    private List<string> LocalizeDifficultyKey;
    [SerializeField]
    private SRPG_ScrollRect mRefScrollRect;
    [SerializeField]
    private float mScrollTime = 0.5f;
    private List<AdvanceQuestEventMapStarIcon> StarRewardIconList = new List<AdvanceQuestEventMapStarIcon>();
    private int mChapterStarCurrent;
    private AdvanceQuestMapIcon mCurrentSelectIcon;
    private AdvanceEventManager mEventManager;
    private AdvanceEventParam mEventParam;
    private QuestDifficulties[] mDifficultyList;

    private AdvanceEventManager EventManager
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mEventManager, (UnityEngine.Object) null))
          this.mEventManager = AdvanceEventManager.Instance;
        return this.mEventManager;
      }
    }

    private AdvanceEventParam EventParam
    {
      get
      {
        if (this.mEventParam == null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventManager, (UnityEngine.Object) null))
          this.mEventParam = MonoSingleton<GameManager>.Instance.GetAdvanceEventParam(this.EventManager.CurrentEventParam.Iname);
        return this.mEventParam;
      }
    }

    private QuestDifficulties[] DifficultyList
    {
      get
      {
        if (this.mDifficultyList == null)
        {
          List<QuestDifficulties> questDifficultiesList = new List<QuestDifficulties>();
          for (int index = 0; index < 3; ++index)
            questDifficultiesList.Add((QuestDifficulties) index);
          this.mDifficultyList = questDifficultiesList.ToArray();
        }
        return this.mDifficultyList;
      }
    }

    private List<QuestParam> GetDifficultyQuestList(QuestDifficulties difficulty)
    {
      if (this.EventParam == null)
        return new List<QuestParam>();
      if (!this.mQuestList.ContainsKey(difficulty))
      {
        List<QuestParam> questParamList = this.EventParam.GetQuestList(difficulty) ?? new List<QuestParam>();
        this.mQuestList[difficulty] = questParamList;
      }
      return this.mQuestList[difficulty];
    }

    private bool Init()
    {
      if (this.EventParam == null)
        return false;
      AdvanceEventModeInfoParam modeInfo = this.EventParam.GetModeInfo(this.EventManager.StageDifficulty);
      if (modeInfo == null || this.EventManager.AdvanceAssets.StageBG.Length <= modeInfo.ModeUiIndex)
        return false;
      this.EventManager.LoadAssets<GameObject>(this.EventManager.AdvanceAssets.StageBG[modeInfo.ModeUiIndex], new AdvanceEventManager.LoadAssetCallback<GameObject>(this.DownLoadedBG));
      AdvanceManager.CurrentChapterParam = this.EventParam.ChapterParam;
      GlobalVars.SelectedChapter.Set(this.EventParam.ChapterParam.iname);
      return true;
    }

    private List<QuestDifficulties> GetAvailableDifficultyList()
    {
      List<QuestDifficulties> availableDifficultyList = new List<QuestDifficulties>();
      for (int index = 0; index < this.DifficultyList.Length; ++index)
      {
        if (this.IsAvailableDifficulty(this.DifficultyList[index]))
          availableDifficultyList.Add(this.DifficultyList[index]);
      }
      return availableDifficultyList;
    }

    private bool Refresh()
    {
      if (this.EventParam == null)
        return false;
      AdvanceEventModeInfoParam modeInfo = this.EventParam.GetModeInfo(this.EventManager.StageDifficulty);
      if (modeInfo == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRefQuestSymbol, (UnityEngine.Object) null))
        return false;
      ((Component) this.mRefQuestSymbol).gameObject.SetActive(false);
      Transform parent = ((Component) this.mRefQuestSymbol).transform.parent;
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.EventManager.StageDifficulty));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterTitle, (UnityEngine.Object) null))
        this.ChapterTitle.text = this.EventParam.Name;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterDifficulty, (UnityEngine.Object) null))
        this.ChapterDifficulty.text = LocalizedText.Get(this.LocalizeDifficultyKey[index1]);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleBack, (UnityEngine.Object) null) && this.TitleBack.Images.Length > index1)
        this.TitleBack.ImageIndex = index1;
      for (int index2 = parent.childCount - 1; index2 >= 0; --index2)
        ((Component) parent.GetChild(index2)).gameObject.SetActive(false);
      this.RefreshDifficultyButton();
      List<QuestParam> difficultyQuestList = this.GetDifficultyQuestList(this.EventManager.StageDifficulty);
      if (!this.mQuestSymbols.ContainsKey(this.EventManager.StageDifficulty))
      {
        List<AdvanceQuestMapSymbol> advanceQuestMapSymbolList = new List<AdvanceQuestMapSymbol>();
        for (int index3 = 0; index3 < difficultyQuestList.Count; ++index3)
        {
          QuestParam questParam = difficultyQuestList[index3];
          if (questParam != null)
          {
            AdvanceQuestMapSymbol advanceQuestMapSymbol = UnityEngine.Object.Instantiate<AdvanceQuestMapSymbol>(this.mRefQuestSymbol, parent);
            advanceQuestMapSymbol.Init(questParam, index3 % 2 == 0, index3 >= difficultyQuestList.Count - 1, new AdvanceQuestMapIcon.OnClickIcon(this.OnClickSymbolIcon), new AdvanceQuestMapIcon.OnClickLockedIcon(this.OnClickLockedIcon));
            advanceQuestMapSymbolList.Add(advanceQuestMapSymbol);
          }
        }
        this.mQuestSymbols[this.EventManager.StageDifficulty] = advanceQuestMapSymbolList;
      }
      List<AdvanceQuestMapSymbol> mQuestSymbol = this.mQuestSymbols[this.EventManager.StageDifficulty];
      foreach (Component component in mQuestSymbol)
        component.gameObject.SetActive(true);
      List<QuestParam> availableQuest = this.GetAvailableQuest(this.EventManager.StageDifficulty);
      if (availableQuest.Count >= 1)
      {
        AdvanceQuestMapIcon icon = (AdvanceQuestMapIcon) null;
        if (AdvanceEventManager.Instance.RestorePointIsStage || AdvanceEventManager.Instance.JumpFromMission)
        {
          QuestParam selected_quest = (QuestParam) null;
          if (AdvanceEventManager.Instance.JumpFromMission)
          {
            selected_quest = availableQuest.Find((Predicate<QuestParam>) (item => item.iname == GlobalVars.SelectedQuestID));
            GlobalVars.SelectedQuestID = string.Empty;
          }
          else
            selected_quest = availableQuest.Find((Predicate<QuestParam>) (item => item.iname == (string) GlobalVars.LastPlayedQuest));
          if (selected_quest != null)
          {
            AdvanceQuestMapSymbol advanceQuestMapSymbol = mQuestSymbol.Find((Predicate<AdvanceQuestMapSymbol>) (item => item.Icon.QuestParam == selected_quest));
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) advanceQuestMapSymbol, (UnityEngine.Object) null))
            {
              icon = advanceQuestMapSymbol.Icon;
              if (AdvanceEventManager.Instance.JumpFromMission)
                this.OnClickSymbolIcon(icon, selected_quest);
              else
                this.SetSelectedQuest(icon, this.EventManager.StageDifficulty);
            }
          }
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null))
          this.SetSelectedQuest(mQuestSymbol[availableQuest.Count - 1].Icon, this.EventManager.StageDifficulty);
        AdvanceEventManager.Instance.SetRestorePointIsStage(false);
        AdvanceEventManager.Instance.SetJumpFromMission(false);
      }
      int maxStarNum = this.EventParam.GetMaxStarNum(difficultyQuestList);
      if (maxStarNum <= 0)
        return false;
      this.mChapterStarCurrent = this.EventParam.GetNowStarNum(difficultyQuestList);
      if (modeInfo.StarParam == null || modeInfo.StarParam.StarList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.StarRewardIconTemplate, (UnityEngine.Object) null))
        return false;
      ((Component) this.StarRewardIconTemplate).gameObject.SetActive(false);
      this.StarRewardIconList.RemoveAll((Predicate<AdvanceQuestEventMapStarIcon>) (icon =>
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) icon).gameObject);
        return true;
      }));
      for (int index4 = 0; index4 < modeInfo.StarParam.StarList.Count; ++index4)
      {
        AdvanceStarRewardParam star = modeInfo.StarParam.StarList[index4];
        if (star != null)
        {
          AdvanceQuestEventMapStarIcon eventMapStarIcon = UnityEngine.Object.Instantiate<AdvanceQuestEventMapStarIcon>(this.StarRewardIconTemplate, ((Component) this.StarRewardIconTemplate).transform.parent);
          Slider component = ((Component) eventMapStarIcon).GetComponent<Slider>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) eventMapStarIcon);
          }
          else
          {
            eventMapStarIcon.Init(modeInfo.StarParam.StarList.Count, index4, this.mChapterStarCurrent, star);
            DataSource.Bind<AdvanceStarRewardParam>(((Component) eventMapStarIcon).gameObject, star);
            this.StarRewardIconList.Add(eventMapStarIcon);
            component.value = (float) star.NeedStarNum / (float) maxStarNum;
            ((Component) eventMapStarIcon).gameObject.SetActive(true);
          }
        }
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.StarPointGauge, (UnityEngine.Object) null))
        return false;
      this.StarPointGauge.value = (float) this.mChapterStarCurrent / (float) maxStarNum;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.StarPointCurrent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.StarPointMax, (UnityEngine.Object) null))
        return false;
      this.StarPointCurrent.text = this.mChapterStarCurrent.ToString();
      this.StarPointMax.text = maxStarNum.ToString();
      this.ScrollTo(this.mCurrentSelectIcon);
      return true;
    }

    private void OnDestroy()
    {
      AdvanceEventManager instance = AdvanceEventManager.Instance;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      instance.SetRestorePointIsStage(false);
      instance.SetJumpFromMission(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.Init())
            break;
          DebugUtility.LogError("マップの初期化に失敗しました。");
          break;
        case 11:
          if (this.Refresh())
            break;
          DebugUtility.LogError("マップの表示更新に失敗しました。");
          break;
        case 41:
          if (!this.SetNextDifficulty())
            break;
          if (!this.Refresh())
          {
            DebugUtility.LogError("難易度の切り替えに失敗しました。");
            break;
          }
          if (this.IsEventOpen())
            break;
          break;
      }
    }

    private bool SetNextDifficulty()
    {
      if (this.GetAvailableDifficultyNum() <= 1)
        return false;
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.EventManager.StageDifficulty));
      for (int index2 = 1; index2 < this.DifficultyList.Length; ++index2)
      {
        int index3 = (index1 + index2) % this.DifficultyList.Length;
        if (this.IsAvailableDifficulty(this.DifficultyList[index3]))
        {
          this.EventManager.SetStageDifficulty(this.DifficultyList[index3]);
          return true;
        }
      }
      return false;
    }

    private void RefreshDifficultyButton()
    {
      if (this.ButtonDifficulty == null)
        return;
      foreach (Component component in this.ButtonDifficulty)
        component.gameObject.SetActive(false);
      int availableDifficultyNum = this.GetAvailableDifficultyNum();
      if (availableDifficultyNum < 2)
        return;
      for (int index = 0; index < availableDifficultyNum; ++index)
        ((Component) this.ButtonDifficulty[index]).gameObject.SetActive(true);
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.EventManager.StageDifficulty));
      int index2 = 0;
      for (int index3 = 0; index3 < this.DifficultyList.Length; ++index3)
      {
        int index4 = (index1 + index3) % this.DifficultyList.Length;
        if (this.IsAvailableDifficulty(this.DifficultyList[index4]))
        {
          this.ButtonDifficulty[index2].ImageIndex = index4;
          ++index2;
        }
      }
    }

    private void FinishLoad()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void DownLoadedBG(GameObject bg_prefab)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) bg_prefab, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("おかしい");
        this.FinishLoad();
      }
      else
      {
        UnityEngine.Object.Instantiate<GameObject>(bg_prefab, this.mBGParent);
        AdvanceEventModeInfoParam modeInfo = this.EventParam.GetModeInfo(this.EventManager.StageDifficulty);
        if (modeInfo == null)
        {
          DebugUtility.LogError("おかしい");
          this.FinishLoad();
        }
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
          this.FinishLoad();
        else
          this.EventManager.LoadAssets<GameObject>(this.EventManager.AdvanceAssets.StagePreview[modeInfo.ModeUiIndex], new AdvanceEventManager.LoadAssetCallback<GameObject>(this.DownLoadedPreview));
      }
    }

    private void DownLoadedPreview(GameObject preview_prefab)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) preview_prefab, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("おかしい");
        this.FinishLoad();
      }
      else
      {
        UnityEngine.Object.Instantiate<GameObject>(preview_prefab, this.mPreviewParent);
        this.FinishLoad();
      }
    }

    private void SetSelectedQuest(AdvanceQuestMapIcon icon, QuestDifficulties difficulty)
    {
      if (this.mRefQuestCursor == null || (QuestDifficulties) this.mRefQuestCursor.Count <= difficulty)
        return;
      foreach (GameObject gameObject in this.mRefQuestCursor)
        gameObject.SetActive(false);
      if (icon.QuestParam == null || !icon.QuestParam.IsQuestCondition())
        return;
      int index = (int) difficulty;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentSelectIcon, (UnityEngine.Object) null))
        this.mCurrentSelectIcon.SetSelected(false);
      if (this.mRefQuestCursor != null)
      {
        Transform transform1 = this.mRefQuestCursor[index].transform;
        Transform transform2 = ((Component) icon).gameObject.transform;
        transform1.SetParent(transform2, false);
        ((Component) transform1).gameObject.SetActive(true);
      }
      this.mCurrentSelectIcon = icon;
      this.mCurrentSelectIcon.SetSelected(true);
    }

    private void OnClickSymbolIcon(AdvanceQuestMapIcon icon, QuestParam param)
    {
      if (!this.IsEventOpen() || UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null) || param == null)
        return;
      this.SetSelectedQuest(icon, this.EventManager.StageDifficulty);
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.list.SetObject("ADVANCE_QUEST_PARAM", (object) param);
      this.ScrollTo(icon);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 141);
    }

    private bool IsEventOpen()
    {
      long serverTime = Network.GetServerTime();
      if (this.EventParam.ChapterParam.IsKeyQuest() && !this.EventParam.ChapterParam.IsKeyUnlock(serverTime))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 143);
        return false;
      }
      if (this.EventParam.ChapterParam.IsDateUnlock(serverTime))
        return true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 142);
      return false;
    }

    private void OnClickLockedIcon(AdvanceQuestMapIcon icon)
    {
      if (!this.IsEventOpen() || icon.QuestParam.cond_quests == null || icon.QuestParam.cond_quests.Length <= 0)
        return;
      string str = string.Empty;
      foreach (string condQuest in icon.QuestParam.cond_quests)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(condQuest);
        if (!string.IsNullOrEmpty(str))
          str += "\n";
        if (quest == null)
        {
          str = str + "invalid cond_quest iname : " + condQuest;
        }
        else
        {
          str += quest.title;
          if (quest.IsAdvanceBoss)
            str = str + " " + quest.name;
        }
      }
      UIUtility.SystemMessage(LocalizedText.Get("sys.ADVANCE_QUEST_MAPSYMBOL_LOCK", (object) str), (UIUtility.DialogResultEvent) null);
    }

    private void ScrollTo(AdvanceQuestMapIcon icon)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRefScrollRect, (UnityEngine.Object) null) || this.mScrollCurve == null)
        return;
      this.StartCoroutine(this.ScrollTo(((Component) icon).transform, this.mRefScrollRect, this.mScrollCurve, this.mScrollTime));
    }

    [DebuggerHidden]
    private IEnumerator ScrollTo(
      Transform target_transform,
      SRPG_ScrollRect scroll_rect,
      AnimationCurve curve,
      float scroll_time)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AdvanceQuestEventMap.\u003CScrollTo\u003Ec__Iterator0()
      {
        scroll_rect = scroll_rect,
        curve = curve,
        target_transform = target_transform,
        scroll_time = scroll_time
      };
    }

    public void OnClickStarRewardIcon(GameObject icon)
    {
      if (!this.IsEventOpen())
        return;
      AdvanceStarRewardParam dataOfClass = DataSource.FindDataOfClass<AdvanceStarRewardParam>(icon, (AdvanceStarRewardParam) null);
      if (dataOfClass == null)
        return;
      AdvanceQuestEventMapStarIcon component1 = icon.GetComponent<AdvanceQuestEventMapStarIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      AdvanceEventManager.Instance.SetStarRewardIndex(component1.Index);
      SerializeValueBehaviour component2 = ((Component) this).GetComponent<SerializeValueBehaviour>();
      component2.list.SetObject("ADVANCE_STAR_REWARD", (object) dataOfClass);
      component2.list.SetField("ADVANCE_STAR_POINT", this.mChapterStarCurrent);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
    }

    private bool IsAvailableDifficulty(QuestDifficulties difficulty)
    {
      if (this.GetAvailableQuest(difficulty).Count > 0)
        return true;
      if (this.GetDifficultyQuestList(difficulty).Count <= 0 || difficulty == QuestDifficulties.Normal)
        return false;
      List<QuestParam> difficultyQuestList = this.GetDifficultyQuestList(difficulty - (byte) 1);
      if (difficultyQuestList.Count <= 0)
        return false;
      foreach (QuestParam questParam in difficultyQuestList)
      {
        if (!MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(questParam.iname))
          return false;
      }
      return true;
    }

    private int GetAvailableDifficultyNum()
    {
      int availableDifficultyNum = 0;
      for (int index = 0; index < this.DifficultyList.Length; ++index)
      {
        if (this.IsAvailableDifficulty(this.DifficultyList[index]))
          ++availableDifficultyNum;
      }
      return availableDifficultyNum;
    }

    private List<QuestParam> GetAvailableQuest(QuestDifficulties difficulty)
    {
      if (!this.mQuestAvailable.ContainsKey(difficulty))
      {
        List<QuestParam> all = this.GetDifficultyQuestList(difficulty).FindAll((Predicate<QuestParam>) (param => MonoSingleton<GameManager>.Instance.Player.IsQuestAvailable(param.iname)));
        this.mQuestAvailable[difficulty] = all == null ? new List<QuestParam>() : all;
      }
      return this.mQuestAvailable[difficulty];
    }
  }
}
