// Decompiled with JetBrains decompiler
// Type: SRPG.GvGBattleTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "Battle", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Refresh Offense", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Refresh Defense", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "Hide Chat", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "Open Chat", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(101, "Battle Start", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Battle Start with Message", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Select Refresh", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "Confirm Party", FlowNode.PinTypes.Output, 104)]
  public class GvGBattleTop : MonoBehaviour, IFlowInterface, IPagination
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_INPUT_BATTLE = 3;
    public const int PIN_INPUT_REFRESH_O = 10;
    public const int PIN_INPUT_REFRESH_D = 11;
    public const int PIN_INPUT_HIDECHAT = 12;
    public const int PIN_INPUT_OPENCHAT = 13;
    public const int PIN_OUTPUT_BATTLE = 101;
    public const int PIN_OUTPUT_BATTLE_WITH_MESSAGE = 102;
    public const int PIN_OUTPUT_SELAUTOREFRESH = 103;
    public const int PIN_OUTPUT_CONFIRMPARTY = 104;
    [SerializeField]
    private Text PhaseText;
    [SerializeField]
    private Text NextText;
    [Space(10f)]
    [SerializeField]
    private Text NodeName;
    [SerializeField]
    private GameObject MapParent;
    [SerializeField]
    private GvGPartyContent PartyTemplate;
    [SerializeField]
    private GvGPartyContent SelfPartyContent;
    [SerializeField]
    private Text TotalAttackText;
    [SerializeField]
    private ImageArray LeaderSkillTarget;
    [SerializeField]
    private Button ChangeLeaderSkillButton;
    [SerializeField]
    private Button PartyEditorButton;
    [SerializeField]
    private Text TeamCountText;
    [SerializeField]
    private Button OccupyButton;
    [SerializeField]
    private string SVBBattleButtonName = "Battle_Btn";
    [SerializeField]
    private bool ISBattleButtonNever;
    [SerializeField]
    private Text RemainCoolTimeText;
    [SerializeField]
    private Button mUpdateButton;
    [Space(10f)]
    [SerializeField]
    private Button PageNextButton;
    [SerializeField]
    private Button PagePrevButton;
    [SerializeField]
    private Text PageCurrentText;
    [SerializeField]
    private Text PageTotalText;
    [SerializeField]
    private ScrollRect EnemyPartyScrollRect;
    [Space(10f)]
    [SerializeField]
    private Text mSelfpartyBeatNumText;
    [SerializeField]
    private Text mSelfTotalBeatNumText;
    [SerializeField]
    private Text mEnemyTotalBeatNumText;
    private DateTime CanRefreshTime;
    private DateTime EndTime;
    private DateTime ElapsedTime;
    private GvGNodeData CurrentNode;
    private int BattleSeed;
    private float AttackCoolTime;
    private bool IsCoolTime;
    private List<GvGParty> DefenseParties = new List<GvGParty>();
    private List<GvGPartyContent> PartyList = new List<GvGPartyContent>();
    private List<Button> BattleButtonList = new List<Button>();
    private int TotalDefensePartyCount;
    private int TotalPage = 1;
    public bool IsExecAutoRefresh;

    public static GvGBattleTop Instance { get; private set; }

    public bool CanRefresh => TimeManager.ServerTime.Ticks <= this.CanRefreshTime.Ticks;

    public long[] EditPartyIds { get; private set; }

    public int CurrentPage { get; private set; }

    public GvGParty SelfParty { get; private set; }

    public int SelfBeatNum { get; private set; }

    public int SelfTotalBeatNum { get; private set; }

    public int EnemyTotalBeatNum { get; private set; }

    public bool IsConfirmParty { get; private set; }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        GvGManager.Instance.SetAutoRefreshStatus(GvGManager.GvGAutoRefreshState.Battle);
      GvGBattleTop.Instance = this;
      this.CurrentNode = GvGManager.Instance.NodeDataList.Find((Predicate<GvGNodeData>) (n => n.NodeId == GvGManager.Instance.SelectNodeId));
      if (this.CurrentNode == null)
        return;
      ChangeMaterialList component = ((Component) this).gameObject.GetComponent<ChangeMaterialList>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      GvGManager.Instance.SetNodeColor(this.CurrentNode, component);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        GvGManager.Instance.RevertAutoRefreshStatus();
      GvGBattleTop.Instance = (GvGBattleTop) null;
    }

    private void Start()
    {
      this.CurrentPage = 1;
      GameUtility.SetGameObjectActive((Component) this.PartyTemplate, false);
      this.RemainCoolTimeText.text = "--:--";
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyEditorButton, (UnityEngine.Object) null))
        return;
      ((Component) this.PartyEditorButton).gameObject.SetActive(false);
    }

    private void Update()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null))
        return;
      if (!this.IsCoolTime)
      {
        if ((double) this.AttackCoolTime > 0.0)
        {
          this.AttackCoolTime -= Time.unscaledDeltaTime;
          if (this.BattleButtonList.Count > 0)
          {
            this.IsCoolTime = true;
            this.BattleButtonList.ForEach((Action<Button>) (b => ((Selectable) b).interactable = false));
          }
        }
      }
      else if ((double) this.AttackCoolTime > 0.0)
      {
        this.RemainCoolTimeText.text = string.Format("{0:D2}:{1:D2}", (object) ((int) this.AttackCoolTime / 60), (object) ((int) this.AttackCoolTime % 60));
        this.AttackCoolTime -= Time.unscaledDeltaTime;
      }
      else
      {
        this.IsCoolTime = false;
        this.RemainCoolTimeText.text = "--:--";
        this.BattleButtonList.ForEach((Action<Button>) (b => ((Selectable) b).interactable = true));
      }
      GameUtility.SetButtonIntaractable(this.mUpdateButton, !this.CanRefresh);
      this.ElapsedTime = this.ElapsedTime.AddSeconds((double) Time.unscaledDeltaTime);
      TimeSpan timeSpan = this.EndTime - this.ElapsedTime;
      if (timeSpan <= TimeSpan.Zero)
        timeSpan = TimeSpan.Zero;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextText, (UnityEngine.Object) null))
        this.NextText.text = string.Format(LocalizedText.Get("sys.GVG_PHASE_TIME"), (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      if (timeSpan <= TimeSpan.Zero)
        this.RefreshPhase();
      if (!GvGManager.Instance.CanAutoRefresh || GvGManager.Instance.AutoRefreshStatus != GvGManager.GvGAutoRefreshState.Battle || this.IsExecAutoRefresh || Network.IsConnecting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      this.IsExecAutoRefresh = true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          this.RefreshOffense();
          this.RefreshDefense();
          this.RefreshPhase();
          break;
        case 3:
          this.Battle();
          break;
        case 10:
          this.RefreshOffense();
          this.RefreshPhase();
          break;
        case 11:
          this.RefreshDefense();
          this.RefreshPhase();
          break;
        case 12:
          GvGManager.Instance.HideChatButton();
          break;
        case 13:
          GvGManager.Instance.HideChatButton(true);
          break;
      }
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) || this.CurrentNode == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.NodeName, (UnityEngine.Object) null))
        return;
      this.NodeName.text = this.CurrentNode.NodeParam.Name;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MapParent, (UnityEngine.Object) null))
        return;
      DataSource.Bind<QuestParam>(this.MapParent, Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.iname == this.CurrentNode.NodeParam.QuestId)));
      GameParameter.UpdateAll(this.MapParent);
      GlobalVars.SelectedQuestID = this.CurrentNode.NodeParam.QuestId;
      this.EditPartyIds = new long[3];
    }

    private void RefreshOffense()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SelfPartyContent))
        this.SelfPartyContent.Setup(this.CurrentNode, 0, this.SelfParty);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalAttackText, (UnityEngine.Object) null))
      {
        int totalAttack = 0;
        this.SelfParty.Units.ForEach((Action<GvGPartyUnit>) (unit =>
        {
          totalAttack += (int) unit.Status.param.atk;
          totalAttack += (int) unit.Status.param.mag;
        }));
        this.TotalAttackText.text = totalAttack.ToString();
      }
      UnitData unitData = (UnitData) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkillTarget, (UnityEngine.Object) null))
      {
        if (this.SelfParty.Units.Count > 0)
          unitData = (UnitData) this.SelfParty.Units[0];
        DataSource.Bind<SkillData>(((Component) this.LeaderSkillTarget).gameObject, unitData == null ? (SkillData) null : unitData.CurrentLeaderSkill);
        this.LeaderSkillTarget.ImageIndex = unitData == null || !unitData.IsEquipConceptLeaderSkill() ? 0 : 1;
        GameParameter.UpdateAll(((Component) this.LeaderSkillTarget).gameObject);
      }
      GlobalVars.SelectedLSChangeUnitUniqueID.Set(unitData == null ? 0L : unitData.UniqueID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChangeLeaderSkillButton, (UnityEngine.Object) null))
        ((Selectable) this.ChangeLeaderSkillButton).interactable = unitData != null && unitData.MainConceptCard != null && unitData.MainConceptCard.LeaderSkillIsAvailable();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyEditorButton, (UnityEngine.Object) null))
        ((Component) this.PartyEditorButton).gameObject.SetActive(this.SelfParty.WinNum != 0);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OccupyButton, (UnityEngine.Object) null))
        ;
      this.BattleButtonList.ForEach((Action<Button>) (b => ((Selectable) b).interactable = this.SelfParty.Units.Count > 0 && (double) this.AttackCoolTime <= 0.0));
    }

    private void RefreshDefense()
    {
      if (this.DefenseParties == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelfpartyBeatNumText, (UnityEngine.Object) null))
        this.mSelfpartyBeatNumText.text = string.Format(LocalizedText.Get("sys.GVG_TEXT_BEATNUM", (object) this.SelfBeatNum));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelfTotalBeatNumText, (UnityEngine.Object) null))
        this.mSelfTotalBeatNumText.text = string.Format(LocalizedText.Get("sys.GVG_TEXT_SELFTOTALBEATNUM", (object) this.SelfTotalBeatNum));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEnemyTotalBeatNumText, (UnityEngine.Object) null))
        this.mEnemyTotalBeatNumText.text = string.Format(LocalizedText.Get("sys.GVG_TEXT_ENEMYTOTALBEATNUM", (object) this.EnemyTotalBeatNum));
      this.PartyList.ForEach((Action<GvGPartyContent>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) p).gameObject)));
      this.PartyList.Clear();
      this.BattleButtonList.Clear();
      for (int index = 0; index < this.DefenseParties.Count; ++index)
      {
        if (this.DefenseParties[index] != null)
        {
          GvGPartyContent gvGpartyContent = UnityEngine.Object.Instantiate<GvGPartyContent>(this.PartyTemplate, ((Component) this.PartyTemplate).transform.parent);
          int number = index + 1 + (this.CurrentPage - 1) * GvGManager.Instance.ONE_PAGE_DEFENSE_PARTY_COUNT_MAX;
          if (gvGpartyContent.Setup(this.CurrentNode, number, this.DefenseParties[index]))
          {
            ((Component) gvGpartyContent).gameObject.SetActive(true);
            this.PartyList.Add(gvGpartyContent);
            SerializeValueBehaviour component = ((Component) gvGpartyContent).GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              Button uiButton = component.list.GetUIButton(this.SVBBattleButtonName);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiButton, (UnityEngine.Object) null))
              {
                this.BattleButtonList.Add(uiButton);
                ((Selectable) uiButton).interactable = this.SelfParty != null && this.SelfParty.Units.Count > 0;
                if (this.ISBattleButtonNever)
                  ((Selectable) uiButton).interactable = true;
              }
            }
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamCountText, (UnityEngine.Object) null))
        this.TeamCountText.text = this.TotalDefensePartyCount.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OccupyButton, (UnityEngine.Object) null))
        ((Component) this.OccupyButton).gameObject.SetActive(this.DefenseParties.Count == 0);
      if (this.SelfParty != null && this.SelfParty.Units != null)
      {
        if (this.ISBattleButtonNever)
          this.BattleButtonList.ForEach((Action<Button>) (b => ((Selectable) b).interactable = (double) this.AttackCoolTime <= 0.0));
        else
          this.BattleButtonList.ForEach((Action<Button>) (b => ((Selectable) b).interactable = this.SelfParty.Units.Count > 0 && (double) this.AttackCoolTime <= 0.0));
      }
      this.RefreshPagination();
    }

    private void RefreshPhase()
    {
      this.ElapsedTime = TimeManager.ServerTime;
      this.EndTime = GvGInfo.SetNextPhaseTime(this.PhaseText, this.NextText);
      this.IsExecAutoRefresh = false;
    }

    private void Battle()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("_self");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      GvGPartyContent componentInParent = gameObject.GetComponentInParent<GvGPartyContent>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null) || componentInParent.Party == null)
        return;
      GlobalVars.GvGOffenseParty.Set(this.SelfParty);
      GlobalVars.GvGDefenseParty.Set(componentInParent.Party);
      GlobalVars.SelectedQuestID = this.CurrentNode.NodeParam.QuestId;
      GlobalVars.GvGBattleSeed.Set(this.BattleSeed);
      GlobalVars.GvGNodeId.Set(GvGManager.Instance.SelectNodeId);
      GlobalVars.GvGGroupId.Set(GvGManager.GvGGroupId);
      GlobalVars.GvGBattleReplay.Set(false);
      this.IsConfirmParty = false;
      if (this.SelfParty.WinNum == 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void RefreshPagination()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageTotalText, (UnityEngine.Object) null))
        this.PageTotalText.text = Mathf.Max(this.TotalPage, 1).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageCurrentText, (UnityEngine.Object) null))
        this.PageCurrentText.text = Mathf.Max(this.CurrentPage, 1).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageNextButton, (UnityEngine.Object) null))
        ((Selectable) this.PageNextButton).interactable = this.CurrentPage < this.TotalPage;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagePrevButton, (UnityEngine.Object) null))
        ((Selectable) this.PagePrevButton).interactable = this.CurrentPage > 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnemyPartyScrollRect, (UnityEngine.Object) null))
        return;
      this.EnemyPartyScrollRect.normalizedPosition = Vector2.up;
    }

    public void NextPage()
    {
      if (this.CurrentPage >= this.TotalPage)
        return;
      ++this.CurrentPage;
    }

    public void PrevPage()
    {
      if (1 >= this.CurrentPage)
        return;
      --this.CurrentPage;
    }

    public void OnselectpartyEdit()
    {
      this.IsConfirmParty = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    public void SetEditParty(long[] units) => units.CopyTo((Array) this.EditPartyIds, 0);

    public bool SetupSelfParty(JSON_GvGParty json, int seed = -1, int cool_time = -1)
    {
      if (json == null)
        return false;
      this.SelfParty = new GvGParty();
      if (!this.SelfParty.Deserialize(json))
        return false;
      if (seed >= 0)
        this.BattleSeed = seed;
      if (cool_time >= 0)
        this.AttackCoolTime = (float) (TimeManager.FromUnixTime((long) cool_time) - TimeManager.ServerTime).TotalSeconds;
      return true;
    }

    public bool SetupDefenseParties(JSON_GvGParty[] json, int totalCount, int totalPage, int sec)
    {
      if (json == null)
        return false;
      this.DefenseParties.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        if (json[index] != null)
        {
          GvGParty gvGparty = new GvGParty();
          if (gvGparty.Deserialize(json[index]))
            this.DefenseParties.Add(gvGparty);
        }
      }
      this.TotalDefensePartyCount = totalCount;
      this.TotalPage = totalPage;
      this.CanRefreshTime = TimeManager.ServerTime.AddSeconds((double) sec);
      return true;
    }

    public bool SetupSelfBeatCount(int selfBeat, int selfTotalBeat)
    {
      if (selfBeat >= 0)
        this.SelfBeatNum = selfBeat;
      if (selfTotalBeat >= 0)
        this.SelfTotalBeatNum = selfTotalBeat;
      return true;
    }

    public bool SetupEnemyBeatCount(int enemyTotalBeat)
    {
      this.EnemyTotalBeatNum = enemyTotalBeat;
      return true;
    }
  }
}
