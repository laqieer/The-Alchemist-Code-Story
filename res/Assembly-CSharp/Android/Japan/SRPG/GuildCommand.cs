// Decompiled with JetBrains decompiler
// Type: SRPG.GuildCommand
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "次に進む", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "前に戻る", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(40, "チャンネル更新", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(50, "おすすめギルド一覧更新", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "おすすめギルド検索条件の設定", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(101, "ギルド編集Reqest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "ギルド加入申請Reqest", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "ギルドメンバー詳細を展開", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(130, "ギルドメンバー役職変更", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(140, "勧誘メッセージ投稿完了", FlowNode.PinTypes.Output, 140)]
  [FlowNode.Pin(150, "ギルド詳細ウィンドウを展開", FlowNode.PinTypes.Output, 150)]
  [FlowNode.Pin(1001, "NGワードエラー", FlowNode.PinTypes.Output, 1001)]
  public class GuildCommand : MonoBehaviour, IFlowInterface
  {
    private List<int> mSearchConditionsPlayerLevelList = new List<int>();
    private List<int> mSearchConditionsGuildLevelList = new List<int>();
    [SerializeField]
    private int mEntryLevelMin = 50;
    [SerializeField]
    private int mEntryPlayerLevelDistance = 20;
    [SerializeField]
    private int mEntryGuildLevelDistance = 10;
    private List<int> mEntryLevelList = new List<int>();
    private List<GuildCommand.CommandProc> mCommands = new List<GuildCommand.CommandProc>();
    private List<GameObject> mRecommendGuildItems = new List<GameObject>();
    private List<GameObject> mGuildEmblemItems = new List<GameObject>();
    private const int PIN_INPUT_NEXT = 10;
    private const int PIN_INPUT_BACK = 20;
    private const int PIN_INPUT_ENTRY_GUILD = 30;
    private const int PIN_INPUT_REFRESH_CHANNEL = 40;
    private const int PIN_INPUT_REFRESH_RECOMMEND_LIST = 50;
    private const int PIN_INPUT_RECOMMEND_LIST_SEARCH = 60;
    private const int PIN_OUTPUT_EDIT_GUILD = 101;
    private const int PIN_OUTPUT_ENTRY_REQUEST = 110;
    private const int PIN_OUTPUT_OPEN_MEMBER_INFO = 120;
    private const int PIN_OUTPUT_CHANGE_ROLE = 130;
    private const int PIN_OUTPUT_END_INVITE_MESSAGE = 140;
    private const int PIN_OUTPUT_OPEN_GUILD_INFO = 150;
    private const int PIN_OUTPUT_NGWORD = 1001;
    [SerializeField]
    private GameObject mGuildEditName;
    [SerializeField]
    private GameObject mGuildSelectEmblem;
    [SerializeField]
    private GameObject mGuildRecommendList;
    [SerializeField]
    private GameObject mGuildEditBoard;
    [SerializeField]
    private GameObject mGuildEditEntryConditions;
    [SerializeField]
    private GameObject mGuildEntryInvite;
    [SerializeField]
    private GameObject mGuildEditInviteMessage;
    [SerializeField]
    private GameObject mGuildChangeRole;
    [SerializeField]
    private GameObject mGuildRecommendListSearch;
    [SerializeField]
    private Text mGuildRenameCostText;
    [SerializeField]
    private Text mGuildEmblemCostText;
    [SerializeField]
    private InputFieldCensorship mInputGuildName;
    [SerializeField]
    private InputFieldCensorship mInputGuildBoard;
    [SerializeField]
    private InputFieldCensorship mInputEntryInviteMessage;
    [SerializeField]
    private InputFieldCensorship mInputInviteMessage;
    [SerializeField]
    private Button mSubmit_EditGuildNameButton;
    [SerializeField]
    private Button mSubmit_EditGuildEmblemButton;
    [SerializeField]
    private Button mSubmit_EditGuildBoardButton;
    [SerializeField]
    private Button mSubmit_EntryInviteButton;
    [SerializeField]
    private Button mSubmit_EditInviteMessageButton;
    [SerializeField]
    private GameObject mEmblemSelectParent;
    [SerializeField]
    private GameObject mEmblemSelectTemplate;
    [SerializeField]
    private GameObject mEmblemSelectButtonsNormal;
    [SerializeField]
    private GameObject mEmblemSelectButtonsNoRequest;
    [SerializeField]
    private GameObject mRecommendGuildParent;
    [SerializeField]
    private GameObject mRecommendGuildTemplate;
    [SerializeField]
    private ScrollablePulldown mPulldown_SearchConditionsPlayerLv;
    [SerializeField]
    private ScrollablePulldown mPulldown_SearchConditionsGuildLv;
    [SerializeField]
    private InputFieldCensorship mSearchConditions_GuildName;
    [SerializeField]
    private SRPG_InputField mSearchConditions_GuildId;
    [SerializeField]
    private ScrollablePulldown mSearchConditions_AutoApproval;
    [SerializeField]
    private int mSearchConditions_GuildLv;
    [SerializeField]
    private GameObject mSearchResultNoneMessage;
    [SerializeField]
    private GameObject mTargetChannelObject;
    [SerializeField]
    private Toggle mAutoApprovalToggle;
    [SerializeField]
    private ScrollablePulldown mEntryLevelPulldown;
    [SerializeField]
    private Toggle mChangeRoleToggle_Mastar;
    [SerializeField]
    private Toggle mChangeRoleToggle_SubMastar;
    [SerializeField]
    private Toggle mChangeRoleToggle_Member;
    private GameObject mConfirmBox;
    private static GuildCommand mInstance;
    private int mCommandProgress;
    private GuildCommand.CommandErrorCheckProc mCommandErrorCheck;
    private GuildData mTempGuild;
    private GuildMemberData mTargetMember;
    private GuildMemberData.eRole mTargetRole;
    private GuildCommand.RecommendSearchConditions mSearchConditions;
    private GuildCommand.eCommand mCurrentCommand;
    private GuildCommand.eMode mCurrentMode;
    private List<ChatUtility.ChatInspectionMaster> mChatInspectonMaster;
    private bool mIsLoadedInspectionMaster;
    private GuildEmblemParam mSelectedEmblem;
    private int mEmblemListPage;
    private GuildEmblemParam[] mNoConditionsEmblemParams;
    private GuildEmblemParam[] mHaveEmblemParams;
    private const string SELECT_BADGE_KEY = "SelectBadge";

    public static GuildCommand Instance
    {
      get
      {
        return GuildCommand.mInstance;
      }
    }

    public GuildData TempGuild
    {
      get
      {
        return this.mTempGuild;
      }
    }

    public GuildMemberData TargetMember
    {
      get
      {
        return this.mTargetMember;
      }
    }

    public GuildMemberData.eRole TargetRole
    {
      get
      {
        return this.mTargetRole;
      }
    }

    public GuildCommand.RecommendSearchConditions SearchConditions
    {
      get
      {
        return this.mSearchConditions;
      }
    }

    private GuildEmblemParam[] NoConditionsEmblemParams
    {
      get
      {
        if (this.mNoConditionsEmblemParams == null)
          this.mNoConditionsEmblemParams = MonoSingleton<GameManager>.Instance.MasterParam.GetNoConditionsGuildEmblemes();
        return this.mNoConditionsEmblemParams;
      }
    }

    private GuildEmblemParam[] HaveEmblemParams
    {
      get
      {
        if (this.mHaveEmblemParams == null)
        {
          List<GuildEmblemParam> guildEmblemParamList = new List<GuildEmblemParam>((IEnumerable<GuildEmblemParam>) MonoSingleton<GameManager>.Instance.MasterParam.GetGuildEmblemes());
          List<string> have = new List<string>((IEnumerable<string>) this.TempGuild.HaveAwards);
          guildEmblemParamList.RemoveAll((Predicate<GuildEmblemParam>) (t =>
          {
            if (!have.Contains(t.Iname))
              return t.ConditionsType > 0;
            return false;
          }));
          this.mHaveEmblemParams = guildEmblemParamList.ToArray();
        }
        return this.mHaveEmblemParams;
      }
    }

    public GuildEmblemParam SelectedEmblem
    {
      get
      {
        return this.mSelectedEmblem;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Next();
          break;
        case 20:
          this.Back();
          break;
        case 30:
          this.SetupEntryGuild();
          break;
        case 40:
          this.RefreshSendTargetChannel();
          break;
        case 50:
          this.RefreshRecommendList(this.mSearchConditions);
          break;
        case 60:
          this.OpenRecommendListSearch();
          break;
      }
    }

    private void Awake()
    {
      GuildCommand.mInstance = this;
      this.HideAll();
    }

    private void Start()
    {
      this.mCurrentCommand = this.GetCommand();
      this.mTempGuild = GuildData.Clone(this.GetGuildData());
      this.SetupUI(this.mTempGuild);
      switch (this.mCurrentCommand)
      {
        case GuildCommand.eCommand.EDIT_GUILD_NAME:
          this.SetupEditGuildName();
          break;
        case GuildCommand.eCommand.SELECT_EMBLEM:
          this.SetupSelectEmblem();
          break;
        case GuildCommand.eCommand.EDIT_BOARD:
          this.SetupEditBoard();
          break;
        case GuildCommand.eCommand.EDIT_ENTRY_CONDITIONS:
          this.SetupEditEntryConditions();
          break;
        case GuildCommand.eCommand.ENTRY_INVITE:
          this.SetupEntryInvite();
          break;
        case GuildCommand.eCommand.RECOMMEND_LIST:
          this.SetupEntryGuild();
          break;
        case GuildCommand.eCommand.EDIT_INVITE_MESSAGE:
          this.SetupEditInviteMessage();
          break;
        case GuildCommand.eCommand.SELECT_EMBLEM_NOREQUEST:
          this.SetupSelectEmblem();
          break;
        case GuildCommand.eCommand.CHANGE_ROLE:
          this.SetupChangeRole();
          break;
      }
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.mSubmit_EditGuildNameButton != (UnityEngine.Object) null && (UnityEngine.Object) this.mInputGuildName != (UnityEngine.Object) null)
        this.mSubmit_EditGuildNameButton.interactable = !string.IsNullOrEmpty(this.mInputGuildName.text) && this.mTempGuild.Name != this.mInputGuildName.text;
      if ((UnityEngine.Object) this.mSubmit_EditGuildEmblemButton != (UnityEngine.Object) null && this.mTempGuild != null)
        this.mSubmit_EditGuildEmblemButton.interactable = this.mSelectedEmblem.Image != this.mTempGuild.Emblem;
      if ((UnityEngine.Object) this.mSubmit_EditGuildBoardButton != (UnityEngine.Object) null && (UnityEngine.Object) this.mInputGuildBoard != (UnityEngine.Object) null)
        this.mSubmit_EditGuildBoardButton.interactable = !string.IsNullOrEmpty(this.mInputGuildBoard.text);
      if ((UnityEngine.Object) this.mSubmit_EntryInviteButton != (UnityEngine.Object) null && (UnityEngine.Object) this.mInputEntryInviteMessage != (UnityEngine.Object) null)
        this.mSubmit_EntryInviteButton.interactable = !string.IsNullOrEmpty(this.mInputEntryInviteMessage.text);
      if (!((UnityEngine.Object) this.mSubmit_EditInviteMessageButton != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mInputInviteMessage != (UnityEngine.Object) null))
        return;
      this.mSubmit_EditInviteMessageButton.interactable = !string.IsNullOrEmpty(this.mInputInviteMessage.text);
    }

    public static void SetCommand(GameObject svb_obj, GuildCommand.eCommand command)
    {
      if ((UnityEngine.Object) svb_obj == (UnityEngine.Object) null)
        return;
      SerializeValueBehaviour component = svb_obj.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.list.SetObject(GuildSVB_Key.COMMNAD, (object) command);
    }

    private GuildCommand.eCommand GetCommand()
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component.list.GetObject<GuildCommand.eCommand>(GuildSVB_Key.COMMNAD, GuildCommand.eCommand.NONE);
      return GuildCommand.eCommand.NONE;
    }

    private GuildData GetGuildData()
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component.list.GetObject<GuildData>(GuildSVB_Key.GUILD, (GuildData) null);
      return (GuildData) null;
    }

    private void SetupUI(GuildData data)
    {
      this.mInputGuildName.SetText(data.Name);
      this.mInputGuildBoard.SetText(data.Board);
      this.mInputInviteMessage.SetText(data.EntryConditions.Comment);
      this.mSelectedEmblem = Array.Find<GuildEmblemParam>(this.NoConditionsEmblemParams, (Predicate<GuildEmblemParam>) (e => e.Image == data.Emblem));
      if (this.mSelectedEmblem == null)
        this.mSelectedEmblem = this.NoConditionsEmblemParams[0];
      this.RefreshSendTargetChannel();
      this.mAutoApprovalToggle.isOn = data.EntryConditions.IsAutoApproval;
      this.mGuildRenameCostText.text = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildRenameCost.ToString();
      this.mGuildEmblemCostText.text = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildEmblemCost.ToString();
    }

    private void SetupTempGuildByUI()
    {
      this.mTempGuild.Name = this.mInputGuildName.text;
      this.mTempGuild.Emblem = this.mSelectedEmblem.Image;
      this.mTempGuild.Board = this.mInputGuildBoard.text;
      this.mTempGuild.EntryConditions.Comment = this.mInputInviteMessage.text;
      this.mTempGuild.EntryConditions.IsAutoApproval = this.mAutoApprovalToggle.isOn;
      if (this.mCurrentCommand != GuildCommand.eCommand.EDIT_ENTRY_CONDITIONS)
        return;
      this.mTempGuild.EntryConditions.LowerLevel = this.mEntryLevelList[this.mEntryLevelPulldown.Selection];
    }

    private void HideAll()
    {
      this.mGuildEditName.SetActive(false);
      this.mGuildSelectEmblem.SetActive(false);
      this.mGuildEditBoard.SetActive(false);
      this.mGuildEditEntryConditions.SetActive(false);
      this.mGuildEditInviteMessage.SetActive(false);
      this.mGuildRecommendList.SetActive(false);
      this.mGuildEntryInvite.SetActive(false);
      this.mGuildChangeRole.SetActive(false);
    }

    private void SetupEntryGuild()
    {
      this.mCurrentMode = GuildCommand.eMode.NONE;
      BitArray bits = new BitArray(10);
      bits.Set(8, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupEditGuildName()
    {
      this.LoadChatInspectonMaster();
      this.mCurrentMode = GuildCommand.eMode.EDIT;
      BitArray bits = new BitArray(10);
      bits.Set(1, true);
      bits.Set(9, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupSelectEmblem()
    {
      this.mCurrentMode = this.mCurrentCommand != GuildCommand.eCommand.SELECT_EMBLEM ? GuildCommand.eMode.SET_PARAM : GuildCommand.eMode.EDIT;
      BitArray bits = new BitArray(10);
      bits.Set(2, true);
      if (this.mCurrentCommand == GuildCommand.eCommand.SELECT_EMBLEM && MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildEmblemCost > 0)
        bits.Set(9, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupEditBoard()
    {
      this.LoadChatInspectonMaster();
      this.mCurrentMode = GuildCommand.eMode.EDIT;
      BitArray bits = new BitArray(10);
      bits.Set(3, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupEditEntryConditions()
    {
      this.mCurrentMode = GuildCommand.eMode.EDIT;
      BitArray bits = new BitArray(10);
      bits.Set(4, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupEntryInvite()
    {
      this.LoadChatInspectonMaster();
      this.mCurrentMode = GuildCommand.eMode.INVITE;
      BitArray bits = new BitArray(10);
      bits.Set(5, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupEditInviteMessage()
    {
      this.LoadChatInspectonMaster();
      this.mCurrentMode = GuildCommand.eMode.EDIT;
      BitArray bits = new BitArray(10);
      bits.Set(6, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupChangeRole()
    {
      this.mCurrentMode = GuildCommand.eMode.CHANGE_ROLE;
      BitArray bits = new BitArray(10);
      bits.Set(7, true);
      this.SetupDelegate(bits);
      this.Invoke();
    }

    private void SetupDelegate(BitArray bits)
    {
      this.mCommands.Clear();
      this.mCommandProgress = 0;
      for (int index = 0; index < bits.Length; ++index)
      {
        if (bits.Get(index))
        {
          switch (index)
          {
            case 1:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_EditGuildName));
              continue;
            case 2:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_SelectEmblem));
              continue;
            case 3:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_EditBoard));
              continue;
            case 4:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_EditEntryConditions));
              continue;
            case 5:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_EntryInvite));
              continue;
            case 6:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_EditInviteMessage));
              continue;
            case 7:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_ChangeRole));
              continue;
            case 8:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_RecommendList));
              continue;
            case 9:
              this.mCommands.Add(new GuildCommand.CommandProc(this.Guild_ConfirmUseCoin));
              continue;
            default:
              continue;
          }
        }
      }
    }

    private void LoadChatInspectonMaster()
    {
      if (this.mIsLoadedInspectionMaster)
        return;
      this.mChatInspectonMaster = ChatUtility.LoadInspectionMaster(ref this.mIsLoadedInspectionMaster);
      if (this.mIsLoadedInspectionMaster)
        return;
      DebugUtility.LogError("FAILED : GUILD_COMMAND => ChatUtility.LoadInspectionMaster");
    }

    private bool IsExistNGWord(string text)
    {
      string str = ChatUtility.ReplaceNGWord(text, this.mChatInspectonMaster, "*");
      return text != str;
    }

    private void SetupPulldown(ScrollablePulldown pulldown, List<int> level_list, int lower_lv, int min_lv, int distance)
    {
      Dictionary<int, string> conditionsLvTable = GuildData.CreateConditionsLvTable(min_lv, distance);
      level_list.Clear();
      pulldown.ClearItems();
      pulldown.Selection = -1;
      bool flag = false;
      int num = 0;
      foreach (int key in conditionsLvTable.Keys)
      {
        pulldown.AddItem(conditionsLvTable[key], num);
        level_list.Add(key);
        if (!flag && lower_lv <= key)
        {
          pulldown.Selection = num;
          flag = true;
        }
        ++num;
      }
    }

    private void SetupPulldown(ScrollablePulldown pulldown, List<int> level_list, int level, int distance)
    {
      level_list.Clear();
      pulldown.ClearItems();
      pulldown.Selection = -1;
      int levelMax = GuildFacilityData.GetLevelMax(GuildFacilityParam.eFacilityType.BASE_CAMP);
      string format = LocalizedText.Get("sys.GUILD_SEARCH_CONDITIONS_GUILD_LV");
      int num1 = 1;
      int num2 = 0;
      while (true)
      {
        if (num2 <= 0)
        {
          string label = LocalizedText.Get("sys.GUILD_ENTRY_CONDITIONS_LV0");
          pulldown.AddItem(label, num2);
          level_list.Add(num2);
          if (level == num2)
            pulldown.Selection = num2;
        }
        else if (num1 + distance < levelMax)
        {
          int num3 = Mathf.Min(num1 + distance - 1, levelMax);
          string label = string.Format(format, (object) num1, (object) num3);
          pulldown.AddItem(label, num2);
          level_list.Add(num1);
          if (level == num1)
            pulldown.Selection = num2;
          num1 += distance;
        }
        else
          break;
        ++num2;
      }
      string label1 = string.Format(format, (object) num1, (object) levelMax);
      pulldown.AddItem(label1, num2);
      level_list.Add(num1);
      if (level != num1)
        return;
      pulldown.Selection = num2;
    }

    private void SetupPulldown(ScrollablePulldown pulldown, GuildCommand.RecommendSearchConditions.AutoApproval auto_approval)
    {
      pulldown.ClearItems();
      pulldown.Selection = -1;
      Dictionary<GuildCommand.RecommendSearchConditions.AutoApproval, string> dictionary = new Dictionary<GuildCommand.RecommendSearchConditions.AutoApproval, string>();
      dictionary.Add(GuildCommand.RecommendSearchConditions.AutoApproval.None, "GUILD_ENTRY_CONDITIONS_LV0");
      dictionary.Add(GuildCommand.RecommendSearchConditions.AutoApproval.On, "GUILD_AUTO_APPROVAL_ON");
      dictionary.Add(GuildCommand.RecommendSearchConditions.AutoApproval.Off, "GUILD_AUTO_APPROVAL_OFF");
      for (int index = 0; index < 3; ++index)
      {
        pulldown.AddItem(LocalizedText.Get("sys." + dictionary[(GuildCommand.RecommendSearchConditions.AutoApproval) index]), index);
        if (auto_approval == (GuildCommand.RecommendSearchConditions.AutoApproval) index)
          pulldown.Selection = index;
      }
    }

    private void Guild_EditGuildName()
    {
      this.HideAll();
      this.mCommandErrorCheck = new GuildCommand.CommandErrorCheckProc(this.ErrorCheck_GuildName);
      DataSource.Bind<GuildData>(this.mGuildEditName, this.mTempGuild, false);
      this.mGuildEditName.SetActive(true);
      GameParameter.UpdateAll(this.mGuildEditName);
    }

    private void Guild_ConfirmUseCoin()
    {
      if ((UnityEngine.Object) this.mConfirmBox != (UnityEngine.Object) null)
        return;
      string text = string.Empty;
      switch (this.mCurrentCommand)
      {
        case GuildCommand.eCommand.EDIT_GUILD_NAME:
          text = string.Format(LocalizedText.Get("sys.GUILD_RENAME_CAUTION"), (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildRenameCost);
          break;
        case GuildCommand.eCommand.SELECT_EMBLEM:
          text = string.Format(LocalizedText.Get("sys.GUILD_EDIT_EMBLEM_CAUTION"), (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildEmblemCost);
          break;
      }
      this.mConfirmBox = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.ConfrimNext), new UIUtility.DialogResultEvent(this.ConfrimBack), (GameObject) null, false, -1, (string) null, (string) null);
    }

    private void ConfrimNext(GameObject obj)
    {
      this.Next();
    }

    private void ConfrimBack(GameObject obj)
    {
      this.Back();
    }

    private void Guild_SelectEmblem()
    {
      this.HideAll();
      this.mCommandErrorCheck = (GuildCommand.CommandErrorCheckProc) null;
      if (this.mGuildEmblemItems.Count <= 0)
      {
        this.mGuildEmblemItems.Clear();
        for (int index = 0; index < this.HaveEmblemParams.Length; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mEmblemSelectTemplate);
          gameObject.transform.SetParent(this.mEmblemSelectParent.transform, false);
          this.mGuildEmblemItems.Add(gameObject);
        }
        this.mEmblemSelectTemplate.SetActive(false);
      }
      if (this.mCurrentCommand == GuildCommand.eCommand.SELECT_EMBLEM_NOREQUEST || MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildEmblemCost <= 0)
      {
        this.mEmblemSelectButtonsNormal.SetActive(false);
        this.mEmblemSelectButtonsNoRequest.SetActive(true);
      }
      else if (this.mCurrentCommand == GuildCommand.eCommand.SELECT_EMBLEM)
      {
        this.mEmblemSelectButtonsNormal.SetActive(true);
        this.mEmblemSelectButtonsNoRequest.SetActive(false);
      }
      this.RefreshSelectGuildEmblemList();
    }

    private void SelectEmblem(GameObject obj)
    {
      if ((UnityEngine.Object) obj == (UnityEngine.Object) null)
        return;
      GuildEmblemParam dataOfClass = DataSource.FindDataOfClass<GuildEmblemParam>(obj, (GuildEmblemParam) null);
      if (dataOfClass != null)
        this.mSelectedEmblem = dataOfClass;
      this.StopSelectEmblemAnimation();
      SerializeValueBehaviour component = obj.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      GameObject gameObject = component.list.GetGameObject("SelectBadge");
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      gameObject.SetActive(true);
    }

    private void StopSelectEmblemAnimation()
    {
      foreach (SerializeValueBehaviour componentsInChild in this.mEmblemSelectParent.GetComponentsInChildren<SerializeValueBehaviour>(true))
      {
        GameObject gameObject = componentsInChild.list.GetGameObject("SelectBadge");
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          gameObject.SetActive(false);
      }
    }

    private void RefreshSelectGuildEmblemList()
    {
      for (int index = 0; index < this.mGuildEmblemItems.Count; ++index)
        this.mGuildEmblemItems[index].SetActive(false);
      this.StopSelectEmblemAnimation();
      for (int index = 0; index < this.mGuildEmblemItems.Count; ++index)
      {
        DataSource.Bind<GuildEmblemParam>(this.mGuildEmblemItems[index], this.HaveEmblemParams[index], false);
        this.mGuildEmblemItems[index].SetActive(true);
        if (this.mSelectedEmblem.Iname == this.HaveEmblemParams[index].Iname)
          this.SelectEmblem(this.mGuildEmblemItems[index]);
      }
      this.mGuildSelectEmblem.SetActive(true);
      GameParameter.UpdateAll(this.mEmblemSelectParent);
    }

    private void Guild_EditBoard()
    {
      this.HideAll();
      this.mCommandErrorCheck = new GuildCommand.CommandErrorCheckProc(this.ErrorCheck_EditBoard);
      DataSource.Bind<GuildData>(this.mGuildEditBoard, this.mTempGuild, false);
      this.mGuildEditBoard.SetActive(true);
      GameParameter.UpdateAll(this.mGuildEditBoard);
    }

    private void Guild_EditEntryConditions()
    {
      this.HideAll();
      this.SetupPulldown(this.mEntryLevelPulldown, this.mEntryLevelList, this.mTempGuild.EntryConditions.LowerLevel, this.mEntryLevelMin, this.mEntryPlayerLevelDistance);
      DataSource.Bind<GuildData>(this.mGuildEditEntryConditions, this.mTempGuild, false);
      this.mGuildEditEntryConditions.SetActive(true);
      GameParameter.UpdateAll(this.mGuildEditEntryConditions);
    }

    private void Guild_EntryInvite()
    {
      this.HideAll();
      this.mCommandErrorCheck = new GuildCommand.CommandErrorCheckProc(this.ErrorCheck_EntryInvite);
      DataSource.Bind<GuildData>(this.mGuildEntryInvite, this.mTempGuild, false);
      this.mGuildEntryInvite.SetActive(true);
      GameParameter.UpdateAll(this.mGuildEntryInvite);
    }

    private void RefreshSendTargetChannel()
    {
      if ((UnityEngine.Object) this.mTargetChannelObject == (UnityEngine.Object) null)
        return;
      int num1 = this.mTargetChannelObject.transform.childCount - 1;
      int length = GlobalVars.CurrentChatChannel.ToString().Length;
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      for (int index = num1; index > 0; --index)
      {
        Transform transform = this.mTargetChannelObject.transform.Find("value_" + Mathf.Pow(10f, (float) (index - 1)).ToString());
        if (!((UnityEngine.Object) transform == (UnityEngine.Object) null))
        {
          if (length < index)
          {
            transform.gameObject.SetActive(false);
          }
          else
          {
            int num2 = (int) Mathf.Pow(10f, (float) (index - 1));
            int num3 = currentChatChannel / num2;
            transform.gameObject.SetActive(true);
            transform.GetComponent<ImageArray>().ImageIndex = num3;
            currentChatChannel %= num2;
          }
        }
      }
    }

    private void Guild_RecommendList()
    {
      this.HideAll();
      this.mCommandErrorCheck = (GuildCommand.CommandErrorCheckProc) null;
      this.RefreshRecommendList((GuildCommand.RecommendSearchConditions) null);
    }

    private void RefreshRecommendList(GuildCommand.RecommendSearchConditions conditions = null)
    {
      if (conditions == null)
        Network.RequestAPI((WebAPI) new ReqGuildRecommendList(new Network.ResponseCallback(this.RequestRecommendListCallback)), false);
      else
        Network.RequestAPI((WebAPI) new ReqGuildSearch(conditions.guild_id, conditions.guild_name, (int) conditions.auto_approval, conditions.player_lv, conditions.guild_lv_from, conditions.guild_lv_to, new Network.ResponseCallback(this.RequestRecommendListCallback)), false);
    }

    private void OpenRecommendListSearch()
    {
      if (this.mSearchConditions == null)
        this.mSearchConditions = new GuildCommand.RecommendSearchConditions();
      this.mPulldown_SearchConditionsPlayerLv.ClosePulldown(false);
      this.mPulldown_SearchConditionsGuildLv.ClosePulldown(false);
      this.mSearchConditions_AutoApproval.ClosePulldown(false);
      this.mSearchConditions_GuildName.text = this.mSearchConditions.guild_name;
      this.mSearchConditions_GuildId.text = this.mSearchConditions.guild_id <= 0L ? string.Empty : this.mSearchConditions.guild_id.ToString();
      this.SetupPulldown(this.mPulldown_SearchConditionsPlayerLv, this.mSearchConditionsPlayerLevelList, this.mSearchConditions.player_lv, this.mEntryLevelMin, this.mEntryPlayerLevelDistance);
      this.SetupPulldown(this.mPulldown_SearchConditionsGuildLv, this.mSearchConditionsGuildLevelList, this.mSearchConditions.guild_lv_from, this.mEntryGuildLevelDistance);
      this.SetupPulldown(this.mSearchConditions_AutoApproval, this.mSearchConditions.auto_approval);
      this.mGuildRecommendListSearch.SetActive(true);
    }

    public void SubmitRecommendListSearch()
    {
      int levelMax = GuildFacilityData.GetLevelMax(GuildFacilityParam.eFacilityType.BASE_CAMP);
      this.mSearchConditions.guild_name = this.mSearchConditions_GuildName.text;
      this.mSearchConditions.guild_id = !string.IsNullOrEmpty(this.mSearchConditions_GuildId.text) ? long.Parse(this.mSearchConditions_GuildId.text) : 0L;
      this.mSearchConditions.player_lv = this.mSearchConditionsPlayerLevelList[this.mPulldown_SearchConditionsPlayerLv.Selection];
      this.mSearchConditions.guild_lv_from = this.mSearchConditionsGuildLevelList[this.mPulldown_SearchConditionsGuildLv.Selection];
      this.mSearchConditions.guild_lv_to = Mathf.Min(levelMax, this.mSearchConditionsGuildLevelList[this.mPulldown_SearchConditionsGuildLv.Selection] + this.mEntryGuildLevelDistance - 1);
      if (this.mPulldown_SearchConditionsGuildLv.Selection >= this.mSearchConditionsGuildLevelList.Count)
        this.mSearchConditions.guild_lv_to = levelMax;
      if (this.mPulldown_SearchConditionsGuildLv.Selection <= 0)
      {
        this.mSearchConditions.guild_lv_from = 0;
        this.mSearchConditions.guild_lv_to = 0;
      }
      this.mSearchConditions.auto_approval = (GuildCommand.RecommendSearchConditions.AutoApproval) this.mSearchConditions_AutoApproval.Selection;
      this.RefreshRecommendList(this.mSearchConditions);
      this.mGuildRecommendListSearch.SetActive(false);
    }

    public void CancelRecommendListSearch()
    {
      this.mGuildRecommendListSearch.SetActive(false);
    }

    private void Guild_EditInviteMessage()
    {
      this.HideAll();
      this.mCommandErrorCheck = new GuildCommand.CommandErrorCheckProc(this.ErrorCheck_EditInviteMessage);
      DataSource.Bind<GuildData>(this.mGuildEditInviteMessage, this.mTempGuild, false);
      this.mGuildEditInviteMessage.SetActive(true);
      GameParameter.UpdateAll(this.mGuildEditBoard);
    }

    private void Guild_ChangeRole()
    {
      this.HideAll();
      this.RefreshChangeRoleUI();
      this.mCommandErrorCheck = (GuildCommand.CommandErrorCheckProc) null;
      DataSource.Bind<GuildData>(this.mGuildChangeRole, this.mTempGuild, false);
      this.mGuildChangeRole.SetActive(true);
      GameParameter.UpdateAll(this.mGuildChangeRole);
    }

    private void RefreshChangeRoleUI()
    {
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      this.mTargetMember = component.list.GetObject<GuildMemberData>(GuildSVB_Key.MEMBER, (GuildMemberData) null);
      if (this.mTargetMember == null)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsSubGuildMaster)
        this.mChangeRoleToggle_Mastar.gameObject.SetActive(false);
      switch (this.mTargetMember.RoleId)
      {
        case GuildMemberData.eRole.MASTAER:
          this.mChangeRoleToggle_Mastar.isOn = true;
          break;
        case GuildMemberData.eRole.MEMBER:
          this.mChangeRoleToggle_Member.isOn = true;
          break;
        case GuildMemberData.eRole.SUB_MASTAER:
          this.mChangeRoleToggle_SubMastar.isOn = true;
          break;
        default:
          this.mChangeRoleToggle_Member.isOn = true;
          break;
      }
    }

    private void SetChangeRoleId()
    {
      this.mTargetRole = GuildMemberData.eRole.NONE;
      if (this.mChangeRoleToggle_Mastar.isOn)
        this.mTargetRole = GuildMemberData.eRole.MASTAER;
      else if (this.mChangeRoleToggle_SubMastar.isOn)
      {
        this.mTargetRole = GuildMemberData.eRole.SUB_MASTAER;
      }
      else
      {
        if (!this.mChangeRoleToggle_Member.isOn)
          return;
        this.mTargetRole = GuildMemberData.eRole.MEMBER;
      }
    }

    private void RequestChangeRole(GameObject obj)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 130);
    }

    private bool ErrorCheck_GuildName()
    {
      return this.IsExistNGWord(this.mInputGuildName.text);
    }

    private bool ErrorCheck_EditBoard()
    {
      return this.IsExistNGWord(this.mInputGuildBoard.text);
    }

    private bool ErrorCheck_EntryInvite()
    {
      return this.IsExistNGWord(this.mInputEntryInviteMessage.text);
    }

    private bool ErrorCheck_EditInviteMessage()
    {
      return this.IsExistNGWord(this.mInputInviteMessage.text);
    }

    public void Invoke()
    {
      if (this.mCommands.Count <= this.mCommandProgress)
        return;
      this.mCommands[this.mCommandProgress]();
    }

    public void Next()
    {
      if (this.mCommandErrorCheck != null && this.mCommandErrorCheck())
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
      else if (this.mCommandProgress >= this.mCommands.Count - 1)
      {
        this.CommandEnd();
      }
      else
      {
        this.mCommandProgress = Mathf.Min(this.mCommandProgress + 1, this.mCommands.Count - 1);
        this.Invoke();
      }
    }

    public void Back()
    {
      if (this.mCommandProgress <= 0)
      {
        this.Close();
      }
      else
      {
        this.mCommandProgress = Mathf.Max(0, this.mCommandProgress - 1);
        this.Invoke();
      }
    }

    private void CommandEnd()
    {
      switch (this.mCurrentMode)
      {
        case GuildCommand.eMode.EDIT:
          this.SetupTempGuildByUI();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case GuildCommand.eMode.INVITE:
          if (!((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mInputEntryInviteMessage != (UnityEngine.Object) null) || string.IsNullOrEmpty(this.mInputEntryInviteMessage.text))
            break;
          MonoSingleton<ChatWindow>.Instance.ChangeChatTypeTab(ChatWindow.eChatType.World);
          MonoSingleton<ChatWindow>.Instance.SetMessageDataToFlowNode(this.mInputEntryInviteMessage.text, true, true, new ChatWindow.FinishSendCallback(this.OnFinishSendInviteMessage));
          break;
        case GuildCommand.eMode.SET_PARAM:
          GuildData guildData = this.GetGuildData();
          if (guildData == null)
            break;
          this.SetupTempGuildByUI();
          guildData.CopyParam(this.mTempGuild);
          this.Close();
          break;
        case GuildCommand.eMode.CHANGE_ROLE:
          this.SetChangeRoleId();
          if (this.mTargetMember == null || this.mTargetRole == GuildMemberData.eRole.NONE || this.mTargetMember.RoleId == this.mTargetRole)
            break;
          if (this.mTargetRole == GuildMemberData.eRole.MASTAER)
          {
            if ((UnityEngine.Object) this.mConfirmBox != (UnityEngine.Object) null)
              break;
            this.mConfirmBox = UIUtility.ConfirmBox(LocalizedText.Get("sys.GUILD_CHANGE_ROLE_MASTER_CAUTION"), new UIUtility.DialogResultEvent(this.RequestChangeRole), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
            break;
          }
          this.RequestChangeRole((GameObject) null);
          break;
      }
    }

    private void Close()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }

    private void OnFinishSendInviteMessage()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 140);
    }

    public void OnSelectGuildEmblem(GameObject obj)
    {
      this.SelectEmblem(obj);
    }

    public void OnRequestEntry(GameObject obj)
    {
      GuildData dataOfClass = DataSource.FindDataOfClass<GuildData>(obj, (GuildData) null);
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if (dataOfClass != null && (UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        component.list.SetObject(GuildSVB_Key.GUILD, (object) dataOfClass);
        component.list.SetObject(GuildSVB_Key.PLAYER, (object) MonoSingleton<GameManager>.Instance.Player);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }

    public void OnGuildDetail(GameObject obj)
    {
      GuildData dataOfClass = DataSource.FindDataOfClass<GuildData>(obj, (GuildData) null);
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if (dataOfClass != null && (UnityEngine.Object) component != (UnityEngine.Object) null)
        component.list.SetObject(GuildSVB_Key.GUILD, (object) dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 150);
    }

    private void RequestRecommendListCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_JoinedAlready:
          case Network.EErrCode.Guild_InputNgWord:
            FlowNode_Network.Back();
            UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<GuildCommand.Json_ResRecommendGuild> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<GuildCommand.Json_ResRecommendGuild>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        GuildData[] guildDataArray = (GuildData[]) null;
        try
        {
          if (jsonObject.body.guild_recommend != null)
          {
            guildDataArray = new GuildData[jsonObject.body.guild_recommend.Length];
            for (int index = 0; index < guildDataArray.Length; ++index)
            {
              guildDataArray[index] = new GuildData();
              guildDataArray[index].Deserialize(jsonObject.body.guild_recommend[index]);
            }
          }
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        for (int index = 0; index < this.mRecommendGuildItems.Count; ++index)
          this.mRecommendGuildItems[index].SetActive(false);
        this.mGuildRecommendList.SetActive(true);
        if ((UnityEngine.Object) this.mRecommendGuildParent != (UnityEngine.Object) null && (UnityEngine.Object) this.mRecommendGuildTemplate != (UnityEngine.Object) null)
        {
          if (guildDataArray != null)
          {
            for (int index = 0; index < guildDataArray.Length; ++index)
            {
              GameObject gameObject;
              if (this.mRecommendGuildItems.Count > index)
              {
                gameObject = this.mRecommendGuildItems[index];
              }
              else
              {
                gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mRecommendGuildTemplate);
                gameObject.transform.SetParent(this.mRecommendGuildParent.transform, false);
                this.mRecommendGuildItems.Add(gameObject);
              }
              DataSource.Bind<GuildData>(gameObject, guildDataArray[index], false);
              gameObject.SetActive(true);
            }
          }
          GameParameter.UpdateAll(this.mRecommendGuildParent);
          this.mRecommendGuildTemplate.SetActive(false);
        }
        SRPG_ScrollRect componentInChildren = this.mGuildRecommendList.GetComponentInChildren<SRPG_ScrollRect>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        {
          componentInChildren.StopMovement();
          componentInChildren.SetNormalizedPosition(Vector2.one, true);
        }
        this.mSearchResultNoneMessage.SetActive(guildDataArray == null || guildDataArray.Length <= 0);
      }
    }

    public enum eCommand
    {
      NONE,
      EDIT_GUILD_NAME,
      SELECT_EMBLEM,
      EDIT_BOARD,
      EDIT_ENTRY_CONDITIONS,
      ENTRY_INVITE,
      RECOMMEND_LIST,
      EDIT_INVITE_MESSAGE,
      SELECT_EMBLEM_NOREQUEST,
      CHANGE_ROLE,
    }

    private enum eMode
    {
      NONE,
      EDIT,
      INVITE,
      SET_PARAM,
      CHANGE_ROLE,
    }

    public enum eBitType
    {
      NONE,
      EDIT_GUILD_NAME,
      SELECT_EMBLEM,
      EDIT_BOARD,
      EDIT_ENTRY_CONDITIONS,
      ENTRY_INVITE,
      EDIT_INVITE_MESSAGE,
      CHANGE_ROLE,
      RECOMMEND_LIST,
      CONFRIM_USE_COIN,
      MAX,
    }

    private delegate void CommandProc();

    private delegate bool CommandErrorCheckProc();

    public class RecommendSearchConditions
    {
      public string guild_name;
      public long guild_id;
      public int guild_lv_from;
      public int guild_lv_to;
      public int player_lv;
      public GuildCommand.RecommendSearchConditions.AutoApproval auto_approval;

      public RecommendSearchConditions()
      {
        this.guild_name = string.Empty;
        this.guild_id = 0L;
        this.guild_lv_from = 0;
        this.guild_lv_to = 0;
        this.player_lv = 0;
        this.auto_approval = GuildCommand.RecommendSearchConditions.AutoApproval.None;
      }

      public enum AutoApproval
      {
        None,
        On,
        Off,
        __MAX,
      }
    }

    public class Json_ResRecommendGuild
    {
      public JSON_Guild[] guild_recommend;
    }
  }
}
