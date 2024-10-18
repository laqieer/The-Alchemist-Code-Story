// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewRoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Select Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "JoinFriendRoom", FlowNode.PinTypes.Input, 3)]
  public class VersusViewRoomInfo : MonoBehaviour, IFlowInterface
  {
    private readonly string FREE_SUFFIX = "_free";
    public GameObject Player1P;
    public GameObject Player2P;
    public Image RoomType;
    public Image RoomIcon;
    public Image MapThumnail;
    public Sprite FreeMatch;
    public Sprite TowerMatch;
    public Sprite FreeIcon;
    public Sprite TowerIcon;
    public Text MapName;
    public Text MapDetail;

    private void Start()
    {
    }

    public void Refresh()
    {
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(((Component) this).gameObject, (MyPhoton.MyRoom) null);
      if (dataOfClass == null)
      {
        ((Component) this).gameObject.SetActive(false);
      }
      else
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(dataOfClass.json);
        if (myPhotonRoomParam.players == null)
        {
          ((Component) this).gameObject.SetActive(false);
        }
        else
        {
          ((Component) this).gameObject.SetActive(true);
          if (Object.op_Inequality((Object) this.Player1P, (Object) null))
          {
            if (myPhotonRoomParam.players.Length > 0)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player1P, myPhotonRoomParam.players[0]);
            VersusViewPlayerInfo component = this.Player1P.GetComponent<VersusViewPlayerInfo>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh();
          }
          if (Object.op_Inequality((Object) this.Player2P, (Object) null))
          {
            if (myPhotonRoomParam.players.Length > 1)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, myPhotonRoomParam.players[1]);
            else
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, (JSON_MyPhotonPlayerParam) null);
            VersusViewPlayerInfo component = this.Player2P.GetComponent<VersusViewPlayerInfo>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh();
          }
          if (Object.op_Inequality((Object) this.RoomType, (Object) null))
            this.RoomType.sprite = dataOfClass.name.IndexOf(this.FREE_SUFFIX) == -1 ? this.TowerMatch : this.FreeMatch;
          if (Object.op_Inequality((Object) this.RoomIcon, (Object) null))
            this.RoomIcon.sprite = dataOfClass.name.IndexOf(this.FREE_SUFFIX) == -1 ? this.TowerIcon : this.FreeIcon;
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
          if (quest == null)
            return;
          if (Object.op_Inequality((Object) this.MapThumnail, (Object) null))
          {
            SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if (Object.op_Inequality((Object) spriteSheet, (Object) null))
              this.MapThumnail.sprite = spriteSheet.GetSprite(quest.VersusThumnail);
          }
          if (Object.op_Inequality((Object) this.MapName, (Object) null))
            this.MapName.text = quest.name;
          if (!Object.op_Inequality((Object) this.MapDetail, (Object) null))
            return;
          this.MapDetail.text = quest.expr;
        }
      }
    }

    public void OnClickRoomInfo()
    {
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(((Component) this).gameObject, (MyPhoton.MyRoom) null);
      if (dataOfClass == null)
        return;
      MonoSingleton<GameManager>.Instance.AudienceRoom = dataOfClass;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "COMFIRM_AUDIENCE");
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        this.Refresh();
      }
      else
      {
        if (pinID != 2)
          return;
        MonoSingleton<GameManager>.Instance.AudienceRoom = (MyPhoton.MyRoom) null;
      }
    }
  }
}
