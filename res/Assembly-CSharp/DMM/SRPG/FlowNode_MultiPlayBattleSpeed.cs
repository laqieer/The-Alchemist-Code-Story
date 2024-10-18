// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayBattleSpeed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayBattleSpeed", 32741)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Update Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Update Failure", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(200, "Test", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "TRUE", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "FALSE", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(250, "ChangeBtlSoeed", FlowNode.PinTypes.Input, 250)]
  [FlowNode.Pin(251, "UpdateBtlSoeed", FlowNode.PinTypes.Input, 251)]
  [FlowNode.Pin(252, "ChangeBtlSoeed output", FlowNode.PinTypes.Output, 252)]
  public class FlowNode_MultiPlayBattleSpeed : FlowNode
  {
    private const int PIN_IN_UPDATE = 100;
    private const int PIN_OUT_SUCCESS = 101;
    private const int PIN_OUT_FAILURE = 102;
    private const int PIN_IN_IS_HISPEED = 200;
    private const int PIN_OUT_IS_HISPEED_TRUE = 201;
    private const int PIN_OUT_IS_HISPEED_FALSE = 202;
    private const int PIN_IN_CHANGE_BATTLE_SPEED = 250;
    private const int PIN_IN_UPDATE_BATTLE_SPEED = 251;
    private const int PIN_OUT_CHANGE_BATTLE_SPEED = 252;
    [SerializeField]
    private bool HiSpeed;
    [SerializeField]
    private ImageArray BattleSpeedImageArray;
    [SerializeField]
    private Button BattleSpeedButton;
    private float[] mSpeedList;
    private int mSelectBattleSpeedIndex;

    protected override void Awake()
    {
      base.Awake();
      GameUtility.SetButtonIntaractable(this.BattleSpeedButton, false);
    }

    public override void OnActivate(int pinID)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
      if (currentRoom == null)
      {
        DebugUtility.Log("CurrentRoom is null");
        this.ActivateOutputLinks(102);
      }
      else
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
        if (myPhotonRoomParam == null)
        {
          DebugUtility.Log("no roomParam");
          this.ActivateOutputLinks(102);
        }
        else
        {
          switch (pinID)
          {
            case 100:
              if (instance.IsOldestPlayer())
              {
                myPhotonRoomParam.btlSpd = !this.HiSpeed ? 1 : 2;
                instance.SetRoomParam(myPhotonRoomParam.Serialize());
                DebugUtility.Log("Hi Speed : " + (object) myPhotonRoomParam.btlSpd);
              }
              PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTI_HI_SPEED, !this.HiSpeed ? 0 : 1);
              GlobalVars.SelectedMultiPlayHiSpeed = this.HiSpeed;
              this.ActivateOutputLinks(101);
              break;
            case 200:
              if (myPhotonRoomParam.btlSpd == 2 == this.HiSpeed)
              {
                DebugUtility.Log("Hi Speed Test : HiSp( " + (!this.HiSpeed ? "Off" : "On") + " ) True");
                this.ActivateOutputLinks(201);
                break;
              }
              DebugUtility.Log("Hi Speed Test : HiSp( " + (!this.HiSpeed ? "Off" : "On") + " ) False");
              this.ActivateOutputLinks(202);
              break;
            case 250:
              if (this.mSpeedList != null)
              {
                this.mSelectBattleSpeedIndex = this.mSelectBattleSpeedIndex + 1 < this.mSpeedList.Length ? this.mSelectBattleSpeedIndex + 1 : 0;
                myPhotonRoomParam.btlSpd = (int) this.mSpeedList[this.mSelectBattleSpeedIndex];
                instance.SetRoomParam(myPhotonRoomParam.Serialize());
                PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.MULTI_BATTLE_SPEED, this.mSpeedList[this.mSelectBattleSpeedIndex]);
              }
              this.ActivateOutputLinks(252);
              break;
            case 251:
              if (instance.IsOldestPlayer())
              {
                GameUtility.SetButtonIntaractable(this.BattleSpeedButton, true);
                if (this.mSpeedList == null)
                {
                  BattleSpeedController.SetupEnableBattleSpeedList(MonoSingleton<GameManager>.Instance.Player.GetExpansionDatas(ExpansionPurchaseParam.eExpansionType.TripleSpeed));
                  this.mSpeedList = BattleSpeedController.EnableSpeedList;
                }
              }
              else if (this.mSpeedList == null)
                this.mSpeedList = BattleSpeedController.CreateBattleSpeedList();
              int speedListIndex = this.GetSpeedListIndex((float) myPhotonRoomParam.btlSpd);
              if (Object.op_Inequality((Object) this.BattleSpeedImageArray, (Object) null))
                this.BattleSpeedImageArray.ImageIndex = speedListIndex;
              this.mSelectBattleSpeedIndex = speedListIndex;
              GlobalVars.SelectedMultiPlayHiSpeed = myPhotonRoomParam.btlSpd > 1;
              GlobalVars.SelectedMultiPlayBtlSpeed = myPhotonRoomParam.btlSpd;
              DebugUtility.Log("Current Btl Speed : " + (object) myPhotonRoomParam.btlSpd);
              break;
          }
        }
      }
    }

    private int GetSpeedListIndex(float speed)
    {
      int speedListIndex = 0;
      if (this.mSpeedList != null)
      {
        for (int index = 0; index < this.mSpeedList.Length; ++index)
        {
          if ((double) this.mSpeedList[index] == (double) speed)
          {
            speedListIndex = index;
            break;
          }
        }
      }
      return speedListIndex;
    }
  }
}
