// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusAward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/Versus/Award", 32741)]
  [FlowNode.Pin(100, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "Out", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_VersusAward : FlowNode
  {
    private readonly int ROOM_MAX_PLAYERCNT = 2;
    public GameObject BindObj;
    public bool MyPlayer;

    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        JSON_MyPhotonPlayerParam data = (JSON_MyPhotonPlayerParam) null;
        if (instance.AudienceMode)
        {
          MyPhoton.MyRoom audienceRoom = instance.AudienceRoom;
          if (audienceRoom != null)
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(audienceRoom.json);
            if (myPhotonRoomParam != null && myPhotonRoomParam.players != null && myPhotonRoomParam.players.Length >= this.ROOM_MAX_PLAYERCNT)
              data = myPhotonRoomParam.players[!this.MyPlayer ? 1 : 0];
          }
        }
        else
        {
          MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
          List<MyPhoton.MyPlayer> roomPlayerList = pt.GetRoomPlayerList();
          if (roomPlayerList != null)
          {
            if (this.MyPlayer)
            {
              data = JSON_MyPhotonPlayerParam.Create(0, 0);
            }
            else
            {
              MyPhoton.MyPlayer myPlayer = roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID != pt.GetMyPlayer().playerID));
              if (myPlayer != null)
                data = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
            }
          }
        }
        if (data != null)
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.BindObj, data, false);
      }
      this.ActivateOutputLinks(200);
    }
  }
}
