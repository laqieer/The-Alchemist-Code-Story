// Decompiled with JetBrains decompiler
// Type: SRPG.GuildCustomMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1001, "プレイヤー詳細", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1010, "加入申請詳細", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "メンバーの除名", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1021, "加入申請の一括拒否", FlowNode.PinTypes.Output, 1021)]
  [FlowNode.Pin(1030, "メンバーの役職変更", FlowNode.PinTypes.Output, 1030)]
  public class GuildCustomMenu : MonoBehaviour, IFlowInterface
  {
    private readonly string[] SORT_KEYS = new string[3]{ "sys.FRIEND_SORT_ENTRY_DATE", "sys.FRIEND_SORT_LAST_LOGIN", "sys.FRIEND_SORT_PLAYER_LEVEL" };
    private List<GameObject> mCreatedItems_MemberList = new List<GameObject>();
    private List<GameObject> mCreatedItems_MemberManagement = new List<GameObject>();
    private List<GameObject> mCreatedItems_EntryRequest = new List<GameObject>();
    private const int PIN_INPUT_REFRESH = 1;
    private const int PIN_OUTPUT_OPEN_PLAYER_INFO = 1001;
    private const int PIN_OUTPUT_OPEN_ENTRY_REQUEST_INFO = 1010;
    private const int PIN_OUTPUT_OPEN_MEMBER_KICK = 1020;
    private const int PIN_OUTPUT_CONFIRM_ENTRY_REQUEST_NG_ALL = 1021;
    private const int PIN_OUTPUT_OPEN_CHANGE_MEMBER = 1030;
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
    private GameObject[] mTabLockObjects;
    [SerializeField]
    private GameObject mNoEntryRequestsCaution;
    [SerializeField]
    private Pulldown mGuildMemberListSort;
    [SerializeField]
    private Pulldown mGuildMemberManagementSort;
    [SerializeField]
    private Button mEntryRequestRejectAllButton;
    private static GuildCustomMenu mInstance;
    private GuildData mTargetGuild;
    private GuildMemberData.eRole mCurrentRole;
    private GameObject[] mPages;
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

    public static GuildCustomMenu Instance
    {
      get
      {
        return GuildCustomMenu.mInstance;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.RefreshAll();
    }

    private void Awake()
    {
      GuildCustomMenu.mInstance = this;
    }

    private void Start()
    {
      this.Init();
    }

    private void Init()
    {
      this.mCurrentRole = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.RoleId;
      this.SetupTabs();
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        this.mTargetGuild = component.list.GetObject<GuildData>(GuildSVB_Key.GUILD, (GuildData) null);
        DataSource.Bind<GuildData>(this.gameObject, this.mTargetGuild, false);
        DataSource.Bind<GuildMemberData>(this.gameObject, MonoSingleton<GameManager>.Instance.Player.Guild.GuildMaster, false);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void SetupTabs()
    {
      this.mPages = new GameObject[5];
      this.mPages[0] = this.mPage_GuildInfo;
      this.mPages[1] = this.mPage_MemebrList;
      this.mPages[2] = this.mPage_GuildEdit;
      this.mPages[3] = this.mPage_MemberManagement;
      this.mPages[4] = this.mPage_EntryRequest;
      this.mTabs[0].onValueChanged.AddListener(new UnityAction<bool>(this.OnSelect_Info));
      this.mTabs[1].onValueChanged.AddListener(new UnityAction<bool>(this.OnSelect_MemberList));
      this.mTabs[2].onValueChanged.AddListener(new UnityAction<bool>(this.OnSelect_Edit));
      this.mTabs[3].onValueChanged.AddListener(new UnityAction<bool>(this.OnSelect_MemberManagement));
      this.mTabs[4].onValueChanged.AddListener(new UnityAction<bool>(this.OnSelect_EntryRequest));
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
      this.Refresh_MemberList(true);
      this.Refresh_MemberManagement(true);
      this.Refresh_EntryRequest(true);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void SetupGuildMemberSortPulldown(Pulldown pulldown)
    {
      if ((UnityEngine.Object) pulldown == (UnityEngine.Object) null)
        return;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT))
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT, 0);
        if (0 <= num && num < 3)
          this.mSortType = (GuildCustomMenu.eSortType) num;
      }
      pulldown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortChange);
      pulldown.ClearItems();
      for (int index = 0; index < this.SORT_KEYS.Length; ++index)
        pulldown.AddItem(LocalizedText.Get(this.SORT_KEYS[index]), index);
      pulldown.Selection = (int) this.mSortType;
      pulldown.interactable = true;
      pulldown.gameObject.SetActive(true);
    }

    private List<GuildMemberData> GetSortedMemberList(GuildCustomMenu.eSortType sort_type)
    {
      Dictionary<GuildMemberData.eRole, List<GuildMemberData>> dictionary = new Dictionary<GuildMemberData.eRole, List<GuildMemberData>>();
      for (int index = 0; index < this.mTargetGuild.Members.Length; ++index)
      {
        if (!dictionary.ContainsKey(this.mTargetGuild.Members[index].RoleId))
          dictionary.Add(this.mTargetGuild.Members[index].RoleId, new List<GuildMemberData>());
        dictionary[this.mTargetGuild.Members[index].RoleId].Add(this.mTargetGuild.Members[index]);
      }
      foreach (GuildMemberData.eRole key in dictionary.Keys)
      {
        switch (sort_type)
        {
          case GuildCustomMenu.eSortType.MIN:
            this.Sort_JoinAt(dictionary[key]);
            continue;
          case GuildCustomMenu.eSortType.LAST_LOGIN:
            this.Sort_LastLogin(dictionary[key]);
            continue;
          case GuildCustomMenu.eSortType.PLAYER_LEVEL:
            this.Sort_PlayerLevel(dictionary[key]);
            continue;
          default:
            continue;
        }
      }
      List<GuildMemberData> guildMemberDataList = new List<GuildMemberData>();
      if (dictionary.ContainsKey(GuildMemberData.eRole.MASTAER))
        guildMemberDataList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.MASTAER]);
      if (dictionary.ContainsKey(GuildMemberData.eRole.SUB_MASTAER))
        guildMemberDataList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.SUB_MASTAER]);
      if (dictionary.ContainsKey(GuildMemberData.eRole.MEMBER))
        guildMemberDataList.AddRange((IEnumerable<GuildMemberData>) dictionary[GuildMemberData.eRole.MEMBER]);
      return guildMemberDataList;
    }

    private void Refresh_MemberList(bool is_update_gameparameter = true)
    {
      if ((UnityEngine.Object) this.mScrollRect_MemberList != (UnityEngine.Object) null)
        this.mScrollRect_MemberList.content.anchoredPosition = Vector2.zero;
      this.SetupGuildMemberSortPulldown(this.mGuildMemberListSort);
      this.RefreshMemberListItem();
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void RefreshMemberListItem()
    {
      if (this.mTargetGuild == null)
        return;
      List<GuildMemberData> sortedMemberList = this.GetSortedMemberList(this.mSortType);
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
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_MemberList[index], sortedMemberList[index], false);
        DataSource.Bind<UnitData>(this.mCreatedItems_MemberList[index], sortedMemberList[index].Unit, false);
      }
      this.mMemberTemplate1.SetActive(false);
      DataSource.Bind<GuildData>(this.gameObject, this.mTargetGuild, false);
    }

    private void Refresh_MemberManagement(bool is_update_gameparameter = true)
    {
      if ((UnityEngine.Object) this.mScrollRect_MemberManagement != (UnityEngine.Object) null)
        this.mScrollRect_MemberManagement.content.anchoredPosition = Vector2.zero;
      this.SetupGuildMemberSortPulldown(this.mGuildMemberManagementSort);
      this.RefreshMemberManagementItem();
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void RefreshMemberManagementItem()
    {
      if (this.mTargetGuild == null)
        return;
      List<GuildMemberData> sortedMemberList = this.GetSortedMemberList(this.mSortType);
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
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_MemberManagement[index], sortedMemberList[index], false);
        DataSource.Bind<UnitData>(this.mCreatedItems_MemberManagement[index], sortedMemberList[index].Unit, false);
        GuildMemberManagementItem component = this.mCreatedItems_MemberManagement[index].GetComponent<GuildMemberManagementItem>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.Init(sortedMemberList[index]);
      }
      this.mMemberTemplate2.SetActive(false);
      DataSource.Bind<GuildData>(this.gameObject, this.mTargetGuild, false);
    }

    private void Refresh_EntryRequest(bool is_update_gameparameter = true)
    {
      if ((UnityEngine.Object) this.mScrollRect_EntryRequest != (UnityEngine.Object) null)
        this.mScrollRect_EntryRequest.content.anchoredPosition = Vector2.zero;
      if ((UnityEngine.Object) GuildManager.Instance == (UnityEngine.Object) null || (UnityEngine.Object) GuildLobby.Instance == (UnityEngine.Object) null)
        return;
      GuildMemberData[] entryRequests = GuildManager.Instance.EntryRequests;
      this.mNoEntryRequestsCaution.SetActive(entryRequests.Length <= 0);
      this.mEntryRequestRejectAllButton.interactable = entryRequests.Length > 0;
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
        DataSource.Bind<GuildMemberData>(this.mCreatedItems_EntryRequest[index], entryRequests[index], false);
        DataSource.Bind<UnitData>(this.mCreatedItems_EntryRequest[index], entryRequests[index].Unit, false);
      }
      this.mEntryRequestTemplate.SetActive(false);
      DataSource.Bind<GuildData>(this.gameObject, this.mTargetGuild, false);
      if (!is_update_gameparameter)
        return;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnSelect_Info(bool isOn)
    {
      if (!((UnityEngine.Object) this.mScrollRect_GuildInfo != (UnityEngine.Object) null))
        return;
      this.mScrollRect_GuildInfo.content.anchoredPosition = Vector2.zero;
    }

    private void OnSelect_MemberList(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_MemberList(true);
    }

    private void OnSelect_Edit(bool isOn)
    {
      if (!((UnityEngine.Object) this.mScrollRect_GuildEdit != (UnityEngine.Object) null))
        return;
      this.mScrollRect_GuildEdit.content.anchoredPosition = Vector2.zero;
    }

    private void OnSelect_MemberManagement(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_MemberManagement(true);
    }

    private void OnSelect_EntryRequest(bool isOn)
    {
      if (!isOn)
        return;
      this.Refresh_EntryRequest(true);
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
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || dataOfClass == null)
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
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      component.list.SetObject(GuildSVB_Key.MEMBER, (object) dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
    }

    public void OnClickChangeRole(GameObject obj)
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || dataOfClass == null)
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
      GameParameter.UpdateAll(this.gameObject);
      if (string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT))
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_GUILD_MEMBER_SORT, idx, true);
    }

    private void Sort_JoinAt(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) => (int) (member2.JoinedAt - member1.JoinedAt)));
    }

    private void Sort_LastLogin(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) => (int) (member2.LastLogin - member1.LastLogin)));
    }

    private void Sort_PlayerLevel(List<GuildMemberData> lists)
    {
      lists.Sort((Comparison<GuildMemberData>) ((member1, member2) =>
      {
        int num = member2.Level - member1.Level;
        if (num != 0)
          return num;
        return (int) (member2.JoinedAt - member1.JoinedAt);
      }));
    }

    private enum eTab
    {
      INFO,
      MEMBER_LIST,
      EDIT,
      MEMBER_MANAGEMENT,
      ENTRY_REQUEST,
      MAX,
    }

    private enum eSortType
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
