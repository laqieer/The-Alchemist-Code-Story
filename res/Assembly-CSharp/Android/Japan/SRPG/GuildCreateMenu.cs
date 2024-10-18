// Decompiled with JetBrains decompiler
// Type: SRPG.GuildCreateMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "ギルド設立", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(30, "表示更新", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(110, "ギルド設立Reqest", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(1001, "NGワード：ギルド名", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "NGワード：ギルド掲示板", FlowNode.PinTypes.Output, 1002)]
  public class GuildCreateMenu : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private int mEntryLevelMin = 50;
    [SerializeField]
    private int mEntryLevelDistance = 20;
    private List<int> mEntryLevelList = new List<int>();
    private const int PIN_INPUT_CREATE_GUILD = 10;
    private const int PIN_INPUT_REFRESH = 30;
    private const int PIN_OUTPUT_CREATE_GUILD = 110;
    private const int PIN_OUTPUT_NGWORD_GUILD_NAME = 1001;
    private const int PIN_OUTPUT_NGWORD_GUILD_BOARD = 1002;
    private static GuildCreateMenu mInstance;
    [SerializeField]
    private InputFieldCensorship mGuildNameInput;
    [SerializeField]
    private InputFieldCensorship mGuildCommentInput;
    [SerializeField]
    private Toggle mAutoApprovalToggle;
    [SerializeField]
    private Button mSubmitButton;
    [SerializeField]
    private Text mCreateGuildCostText;
    [SerializeField]
    private ScrollablePulldown mEntryLevelPulldown;
    private List<ChatUtility.ChatInspectionMaster> mChatInspectonMaster;
    private bool mIsLoadedInspectionMaster;
    private GuildData mTempGuildData;
    private SerializeValueBehaviour mSerializeValueBehaviour;
    private GameObject mConfirmBox;

    public static GuildCreateMenu Instance
    {
      get
      {
        return GuildCreateMenu.mInstance;
      }
    }

    public GuildData TempGuildData
    {
      get
      {
        return this.mTempGuildData;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 30)
          return;
        this.Refresh();
      }
      else
        this.OnCreate();
    }

    private void Awake()
    {
      GuildCreateMenu.mInstance = this;
      this.mSerializeValueBehaviour = this.GetComponent<SerializeValueBehaviour>();
      if (!((UnityEngine.Object) this.mCreateGuildCostText != (UnityEngine.Object) null))
        return;
      this.mCreateGuildCostText.text = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildCreateCost.ToString();
    }

    private void Start()
    {
      this.Init();
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.mSubmitButton != (UnityEngine.Object) null))
        return;
      this.mSubmitButton.interactable = (UnityEngine.Object) this.mGuildNameInput != (UnityEngine.Object) null && (!string.IsNullOrEmpty(this.mGuildNameInput.text) && (UnityEngine.Object) this.mGuildCommentInput != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.mGuildCommentInput.text));
    }

    private void Init()
    {
      this.mTempGuildData = GuildData.CreateDefault();
      this.mTempGuildData.Board = LocalizedText.Get("sys.GUILD_DEFAULT_BOARD");
      this.mTempGuildData.EntryConditions.Comment = LocalizedText.Get("sys.GUILD_DEFAULT_MESSAGE");
      this.mSerializeValueBehaviour.list.SetObject(GuildSVB_Key.GUILD, (object) this.mTempGuildData);
      DataSource.Bind<GuildData>(this.gameObject, this.mTempGuildData, false);
      this.mGuildNameInput.text = this.mTempGuildData.Name;
      this.mGuildCommentInput.text = this.mTempGuildData.EntryConditions.Comment;
      this.mAutoApprovalToggle.isOn = this.mTempGuildData.EntryConditions.IsAutoApproval;
      this.SetupEntryLevelPulldown(this.mTempGuildData);
      this.LoadChatInspectonMaster();
    }

    private void Refresh()
    {
      GameParameter.UpdateAll(this.gameObject);
    }

    private void CreateGuild(GameObject obj)
    {
      if (this.IsExistNGWord(this.mGuildNameInput.text))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
      else if (this.IsExistNGWord(this.mGuildCommentInput.text))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1002);
      }
      else
      {
        if (this.mTempGuildData == null)
          return;
        this.mTempGuildData.Name = this.mGuildNameInput.text;
        this.mTempGuildData.EntryConditions.Comment = this.mGuildCommentInput.text;
        this.mTempGuildData.EntryConditions.IsAutoApproval = this.mAutoApprovalToggle.isOn;
        this.mTempGuildData.EntryConditions.LowerLevel = this.mEntryLevelList[this.mEntryLevelPulldown.Selection];
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
    }

    public void OnCreate()
    {
      if ((UnityEngine.Object) this.mConfirmBox != (UnityEngine.Object) null)
        return;
      this.mConfirmBox = UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.GUILD_CREATE_CAUTION"), (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GuildCreateCost), new UIUtility.DialogResultEvent(this.CreateGuild), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
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

    private void SetupEntryLevelPulldown(GuildData guild)
    {
      Dictionary<int, string> conditionsLvTable = GuildData.CreateConditionsLvTable(this.mEntryLevelMin, this.mEntryLevelDistance);
      this.mEntryLevelList.Clear();
      this.mEntryLevelPulldown.Selection = -1;
      bool flag = false;
      int num = 0;
      foreach (int key in conditionsLvTable.Keys)
      {
        this.mEntryLevelPulldown.AddItem(conditionsLvTable[key], num);
        this.mEntryLevelList.Add(key);
        if (!flag && guild.EntryConditions.LowerLevel <= key)
        {
          this.mEntryLevelPulldown.Selection = num;
          flag = true;
        }
        ++num;
      }
    }
  }
}
