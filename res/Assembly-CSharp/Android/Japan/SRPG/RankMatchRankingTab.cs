// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchRankingTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if ((UnityEngine.Object) this.PlayerUnit != (UnityEngine.Object) null && (UnityEngine.Object) this.PlayerGO != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        DataSource.Bind<PlayerData>(this.PlayerGO, player, false);
        DataSource.Bind<UnitData>(this.PlayerUnit, player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID), false);
        GameParameter.UpdateAll(this.PlayerUnit);
      }
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ClearItems();
      this.ListItem.gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchRanking(new Network.ResponseCallback(this.ResponseCallback)), false);
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
            this.enabled = false;
            break;
          default:
            if (errCode != Network.EErrCode.OutOfDateQuest)
            {
              if (errCode == Network.EErrCode.MultiVersionMismatch || errCode == Network.EErrCode.VS_Version)
              {
                Network.RemoveAPI();
                Network.ResetError();
                this.enabled = false;
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            this.enabled = false;
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
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchRanking.ResponceRanking>(listItemEvents.gameObject, ranking, false);
            FriendData data = new FriendData();
            data.Deserialize(ranking.enemy);
            DataSource.Bind<FriendData>(listItemEvents.gameObject, data, false);
            DataSource.Bind<ViewGuildData>(listItemEvents.gameObject, data.ViewGuild, false);
            DataSource.Bind<UnitData>(listItemEvents.gameObject, data.Unit, false);
            SerializeValueBehaviour component = listItemEvents.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              long num = data.ViewGuild == null ? 0L : (long) data.ViewGuild.id;
              component.list.SetField(GuildSVB_Key.GUILD_ID, (int) num);
            }
            this.AddItem(listItemEvents);
            listItemEvents.transform.SetParent(this.transform, false);
            listItemEvents.gameObject.SetActive(true);
          }
          Network.RemoveAPI();
        }
      }
    }
  }
}
