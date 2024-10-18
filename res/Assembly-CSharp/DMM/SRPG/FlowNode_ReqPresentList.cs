// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqPresentList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/PresentList", 32741)]
  [FlowNode.Pin(100, "一覧取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "プレゼント一覧取得完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "プレゼント一覧取得失敗", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(200, "一括受け取り", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(210, "プレゼント一括受け取り完了", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(220, "プレゼント一括受け取り失敗", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(300, "一括送付", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "プレゼント一括送付完了", FlowNode.PinTypes.Output, 310)]
  [FlowNode.Pin(320, "プレゼント一括送付失敗", FlowNode.PinTypes.Output, 320)]
  [FlowNode.Pin(400, "贈ってくれた人", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(410, "プレゼント贈ってくれた人完了", FlowNode.PinTypes.Output, 410)]
  [FlowNode.Pin(420, "プレゼント贈ってくれた人失敗", FlowNode.PinTypes.Output, 420)]
  public class FlowNode_ReqPresentList : FlowNode_Network
  {
    private FlowNode_ReqPresentList.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        switch (pinID)
        {
          case 100:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentList(this);
            break;
          case 200:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentListExec(this);
            break;
          case 300:
            this.m_Api = (FlowNode_ReqPresentList.ApiBase) new FlowNode_ReqPresentList.Api_PresentListSend(this);
            break;
          case 400:
            if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
            {
              new FlowNode_ReqPresentList.Api_PresentListGave(this, currentValue.GetGameObject("item")).Start();
              break;
            }
            break;
        }
        if (this.m_Api == null)
          return;
        this.m_Api.Start();
        ((Behaviour) this).enabled = true;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqPresentList.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqPresentList m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqPresentList node) => this.m_Node = node;

      public virtual string url => string.Empty;

      public virtual string req => (string) null;

      public virtual void Success()
      {
      }

      public virtual void Failed()
      {
      }

      public virtual void Complete(WWWResult www)
      {
      }

      public virtual void Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
          this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
        else
          this.Failed();
      }
    }

    public class Api_PresentList : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentList(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url => "presentlist";

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(120);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }

    public class Api_PresentListExec : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentListExec(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url => "presentlist/exec";

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(210);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(220);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).enabled = false;
      }

      private RewardData ReceiveDataToRewardData(
        FlowNode_ReqPresentList.Api_PresentListExec.JsonItem receiveData)
      {
        FriendPresentItemParam presentItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(receiveData.iname);
        if (presentItemParam == null)
          return (RewardData) null;
        RewardData dataToRewardData = new RewardData();
        dataToRewardData.Exp = 0;
        dataToRewardData.Coin = 0;
        dataToRewardData.Gold = 0;
        dataToRewardData.Stamina = 0;
        dataToRewardData.MultiCoin = 0;
        dataToRewardData.KakeraCoin = 0;
        if (presentItemParam.IsItem())
          dataToRewardData.AddReward(presentItemParam.item, presentItemParam.num * receiveData.num);
        else
          dataToRewardData.Gold = presentItemParam.zeny * receiveData.num;
        return dataToRewardData;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListExec.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListExec.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          bool flag = false;
          if (jsonObject.body != null)
          {
            if (jsonObject.body.player != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            if (jsonObject.body.items != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.presents != null)
            {
              RewardData rewardData = new RewardData();
              for (int index = 0; index < jsonObject.body.presents.Length; ++index)
              {
                FlowNode_ReqPresentList.Api_PresentListExec.JsonItem present = jsonObject.body.presents[index];
                if (present != null)
                {
                  RewardData dataToRewardData = this.ReceiveDataToRewardData(present);
                  if (dataToRewardData != null)
                  {
                    rewardData.Exp += dataToRewardData.Exp;
                    rewardData.Stamina += dataToRewardData.Stamina;
                    rewardData.Coin += dataToRewardData.Coin;
                    rewardData.Gold += dataToRewardData.Gold;
                    rewardData.ArenaMedal += dataToRewardData.ArenaMedal;
                    rewardData.MultiCoin += dataToRewardData.MultiCoin;
                    rewardData.KakeraCoin += dataToRewardData.KakeraCoin;
                    foreach (GiftRecieveItemData giftRecieveItemData in dataToRewardData.GiftRecieveItemDataDic.Values)
                      rewardData.AddReward(giftRecieveItemData);
                    flag = true;
                  }
                }
              }
              GlobalVars.LastReward.Set(rewardData);
              if (rewardData != null)
                MonoSingleton<GameManager>.Instance.Player.OnGoldChange(rewardData.Gold);
              MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = false;
            }
          }
          Network.RemoveAPI();
          if (flag)
            this.Success();
          else
            this.Failed();
        }
      }

      [Serializable]
      public class JsonItem
      {
        public string iname;
        public int num;
      }

      [Serializable]
      public class Json
      {
        public Json_PlayerData player;
        public Json_Item[] items;
        public FlowNode_ReqPresentList.Api_PresentListExec.JsonItem[] presents;
      }
    }

    public class Api_PresentListSend : FlowNode_ReqPresentList.ApiBase
    {
      public Api_PresentListSend(FlowNode_ReqPresentList node)
        : base(node)
      {
      }

      public override string url => "present/send";

      public override void Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateSendFriendPresentTrophy();
          string trophy_progs;
          string bingo_progs;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
          this.m_Node.ExecRequest((WebAPI) new ReqFriendPresentSend(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req, trophy_progs, bingo_progs));
        }
        else
          this.Failed();
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(310);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(320);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListSend.Json> jsonBodyResponse = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqPresentList.Api_PresentListSend.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.result && FriendPresentRootWindow.instance != null)
            FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.SENDING);
          Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public bool result;
      }
    }

    public class Api_PresentListGave : FlowNode_ReqPresentList.ApiBase
    {
      private FriendPresentItemParam m_Param;

      public Api_PresentListGave(FlowNode_ReqPresentList node, GameObject gobj)
        : base(node)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gobj, (UnityEngine.Object) null))
          return;
        ContentNode component = gobj.GetComponent<ContentNode>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam itemParam = component.GetParam<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>();
        if (itemParam == null)
          return;
        this.m_Param = itemParam.present;
      }

      public override string url => "presentlist/exec";

      public override void Start() => this.Complete();

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(410);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(320);
        ((Behaviour) this.m_Node).enabled = false;
      }

      public void Complete()
      {
        FriendPresentGaveWindow instance = FriendPresentGaveWindow.instance;
        if (instance != null && this.m_Param != null)
        {
          instance.ClearFuids();
          FriendPresentReceiveList.Param obj = MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.GetParam(this.m_Param.iname);
          if (obj != null)
          {
            for (int index = 0; index < obj.uids.Count; ++index)
              instance.AddUid(obj.uids[index]);
            this.Success();
          }
          else
            this.Failed();
        }
        else
          this.Failed();
      }
    }
  }
}
