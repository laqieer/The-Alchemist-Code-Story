// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "結果表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "結果表示終了", FlowNode.PinTypes.Output, 100)]
  public class AutoRepeatQuestResult : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_SHOW_RESULT = 20;
    private const int PIN_OUTPUT_SHOW_RESULT_END = 100;
    private LoadRequest mReqRaidResultWindow;
    private AutoRepeatQuestData mAutoRepeatQuestResult;
    private AutoRepeatQuest_OldData mOldData;
    private List<RuneData> mRuneDatas;
    [SerializeField]
    private Text mRepeatCountText;
    [SerializeField]
    private Text mDropCountText;
    [SerializeField]
    private Text mDropMaxText;
    [SerializeField]
    private ContentController mContentController;
    [SerializeField]
    private GameObject mDropItemEmpty;
    [StringIsResourcePath(typeof (RaidResultWindow))]
    [SerializeField]
    private string mRaidResultPrefab;

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        this.ShowResult();
      }
      else
        this.Init();
    }

    private void Awake() => this.mContentController.SetWork((object) this);

    private void Init()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        this.mOldData = component.list.GetObject<AutoRepeatQuest_OldData>(AutoRepeatQuestSVB_Key.OLD);
        this.mAutoRepeatQuestResult = component.list.GetObject<AutoRepeatQuestData>(AutoRepeatQuestSVB_Key.RESULT);
        this.mRuneDatas = component.list.GetObject<List<RuneData>>(AutoRepeatQuestSVB_Key.RUNES);
      }
      List<Unit.DropItem> dropItemList = new List<Unit.DropItem>();
      if (this.mAutoRepeatQuestResult != null)
        dropItemList = this.mAutoRepeatQuestResult.GetDropItem();
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      for (int index = 0; index < dropItemList.Count; ++index)
      {
        QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
        if (dropItemList[index].isItem)
          dropItemData.SetupDropItemData(dropItemList[index].BattleRewardType, 0L, dropItemList[index].itemParam.iname, (int) dropItemList[index].num);
        else if (dropItemList[index].isConceptCard)
          dropItemData.SetupDropItemData(dropItemList[index].BattleRewardType, 0L, dropItemList[index].conceptCardParam.iname, (int) dropItemList[index].num);
        dropItemDataList.Add(dropItemData);
      }
      List<DropItemSource.DropItemParam> dropItemParamList = new List<DropItemSource.DropItemParam>();
      for (int index = 0; index < dropItemDataList.Count; ++index)
        dropItemParamList.Add(new DropItemSource.DropItemParam(dropItemDataList[index]));
      DropItemSource source = new DropItemSource();
      source.SetTable((ContentSource.Param[]) dropItemParamList.ToArray());
      this.mContentController.Initialize((ContentSource) source, Vector2.zero);
      if (Object.op_Inequality((Object) this.mRepeatCountText, (Object) null))
        this.mRepeatCountText.text = this.mAutoRepeatQuestResult.CurrentLap.ToString();
      if (Object.op_Inequality((Object) this.mDropCountText, (Object) null))
        this.mDropCountText.text = dropItemList.Count.ToString();
      if (Object.op_Inequality((Object) this.mDropMaxText, (Object) null))
        this.mDropMaxText.text = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.Size.ToString();
      GameUtility.SetGameObjectActive(this.mDropItemEmpty, dropItemDataList.Count <= 0);
      this.mReqRaidResultWindow = AssetManager.LoadAsync<RaidResultWindow>(this.mRaidResultPrefab);
    }

    private void ShowResult()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.mAutoRepeatQuestResult.QuestIname);
      PartyWindow2.EditPartyTypes type = PartyUtility.GetEditPartyTypes(quest);
      if (type == PartyWindow2.EditPartyTypes.Auto)
        type = PartyWindow2.EditPartyTypes.Normal;
      RaidResult raidResult = new RaidResult(type.ToPlayerPartyType());
      bool flag = player.Lv != this.mOldData.player_lv;
      RaidQuestResult raidQuestResult = new RaidQuestResult();
      raidQuestResult.pexp = quest.pexp * this.mAutoRepeatQuestResult.CurrentLap;
      raidQuestResult.uexp = quest.uexp * this.mAutoRepeatQuestResult.CurrentLap;
      raidQuestResult.gold = this.mAutoRepeatQuestResult.Gold;
      raidQuestResult.drops = this.mAutoRepeatQuestResult.GetDropItemData().ToArray();
      raidResult.quest = quest;
      raidResult.members.AddRange((IEnumerable<UnitData>) this.mOldData.units);
      raidResult.results.Add(raidQuestResult);
      raidResult.campaignIds = (string[]) null;
      raidResult.pexp = raidQuestResult.pexp;
      raidResult.uexp = raidQuestResult.uexp;
      raidResult.gold = raidQuestResult.gold;
      raidResult.chquest = new QuestParam[raidResult.members.Count];
      for (int index = 0; index < raidResult.members.Count; ++index)
      {
        UnitData.CharacterQuestParam charaEpisodeData = raidResult.members[index].GetCurrentCharaEpisodeData();
        if (charaEpisodeData != null)
          raidResult.chquest[index] = charaEpisodeData.Param;
      }
      if (this.mRuneDatas != null)
        raidResult.runes_detail = this.mRuneDatas;
      GlobalVars.RaidResult = raidResult;
      GlobalVars.PlayerExpOld.Set(this.mOldData.player_exp);
      GlobalVars.PlayerExpNew.Set(player.Exp);
      GlobalVars.PlayerLevelChanged.Set(flag);
      if (flag)
        player.OnPlayerLevelChange(player.Lv - this.mOldData.player_lv);
      player.OnQuestWin(quest.iname, currentUnits: this.mAutoRepeatQuestResult.GetUnitDatas().ToArray(), addCnt: this.mAutoRepeatQuestResult.CurrentLap);
      player.OnGoldChange(this.mAutoRepeatQuestResult.Gold);
      MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.All);
      this.StartCoroutine(this.ShowResultAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowResultAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AutoRepeatQuestResult.\u003CShowResultAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
