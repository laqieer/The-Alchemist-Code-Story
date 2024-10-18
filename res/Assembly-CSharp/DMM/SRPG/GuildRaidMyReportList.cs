// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidMyReportList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Change Main", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Change Mock", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(11, "Refresh", FlowNode.PinTypes.Output, 11)]
  public class GuildRaidMyReportList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INITIALIZE = 1;
    private const int PIN_IN_BATTLE_TYPE_MAIN = 2;
    private const int PIN_IN_BATTLE_TYPE_MOCK = 3;
    private const int PIN_OUT_REFRESH = 11;
    private const string TAB_BOSS_ID = "boss_id";
    private static GuildRaidMyReportList mInstance;
    [SerializeField]
    private GameObject ReportTemplate;
    [SerializeField]
    [StringIsResourcePath(typeof (GuildRaidPartyList))]
    private string PartyListPrefabPath;
    [SerializeField]
    private ToggleGroup ToggleGroup;
    [SerializeField]
    private Toggle[] Toggles;
    [SerializeField]
    private SRPG_ScrollRect Scroll;
    [SerializeField]
    private RectTransform ScrollContent;
    private GuildRaidPartyList PartyListPrefab;
    private Dictionary<GuildRaidBattleType, Dictionary<int, GuildRaidMyReportList.ReportData>> Reports = new Dictionary<GuildRaidBattleType, Dictionary<int, GuildRaidMyReportList.ReportData>>();
    private List<GameObject> ReportGOList = new List<GameObject>();
    private Coroutine loadDeck;
    private bool IsLoading = true;
    private bool NeedAllRefresh = true;

    public static GuildRaidMyReportList Instance => GuildRaidMyReportList.mInstance;

    public GuildRaidBattleType BattleType { get; private set; }

    public int BossId { get; private set; }

    public GuildRaidMyReportList.ReportData CurrentReportData
    {
      get => this.Reports[this.BattleType][this.BossId];
    }

    private void Awake()
    {
      GuildRaidMyReportList.mInstance = this;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReportTemplate, (UnityEngine.Object) null))
        this.ReportTemplate.SetActive(false);
      List<GuildRaidBossParam> raidBossByPeriod = MonoSingleton<GameManager>.Instance.GetGuildRaidBossByPeriod(GuildRaidManager.Instance.PeriodId);
      if (raidBossByPeriod == null || this.Toggles == null || raidBossByPeriod.Count + 1 != this.Toggles.Length)
        return;
      this.BattleType = GuildRaidBattleType.Main;
      this.BossId = 0;
      this.Reports.Add(GuildRaidBattleType.Main, new Dictionary<int, GuildRaidMyReportList.ReportData>());
      this.Reports.Add(GuildRaidBattleType.Mock, new Dictionary<int, GuildRaidMyReportList.ReportData>());
      this.Reports[GuildRaidBattleType.Main].Add(0, new GuildRaidMyReportList.ReportData());
      this.Reports[GuildRaidBattleType.Mock].Add(0, new GuildRaidMyReportList.ReportData());
      for (int index = 0; index < raidBossByPeriod.Count; ++index)
      {
        this.Reports[GuildRaidBattleType.Main].Add(raidBossByPeriod[index].Id, new GuildRaidMyReportList.ReportData());
        this.Reports[GuildRaidBattleType.Mock].Add(raidBossByPeriod[index].Id, new GuildRaidMyReportList.ReportData());
      }
      if (!string.IsNullOrEmpty(this.PartyListPrefabPath))
        this.PartyListPrefab = AssetManager.Load<GuildRaidPartyList>(this.PartyListPrefabPath);
      if (this.Toggles == null)
        return;
      for (int index = 0; index < this.Toggles.Length; ++index)
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Toggles[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChange)));
        SerializeValueBehaviour serializeValueBehaviour = ((Component) this.Toggles[index]).gameObject.AddComponent<SerializeValueBehaviour>();
        if (index == 0)
          serializeValueBehaviour.list.SetObject("boss_id", (object) 0);
        else
          serializeValueBehaviour.list.SetObject("boss_id", (object) raidBossByPeriod[index - 1].Id);
      }
    }

    private void OnDestroy()
    {
      GuildRaidMyReportList.mInstance = (GuildRaidMyReportList) null;
      if (this.loadDeck == null)
        return;
      this.StopCoroutine(this.loadDeck);
    }

    private void Update()
    {
      if (this.IsLoading || this.CurrentReportData.CurrentPage >= this.CurrentReportData.TotalPage || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Scroll, (UnityEngine.Object) null) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ScrollContent) || (double) this.Scroll.verticalNormalizedPosition * (double) this.ScrollContent.sizeDelta.y >= 10.0)
        return;
      this.IsLoading = true;
      this.NeedAllRefresh = false;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    public void SetupReport(JSON_GuildRaidReport[] json, int totalPage)
    {
      if (json == null || json.Length <= 0)
        return;
      for (int index = 0; index < json.Length; ++index)
      {
        GuildRaidReportData guildRaidReportData = new GuildRaidReportData();
        guildRaidReportData.Deserialize(json[index]);
        this.CurrentReportData.Reports.Add(guildRaidReportData);
      }
      ++this.CurrentReportData.CurrentPage;
      this.CurrentReportData.TotalPage = totalPage;
    }

    public void Activated(int pinId)
    {
      switch (pinId)
      {
        case 1:
          this.Initialize();
          break;
        case 2:
          this.ChangeBattleType(GuildRaidBattleType.Main);
          break;
        case 3:
          this.ChangeBattleType(GuildRaidBattleType.Mock);
          break;
      }
    }

    private void ChangeBattleType(GuildRaidBattleType type)
    {
      if (this.BattleType == type)
        return;
      this.Scroll.verticalNormalizedPosition = 1f;
      this.BattleType = type;
      this.NeedAllRefresh = true;
      if (this.CurrentReportData.TotalPage > 0)
        this.Initialize();
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ReportTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PartyListPrefab, (UnityEngine.Object) null))
        return;
      Dictionary<Transform, List<UnitData>> decks = new Dictionary<Transform, List<UnitData>>();
      int num = 0;
      if (this.NeedAllRefresh)
      {
        this.ReportGOList.ForEach((Action<GameObject>) (go => UnityEngine.Object.Destroy((UnityEngine.Object) go)));
        this.ReportGOList.Clear();
      }
      else
        num = this.ReportGOList.Count;
      for (int index = num; index < this.CurrentReportData.Reports.Count; ++index)
      {
        GuildRaidReportData report = this.CurrentReportData.Reports[index];
        GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(report.BossId);
        if (guildRaidBossParam != null)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
          if (unitParam != null)
          {
            GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.ReportTemplate, this.ReportTemplate.transform.parent);
            GameObject gameObject2 = gameObject1.GetComponent<SerializeValueBehaviour>().list.GetGameObject("DeckParent");
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2.transform, (UnityEngine.Object) null))
            {
              UnityEngine.Object.Destroy((UnityEngine.Object) gameObject1);
            }
            else
            {
              DataSource.Bind<GuildRaidReportData>(gameObject1, report);
              DataSource.Bind<GuildRaidBossParam>(gameObject1, guildRaidBossParam);
              DataSource.Bind<UnitParam>(gameObject1, unitParam);
              JSON_GuildRaidBattleLog json = new JSON_GuildRaidBattleLog()
              {
                report_id = report.ReportId
              };
              GuildRaidBattleLog data = new GuildRaidBattleLog();
              data.Deserialize(json);
              DataSource.Bind<GuildRaidBattleLog>(gameObject1, data);
              gameObject1.SetActive(true);
              this.ReportGOList.Add(gameObject1);
              decks.Add(gameObject2.transform, report.Deck);
            }
          }
        }
      }
      if (this.loadDeck != null)
        this.StopCoroutine(this.loadDeck);
      this.loadDeck = this.StartCoroutine(this._LoadDeck(decks));
      this.IsLoading = false;
    }

    [DebuggerHidden]
    private IEnumerator _LoadDeck(Dictionary<Transform, List<UnitData>> decks)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GuildRaidMyReportList.\u003C_LoadDeck\u003Ec__Iterator0()
      {
        decks = decks,
        \u0024this = this
      };
    }

    private void OnChange(bool on)
    {
      if (!on)
        return;
      SerializeValueBehaviour component = ((Component) this.ToggleGroup.ActiveToggles().FirstOrDefault<Toggle>()).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      int num = component.list.GetObject<int>("boss_id");
      if (this.BossId == num)
        return;
      this.Scroll.verticalNormalizedPosition = 1f;
      this.BossId = num;
      this.NeedAllRefresh = true;
      if (this.CurrentReportData.TotalPage > 0)
        this.Initialize();
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    public class ReportData
    {
      public List<GuildRaidReportData> Reports = new List<GuildRaidReportData>();
      public int CurrentPage;
      public int TotalPage;
    }
  }
}
