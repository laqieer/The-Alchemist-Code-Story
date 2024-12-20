﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BlackListCheckInRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.NodeType("Multi/CheckBlockList", 32741)]
  [FlowNode.Pin(10, "ブロックリストをチェック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1010, "ブロックリストに該当あり", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "ブロックリストに該当なし", FlowNode.PinTypes.Output, 1020)]
  public class FlowNode_BlackListCheckInRoom : FlowNode
  {
    private const int PIN_INPUT_CHECK_START = 10;
    private const int PIN_OUTPUT_EXIST = 1010;
    private const int PIN_OUTPUT_NOT_EXIST = 1020;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.CheckBlockList();
    }

    private void CheckBlockList()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null && instance.IsConnected() && GlobalVars.BlockList != null)
      {
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        for (int index = 0; index < roomPlayerList.Count; ++index)
        {
          if (myPlayer.playerID != roomPlayerList[index].playerID)
          {
            JSON_MyPhotonPlayerParam target = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
            if (GlobalVars.BlockList.FindIndex((Predicate<string>) (uid => uid == target.UID)) != -1)
            {
              this.ActivateOutputLinks(1010);
              return;
            }
          }
        }
      }
      this.ActivateOutputLinks(1020);
    }
  }
}
