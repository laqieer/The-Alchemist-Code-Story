﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayUpdateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
              PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, myPhotonRoomParam.comment, false);
              this.ActivateOutputLinks(1);
            }
          }
        }
      }
    }
  }
}
