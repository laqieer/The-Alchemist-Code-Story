// Decompiled with JetBrains decompiler
// Type: SRPG.VersusAudienceFriendRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "IGNORE_CONNECT_ON", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "IGNORE_CONNECT_OFF", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(11, "NETWORK_ABORT", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "FORCE_LEAVE", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "AUDIENCE_DISABLE", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "START_AUDIENCE", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "WAIT_AUDIENCE", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(105, "DISBANDED", FlowNode.PinTypes.Output, 105)]
  public class VersusAudienceFriendRoom : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_IGNORE_CONNECT_ON = 1;
    private const int PIN_IN_IGNORE_CONNECT_OFF = 2;
    private const int PIN_IN_NETWORK_ABORT = 11;
    private const int PIN_OUT_FORCE_LEAVE = 101;
    private const int PIN_OUT_AUDIENCE_DISABLE = 102;
    private const int PIN_OUT_START_AUDIENCE = 103;
    private const int PIN_OUT_WAIT_AUDIENCE = 104;
    private const int PIN_OUT_DISBANDED = 105;
    private readonly float UPDATE_INTERVAL = 2f;
    public GameObject RoomObj;
    public Text AudienceTxt;
    private float mUpdateTime;
    private bool mFinishWait;
    private bool mIgnoreConnect;

    private void Start() => this.Refresh();

    private void Refresh(MyPhoton.MyRoom room = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Inequality((Object) this.RoomObj, (Object) null) || instance.AudienceRoom == null)
        return;
      DataSource.Bind<MyPhoton.MyRoom>(this.RoomObj, room == null ? instance.AudienceRoom : room);
      VersusViewRoomInfo component = this.RoomObj.GetComponent<VersusViewRoomInfo>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.Refresh();
      if (!Object.op_Inequality((Object) this.AudienceTxt, (Object) null))
        return;
      int audience = instance.AudienceRoom.audience;
      int audienceMax = instance.AudienceRoom.audienceMax;
      this.AudienceTxt.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_NUM"), (object) GameUtility.HalfNum2FullNum(audience.ToString()), (object) GameUtility.HalfNum2FullNum(audienceMax.ToString()));
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mIgnoreConnect = true;
          break;
        case 2:
          this.mIgnoreConnect = false;
          break;
        case 11:
          this.NetworkAbort();
          break;
      }
    }

    private void Update()
    {
      this.mUpdateTime -= Time.deltaTime;
      if ((double) this.mUpdateTime > 0.0)
        return;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (!Object.op_Inequality((Object) instance2, (Object) null) || instance1.AudienceRoom == null || instance1.AudienceRoom.battle && instance1.AudienceRoom.draft)
        return;
      JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(instance1.AudienceRoom.json);
      if (!this.mIgnoreConnect && !instance2.IsConnected() && (!instance1.AudienceRoom.battle || myPhotonRoomParam.draft_type == 1 && !instance1.AudienceRoom.draft))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        instance1.AudienceRoom = (MyPhoton.MyRoom) null;
      }
      else
      {
        if (!instance2.IsRoomListUpdated)
          return;
        if (myPhotonRoomParam != null)
        {
          MyPhoton.MyRoom room = instance2.SearchRoom(myPhotonRoomParam.roomid);
          if (room != null)
          {
            if (!room.json.Equals(instance1.AudienceRoom.json))
            {
              this.Refresh(room);
              myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(room.json);
              if (myPhotonRoomParam != null && myPhotonRoomParam.audience == 0)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
                instance1.AudienceRoom = (MyPhoton.MyRoom) null;
                return;
              }
            }
            instance1.AudienceRoom = room;
            if (myPhotonRoomParam.draft_type == 1)
            {
              MonoSingleton<GameManager>.Instance.VSDraftId = (long) myPhotonRoomParam.draft_deck_id;
              if (room.draft)
              {
                this.NetworkAbort();
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
              }
              else if (room.battle && !this.mFinishWait)
              {
                this.mFinishWait = true;
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
              }
            }
            else if (room.battle)
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
          }
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
        }
        instance2.IsRoomListUpdated = false;
        this.mUpdateTime = this.UPDATE_INTERVAL;
      }
    }

    public void NetworkAbort()
    {
      if (Network.IsConnecting)
        Network.Abort();
      MonoSingleton<GameManager>.Instance.DestroyAudienceManager();
    }
  }
}
