// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestEventMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Refresh", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(21, "難易度変更", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "OpenStarReward", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(121, "クエスト選択された", FlowNode.PinTypes.Output, 121)]
  [FlowNode.Pin(131, "読み込み完了", FlowNode.PinTypes.Output, 131)]
  public class GenesisQuestEventMap : MonoBehaviour, IFlowInterface
  {
    private Dictionary<QuestDifficulties, List<QuestParam>> mQuestList = new Dictionary<QuestDifficulties, List<QuestParam>>();
    private Dictionary<QuestDifficulties, List<GenesisQuestMapSymbol>> mQuestSymbols = new Dictionary<QuestDifficulties, List<GenesisQuestMapSymbol>>();
    private Dictionary<QuestDifficulties, List<QuestParam>> mQuestAvailable = new Dictionary<QuestDifficulties, List<QuestParam>>();
    [SerializeField]
    private bool mDiffOpenCheckCacheOnPlayerPrefs = true;
    private List<GenesisQuestEventMapStarIcon> StarRewardIconList = new List<GenesisQuestEventMapStarIcon>();
    public const int PIN_IN_INIT = 1;
    public const int PIN_IN_REFRESH = 11;
    public const int PIN_IN_DIFFICULTY = 21;
    public const int PIN_OUT_INIT = 101;
    public const int PIN_OUT_OPEN_STAR_REWARD = 111;
    public const int PIN_OUT_SELECT_QUEST = 121;
    public const int PIN_OUT_LOADED = 131;
    public const string GENESIS_STAR_REWARD_SVB_KEY = "GENESIS_STAR_REWARD";
    public const string GENESIS_STAR_POINT_SVB_KEY = "GENESIS_STAR_POINT";
    public const string GENESIS_QUEST_PARAM_KEY = "GENESIS_QUEST_PARAM";
    [SerializeField]
    private Transform mBGParent;
    [SerializeField]
    private Transform mPreviewParent;
    [SerializeField]
    private GenesisQuestMapSymbol mRefQuestSymbol;
    [SerializeField]
    private List<GameObject> mRefQuestCursor;
    [SerializeField]
    private GenesisQuestEventMapStarIcon StarRewardIconTemplate;
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
    private int mChapterStarCurrent;
    private GenesisQuestMapIcon mCurrentSelectIcon;
    private GenesisChapterManager mChapterManager;
    private GenesisChapterParam mChapterParam;
    private QuestDifficulties[] mDifficultyList;
    private const float ScrollTime = 0.3f;

    private GenesisChapterManager ChapterManager
    {
      get
      {
        if ((UnityEngine.Object) this.mChapterManager == (UnityEngine.Object) null)
          this.mChapterManager = GenesisChapterManager.Instance;
        return this.mChapterManager;
      }
    }

    private GenesisChapterParam ChapterParam
    {
      get
      {
        if (this.mChapterParam == null && (UnityEngine.Object) this.ChapterManager != (UnityEngine.Object) null)
          this.mChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(this.ChapterManager.CurrentChapterParam.Iname);
        return this.mChapterParam;
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
      if (this.ChapterParam == null)
        return new List<QuestParam>();
      if (!this.mQuestList.ContainsKey(difficulty))
      {
        List<QuestParam> questParamList = this.ChapterParam.GetQuestList(difficulty, false) ?? new List<QuestParam>();
        this.mQuestList[difficulty] = questParamList;
      }
      return this.mQuestList[difficulty];
    }

    private bool Init()
    {
      if (this.ChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = this.ChapterParam.GetModeInfo(this.ChapterManager.StageDifficulty);
      if (modeInfo == null || this.ChapterManager.GenesisAssets.StageBG.Length <= modeInfo.ModeUiIndex)
        return false;
      this.ChapterManager.LoadAssets<GameObject>(this.ChapterManager.GenesisAssets.StageBG[modeInfo.ModeUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.DownLoadedBG));
      bool flag = this.Refresh();
      if (this.mDiffOpenCheckCacheOnPlayerPrefs)
      {
        string last_played_genesis_quest = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_DIFFICULTY_OPEN_QUEST_CACHE, string.Empty);
        if (!string.IsNullOrEmpty(last_played_genesis_quest))
        {
          QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
          for (int index = 0; index < quests.Length; ++index)
          {
            if (!(quests[index].iname == last_played_genesis_quest) && quests[index].type == QuestTypes.GenesisStory && (quests[index].cond_quests != null && !string.IsNullOrEmpty(Array.Find<string>(quests[index].cond_quests, (Predicate<string>) (cond_quest => cond_quest == last_played_genesis_quest)))) && this.GetAvailableQuest(quests[index].difficulty).Count == 1)
            {
              this.PopupMessageDifficultyOpened(quests[index].difficulty);
              break;
            }
          }
        }
      }
      else
      {
        if (GenesisChapterManager.Instance.RestorePointIsStage)
        {
          List<QuestDifficulties> availableDifficultyList = this.GetAvailableDifficultyList();
          List<QuestDifficulties> availableDifficulties = GlobalVars.GenesisAvailableDifficulties;
          foreach (QuestDifficulties questDifficulties in availableDifficultyList)
          {
            QuestDifficulties difficulty = questDifficulties;
            if (availableDifficulties.FindIndex((Predicate<QuestDifficulties>) (item => item == difficulty)) < 0)
            {
              this.PopupMessageDifficultyOpened(difficulty);
              break;
            }
          }
        }
        GlobalVars.GenesisAvailableDifficulties = this.GetAvailableDifficultyList();
      }
      return flag;
    }

    private void PopupMessageDifficultyOpened(QuestDifficulties difficulty)
    {
      int index = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == difficulty));
      if (index < 0)
        return;
      UIUtility.SystemMessage(LocalizedText.Get("sys.GENESIS_QUEST_OPEN_DIFFICULTY", new object[1]
      {
        (object) LocalizedText.Get(this.LocalizeDifficultyKey[index])
      }), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private List<QuestDifficulties> GetAvailableDifficultyList()
    {
      List<QuestDifficulties> questDifficultiesList = new List<QuestDifficulties>();
      for (int index = 0; index < this.DifficultyList.Length; ++index)
      {
        if (this.IsAvailableDifficulty(this.DifficultyList[index]))
          questDifficultiesList.Add(this.DifficultyList[index]);
      }
      return questDifficultiesList;
    }

    private bool Refresh()
    {
      if (this.ChapterParam == null)
        return false;
      GenesisChapterModeInfoParam modeInfo = this.ChapterParam.GetModeInfo(this.ChapterManager.StageDifficulty);
      if (modeInfo == null || (UnityEngine.Object) this.mRefQuestSymbol == (UnityEngine.Object) null)
        return false;
      this.mRefQuestSymbol.gameObject.SetActive(false);
      Transform parent = this.mRefQuestSymbol.transform.parent;
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.ChapterManager.StageDifficulty));
      if ((UnityEngine.Object) this.ChapterTitle != (UnityEngine.Object) null)
        this.ChapterTitle.text = this.ChapterParam.Name;
      if ((UnityEngine.Object) this.ChapterDifficulty != (UnityEngine.Object) null)
        this.ChapterDifficulty.text = LocalizedText.Get(this.LocalizeDifficultyKey[index1]);
      if ((UnityEngine.Object) this.TitleBack != (UnityEngine.Object) null && this.TitleBack.Images.Length > index1)
        this.TitleBack.ImageIndex = index1;
      for (int index2 = parent.childCount - 1; index2 >= 0; --index2)
        parent.GetChild(index2).gameObject.SetActive(false);
      this.RefreshDifficultyButton();
      List<QuestParam> difficultyQuestList = this.GetDifficultyQuestList(this.ChapterManager.StageDifficulty);
      if (!this.mQuestSymbols.ContainsKey(this.ChapterManager.StageDifficulty))
      {
        List<GenesisQuestMapSymbol> genesisQuestMapSymbolList = new List<GenesisQuestMapSymbol>();
        for (int index2 = 0; index2 < difficultyQuestList.Count; ++index2)
        {
          QuestParam questParam = difficultyQuestList[index2];
          if (questParam != null)
          {
            GenesisQuestMapSymbol genesisQuestMapSymbol = UnityEngine.Object.Instantiate<GenesisQuestMapSymbol>(this.mRefQuestSymbol, parent);
            genesisQuestMapSymbol.Init(questParam, index2 % 2 == 0, index2 >= difficultyQuestList.Count - 1, new GenesisQuestMapIcon.OnClickIcon(this.OnClickSymbolIcon), new GenesisQuestMapIcon.OnClickLockedIcon(this.OnClickLockedIcon));
            genesisQuestMapSymbolList.Add(genesisQuestMapSymbol);
          }
        }
        this.mQuestSymbols[this.ChapterManager.StageDifficulty] = genesisQuestMapSymbolList;
      }
      List<GenesisQuestMapSymbol> mQuestSymbol = this.mQuestSymbols[this.ChapterManager.StageDifficulty];
      foreach (Component component in mQuestSymbol)
        component.gameObject.SetActive(true);
      List<QuestParam> availableQuest = this.GetAvailableQuest(this.ChapterManager.StageDifficulty);
      if (availableQuest.Count >= 1)
      {
        GenesisQuestMapIcon icon = (GenesisQuestMapIcon) null;
        if (GenesisChapterManager.Instance.RestorePointIsStage)
        {
          QuestParam last_played_quest_param = availableQuest.Find((Predicate<QuestParam>) (item => item.iname == (string) GlobalVars.LastPlayedQuest));
          if (last_played_quest_param != null)
          {
            GenesisQuestMapSymbol genesisQuestMapSymbol = mQuestSymbol.Find((Predicate<GenesisQuestMapSymbol>) (item => item.Icon.QuestParam == last_played_quest_param));
            if ((UnityEngine.Object) genesisQuestMapSymbol != (UnityEngine.Object) null)
              icon = genesisQuestMapSymbol.Icon;
          }
        }
        if ((UnityEngine.Object) icon == (UnityEngine.Object) null)
          icon = mQuestSymbol[availableQuest.Count - 1].Icon;
        this.SetSelectedQuest(icon, this.ChapterManager.StageDifficulty);
        this.ScrollTo(icon);
      }
      int maxStarNum = this.ChapterParam.GetMaxStarNum(difficultyQuestList);
      if (maxStarNum <= 0)
        return false;
      this.mChapterStarCurrent = this.ChapterParam.GetNowStarNum(difficultyQuestList);
      if (modeInfo.StarParam == null || modeInfo.StarParam.StarList == null || (UnityEngine.Object) this.StarRewardIconTemplate == (UnityEngine.Object) null)
        return false;
      this.StarRewardIconTemplate.gameObject.SetActive(false);
      this.StarRewardIconList.RemoveAll((Predicate<GenesisQuestEventMapStarIcon>) (icon =>
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) icon.gameObject);
        return true;
      }));
      for (int index2 = 0; index2 < modeInfo.StarParam.StarList.Count; ++index2)
      {
        GenesisStarRewardParam star = modeInfo.StarParam.StarList[index2];
        if (star != null)
        {
          GenesisQuestEventMapStarIcon eventMapStarIcon = UnityEngine.Object.Instantiate<GenesisQuestEventMapStarIcon>(this.StarRewardIconTemplate, this.StarRewardIconTemplate.transform.parent);
          Slider component = eventMapStarIcon.GetComponent<Slider>();
          if ((UnityEngine.Object) component == (UnityEngine.Object) null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) eventMapStarIcon);
          }
          else
          {
            eventMapStarIcon.Init(modeInfo.StarParam.StarList.Count, index2, this.mChapterStarCurrent, star);
            DataSource.Bind<GenesisStarRewardParam>(eventMapStarIcon.gameObject, star, false);
            eventMapStarIcon.OnSelect = new ListItemEvents.ListItemEvent(this.OnClickStarRewardIcon);
            this.StarRewardIconList.Add(eventMapStarIcon);
            component.value = (float) star.NeedStarNum / (float) maxStarNum;
            eventMapStarIcon.gameObject.SetActive(true);
          }
        }
      }
      if ((UnityEngine.Object) this.StarPointGauge == (UnityEngine.Object) null)
        return false;
      this.StarPointGauge.value = (float) this.mChapterStarCurrent / (float) maxStarNum;
      if ((UnityEngine.Object) this.StarPointCurrent == (UnityEngine.Object) null || (UnityEngine.Object) this.StarPointMax == (UnityEngine.Object) null)
        return false;
      this.StarPointCurrent.text = this.mChapterStarCurrent.ToString();
      this.StarPointMax.text = maxStarNum.ToString();
      return true;
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) GenesisChapterManager.Instance != (UnityEngine.Object) null))
        return;
      GenesisChapterManager.Instance.SetRestorePointIsStage(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!this.Init())
            DebugUtility.LogError("おかしい");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 11:
          if (this.Refresh())
            break;
          DebugUtility.LogError("おかしい");
          break;
        case 21:
          if (!this.SetNextDifficulty() || this.Refresh())
            break;
          DebugUtility.LogError("おかしい");
          break;
      }
    }

    private bool SetNextDifficulty()
    {
      if (this.GetAvailableDifficultyNum() <= 1)
        return false;
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.ChapterManager.StageDifficulty));
      for (int index2 = 1; index2 < this.DifficultyList.Length; ++index2)
      {
        int index3 = (index1 + index2) % this.DifficultyList.Length;
        if (this.IsAvailableDifficulty(this.DifficultyList[index3]))
        {
          this.ChapterManager.SetStageDifficulty(this.DifficultyList[index3]);
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
        this.ButtonDifficulty[index].gameObject.SetActive(true);
      int index1 = Array.FindIndex<QuestDifficulties>(this.DifficultyList, (Predicate<QuestDifficulties>) (item => item == this.ChapterManager.StageDifficulty));
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

    private void DownLoadedBG(GameObject bg_prefab)
    {
      if ((UnityEngine.Object) bg_prefab == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("おかしい");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
      }
      else
      {
        UnityEngine.Object.Instantiate<GameObject>(bg_prefab, this.mBGParent);
        GenesisChapterModeInfoParam modeInfo = this.ChapterParam.GetModeInfo(this.ChapterManager.StageDifficulty);
        if (modeInfo == null)
        {
          DebugUtility.LogError("おかしい");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
        }
        else
          this.ChapterManager.LoadAssets<GameObject>(this.ChapterManager.GenesisAssets.StagePreview[modeInfo.ModeUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.DownLoadedPreview));
      }
    }

    private void DownLoadedPreview(GameObject preview_prefab)
    {
      if ((UnityEngine.Object) preview_prefab == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("おかしい");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
      }
      else
      {
        UnityEngine.Object.Instantiate<GameObject>(preview_prefab, this.mPreviewParent);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
      }
    }

    private void SetSelectedQuest(GenesisQuestMapIcon icon, QuestDifficulties difficulty)
    {
      if (this.mRefQuestCursor == null || (QuestDifficulties) this.mRefQuestCursor.Count <= difficulty)
        return;
      foreach (GameObject gameObject in this.mRefQuestCursor)
        gameObject.SetActive(false);
      int index = (int) difficulty;
      if ((UnityEngine.Object) this.mCurrentSelectIcon != (UnityEngine.Object) null)
        this.mCurrentSelectIcon.SetSelected(false);
      if (this.mRefQuestCursor != null)
      {
        Transform transform1 = this.mRefQuestCursor[index].transform;
        Transform transform2 = icon.gameObject.transform;
        transform1.SetParent(transform2, false);
        transform1.gameObject.SetActive(true);
      }
      this.mCurrentSelectIcon = icon;
      this.mCurrentSelectIcon.SetSelected(true);
    }

    private void OnClickSymbolIcon(GenesisQuestMapIcon icon, QuestParam param)
    {
      if ((UnityEngine.Object) icon == (UnityEngine.Object) null || param == null)
        return;
      this.SetSelectedQuest(icon, this.ChapterManager.StageDifficulty);
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      component.list.SetObject("GENESIS_QUEST_PARAM", (object) param);
      this.ScrollTo(icon);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 121);
    }

    private void OnClickLockedIcon(GenesisQuestMapIcon icon)
    {
      if (icon.QuestParam.cond_quests == null || icon.QuestParam.cond_quests.Length <= 0)
        return;
      string str = string.Empty;
      foreach (string condQuest in icon.QuestParam.cond_quests)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(condQuest);
        if (!string.IsNullOrEmpty(str))
          str += "\n";
        str = quest != null ? str + quest.title : str + "invalid cond_quest iname : " + condQuest;
      }
      UIUtility.SystemMessage(LocalizedText.Get("sys.GENESIS_QUEST_MAPSYMBOL_LOCK", new object[1]
      {
        (object) str
      }), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private void ScrollTo(GenesisQuestMapIcon icon)
    {
      if ((UnityEngine.Object) this.mRefScrollRect == (UnityEngine.Object) null)
        return;
      this.StartCoroutine(this.ScrollTo(icon, this.mRefScrollRect));
    }

    [DebuggerHidden]
    private IEnumerator ScrollTo(GenesisQuestMapIcon icon, SRPG_ScrollRect scroll_rect)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GenesisQuestEventMap.\u003CScrollTo\u003Ec__Iterator0() { icon = icon, scroll_rect = scroll_rect, \u0024this = this };
    }

    private void OnClickStarRewardIcon(GameObject icon)
    {
      GenesisStarRewardParam dataOfClass = DataSource.FindDataOfClass<GenesisStarRewardParam>(icon, (GenesisStarRewardParam) null);
      if (dataOfClass == null)
        return;
      GenesisQuestEventMapStarIcon component1 = icon.GetComponent<GenesisQuestEventMapStarIcon>();
      if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
        return;
      GenesisChapterManager.Instance.SetStarRewardIndex(component1.Index);
      SerializeValueBehaviour component2 = this.GetComponent<SerializeValueBehaviour>();
      component2.list.SetObject("GENESIS_STAR_REWARD", (object) dataOfClass);
      component2.list.SetField("GENESIS_STAR_POINT", this.mChapterStarCurrent);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    private bool IsAvailableDifficulty(QuestDifficulties difficulty)
    {
      return this.GetAvailableQuest(difficulty).Count > 0;
    }

    private int GetAvailableDifficultyNum()
    {
      int num = 0;
      for (int index = 0; index < this.DifficultyList.Length; ++index)
      {
        if (this.IsAvailableDifficulty(this.DifficultyList[index]))
          ++num;
      }
      return num;
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
