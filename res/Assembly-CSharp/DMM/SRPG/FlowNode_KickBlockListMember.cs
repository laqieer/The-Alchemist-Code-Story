// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_KickBlockListMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/KickBlockListMember", 32741)]
  [FlowNode.Pin(10, "ブロックリストをキック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1010, "完了", FlowNode.PinTypes.Output, 1010)]
  public class FlowNode_KickBlockListMember : FlowNode
  {
    private const int PIN_INPUT_START = 10;
    private const int PIN_OUTPUT_FINISH = 1010;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.KickBlockListMember();
    }

    private void KickBlockListMember()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsConnected() && GlobalVars.BlockList != null)
      {
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        for (int index = 0; index < roomPlayerList.Count; ++index)
        {
          if (myPlayer.playerID != roomPlayerList[index].playerID)
          {
            JSON_MyPhotonPlayerParam target = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
            if (GlobalVars.BlockList.FindIndex((Predicate<string>) (uid => uid == target.UID)) != -1)
              instance.KickMember(roomPlayerList[index]);
          }
        }
      }
      this.ActivateOutputLinks(1010);
    }
  }
}
