// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.PlayerGO != (UnityEngine.Object) null)
        DataSource.Bind<PlayerData>(this.PlayerGO, MonoSingleton<GameManager>.Instance.Player);
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ClearItems();
      this.ListItem.gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchHistory(new Network.ResponseCallback(this.ResponseCallback)), false);
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
        WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null || jsonObject.body.histories == null || jsonObject.body.histories.list == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          long unixtime = 0;
          for (int index = 0; index < jsonObject.body.histories.list.Length; ++index)
          {
            ReqRankMatchHistory.ResponceHistoryList data1 = jsonObject.body.histories.list[index];
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem);
            if ((UnityEngine.Object) listItemEvents != (UnityEngine.Object) null)
              listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            DataSource.Bind<ReqRankMatchHistory.ResponceHistoryList>(listItemEvents.gameObject, data1);
            FriendData data2 = new FriendData();
            data2.Deserialize(data1.enemy);
            DataSource.Bind<FriendData>(listItemEvents.gameObject, data2);
            DataSource.Bind<UnitData>(listItemEvents.gameObject, data2.Unit);
            this.AddItem(listItemEvents);
            listItemEvents.transform.SetParent(this.transform, false);
            listItemEvents.gameObject.SetActive(true);
            if (unixtime < data1.time_end)
              unixtime = data1.time_end;
          }
          if ((UnityEngine.Object) this.LastBattleDate != (UnityEngine.Object) null && unixtime > 0L)
            this.LastBattleDate.text = TimeManager.FromUnixTime(unixtime).ToString("MM/dd HH:mm");
          Network.RemoveAPI();
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      ReqRankMatchHistory.ResponceHistoryList dataOfClass = DataSource.FindDataOfClass<ReqRankMatchHistory.ResponceHistoryList>(go, (ReqRankMatchHistory.ResponceHistoryList) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedFriendID = dataOfClass.id.ToString();
      FlowNode_OnFriendSelect nodeOnFriendSelect = this.GetComponent<FlowNode_OnFriendSelect>();
      if ((UnityEngine.Object) nodeOnFriendSelect == (UnityEngine.Object) null)
        nodeOnFriendSelect = UnityEngine.Object.FindObjectOfType<FlowNode_OnFriendSelect>();
      if (!((UnityEngine.Object) nodeOnFriendSelect != (UnityEngine.Object) null))
        return;
      nodeOnFriendSelect.Selected();
    }
  }
}
