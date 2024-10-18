// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchRankingTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RankMatchRankingTab : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private GameObject PlayerUnit;
    [SerializeField]
    private ListItemEvents ListItem;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.PlayerUnit, (Object) null) && Object.op_Inequality((Object) this.PlayerGO, (Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.RankMatch);
        if (partyOfType != null)
          DataSource.Bind<PartyData>(this.PlayerGO, partyOfType);
        DataSource.Bind<PlayerData>(this.PlayerGO, player);
        DataSource.Bind<UnitData>(this.PlayerUnit, player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID));
        GameParameter.UpdateAll(this.PlayerUnit);
      }
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
        return;
      this.ClearItems();
      ((Component) this.ListItem).gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchRanking(new Network.ResponseCallback(this.ResponseCallback)));
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            ((Behaviour) this).enabled = false;
            break;
          default:
            if (errCode != Network.EErrCode.OutOfDateQuest)
            {
              if (errCode == Network.EErrCode.MultiVersionMismatch || errCode == Network.EErrCode.VS_Version)
              {
                Network.RemoveAPI();
                Network.ResetError();
                ((Behaviour) this).enabled = false;
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).enabled = false;
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response> jsonBodyResponse = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          if (jsonBodyResponse.body.rankings == null)
            return;
          for (int index = 0; index < jsonBodyResponse.body.rankings.Length; ++index)
          {
            ReqRankMatchRanking.ResponceRanking ranking = jsonBodyResponse.body.rankings[index];
            ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchRanking.ResponceRanking>(((Component) listItemEvents).gameObject, ranking);
            FriendData data = new FriendData();
            data.Deserialize(ranking.enemy);
            DataSource.Bind<FriendData>(((Component) listItemEvents).gameObject, data);
            DataSource.Bind<ViewGuildData>(((Component) listItemEvents).gameObject, data.ViewGuild);
            DataSource.Bind<UnitData>(((Component) listItemEvents).gameObject, data.Unit);
            SerializeValueBehaviour component = ((Component) listItemEvents).GetComponent<SerializeValueBehaviour>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              long id = data.ViewGuild == null ? 0L : (long) data.ViewGuild.id;
              component.list.SetField(GuildSVB_Key.GUILD_ID, (int) id);
            }
            this.AddItem(listItemEvents);
            ((Component) listItemEvents).transform.SetParent(((Component) this).transform, false);
            ((Component) listItemEvents).gameObject.SetActive(true);
          }
          Network.RemoveAPI();
        }
      }
    }
  }
}
