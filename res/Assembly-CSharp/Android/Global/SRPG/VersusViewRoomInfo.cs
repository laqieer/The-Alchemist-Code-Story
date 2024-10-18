// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewRoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "JoinFriendRoom", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "Select Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
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
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(this.gameObject, (MyPhoton.MyRoom) null);
      if (dataOfClass == null)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(dataOfClass.json);
        if (myPhotonRoomParam.players == null)
        {
          this.gameObject.SetActive(false);
        }
        else
        {
          this.gameObject.SetActive(true);
          if ((UnityEngine.Object) this.Player1P != (UnityEngine.Object) null)
          {
            if (myPhotonRoomParam.players.Length > 0)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player1P, myPhotonRoomParam.players[0]);
            VersusViewPlayerInfo component = this.Player1P.GetComponent<VersusViewPlayerInfo>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.Refresh();
          }
          if ((UnityEngine.Object) this.Player2P != (UnityEngine.Object) null)
          {
            if (myPhotonRoomParam.players.Length > 1)
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, myPhotonRoomParam.players[1]);
            else
              DataSource.Bind<JSON_MyPhotonPlayerParam>(this.Player2P, (JSON_MyPhotonPlayerParam) null);
            VersusViewPlayerInfo component = this.Player2P.GetComponent<VersusViewPlayerInfo>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.Refresh();
          }
          if ((UnityEngine.Object) this.RoomType != (UnityEngine.Object) null)
            this.RoomType.sprite = dataOfClass.name.IndexOf(this.FREE_SUFFIX) == -1 ? this.TowerMatch : this.FreeMatch;
          if ((UnityEngine.Object) this.RoomIcon != (UnityEngine.Object) null)
            this.RoomIcon.sprite = dataOfClass.name.IndexOf(this.FREE_SUFFIX) == -1 ? this.TowerIcon : this.FreeIcon;
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
          if (quest == null)
            return;
          if ((UnityEngine.Object) this.MapThumnail != (UnityEngine.Object) null)
          {
            SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("pvp/pvp_map");
            if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null)
              this.MapThumnail.sprite = spriteSheet.GetSprite(quest.VersusThumnail);
          }
          if ((UnityEngine.Object) this.MapName != (UnityEngine.Object) null)
            this.MapName.text = quest.name;
          if (!((UnityEngine.Object) this.MapDetail != (UnityEngine.Object) null))
            return;
          this.MapDetail.text = quest.expr;
        }
      }
    }

    public void OnClickRoomInfo()
    {
      MyPhoton.MyRoom dataOfClass = DataSource.FindDataOfClass<MyPhoton.MyRoom>(this.gameObject, (MyPhoton.MyRoom) null);
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
