// Decompiled with JetBrains decompiler
// Type: SRPG.BeginnerTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Open Tab Basic", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "Open Tab Practice", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "Open Tab Banner", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "Select Tips", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Select Practice", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Select Banner", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(100, "Reset Status", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(10000, "Tips Detail", FlowNode.PinTypes.Output, 10000)]
  [FlowNode.Pin(10001, "Go To Quest", FlowNode.PinTypes.Output, 10001)]
  public class BeginnerTop : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mTipsItems = new List<GameObject>();
    private List<GameObject> mQuestItems = new List<GameObject>();
    private const int START = 1;
    private const int REFRESH = 2;
    private const int TAB_BASIC = 3;
    private const int TAB_PRACTICE = 4;
    private const int TAB_BANNER = 5;
    private const int ON_SELECT_TIPS = 10;
    private const int ON_SELECT_PRACTICE = 11;
    private const int ON_SELECT_BANNER = 12;
    private const int RESET_STATUS = 100;
    private const int OUTPUT_SHOW_TIPS_DETAIL = 10000;
    private const int OUTPUT_GOTO_QUEST = 10001;
    [SerializeField]
    private Toggle ToggleTips;
    [SerializeField]
    private Toggle TogglePractice;
    [SerializeField]
    private Toggle ToggleBanners;
    [Space(8f)]
    [SerializeField]
    private GameObject BadgeTips;
    [SerializeField]
    private GameObject BadgePractice;
    [Space(8f)]
    [SerializeField]
    private GameObject BasicPanel;
    [SerializeField]
    private GameObject PracticePanel;
    [SerializeField]
    private GameObject BannerPanel;
    [Space(8f)]
    [SerializeField]
    private Transform BasicHolder;
    [SerializeField]
    private Transform PracticeHolder;
    [SerializeField]
    private GameObject BasicTemplate;
    [SerializeField]
    private GameObject PracticeTemplate;
    private BeginnerTop.TabType mCurrentTabType;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED, 1, false);
          Network.RequestAPI((WebAPI) new ReqGetTipsAlreadyRead(new Network.ResponseCallback(this.ResponseCallback)), false);
          break;
        case 2:
          this.Refresh();
          break;
        case 3:
          this.ChangeTab(BeginnerTop.TabType.Basic, false);
          break;
        case 4:
          this.ChangeTab(BeginnerTop.TabType.Practice, false);
          break;
        case 5:
          this.ChangeTab(BeginnerTop.TabType.Banners, false);
          break;
        case 10:
          this.OnSelectBasic();
          break;
        case 11:
          this.OnSelectPractice();
          break;
        case 12:
          this.OnSelectBanner();
          break;
        case 100:
          GlobalVars.RestoreBeginnerQuest = false;
          break;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.BasicTemplate != (UnityEngine.Object) null)
        this.BasicTemplate.SetActive(false);
      if ((UnityEngine.Object) this.PracticeTemplate != (UnityEngine.Object) null)
        this.PracticeTemplate.SetActive(false);
      GlobalVars.SelectTips = (string) null;
    }

    private void OnDestroy()
    {
      this.DeleteAllObj();
      GlobalVars.SelectTips = (string) null;
    }

    private void Refresh()
    {
      if (this.mCurrentTabType != BeginnerTop.TabType.Basic)
        return;
      bool flag1 = true;
      foreach (GameObject mTipsItem in this.mTipsItems)
      {
        TipsItem component = mTipsItem.GetComponent<TipsItem>();
        TipsParam dataOfClass = DataSource.FindDataOfClass<TipsParam>(component.gameObject, (TipsParam) null);
        bool hide = dataOfClass.hide;
        bool flag2 = MonoSingleton<GameManager>.Instance.Tips.Contains(dataOfClass.iname);
        if (!hide && !flag2)
          flag1 = false;
        component.IsCompleted = flag2;
        component.IsHidden = hide && !flag2;
        component.Title = !hide || flag2 ? dataOfClass.title : dataOfClass.cond_text;
        component.UpdateContent();
      }
      if (!((UnityEngine.Object) this.BadgeTips != (UnityEngine.Object) null))
        return;
      this.BadgeTips.SetActive(!flag1);
    }

    private void OnSelectBasic()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GlobalVars.SelectTips = DataSource.FindDataOfClass<TipsParam>(currentValue.GetGameObject("item"), (TipsParam) null).iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10000);
    }

    private void CreateAllTabs()
    {
      bool basicTabContent = this.CreateBasicTabContent();
      if ((UnityEngine.Object) this.BadgeTips != (UnityEngine.Object) null)
        this.BadgeTips.SetActive(!basicTabContent);
      bool practiceTabContent = this.CreatePracticeTabContent();
      if (!((UnityEngine.Object) this.BadgePractice != (UnityEngine.Object) null))
        return;
      this.BadgePractice.SetActive(!practiceTabContent);
    }

    private void ChangeTab(BeginnerTop.TabType tabType, bool forceRefresh = false)
    {
      if (!forceRefresh && tabType == this.mCurrentTabType)
        return;
      switch (tabType)
      {
        case BeginnerTop.TabType.Basic:
          this.ToggleTips.isOn = true;
          this.mCurrentTabType = BeginnerTop.TabType.Basic;
          this.RefreshBasicTabPage();
          break;
        case BeginnerTop.TabType.Practice:
          this.TogglePractice.isOn = true;
          this.mCurrentTabType = BeginnerTop.TabType.Practice;
          this.RefreshPracticeTabPage();
          break;
        case BeginnerTop.TabType.Banners:
          this.ToggleBanners.isOn = true;
          this.mCurrentTabType = BeginnerTop.TabType.Banners;
          this.RefreshBannerTabPage();
          break;
      }
    }

    private void DeleteAllObj()
    {
      foreach (GameObject mTipsItem in this.mTipsItems)
        UnityEngine.Object.Destroy((UnityEngine.Object) mTipsItem.gameObject);
      this.mTipsItems.Clear();
      foreach (GameObject mQuestItem in this.mQuestItems)
        UnityEngine.Object.Destroy((UnityEngine.Object) mQuestItem.gameObject);
      this.mQuestItems.Clear();
    }

    private bool CreateBasicTabContent()
    {
      bool flag1 = true;
      foreach (TipsParam data in (IEnumerable<TipsParam>) ((IEnumerable<TipsParam>) MonoSingleton<GameManager>.Instance.MasterParam.Tips).OrderBy<TipsParam, int>((Func<TipsParam, int>) (t => t.order)))
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BasicTemplate);
        TipsItem component = gameObject.GetComponent<TipsItem>();
        bool hide = data.hide;
        bool flag2 = MonoSingleton<GameManager>.Instance.Tips.Contains(data.iname);
        if (!hide && !flag2)
          flag1 = false;
        component.IsCompleted = flag2;
        component.IsHidden = hide && !flag2;
        component.Title = !hide || flag2 ? data.title : data.cond_text;
        gameObject.transform.SetParent(this.BasicHolder, false);
        gameObject.SetActive(true);
        DataSource.Bind<TipsParam>(gameObject, data, false);
        this.mTipsItems.Add(gameObject);
        component.UpdateContent();
      }
      return flag1;
    }

    private bool CreatePracticeTabContent()
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      List<QuestParam> questParamList = new List<QuestParam>();
      foreach (QuestParam questParam in availableQuests)
      {
        if (questParam.type == QuestTypes.Beginner && questParam.IsDateUnlock(-1L))
          questParamList.Add(questParam);
      }
      bool flag1 = true;
      foreach (QuestParam data in questParamList)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PracticeTemplate);
        TipsQuestItem component = gameObject.GetComponent<TipsQuestItem>();
        component.Title = data.title;
        component.Name = data.name;
        bool flag2 = data.state == QuestStates.Cleared;
        if (!flag2)
          flag1 = false;
        component.IsCompleted = flag2;
        gameObject.transform.SetParent(this.PracticeHolder, false);
        gameObject.SetActive(true);
        DataSource.Bind<QuestParam>(gameObject, data, false);
        this.mQuestItems.Add(gameObject);
        component.UpdateContent();
      }
      return flag1;
    }

    private void RefreshBasicTabPage()
    {
      this.BasicPanel.SetActive(true);
      this.PracticePanel.SetActive(false);
      this.BannerPanel.SetActive(false);
    }

    private void RefreshPracticeTabPage()
    {
      this.BasicPanel.SetActive(false);
      this.PracticePanel.SetActive(true);
      this.BannerPanel.SetActive(false);
    }

    private void RefreshBannerTabPage()
    {
      this.BasicPanel.SetActive(false);
      this.PracticePanel.SetActive(false);
      this.BannerPanel.SetActive(true);
    }

    private void OnSelectPractice()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      GlobalVars.SelectedQuestID = DataSource.FindDataOfClass<QuestParam>(currentValue.GetGameObject("item"), (QuestParam) null).iname;
      GlobalVars.RestoreBeginnerQuest = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10001);
    }

    private void OnSelectBanner()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      string url = currentValue.GetString("url");
      if (string.IsNullOrEmpty(url))
        return;
      Application.OpenURL(url);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_Tips> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_Tips>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body.tips != null)
          MonoSingleton<GameManager>.Instance.Tips = ((IEnumerable<string>) jsonObject.body.tips).ToList<string>();
        this.CreateAllTabs();
        if (GlobalVars.RestoreBeginnerQuest)
        {
          GlobalVars.RestoreBeginnerQuest = false;
          this.ChangeTab(BeginnerTop.TabType.Practice, true);
        }
        else
          this.ChangeTab(BeginnerTop.TabType.Basic, true);
      }
    }

    private enum TabType
    {
      Basic,
      Practice,
      Banners,
    }
  }
}
