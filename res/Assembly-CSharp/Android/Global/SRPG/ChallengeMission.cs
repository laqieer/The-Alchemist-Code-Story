// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "詳細", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "現在表示中のカテゴリを維持しつつ更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "次のページ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "前のページ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(99, "メッセージ非表示", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(101, "報酬受け取り", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "コンプリート報酬受け取り", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "終了", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "コンプリート報酬受け取りとレビュー表示", FlowNode.PinTypes.Output, 104)]
  public class ChallengeMission : MonoBehaviour, IFlowInterface
  {
    public List<GameObject> mDotsList = new List<GameObject>();
    public bool UseCharMessage = true;
    public float CharMessageDelay = 3f;
    private List<ChallengeMissionCategoryButton> mCategoryButtons = new List<ChallengeMissionCategoryButton>();
    private const int PIN_REFRESH = 0;
    private const int PIN_REFRESH_KEEP_CATEGORY = 1;
    private const int PIN_NEXT_PAGE = 10;
    private const int PIN_BACK_PAGE = 11;
    private const int PIN_MESSAGE_CLOSE = 99;
    private const int PIN_DETAIL = 100;
    private const int PIN_REWARD = 101;
    private const int PIN_COMPLETE = 102;
    private const int PIN_END = 103;
    private const int PIN_COMPLETE_REVIEW = 104;
    public Image ImageReward;
    public List<ChallengeMissionItem> Items;
    public GameObject PageDotsHolder;
    public GameObject PageDotTemplate;
    public ChallengeMissionDetail DetailWindow;
    public GameObject CharMessageWindow;
    public Text MessageText;
    public Image Shadow;
    public Text ShadowText;
    public Text PageNumText;
    public Text PageMaxNumText;
    public Button NextPageButton;
    public Button BackPageButton;
    public Image CompleteBadge;
    private int mRootCount;
    private int mCurrentCategoryIndex;
    private ChallengeCategoryParam[] mCategories;
    public Transform CategoryButtonContainer;
    public GameObject CategoryButtonTemplate;
    private int mCurrentPage;
    private Coroutine mMessageCloseCoroutine;

    public static TrophyParam[] GetTropies()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Trophies;
    }

    public static TrophyParam GetTrophy(string key)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrophy(key);
    }

    public static TrophyState GetTrophyCounter(TrophyParam trophy)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Player.GetTrophyCounter(trophy, false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(true, true, false);
          break;
        case 1:
          this.Refresh(true, false, false);
          break;
        case 10:
          if (this.mCurrentPage >= this.mRootCount)
            break;
          ++this.mCurrentPage;
          this.Refresh(false, false, false);
          break;
        case 11:
          if (this.mCurrentPage <= 0)
            break;
          --this.mCurrentPage;
          this.Refresh(false, false, false);
          break;
        case 99:
          if (!((UnityEngine.Object) this.CharMessageWindow != (UnityEngine.Object) null))
            break;
          this.ResetMessageCloseCoroutine();
          this.CharMessageWindow.SetActive(false);
          break;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.DetailWindow != (UnityEngine.Object) null)
        this.DetailWindow.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.CharMessageWindow != (UnityEngine.Object) null)
        this.CharMessageWindow.SetActive(false);
      this.mCategories = ChallengeMission.GetOpeningCategory();
      if (!((UnityEngine.Object) this.CategoryButtonTemplate != (UnityEngine.Object) null))
        return;
      this.CategoryButtonTemplate.SetActive(false);
      for (int index1 = 0; index1 < this.mCategories.Length; ++index1)
      {
        ChallengeCategoryParam mCategory = this.mCategories[index1];
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CategoryButtonTemplate);
        ChallengeMissionCategoryButton component = gameObject.GetComponent<ChallengeMissionCategoryButton>();
        int index = index1;
        component.Button.onClick.AddListener((UnityAction) (() => this.OnClickCategoryButton(index)));
        component.SetChallengeCategory(mCategory);
        this.mCategoryButtons.Add(component);
        DataSource.Bind<ChallengeCategoryParam>(component.gameObject, mCategory);
        gameObject.transform.SetParent(this.CategoryButtonContainer, false);
        gameObject.SetActive(true);
      }
    }

    private void StartMessageCloseCoroutine()
    {
      this.mMessageCloseCoroutine = this.StartCoroutine(this.CloseMessageWindow());
    }

    private void ResetMessageCloseCoroutine()
    {
      if (this.mMessageCloseCoroutine == null)
        return;
      this.StopCoroutine(this.mMessageCloseCoroutine);
      this.mMessageCloseCoroutine = (Coroutine) null;
    }

    private static ChallengeCategoryParam[] GetOpeningCategory()
    {
      ChallengeCategoryParam[] array = ((IEnumerable<ChallengeCategoryParam>) MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories).OrderByDescending<ChallengeCategoryParam, int>((Func<ChallengeCategoryParam, int>) (cat => cat.prio)).ToArray<ChallengeCategoryParam>();
      List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>();
      foreach (ChallengeCategoryParam category in array)
      {
        if (ChallengeMission.IsCategoryOpening(category))
          challengeCategoryParamList.Add(category);
      }
      return challengeCategoryParamList.ToArray();
    }

    private static bool IsCategoryOpening(ChallengeCategoryParam category)
    {
      if (category.begin_at == null || category.end_at == null)
        return true;
      DateTime serverTime = TimeManager.ServerTime;
      return serverTime >= category.begin_at.DateTimes && serverTime <= category.end_at.DateTimes;
    }

    private void OnClickCategoryButton(int index)
    {
      if (index == this.mCurrentCategoryIndex)
        return;
      this.mCurrentCategoryIndex = index;
      this.Refresh(false, false, true);
    }

    private void Refresh(bool doInitialize, bool autoCategorySelection, bool changeCategory = false)
    {
      string category;
      if (autoCategorySelection)
      {
        category = ChallengeMission.GetTopMostPriorityCategory(this.mCategories);
        int index = Array.FindIndex<ChallengeCategoryParam>(this.mCategories, (Predicate<ChallengeCategoryParam>) (cat => cat.iname == category));
        this.mCurrentCategoryIndex = index >= 0 ? index : 0;
      }
      else
        category = this.mCategories[this.mCurrentCategoryIndex].iname;
      for (int index = 0; index < this.mCategoryButtons.Count; ++index)
        this.mCategoryButtons[index].SelectionFrame.gameObject.SetActive(index == this.mCurrentCategoryIndex);
      if (doInitialize)
      {
        for (int index = 0; index < this.mCategories.Length && index < this.mCategoryButtons.Count; ++index)
        {
          bool flag1 = false;
          bool flag2 = true;
          TrophyParam currentRootTrophy = ChallengeMission.GetCurrentRootTrophy(this.mCategories[index].iname);
          bool flag3;
          if (currentRootTrophy != null)
          {
            foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(currentRootTrophy))
            {
              TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
              if (!trophyCounter.IsEnded)
              {
                flag2 = false;
                if (trophyCounter.IsCompleted)
                {
                  flag1 = true;
                  break;
                }
              }
            }
            TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(currentRootTrophy);
            flag3 = flag1 || flag2 && !trophyCounter1.IsEnded;
          }
          else
            flag3 = false;
          this.mCategoryButtons[index].Badge.gameObject.SetActive(flag3);
        }
      }
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies(category);
      int activeTrophyIndex = ChallengeMission.GetCurrentActiveTrophyIndex(rootTropies);
      if (doInitialize || changeCategory)
        this.mCurrentPage = activeTrophyIndex;
      TrophyParam trophyParam = rootTropies[this.mCurrentPage];
      this.mRootCount = rootTropies.Length;
      this.PageNumText.text = (this.mCurrentPage + 1).ToString();
      this.PageMaxNumText.text = this.mRootCount.ToString();
      this.ChangeRewardImage(trophyParam);
      this.PageDotTemplate.SetActive(false);
      using (List<GameObject>.Enumerator enumerator = this.mDotsList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current);
      }
      this.mDotsList.Clear();
      for (int index = 0; index < this.mRootCount; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PageDotTemplate);
        this.mDotsList.Add(gameObject);
        gameObject.SetActive(true);
        gameObject.GetComponent<Toggle>().isOn = index == this.mCurrentPage;
        gameObject.transform.SetParent(this.PageDotsHolder.transform, false);
      }
      this.BackPageButton.interactable = true;
      this.NextPageButton.interactable = true;
      if (this.mCurrentPage <= 0)
        this.BackPageButton.interactable = false;
      if (this.mCurrentPage >= this.mRootCount - 1)
        this.NextPageButton.interactable = false;
      if (this.mCurrentPage > activeTrophyIndex)
        this.OpenNonAchievedPage(this.mCurrentPage);
      else
        this.OpenNewPage(trophyParam, doInitialize);
    }

    private void ChangeRewardImage(TrophyParam trophy)
    {
      DataSource.Bind<TrophyParam>(this.ImageReward.gameObject, trophy);
      GameParameter.UpdateAll(this.ImageReward.gameObject);
    }

    private void OpenNewPage(TrophyParam rootTrophy, bool doInitialize)
    {
      if (rootTrophy == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        TrophyParam[] childeTrophies = ChallengeMission.GetChildeTrophies(rootTrophy);
        if (childeTrophies.Length != this.Items.Count)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
        }
        else
        {
          this.Shadow.gameObject.SetActive(false);
          bool flag1 = false;
          bool flag2 = true;
          for (int index = 0; index < this.Items.Count; ++index)
          {
            TrophyParam trophy = childeTrophies[index];
            TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(trophy);
            if (!trophyCounter.IsEnded)
            {
              flag2 = false;
              if (trophyCounter.IsCompleted)
                flag1 = true;
            }
            this.Items[index].OnClick = (UnityAction) (() => this.OnSelectItem(trophy));
            DataSource.Bind<TrophyParam>(this.Items[index].gameObject, trophy);
            ItemParam data = (ItemParam) null;
            if (trophy.Coin != 0)
              data = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            else if (trophy.Items != null && trophy.Items.Length > 0)
              data = MonoSingleton<GameManager>.Instance.GetItemParam(trophy.Items[0].iname);
            if (data != null)
              DataSource.Bind<ItemParam>(this.Items[index].gameObject, data);
            this.Items[index].Refresh();
          }
          this.CompleteBadge.gameObject.SetActive(flag2);
          if (!flag2 && !doInitialize)
            return;
          TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(rootTrophy);
          if (this.UseCharMessage && (UnityEngine.Object) this.MessageText != (UnityEngine.Object) null && !trophyCounter1.IsEnded)
          {
            string str1 = (string) null;
            if (flag1)
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_CLEAR");
            else if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0) == 0)
            {
              string str2 = string.Empty;
              if (rootTrophy.Gold != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) rootTrophy.Gold);
              else if (rootTrophy.Exp != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) rootTrophy.Exp);
              else if (rootTrophy.Coin != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) rootTrophy.Coin);
              else if (rootTrophy.Stamina != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) rootTrophy.Stamina);
              else if (rootTrophy.Items != null && rootTrophy.Items.Length > 0)
              {
                ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(rootTrophy.Items[0].iname);
                if (itemParam != null)
                {
                  if (itemParam.type == EItemType.Unit)
                  {
                    UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
                    if (unitParam != null)
                      str2 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
                  }
                  else
                    str2 = LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM", (object) itemParam.name, (object) rootTrophy.Items[0].Num);
                }
              }
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_INFO", new object[1]
              {
                (object) str2
              });
              PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 1, false);
            }
            if (str1 != null && !MonoSingleton<GameManager>.Instance.IsTutorial())
            {
              this.MessageText.text = str1;
              if ((UnityEngine.Object) this.CharMessageWindow != (UnityEngine.Object) null)
              {
                this.ResetMessageCloseCoroutine();
                this.CharMessageWindow.SetActive(true);
                this.StartMessageCloseCoroutine();
              }
            }
          }
          if (!flag2 || trophyCounter1.IsEnded)
            return;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnChallengeMissionComplete(rootTrophy.iname);
          GlobalVars.SelectedChallengeMissionTrophy = rootTrophy.iname;
          GlobalVars.SelectedTrophy.Set(rootTrophy.iname);
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0, false);
          if (rootTrophy.iname == "CHALLENGE_01")
          {
            UnityEngine.Debug.Log((object) ("<color=yellow>11 " + rootTrophy.iname + "</color>"));
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
          }
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
      }
    }

    private void OpenNonAchievedPage(int newPage)
    {
      this.Shadow.gameObject.SetActive(true);
      this.ShadowText.text = LocalizedText.Get("sys.SG_CHALLENGE_UNLOCK_COND", new object[1]
      {
        (object) newPage
      });
      this.CompleteBadge.gameObject.SetActive(false);
      using (List<ChallengeMissionItem>.Enumerator enumerator = this.Items.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ChallengeMissionItem current = enumerator.Current;
          current.OnClick = (UnityAction) null;
          DataSource component = current.GetComponent<DataSource>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) component);
          current.Refresh();
        }
      }
    }

    private string HankakuToZenkakuNumber(int num)
    {
      switch (num)
      {
        case 0:
          return "１";
        case 1:
          return "１";
        case 2:
          return "２";
        case 3:
          return "３";
        case 4:
          return "４";
        case 5:
          return "５";
        case 6:
          return "６";
        case 7:
          return "７";
        case 8:
          return "８";
        case 9:
          return "９";
        default:
          return "０";
      }
    }

    [DebuggerHidden]
    private IEnumerator CloseMessageWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMission.\u003CCloseMessageWindow\u003Ec__IteratorED() { \u003C\u003Ef__this = this };
    }

    private void OnSelectItem(TrophyParam selectTrophy)
    {
      if (ChallengeMission.GetTrophyCounter(selectTrophy).IsCompleted)
      {
        GlobalVars.SelectedChallengeMissionTrophy = selectTrophy.iname;
        GlobalVars.SelectedTrophy.Set(selectTrophy.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
          return;
        DataSource.Bind<TrophyParam>(this.DetailWindow.gameObject, selectTrophy);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public static TrophyParam[] GetRootTropies()
    {
      ChallengeCategoryParam[] challengeCategories = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      TrophyParam[] tropies = ChallengeMission.GetTropies();
      foreach (TrophyParam trophyParam in tropies)
      {
        TrophyParam trophy = trophyParam;
        if (trophy.IsChallengeMissionRoot)
        {
          ChallengeCategoryParam challengeCategoryParam = ((IEnumerable<ChallengeCategoryParam>) challengeCategories).FirstOrDefault<ChallengeCategoryParam>((Func<ChallengeCategoryParam, bool>) (cat => cat.iname == trophy.Category));
          if (challengeCategoryParam != null)
          {
            if (challengeCategoryParam.begin_at == null || challengeCategoryParam.end_at == null)
            {
              trophyParamList.Add(trophy);
            }
            else
            {
              DateTime serverTime = TimeManager.ServerTime;
              if (serverTime >= challengeCategoryParam.begin_at.DateTimes && serverTime <= challengeCategoryParam.end_at.DateTimes)
                trophyParamList.Add(trophy);
            }
          }
        }
      }
      return trophyParamList.ToArray();
    }

    public static TrophyParam[] GetRootTropies(string category)
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMissionRoot && tropy.Category == category)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    public static TrophyParam GetCurrentRootTrophy(string category)
    {
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies(category);
      if (rootTropies == null || rootTropies.Length == 0)
        return (TrophyParam) null;
      foreach (TrophyParam trophy in rootTropies)
      {
        if (!ChallengeMission.GetTrophyCounter(trophy).IsEnded)
          return trophy;
      }
      return (TrophyParam) null;
    }

    public static TrophyParam[] GetChildeTrophies(TrophyParam root)
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMission && tropy.Category == root.Category && root.iname == tropy.ParentTrophy)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    private static string GetTopMostPriorityCategory(ChallengeCategoryParam[] categories)
    {
      return ChallengeMission.GetTopMostPriorityTrophy(categories)?.Category;
    }

    public static TrophyParam GetTopMostPriorityTrophy()
    {
      return ChallengeMission.GetTopMostPriorityTrophy(ChallengeMission.GetOpeningCategory());
    }

    public static TrophyParam GetTopMostPriorityTrophy(ChallengeCategoryParam[] categories)
    {
      TrophyParam trophyParam1 = (TrophyParam) null;
      TrophyParam trophyParam2 = (TrophyParam) null;
      TrophyParam trophyParam3 = (TrophyParam) null;
      foreach (ChallengeCategoryParam category in categories)
      {
        bool flag = true;
        TrophyParam currentRootTrophy = ChallengeMission.GetCurrentRootTrophy(category.iname);
        if (currentRootTrophy != null)
        {
          foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(currentRootTrophy))
          {
            TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
            if (!trophyCounter.IsEnded)
            {
              flag = false;
              if (trophyCounter.IsCompleted)
              {
                if (trophyParam2 == null)
                  trophyParam2 = childeTrophy;
              }
              else if (trophyParam1 == null)
                trophyParam1 = childeTrophy;
            }
          }
          TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(currentRootTrophy);
          if (!trophyCounter1.IsEnded)
          {
            if (flag)
            {
              if (trophyParam3 == null)
                trophyParam3 = currentRootTrophy;
            }
            else if (trophyCounter1.IsCompleted)
            {
              if (trophyParam2 == null)
                trophyParam2 = currentRootTrophy;
            }
            else if (trophyParam1 == null)
              trophyParam1 = currentRootTrophy;
          }
        }
      }
      return trophyParam3 ?? trophyParam2 ?? trophyParam1;
    }

    public static TrophyParam[] GetTrophiesSortedByPriority(TrophyParam[] trophies)
    {
      ChallengeCategoryParam[] challengeCategories = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
      if (challengeCategories == null || challengeCategories.Length < 1)
        return (TrophyParam[]) null;
      Dictionary<string, int> priorities = ((IEnumerable<ChallengeCategoryParam>) challengeCategories).ToDictionary<ChallengeCategoryParam, string, int>((Func<ChallengeCategoryParam, string>) (c => c.iname), (Func<ChallengeCategoryParam, int>) (c => c.prio));
      if (trophies == null || trophies.Length < 1)
        return (TrophyParam[]) null;
      return ((IEnumerable<TrophyParam>) trophies).OrderByDescending<TrophyParam, int>((Func<TrophyParam, int>) (trophy =>
      {
        int num;
        if (priorities.TryGetValue(trophy.Category, out num))
          return num;
        return 0;
      })).ToArray<TrophyParam>();
    }

    public static TrophyParam[] GetRootTrophiesSortedByPriority()
    {
      return ChallengeMission.GetTrophiesSortedByPriority(ChallengeMission.GetRootTropies());
    }

    private static int GetCurrentActiveTrophyIndex(TrophyParam[] trophies)
    {
      if (trophies == null || trophies.Length == 0)
        return -1;
      int num = 0;
      foreach (TrophyParam trophy in trophies)
      {
        if (ChallengeMission.GetTrophyCounter(trophy).IsEnded)
          ++num;
      }
      if (num >= trophies.Length)
        return trophies.Length - 1;
      return num;
    }
  }
}
