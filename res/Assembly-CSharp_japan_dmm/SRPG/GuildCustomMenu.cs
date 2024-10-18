// Decompiled with JetBrains decompiler
// Type: SRPG.GuildCustomMenu
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
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1001, "プレイヤー詳細", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1010, "加入申請詳細", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "メンバーの除名", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1021, "加入申請の一括拒否", FlowNode.PinTypes.Output, 1021)]
  [FlowNode.Pin(1030, "メンバーの役職変更", FlowNode.PinTypes.Output, 1030)]
  [FlowNode.Pin(1040, "ポートバトル変更", FlowNode.PinTypes.Output, 1040)]
  public class GuildCustomMenu : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_REFRESH = 1;
    private const int PIN_OUTPUT_OPEN_PLAYER_INFO = 1001;
    private const int PIN_OUTPUT_OPEN_ENTRY_REQUEST_INFO = 1010;
    private const int PIN_OUTPUT_OPEN_MEMBER_KICK = 1020;
    private const int PIN_OUTPUT_CONFIRM_ENTRY_REQUEST_NG_ALL = 1021;
    private const int PIN_OUTPUT_OPEN_CHANGE_MEMBER = 1030;
    private const int PIN_OUTPUT_CHANGE_GVGJOIN = 1040;
    private readonly string[] SORT_KEYS = new string[3]
    {
      "sys.FRIEND_SORT_ENTRY_DATE",
      "sys.FRIEND_SORT_LAST_LOGIN",
      "sys.FRIEND_SORT_PLAYER_LEVEL"
    };
    private GuildCustomMenu.eSortType mSortType;
    [SerializeField]
    private Toggle[] mTabs;
    [SerializeField]
    private GameObject mMemberTemplate1;
    [SerializeField]
    private GameObject mMemberTemplate2;
    [SerializeField]
    private GameObject mEntryRequestTemplate;
    [SerializeField]
    private GameObject mPage_GuildInfo;
    [SerializeField]
    private GameObject mPage_MemebrList;
    [SerializeField]
    private GameObject mPage_GuildEdit;
    [SerializeField]
    private GameObject mPage_MemberManagement;
    [SerializeField]
    private GameObject mPage_EntryRequest;
    [SerializeField]
    private GameObject mPage_GvG;
    [SerializeField]
    private GameObject[] mTabLockObjects;
    [SerializeField]
    private GameObject mNoEntryRequestsCaution;
    [SerializeField]
    private Pulldown mGuildMemberListSort;
    [SerializeField]
    private Pulldown mGuildMemberManagementSort;
    [SerializeField]
    private Button mEntryRequestRejectAllButton;
    [SerializeField]
    private GameObject mExistInviteBadge;
    [SerializeField]
    private Toggle mGvGJoinTab;
    [SerializeField]
    private Toggle mGvGCancelTab;
    [SerializeField]
    private bool isGvGJoin = true;
    private static GuildCustomMenu mInstance;
    private GuildData mTargetGuild;
    private GuildMemberData.eRole mCurrentRole;
    private GameObject[] mPages;
    private Toggle SelectTab;
    private List<GameObject> mCreatedItems_MemberList = new List<GameObject>();
    private List<GameObject> mCreatedItems_MemberManagement = new List<GameObject>();
    private List<GameObject> mCreatedItems_EntryRequest = new List<GameObject>();
    [SerializeField]
    private ScrollRect mScrollRect_GuildInfo;
    [SerializeField]
    private ScrollRect mScrollRect_MemberList;
    [SerializeField]
    private ScrollRect mScrollRect_GuildEdit;
    [SerializeField]
    private ScrollRect mScrollRect_MemberManagement;
    [SerializeField]
    private ScrollRect mScrollRect_EntryRequest;
    [SerializeField]
    private ScrollRect mScrollRect_GvG;

    public static GuildCustomMenu Instance => GuildCustomMenu.mInstance;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.RefreshAll();
    }

    private void Awake() => GuildCustomMenu.mInstance = this;

    private void Start() => this.Init();

    private void Init()
    {
      this.mCurrentRole = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId;
      this.SetupTabs();
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        this.mTargetGuild = component.list.GetObject<GuildData>(GuildSVB_Key.GUILD, (GuildData) null);
        DataSource.Bind<GuildData>(((Component) this).gameObject, this.mTargetGuild);
        DataSource.Bind<GuildMemberData>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mExistInviteBadge, (UnityEngine.Object) null))
        this.mExistInviteBadge.SetActive(GuildManager.Instance.EntryRequests.Length > 0);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void SetupTabs()
    {
      this.mPages = new GameObject[6];
      this.mPages[0] = this.mPage_GuildInfo;
      this.mPages[1] = this.mPage_MemebrList;
      this.mPages[2] = this.mPage_GuildEdit;
      this.mPages[3] = this.mPage_MemberManagement;
      this.mPages[4] = this.mPage_EntryRequest;
      if (this.mPages.Length > 5)
        this.mPages[5] = this.mPage_GvG;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mTabs[0].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_Info)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mTabs[1].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_MemberList)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mTabs[2].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_Edit)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mTabs[3].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_MemberManagement)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mTabs[4].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_EntryRequest)));
      if (this.mTabs.Length > 5)
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.mTabs[5].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect_GvG)));
      }
      this.mTabs[0].isOn = true;
    }

    private void HidePageAll()
    {
      for (int index = 0; index < this.mPages.Length; ++index)
        this.mPages[index].SetActive(false);
    }

    private void RefreshAll()
    {
      if (this.mCurrentRole != MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId)
      {
        for (int index = 0; index < this.mTabLockObjects.Length; ++index)
          this.mTabLockObjects[index].SetActive(true);
        this.Init();
      }
      this.Refresh_GvG();
      this.Refresh_MemberList();
      this.Refresh_MemberManagement();
      this.Refresh_EntryRequest();
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void SetupGuildMemberSortPulldown(Pulldown pulldown)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) pulldown, (UnityEngine.Object) null))
        return;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT))
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT);
        if (0 <= num && num < 3)
          this.mSortType = (GuildCustomMenu.eSortType) num;
      }
      pulldown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortChange);
      pulldown.ClearItems();
      for (int index = 0; index < this.SORT_KEYS.Length; ++index)
        pulldown.AddItem(LocalizedText.Get(this.SORT_KEYS[index]), index);
      pulldown.Selection = (int) this.mSortType;
      pulldown.interactable = true;
      ((Component) pulldown).gameObject.SetActive(true);
    }

    public static List<GuildMemberData> GetSortedMemberList(
      GuildMemberData[] members,
      GuildCustomMenu.eSortType sort_type)
    {
      if (members == null)
        return new List<GuildMemberData>();
      Dictionary<GuildMemberData.eRole, List<GuildMemberData>> dictionary = new Dictionary<GuildMemberData.eRole, List<GuildMemberData>>();
      for (int index = 0; index < members.Length; ++index)
      {
        if (!dictionary.ContainsKey(members[index].RoleId))
          dictionary.Add(members[index].RoleId, new List<GuildMemberData>());
        dictionary[members[index].RoleId].Add(members[index]);
      }
      foreach (GuildMemberData.eRole key in dictionary.Keys)
      {
        switch (sort_type)
        {
          case GuildCustomMenu.eSortType.MIN:
            GuildCustomMenu.Sort_JoinAt(dictionary[key]);
            continue;
          case GuildCustomMenu.eSortType.LAST_LOGIN:
            GuildCustomMenu.Sort_LastLogin(dictionary[key]);
            continue;
          case GuildCustomMenu.eSortType.PLAYER_LEVEL:
            GuildCustomMenu.Sort_PlayerLevel(dictionary[key]);
            continue;
          default:
            continue;
        }
      }
      List<GuildMemberData> sortedMemberList = new List<GuildMemberData>();
      if (dictionary.ContainsKey(GuildMemberData.eRole.MASTAER))
        sortedMemberList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.MASTAER]);
      if (dictionary.ContainsKey(GuildMemberData.eRole.SUB_MASTAER))
        sortedMemberList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.SUB_MASTAER]);
      if (dictionary.ContainsKey(GuildMemberData.eRole.MEMBER))
        sortedMemberList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.MEMBER]);
      return sortedMemberList;
    }

    private void Refresh_MemberList(bool is_update_gameparameter = true)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect_MemberList, (UnityEngine.Object) null))
        this.mScrollRect_MemberList.content.anchoredPosition = Vector2.zero;
      this.SetupGuildMemberSortPulldown(this.mGuildMemberListSort);
      this.RefreshMemberListItem();
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void RefreshMemberListItem()
    {
      if (this.mTargetGuild == null)
        return;
      List<GuildMemberData> sortedMemberList = GuildCustomMenu.GetSortedMemberList(this.mTargetGuild.Members, this.mSortType);
      int num = sortedMemberList.Count - this.mCreatedItems_MemberList.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mMemberTemplate1);
        gameObject.transform.SetParent(this.mMemberTemplate1.transform.parent, false);
        this.mCreatedItems_MemberList.Add(gameObject);
      }
      for (int index = 0; index < this.mCreatedItems_MemberList.Count; ++index)
        this.mCreatedItems_MemberList[index].SetActive(false);
      for (int index = 0; index < sortedMemberList.Count; ++index)
      {
        this.mCreatedItems_MemberList[index].SetActive(true);
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_MemberList[index], sortedMemberList[index]);
        DataSource.Bind<UnitData>(this.mCreatedItems_MemberList[index], sortedMemberList[index].Unit);
      }
      this.mMemberTemplate1.SetActive(false);
      DataSource.Bind<GuildData>(((Component) this).gameObject, this.mTargetGuild);
    }

    private void Refresh_MemberManagement(bool is_update_gameparameter = true)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect_MemberManagement, (UnityEngine.Object) null))
        this.mScrollRect_MemberManagement.content.anchoredPosition = Vector2.zero;
      this.SetupGuildMemberSortPulldown(this.mGuildMemberManagementSort);
      this.RefreshMemberManagementItem();
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void RefreshMemberManagementItem()
    {
      if (this.mTargetGuild == null)
        return;
      List<GuildMemberData> sortedMemberList = GuildCustomMenu.GetSortedMemberList(this.mTargetGuild.Members, this.mSortType);
      int num = sortedMemberList.Count - this.mCreatedItems_MemberManagement.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mMemberTemplate2);
        gameObject.transform.SetParent(this.mMemberTemplate2.transform.parent, false);
        this.mCreatedItems_MemberManagement.Add(gameObject);
      }
      for (int index = 0; index < this.mCreatedItems_MemberManagement.Count; ++index)
        this.mCreatedItems_MemberManagement[index].SetActive(false);
      for (int index = 0; index < sortedMemberList.Count; ++index)
      {
        this.mCreatedItems_MemberManagement[index].SetActive(true);
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_MemberManagement[index], sortedMemberList[index]);
        DataSource.Bind<UnitData>(this.mCreatedItems_MemberManagement[index], sortedMemberList[index].Unit);
        GuildMemberManagementItem component = this.mCreatedItems_MemberManagement[index].GetComponent<GuildMemberManagementItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Init(sortedMemberList[index]);
      }
      this.mMemberTemplate2.SetActive(false);
      DataSource.Bind<GuildData>(((Component) this).gameObject, this.mTargetGuild);
    }

    private void Refresh_EntryRequest(bool is_update_gameparameter = true)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect_EntryRequest, (UnityEngine.Object) null))
        this.mScrollRect_EntryRequest.content.anchoredPosition = Vector2.zero;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) GuildLobby.Instance, (UnityEngine.Object) null))
        return;
      GuildMemberData[] entryRequests = GuildManager.Instance.EntryRequests;
      bool flag = entryRequests.Length > 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mExistInviteBadge, (UnityEngine.Object) null))
        this.mExistInviteBadge.SetActive(flag);
      this.mNoEntryRequestsCaution.SetActive(!flag);
      ((Selectable) this.mEntryRequestRejectAllButton).interactable = flag;
      int num = entryRequests.Length - this.mCreatedItems_EntryRequest.Count;
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mEntryRequestTemplate);
        gameObject.transform.SetParent(this.mEntryRequestTemplate.transform.parent, false);
        this.mCreatedItems_EntryRequest.Add(gameObject);
      }
      for (int index = 0; index < this.mCreatedItems_EntryRequest.Count; ++index)
        this.mCreatedItems_EntryRequest[index].SetActive(false);
      for (int index = 0; index < entryRequests.Length; ++index)
      {
        this.mCreatedItems_EntryRequest[index].SetActive(true);
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_EntryRequest[index], entryRequests[index]);
        DataSource.Bind<UnitData>(this.mCreatedItems_EntryRequest[index], entryRequests[index].Unit);
      }
      this.mEntryRequestTemplate.SetActive(false);
      DataSource.Bind<GuildData>(((Component) this).gameObject, this.mTargetGuild);
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void Refresh_GvG(bool is_update_gameparameter = true)
    {
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnSelect_Info(bool isOn)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect_GuildInfo, (UnityEngine.Object) null))
        return;
      this.mScrollRect_GuildInfo.content.anchoredPosition = Vector2.zero;
    }

    private void OnSelect_MemberList(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_MemberList();
    }

    private void OnSelect_Edit(bool isOn)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScrollRect_GuildEdit, (UnityEngine.Object) null))
        return;
      this.mScrollRect_GuildEdit.content.anchoredPosition = Vector2.zero;
    }

    private void OnSelect_MemberManagement(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_MemberManagement();
    }

    private void OnSelect_EntryRequest(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_EntryRequest();
    }

    private void OnSelect_GvG(bool isOn)
    {
      if (!isOn)
        return;
      bool flag = MonoSingleton<GameManager>.Instance.Player.Guild.GvGJoinStatus == 1;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGJoinTab, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mGvGJoinTab, flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGCancelTab, (UnityEngine.Object) null))
        GameUtility.SetToggle(this.mGvGCancelTab, !flag);
      this.SelectTab = !flag ? this.mGvGCancelTab : this.mGvGJoinTab;
      this.Refresh_GvG();
    }

    public void OnClickMemberUnitIcon(GameObject obj)
    {
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (dataOfClass == null)
        return;
      FlowNode_Variable.Set("SelectUserID", dataOfClass.Uid);
      FlowNode_Variable.Set("IsBlackList", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }

    public void OnClickEntryRequestInfo(GameObject obj)
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      component.list.SetObject(GuildSVB_Key.MEMBER, (object) dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    public void OnClickEntryRequestRejectAll()
    {
      if (GuildManager.Instance.EntryRequests.Length <= 0)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
    }

    public void OnClickMemberKick(GameObject obj)
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      component.list.SetObject(GuildSVB_Key.MEMBER, (object) dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    public void OnClickChangeRole(GameObject obj)
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      component.list.SetObject(GuildSVB_Key.MEMBER, (object) dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1030);
    }

    private void OnSortChange(int idx)
    {
      if ((GuildCustomMenu.eSortType) idx == this.mSortType || idx < 0 || idx >= 3)
        return;
      this.mSortType = (GuildCustomMenu.eSortType) idx;
      this.RefreshMemberListItem();
      this.RefreshMemberManagementItem();
      GameParameter.UpdateAll(((Component) this).gameObject);
      if (string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT))
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT, idx, true);
    }

    public void OnClickChangeGvGJoin(Toggle obj)
    {
      if (!obj.isOn || UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectTab, (UnityEngine.Object) obj))
        return;
      if (this.isGvGJoin && (GvGPeriodParam.GetGvGPeriod() != null || GvGPeriodParam.IsGvGPrepare()))
      {
        bool flag = MonoSingleton<GameManager>.Instance.Player.Guild.GvGJoinStatus == 1;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGJoinTab, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.mGvGJoinTab, flag);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGvGCancelTab, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.mGvGCancelTab, !flag);
        this.SelectTab = !flag ? this.mGvGCancelTab : this.mGvGJoinTab;
        UIUtility.SystemMessage(LocalizedText.Get("sys.GVG_JOIN_FAILED_MESSAGE"), (UIUtility.DialogResultEvent) null, systemModal: true);
      }
      else
      {
        this.SelectTab = obj;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGvGJoinTab, (UnityEngine.Object) obj))
          MonoSingleton<GameManager>.Instance.Player.Guild.GvGJoinStatus = 1;
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGvGCancelTab, (UnityEngine.Object) obj))
          MonoSingleton<GameManager>.Instance.Player.Guild.GvGJoinStatus = 2;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1040);
      }
    }

    public static void Sort_JoinAt(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) => (int) (member2.JoinedAt - member1.JoinedAt)));
    }

    public static void Sort_LastLogin(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) => (int) (member2.LastLogin - member1.LastLogin)));
    }

    public static void Sort_PlayerLevel(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) =>
      {
        int num = member2.Level - member1.Level;
        return num != 0 ? num : (int) (member2.JoinedAt - member1.JoinedAt);
      }));
    }

    private enum eTab
    {
      INFO,
      MEMBER_LIST,
      EDIT,
      MEMBER_MANAGEMENT,
      ENTRY_REQUEST,
      GVG,
      MAX,
    }

    public enum eSortType
    {
      DEFAULT = 0,
      ENTRY_DATE = 0,
      MIN = 0,
      LAST_LOGIN = 1,
      PLAYER_LEVEL = 2,
      MAX = 3,
    }
  }
}
