﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayUpdateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayUpdateRoomComment", 32741)]
  [FlowNode.Pin(101, "Update", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayUpdateRoomComment : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!instance.IsOldestPlayer())
      {
        DebugUtility.Log("I'm not room owner");
        this.ActivateOutputLinks(2);
      }
      else
      {
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        if (currentRoom == null)
        {
          DebugUtility.Log("CurrentRoom is null");
          this.ActivateOutputLinks(2);
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          if (myPhotonRoomParam == null)
          {
            DebugUtility.Log("no roomParam");
            this.ActivateOutputLinks(2);
          }
          else
          {
            GlobalVars.SelectedMultiPlayRoomComment = GlobalVars.EditMultiPlayRoomComment;
            myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
            myPhotonRoomParam.comment = GlobalVars.EditMultiPlayRoomComment;
            if (!MyMsgInput.isLegal(myPhotonRoomParam.comment))
            {
              DebugUtility.Log("comment is not legal");
              this.ActivateOutputLinks(2);
            }
            else
            {
              DebugUtility.Log("comment:" + myPhotonRoomParam.comment);
              instance.SetRoomParam(myPhotonRoomParam.Serialize());
              PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, myPhotonRoomParam.comment);
              this.ActivateOutputLinks(1);
            }
          }
        }
      }
    }
  }
}
