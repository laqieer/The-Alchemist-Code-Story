// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidReportInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Copy", FlowNode.PinTypes.Input, 2)]
  public class GuildRaidReportInfo : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INITIALIZE = 1;
    private const int PIN_IN_COPY = 2;
    private static GuildRaidReportInfo mInstance;
    public static GuildRaidReportData StaticReportData;
    private GuildRaidReportData ReportData;
    private GuildRaidPartyList PartyList;
    [SerializeField]
    private Transform PartyListParent;
    [SerializeField]
    [StringIsResourcePath(typeof (GuildRaidPartyList))]
    private string PartyListPrefabPath;
    [SerializeField]
    private FixedScrollablePulldown TeamPulldown;
    [SerializeField]
    private GameObject PostButton;
    private bool IsPerfect = true;
    private bool IsChangeCardLeader;
    private UnitData LeaderUnit;

    public static int CurrentReportId
    {
      get
      {
        return UnityEngine.Object.op_Equality((UnityEngine.Object) GuildRaidReportInfo.mInstance, (UnityEngine.Object) null) ? 0 : GuildRaidReportInfo.mInstance.ReportData.ReportId;
      }
    }

    private void Start()
    {
      GuildRaidReportInfo.mInstance = this;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.PartyNumGuildRaid <= 1)
      {
        ((Component) this.TeamPulldown).gameObject.SetActive(false);
      }
      else
      {
        this.TeamPulldown.ResetAllItems();
        List<PartyEditData> partyEditDataList = PartyUtility.LoadTeamPresets(PlayerPartyTypes.GuildRaid, out int _);
        for (int index = 0; index < partyEditDataList.Count && index < this.TeamPulldown.ItemCount; ++index)
          this.TeamPulldown.SetItem(partyEditDataList[index].Name, index, index);
        this.TeamPulldown.Selection = 0;
        ((Component) this.TeamPulldown).gameObject.SetActive(true);
      }
    }

    private void OnDestroy() => GuildRaidReportInfo.mInstance = (GuildRaidReportInfo) null;

    public void Activated(int pinId)
    {
      if (pinId != 1)
      {
        if (pinId != 2)
          return;
        this.CopyParty();
      }
      else
        this.Initialize();
    }

    private void Initialize()
    {
      if (GuildRaidReportInfo.StaticReportData == null)
        return;
      this.ReportData = GuildRaidReportInfo.StaticReportData;
      GuildRaidReportInfo.StaticReportData = (GuildRaidReportData) null;
      DataSource.Bind<GuildRaidReportData>(((Component) this).gameObject, this.ReportData);
      GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(this.ReportData.BossId);
      if (guildRaidBossParam == null)
        return;
      DataSource.Bind<GuildRaidBossParam>(((Component) this).gameObject, guildRaidBossParam);
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
      if (unitParam == null)
        return;
      DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      if (string.IsNullOrEmpty(this.PartyListPrefabPath) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PartyListParent, (UnityEngine.Object) null))
        return;
      this.PartyList = UnityEngine.Object.Instantiate<GuildRaidPartyList>(AssetManager.Load<GuildRaidPartyList>(this.PartyListPrefabPath), this.PartyListParent);
      this.PartyList.Setup(this.ReportData.Deck);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PostButton, (UnityEngine.Object) null))
        return;
      this.PostButton.SetActive(this.ReportData.CanPost);
    }

    private void CopyParty()
    {
      this.IsPerfect = true;
      bool flag1 = false;
      this.IsChangeCardLeader = false;
      List<KeyValuePair<long, long>> req_set_list = new List<KeyValuePair<long, long>>();
      int selection = this.TeamPulldown.Selection;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PlayerPartyTypes.GuildRaid, out int _);
      teams[selection].Reset();
      List<UnitData> unitDataList = new List<UnitData>();
      for (int index1 = 0; index1 < this.ReportData.Deck.Count; ++index1)
      {
        UnitData unitData = (UnitData) null;
        if (this.ReportData.Deck[index1] != null)
        {
          UnitData reportUnit = this.ReportData.Deck[index1];
          unitData = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (unit => unit.UnitID == reportUnit.UnitID));
          if (unitData != null)
          {
            ConceptCardData conceptCard = (ConceptCardData) null;
            ConceptCardData report_unit_card = reportUnit.MainConceptCard;
            ConceptCardData mainConceptCard = unitData.MainConceptCard;
            bool flag2 = false;
            if (report_unit_card != null)
            {
              for (int conceptCardSlotIndex = 0; conceptCardSlotIndex < unitData.ConceptCards.Length; ++conceptCardSlotIndex)
              {
                if (!ConceptCardData.IsMainSlot(conceptCardSlotIndex) && unitData.ConceptCards[conceptCardSlotIndex] != null && unitData.ConceptCards[conceptCardSlotIndex].Param.iname == report_unit_card.Param.iname)
                {
                  flag2 = true;
                  break;
                }
              }
              if (!flag2)
              {
                List<ConceptCardData> all = MonoSingleton<GameManager>.Instance.Player.ConceptCards.FindAll((Predicate<ConceptCardData>) (cc => cc.Param.iname == report_unit_card.Param.iname));
                all.Sort((Comparison<ConceptCardData>) ((a, b) =>
                {
                  if ((int) a.Lv == (int) report_unit_card.Lv)
                    return -1;
                  if ((int) b.Lv == (int) report_unit_card.Lv)
                    return 1;
                  return (int) b.Lv == (int) a.Lv ? (int) ((long) a.UniqueID - (long) b.UniqueID) : (int) b.Lv - (int) a.Lv;
                }));
                for (int index2 = 0; index2 < all.Count; ++index2)
                {
                  if (!all[index2].IsEquipedSubSlot)
                  {
                    UnitData equipedUnit = all[index2].GetOwner();
                    if (equipedUnit == null)
                    {
                      conceptCard = all[index2];
                      break;
                    }
                    if (unitDataList.Find((Predicate<UnitData>) (u => u != null && u.UniqueID == equipedUnit.UniqueID)) == null)
                    {
                      conceptCard = all[index2];
                      break;
                    }
                  }
                }
              }
              if (conceptCard != null)
              {
                if (mainConceptCard == null || (long) mainConceptCard.UniqueID != (long) conceptCard.UniqueID)
                {
                  flag1 = true;
                  if (conceptCard.GetOwner() != null)
                    conceptCard.GetOwner().DetachConceptCard((long) conceptCard.UniqueID);
                  unitData.SetConceptCardMainSlot(conceptCard);
                  req_set_list.Add(new KeyValuePair<long, long>(unitData.UniqueID, (long) conceptCard.UniqueID));
                  if (index1 == 0)
                    this.IsChangeCardLeader = true;
                }
              }
              else
              {
                if (mainConceptCard != null)
                {
                  req_set_list.Add(new KeyValuePair<long, long>(0L, (long) mainConceptCard.UniqueID));
                  unitData.DetachConceptCard((long) mainConceptCard.UniqueID);
                  flag1 = true;
                }
                this.IsPerfect = false;
              }
            }
            else if (mainConceptCard != null)
            {
              req_set_list.Add(new KeyValuePair<long, long>(0L, (long) mainConceptCard.UniqueID));
              unitData.DetachConceptCardMainSlot();
              flag1 = true;
            }
          }
          else
          {
            if (index1 == 0)
            {
              this.ShowMessage(GuildRaidReportInfo.MessageType.NO_LEADER);
              return;
            }
            this.IsPerfect = false;
          }
        }
        unitDataList.Add(unitData);
      }
      this.LeaderUnit = unitDataList[0];
      teams[selection].SetUnits(unitDataList.ToArray());
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.GuildRaid, selection, teams);
      if (flag1)
        SRPG.Network.RequestAPI((WebAPI) new ReqSetConceptCardList(req_set_list, new SRPG.Network.ResponseCallback(this.EquipConceptCardCallBack), EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
      else
        this.CopyPartyCardLS();
    }

    private void CopyPartyCardLS()
    {
      if (this.ReportData.Deck[0].IsEquipConceptLeaderSkill())
      {
        if (this.IsChangeCardLeader)
        {
          if (this.LeaderUnit.MainConceptCard.LeaderSkillIsAvailable())
          {
            SRPG.Network.RequestAPI((WebAPI) new ReqSetConceptLeaderSkill(this.LeaderUnit.UniqueID, true, new SRPG.Network.ResponseCallback(this.ChangeCardLSCallBack)));
            return;
          }
          if (this.IsPerfect)
          {
            this.ShowMessage(GuildRaidReportInfo.MessageType.NO_CARD_LS);
            return;
          }
        }
        else if (this.LeaderUnit.MainConceptCard != null && !this.LeaderUnit.IsEquipConceptLeaderSkill())
        {
          if (this.LeaderUnit.MainConceptCard.LeaderSkillIsAvailable())
          {
            SRPG.Network.RequestAPI((WebAPI) new ReqSetConceptLeaderSkill(this.LeaderUnit.UniqueID, true, new SRPG.Network.ResponseCallback(this.ChangeCardLSCallBack)));
            return;
          }
          this.ShowMessage(GuildRaidReportInfo.MessageType.NO_CARD_LS);
          return;
        }
      }
      else if (!this.IsChangeCardLeader && this.LeaderUnit.IsEquipConceptLeaderSkill())
      {
        SRPG.Network.RequestAPI((WebAPI) new ReqSetConceptLeaderSkill(this.LeaderUnit.UniqueID, false, new SRPG.Network.ResponseCallback(this.ChangeCardLSCallBack)));
        return;
      }
      if (this.IsPerfect)
        this.ShowMessage(GuildRaidReportInfo.MessageType.SUCCESS);
      else
        this.ShowMessage(GuildRaidReportInfo.MessageType.NOT_PERFECT);
    }

    private void EquipConceptCardCallBack(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        switch (SRPG.Network.ErrCode)
        {
          case SRPG.Network.EErrCode.NoUnitParty:
          case SRPG.Network.EErrCode.NotExistConceptCard:
            FlowNode_Network.Failed();
            return;
        }
      }
      ReqSetConceptCardList.Response body;
      if (EncodingTypes.IsJsonSerializeCompressSelected(!GlobalVars.SelectedSerializeCompressMethodWasNodeSet ? EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK : GlobalVars.SelectedSerializeCompressMethod))
      {
        WebAPI.JSON_BodyResponse<ReqSetConceptCardList.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqSetConceptCardList.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "jsonRes == null");
        body = jsonObject.body;
      }
      else
      {
        PartyWindow2.MP_Response_SetConceptCardList setConceptCardList = SerializerCompressorHelper.Decode<PartyWindow2.MP_Response_SetConceptCardList>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK));
        DebugUtility.Assert(setConceptCardList != null, "mpRes == null");
        body = setConceptCardList.body;
      }
      try
      {
        if (body == null)
          throw new Exception("response parse error!");
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.player);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(body.units);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      SRPG.Network.RemoveAPI();
      this.CopyPartyCardLS();
    }

    private void ChangeCardLSCallBack(WWWResult www)
    {
      if (SRPG.Network.IsError)
      {
        int errCode = (int) SRPG.Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        SRPG.Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        if (this.IsPerfect)
          this.ShowMessage(GuildRaidReportInfo.MessageType.SUCCESS);
        else
          this.ShowMessage(GuildRaidReportInfo.MessageType.NOT_PERFECT);
      }
    }

    private void ShowMessage(GuildRaidReportInfo.MessageType type)
    {
      string empty = string.Empty;
      switch (type)
      {
        case GuildRaidReportInfo.MessageType.NO_LEADER:
          empty = LocalizedText.Get("sys.GUILDRAID_COPY_PARTY_FAILED_NO_LEADER");
          break;
        case GuildRaidReportInfo.MessageType.NO_CARD_LS:
          empty = LocalizedText.Get("sys.GUILDRAID_COPY_PARTY_FAILED_NO_CARD_LS");
          break;
        case GuildRaidReportInfo.MessageType.NOT_PERFECT:
          empty = LocalizedText.Get("sys.GUILDRAID_COPY_PARTY_FAILED_NOT_PERFECT");
          break;
        case GuildRaidReportInfo.MessageType.SUCCESS:
          empty = LocalizedText.Get("sys.GUILDRAID_COPY_PARTY_SUCCESS");
          break;
      }
      UIUtility.SystemMessage(empty, (UIUtility.DialogResultEvent) null, systemModal: true);
    }

    private enum MessageType
    {
      NO_LEADER,
      NO_CARD_LS,
      NOT_PERFECT,
      SUCCESS,
    }
  }
}
