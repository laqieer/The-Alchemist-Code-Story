// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RankMatchHistory : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private ListItemEvents ListItem;
    [Space(10f)]
    [SerializeField]
    private Text LastBattleDate;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.PlayerGO, (Object) null))
        DataSource.Bind<PlayerData>(this.PlayerGO, MonoSingleton<GameManager>.Instance.Player);
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
        return;
      this.ClearItems();
      ((Component) this.ListItem).gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchHistory(new Network.ResponseCallback(this.ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response> jsonBodyResponse = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          if (jsonBodyResponse.body.histories == null || jsonBodyResponse.body.histories.list == null)
            return;
          long unixtime = 0;
          for (int index = 0; index < jsonBodyResponse.body.histories.list.Length; ++index)
          {
            ReqRankMatchHistory.ResponceHistoryList data1 = jsonBodyResponse.body.histories.list[index];
            ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchHistory.ResponceHistoryList>(((Component) listItemEvents).gameObject, data1);
            FriendData data2 = new FriendData();
            data2.Deserialize(data1.enemy);
            DataSource.Bind<FriendData>(((Component) listItemEvents).gameObject, data2);
            DataSource.Bind<ViewGuildData>(((Component) listItemEvents).gameObject, data2.ViewGuild);
            DataSource.Bind<UnitData>(((Component) listItemEvents).gameObject, data2.Unit);
            SerializeValueBehaviour component = ((Component) listItemEvents).GetComponent<SerializeValueBehaviour>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              long id = data2.ViewGuild == null ? 0L : (long) data2.ViewGuild.id;
              component.list.SetField(GuildSVB_Key.GUILD_ID, (int) id);
            }
            this.AddItem(listItemEvents);
            ((Component) listItemEvents).transform.SetParent(((Component) this).transform, false);
            ((Component) listItemEvents).gameObject.SetActive(true);
            if (unixtime < data1.time_end)
              unixtime = data1.time_end;
          }
          if (Object.op_Inequality((Object) this.LastBattleDate, (Object) null) && unixtime > 0L)
            this.LastBattleDate.text = TimeManager.FromUnixTime(unixtime).ToString("MM/dd HH:mm");
          Network.RemoveAPI();
        }
      }
    }
  }
}
