// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Refresh Enemy", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(110, "Refresh Party", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(120, "Refresh Party ToolTip", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Player Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(150, "Open IAP Window", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(151, "Reset Cooldown", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(152, "Reset Tickets", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(200, "Battle Start Confirm", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(201, "Battle Start (OK)", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(202, "Battle Start (Cancel)", FlowNode.PinTypes.Output, 102)]
  public class ArenaWindow : MonoBehaviour, IFlowInterface, IWebHelp
  {
    public const int PINID_REFRESH_ENEMYLIST = 100;
    public const int PINID_REFRESH_PARTY = 110;
    public const int PINID_REFRESH_PARTY_TOOLTIP = 120;
    public const int PINID_PLAYER_SELECTED = 101;
    public const int PINID_OPEN_IAPWINDOW = 150;
    public const int PINID_RESET_COOLDOWN = 151;
    public const int PINID_RESET_TICKETS = 152;
    public const int PINID_BATTLE_START_CONFIRM = 200;
    public const int PINID_BATTLE_START_OK = 201;
    public const int PINID_BATTLE_START_CANCEL = 202;
    public GameObject PartyInfo;
    public GameObject DefensePartyInfo;
    public GameObject VsPartyInfo;
    public GameObject VsEnemyPartyInfo;
    public SRPG_ListBase EnemyPlayerList;
    public ListItemEvents EnemyPlayerItem;
    public GameObject EnemyPlayerDetail;
    public GameObject HistoryObject;
    public bool RefreshEnemyListOnStart;
    public bool RefreshPartyOnStart;
    public GameObject[] PartyUnitSlots = new GameObject[3];
    public GameObject[] PartyUnitSameObject = new GameObject[3];
    public GameObject PartyUnitLeader;
    public GameObject PartyUnitLeaderVS;
    public GameObject[] DefenseUnitSlots = new GameObject[3];
    public GameObject[] DefenseUnitSameObject = new GameObject[3];
    public GameObject DefenseUnitLeader;
    public GameObject CooldownTimer;
    public Button CooldownResetButton;
    public GameObject BpHolder;
    public GameObject BattlePreWindow;
    public GameObject AttackDeckWindow;
    public GameObject AttackDeckWindowIcon;
    public GameObject DefenseDeckWindow;
    public GameObject DefenseDeckWindowIcon;
    public GameObject EnemyListWindow;
    public GameObject PlayerStatusWindow;
    [FormerlySerializedAs("TotalAtkText")]
    public Text AtkTotalCombatPowerText;
    [FormerlySerializedAs("TotalDefText")]
    public Text DefTotalCombatPowerText;
    public Text ReadyPlayerTotalCombatPowerText;
    private bool mIsActiveAttackParty;
    public Button MatchingButton;
    public Button DeckNextButton;
    public Button DeckPrevButton;
    public Button MatchingCloseButton;
    public Button BattleBackButton;
    public Button ChangeLeaderSkillButton_Atk;
    public Button ChangeLeaderSkillButton_Def;
    public Text LastBattleAtText;
    [Space(10f)]
    public GameObject GoMapInfo;
    public GameObject GoMapInfoThumbnail;
    public GameObject GoMapInfoEndAt;
    public Text TextMapInfoEndAt;
    private bool mIsUpdateMapInfoEndAt;
    private float mPassedTimeMapInfoEndAt;
    private static ArenaWindow mInstance;

    public static ArenaWindow Instance => ArenaWindow.mInstance;

    private void Awake() => ArenaWindow.mInstance = this;

    private void OnDestroy() => ArenaWindow.mInstance = (ArenaWindow) null;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnemyPlayerItem, (UnityEngine.Object) null))
        ((Component) this.EnemyPlayerItem).gameObject.SetActive(false);
      if (this.RefreshEnemyListOnStart)
        this.RefreshEnemyList();
      if (this.RefreshPartyOnStart)
        this.RefreshParty();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CooldownResetButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CooldownResetButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCooldownButtonClick)));
      }
      this.BattlePreWindow.SetActive(false);
      this.ChangeDrawDeck(true);
      this.ChangeDrawInformation(true);
      this.RefreshBattleCount();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MatchingButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MatchingButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMatchingButtonClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MatchingCloseButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MatchingCloseButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMatchingCloseButtonClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DeckNextButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DeckNextButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDeckNextButtonClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DeckPrevButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DeckPrevButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDeckPrevButtonClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleBackButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BattleBackButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnBattleBackButtonClick)));
      }
      DataSource.Bind<PlayerPartyTypes>(this.AttackDeckWindow, PlayerPartyTypes.Arena);
      DataSource.Bind<PlayerPartyTypes>(this.DefenseDeckWindow, PlayerPartyTypes.ArenaDef);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.RefreshEnemyList();
          break;
        case 110:
          this.RefreshParty();
          break;
        case 120:
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) PartyWindow2.Instance, (UnityEngine.Object) null))
            break;
          this.RefreshParty();
          break;
        case 200:
          this.BattleStartConfirm();
          break;
      }
    }

    private void BattleStartConfirm()
    {
      List<UnitData> entryPlayerUnits = this.GetBattleEntryPlayerUnits();
      if (entryPlayerUnits != null)
      {
        List<UnitSameGroupParam> unitSameGroupParamList = UnitSameGroupParam.IsSameUnitInParty(entryPlayerUnits.ToArray());
        if (unitSameGroupParamList != null)
        {
          string empty = string.Empty;
          for (int index = 0; index < unitSameGroupParamList.Count; ++index)
          {
            if (unitSameGroupParamList[index] != null)
            {
              if (index != 0)
                empty += LocalizedText.Get("sys.PARTYEDITOR_SAMEUNIT_PLUS");
              empty += unitSameGroupParamList[index].GetGroupUnitAllNameText();
            }
          }
          if (!string.IsNullOrEmpty(empty))
          {
            UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTY_SAMEUNIT_INPARTY"), (object) empty), (UIUtility.DialogResultEvent) (dialog => { }));
            this.SameUnitInParty();
            return;
          }
        }
      }
      if (AssetDownloader.IsEnableShowSizeBeforeDownloading())
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        AssetDownloader.StartConfirmDownloadQuestContentYesNo(this.GetBattleEntryUnits(), (List<ItemData>) null, quest, (UIUtility.DialogResultEvent) (ok => this.DownloadApproved()), (UIUtility.DialogResultEvent) (no => this.DownloadNotApproved()));
      }
      else
        this.DownloadApproved();
    }

    private void DownloadApproved()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
    }

    private void DownloadNotApproved()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
    }

    private void SameUnitInParty()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
    }

    private void RefreshParty()
    {
      this.RefreshAttackParty();
      this.RefreshDefenseParty();
      this.ChangeDrawDeck(this.mIsActiveAttackParty);
    }

    private void RefreshAttackParty()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.Arena);
      List<UnitData> ownUnits = new List<UnitData>();
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData data = UnitOverWriteUtility.Apply(player.FindUnitDataByUniqueID(unitUniqueId), PartyWindow2.EditPartyTypes.Arena);
        if (index == 0)
        {
          DataSource.Bind<UnitData>(this.PartyUnitLeader, data);
          DataSource.Bind<UnitData>(this.PartyUnitLeaderVS, data);
          GameParameter.UpdateAll(this.PartyUnitLeader);
          GameParameter.UpdateAll(this.PartyUnitLeaderVS);
        }
        DataSource.Bind<UnitData>(this.PartyUnitSlots[index], data);
        GameParameter.UpdateAll(this.PartyUnitSlots[index]);
        ownUnits.Add(data);
      }
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData unitData = UnitOverWriteUtility.Apply(player.FindUnitDataByUniqueID(unitUniqueId), PartyWindow2.EditPartyTypes.Arena);
        GameUtility.SetGameObjectActive(this.PartyUnitSameObject[index], false);
        if (unitData != null && UnitSameGroupParam.IsSameUnitInParty(ownUnits.ToArray(), unitData.UnitID))
          GameUtility.SetGameObjectActive(this.PartyUnitSameObject[index], true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyInfo, (UnityEngine.Object) null))
      {
        DataSource.Bind<PartyData>(this.PartyInfo, partyOfType);
        GameParameter.UpdateAll(this.PartyInfo);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VsPartyInfo, (UnityEngine.Object) null))
      {
        DataSource.Bind<PartyData>(this.VsPartyInfo, partyOfType);
        GameParameter.UpdateAll(this.VsPartyInfo);
      }
      PartyEditData party = new PartyEditData(string.Empty, partyOfType);
      party.SetUnitsForce(ownUnits.ToArray());
      int num = PartyUtility.CalcTotalCombatPower(party, ownUnits);
      this.AtkTotalCombatPowerText.text = num.ToString();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReadyPlayerTotalCombatPowerText, (UnityEngine.Object) null))
        return;
      this.ReadyPlayerTotalCombatPowerText.text = num.ToString();
    }

    private void RefreshDefenseParty()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.ArenaDef);
      List<UnitData> ownUnits = new List<UnitData>();
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData data = UnitOverWriteUtility.Apply(player.FindUnitDataByUniqueID(unitUniqueId), PartyWindow2.EditPartyTypes.ArenaDef);
        if (index == 0)
        {
          DataSource.Bind<UnitData>(this.DefenseUnitLeader, data);
          GameParameter.UpdateAll(this.DefenseUnitLeader);
        }
        DataSource.Bind<UnitData>(this.DefenseUnitSlots[index], data);
        GameParameter.UpdateAll(this.DefenseUnitSlots[index]);
        ownUnits.Add(data);
      }
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData unitData = UnitOverWriteUtility.Apply(player.FindUnitDataByUniqueID(unitUniqueId), PartyWindow2.EditPartyTypes.Arena);
        GameUtility.SetGameObjectActive(this.DefenseUnitSameObject[index], false);
        if (unitData != null && UnitSameGroupParam.IsSameUnitInParty(ownUnits.ToArray(), unitData.UnitID))
          GameUtility.SetGameObjectActive(this.DefenseUnitSameObject[index], true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefensePartyInfo, (UnityEngine.Object) null))
      {
        DataSource.Bind<PartyData>(this.DefensePartyInfo, partyOfType);
        GameParameter.UpdateAll(this.DefensePartyInfo);
      }
      PartyEditData party = new PartyEditData(string.Empty, partyOfType);
      party.SetUnitsForce(ownUnits.ToArray());
      this.DefTotalCombatPowerText.text = PartyUtility.CalcTotalCombatPower(party, ownUnits).ToString();
    }

    private void RefreshEnemyList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.EnemyPlayerList, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.EnemyPlayerItem, (UnityEngine.Object) null))
        return;
      this.EnemyPlayerList.ClearItems();
      ArenaPlayer[] arenaPlayers = MonoSingleton<GameManager>.Instance.ArenaPlayers;
      Transform transform = ((Component) this.EnemyPlayerList).transform;
      for (int index = 0; index < arenaPlayers.Length; ++index)
      {
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.EnemyPlayerItem);
        DataSource.Bind<ArenaPlayer>(((Component) listItemEvents).gameObject, arenaPlayers[index]);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnEnemySelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnEnemyDetailSelect);
        this.EnemyPlayerList.AddItem(listItemEvents);
        ((Component) listItemEvents).transform.SetParent(transform, false);
        ((Component) listItemEvents).gameObject.SetActive(true);
        AssetManager.PrepareAssets(AssetPath.UnitSkinImage(arenaPlayers[index].Unit[0].UnitParam, arenaPlayers[index].Unit[0].GetSelectedSkin(), arenaPlayers[index].Unit[0].CurrentJobId));
      }
      if (!AssetDownloader.isDone)
        AssetDownloader.StartDownload(false);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapInfo))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || player == null)
        return;
      DataSource component = this.GoMapInfo.GetComponent<DataSource>();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        component.Clear();
      DataSource.Bind<QuestParam>(this.GoMapInfo, instance.FindQuest(GlobalVars.SelectedQuestID));
      GameParameter.UpdateAll(this.GoMapInfo);
      this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
    }

    private bool RefreshMapInfoEndAt()
    {
      bool is_display = false;
      bool is_need_refresh = false;
      string end_at_text = string.Empty;
      bool mapInfoViewData = ArenaWindow.GetMapInfoViewData(out is_display, out end_at_text, out is_need_refresh);
      GameUtility.SetGameObjectActive(this.GoMapInfoEndAt, is_display);
      if (!is_display && is_need_refresh)
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "REFRESH_ARENA_INFO");
      if (mapInfoViewData)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextMapInfoEndAt) && this.TextMapInfoEndAt.text != end_at_text)
          this.TextMapInfoEndAt.text = end_at_text;
        this.mPassedTimeMapInfoEndAt = 1f;
      }
      return mapInfoViewData;
    }

    public static bool GetMapInfoViewData(
      out bool is_display,
      out string end_at_text,
      out bool is_need_refresh)
    {
      is_display = false;
      is_need_refresh = false;
      end_at_text = string.Empty;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.Player == null)
        return false;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      DateTime serverTime = TimeManager.ServerTime;
      TimeSpan timeSpan = player.ArenaEndAt - serverTime;
      is_display = player.ArenaEndAt > GameUtility.UnixtimeToLocalTime(0L);
      if (is_display && timeSpan.TotalSeconds < 0.0)
      {
        is_display = false;
        is_need_refresh = true;
      }
      if (!is_display)
        return false;
      string str = "sys.ARENA_TIMELIMIT_";
      if (timeSpan.Days != 0)
        end_at_text = LocalizedText.Get(str + "D", (object) timeSpan.Days);
      else if (timeSpan.Hours != 0)
        end_at_text = LocalizedText.Get(str + "H", (object) timeSpan.Hours);
      else
        end_at_text = LocalizedText.Get(str + "M", (object) Mathf.Max(timeSpan.Minutes, 0));
      return true;
    }

    private void UpdateMapInfoEndAt()
    {
      if (!this.mIsUpdateMapInfoEndAt)
        return;
      if ((double) this.mPassedTimeMapInfoEndAt > 0.0)
      {
        this.mPassedTimeMapInfoEndAt -= Time.fixedDeltaTime;
        if ((double) this.mPassedTimeMapInfoEndAt > 0.0)
          return;
      }
      this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
    }

    private void OnEnemySelect(GameObject go)
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null || !AssetDownloader.isDone)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.ChallengeArenaNum <= 0)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARENA_DAYLIMIT"), (UIUtility.DialogResultEvent) null);
      else if (player.GetNextChallengeArenaCoolDownSec() > 0L)
      {
        this.OnCooldownButtonClick();
      }
      else
      {
        GlobalVars.SelectedArenaPlayer.Set(dataOfClass);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VsEnemyPartyInfo, (UnityEngine.Object) null))
        {
          DataSource.Bind<ArenaPlayer>(this.VsEnemyPartyInfo, dataOfClass);
          GameParameter.UpdateAll(this.VsEnemyPartyInfo);
        }
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        this.BattlePreWindow.SetActive(true);
      }
    }

    private void OnResetChallengeTickets(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.Coin < instance.Player.GetChallengeArenaCost())
        UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), (UIUtility.DialogResultEvent) null);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 152);
    }

    private void OpenCoinShop(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 150);
    }

    private void OnEnemyDetailSelect(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.EnemyPlayerDetail, (UnityEngine.Object) null))
        return;
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EnemyPlayerDetail);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass);
      gameObject.GetComponent<ArenaPlayerInfo>().UpdateValue();
    }

    private void OnCooldownButtonClick()
    {
      if (MonoSingleton<GameManager>.Instance.Player.ChallengeArenaCoolDownSec <= 0L)
        return;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARENA_WAIT_COOLDOWN"), (UIUtility.DialogResultEvent) null);
    }

    private void OnResetCooldown(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.Coin < (int) instance.MasterParam.FixParam.ArenaResetCooldownCost)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), (UIUtility.DialogResultEvent) null);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 151);
    }

    private void Update()
    {
      this.RefreshCooldowns();
      if (string.IsNullOrEmpty(this.LastBattleAtText.text))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player.ArenaLastAt > GameUtility.UnixtimeToLocalTime(0L))
          this.LastBattleAtText.text = player.ArenaLastAt.ToString("MM/dd HH:mm");
      }
      this.UpdateMapInfoEndAt();
    }

    private void RefreshCooldowns()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      player.UpdateChallengeArenaTimer();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CooldownTimer, (UnityEngine.Object) null))
        return;
      bool flag = player.GetNextChallengeArenaCoolDownSec() > 0L && player.ChallengeArenaNum > 0;
      this.CooldownTimer.SetActive(flag);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.BpHolder))
        return;
      CanvasRenderer component = this.BpHolder.GetComponent<CanvasRenderer>();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        return;
      component.SetColor(!flag ? Color.white : Color.gray);
    }

    private void ChangeDrawDeck(bool attack)
    {
      this.mIsActiveAttackParty = attack;
      this.AttackDeckWindow.SetActive(attack);
      this.AttackDeckWindowIcon.SetActive(attack);
      ((Component) this.DeckNextButton).gameObject.SetActive(attack);
      this.DefenseDeckWindow.SetActive(!attack);
      this.DefenseDeckWindowIcon.SetActive(!attack);
      ((Component) this.DeckPrevButton).gameObject.SetActive(!attack);
      this.SyncArenaGlobalVars();
      Button button = !attack ? this.ChangeLeaderSkillButton_Def : this.ChangeLeaderSkillButton_Atk;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) button, (UnityEngine.Object) null))
      {
        bool flag = false;
        UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(!attack ? this.DefenseUnitLeader : this.PartyUnitLeader, (UnitData) null);
        if (dataOfClass != null && dataOfClass.MainConceptCard != null && dataOfClass.MainConceptCard.LeaderSkillIsAvailable())
          flag = true;
        ((Selectable) button).interactable = flag;
      }
      GameParameter.UpdateAll(!attack ? this.DefenseDeckWindow : this.AttackDeckWindow);
    }

    private void ChangeDrawInformation(bool playerinfo)
    {
      this.PlayerStatusWindow.SetActive(playerinfo);
      this.EnemyListWindow.SetActive(!playerinfo);
    }

    private void RefreshBattleCount()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BpHolder, (UnityEngine.Object) null))
        return;
      int challengeArenaMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
      int num = MonoSingleton<GameManager>.Instance.Player.ChallengeArenaNum;
      if (num >= challengeArenaMax)
        num = challengeArenaMax;
      for (int index = 0; index < challengeArenaMax; ++index)
        ((Component) this.BpHolder.transform.Find("bp" + (index + 1).ToString())).gameObject.SetActive(index + 1 <= num);
    }

    private void RefreshBattleCountOnDayChange()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BpHolder, (UnityEngine.Object) null))
        return;
      int challengeArenaMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
      int num = challengeArenaMax;
      for (int index = 0; index < challengeArenaMax; ++index)
        ((Component) this.BpHolder.transform.Find("bp" + (index + 1).ToString())).gameObject.SetActive(index + 1 <= num);
    }

    private void SetGlobalVars_LeaderSkillChangeUnitUniqueID(bool is_attack)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(!is_attack ? this.DefenseUnitLeader : this.PartyUnitLeader, (UnitData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedLSChangeUnitUniqueID.Set(dataOfClass.UniqueID);
    }

    public void SyncArenaGlobalVars()
    {
      eOverWritePartyType overWritePartyType = !this.mIsActiveAttackParty ? eOverWritePartyType.ArenaDef : eOverWritePartyType.Arena;
      GlobalVars.OverWritePartyType.Set(overWritePartyType);
      this.SetGlobalVars_LeaderSkillChangeUnitUniqueID(this.mIsActiveAttackParty);
      GameParameter.UpdateAll(!this.mIsActiveAttackParty ? this.DefenseDeckWindow : this.AttackDeckWindow);
    }

    private void OnEnable()
    {
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange);
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange);
    }

    public void OnMatchingButtonClick() => this.ChangeDrawInformation(false);

    public void OnMatchingCloseButtonClick() => this.ChangeDrawInformation(true);

    public void OnDeckNextButtonClick() => this.ChangeDrawDeck(false);

    public void OnDeckPrevButtonClick() => this.ChangeDrawDeck(true);

    public void OnBattleBackButtonClick() => this.BattlePreWindow.SetActive(false);

    public void OnHellpButtonClick(GameObject go) => this.BattlePreWindow.SetActive(false);

    public void OnHistoryDisp()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HistoryObject, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Instantiate<GameObject>(this.HistoryObject);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
      {
        QuestParam quest = instanceDirect.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null && quest.Chapter != null)
        {
          title = quest.Chapter.name;
          url = quest.Chapter.helpURL;
          return true;
        }
      }
      title = (string) null;
      url = (string) null;
      return false;
    }

    private List<UnitData> GetBattleEntryUnits()
    {
      List<UnitData> battleEntryUnits = new List<UnitData>();
      battleEntryUnits.AddRange((IEnumerable<UnitData>) this.GetBattleEntryPlayerUnits());
      ArenaPlayer arenaPlayer = GlobalVars.SelectedArenaPlayer.Get();
      if (arenaPlayer != null && arenaPlayer.Unit != null)
      {
        for (int index = 0; index < arenaPlayer.Unit.Length; ++index)
        {
          if (arenaPlayer.Unit[index] != null)
            battleEntryUnits.Add(arenaPlayer.Unit[index]);
        }
      }
      return battleEntryUnits;
    }

    private List<UnitData> GetBattleEntryPlayerUnits()
    {
      List<UnitData> entryPlayerUnits = new List<UnitData>();
      PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(PlayerPartyTypes.Arena);
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(partyOfType.GetUnitUniqueID(index));
        if (unitDataByUniqueId != null)
          entryPlayerUnits.Add(unitDataByUniqueId);
      }
      return entryPlayerUnits;
    }
  }
}
