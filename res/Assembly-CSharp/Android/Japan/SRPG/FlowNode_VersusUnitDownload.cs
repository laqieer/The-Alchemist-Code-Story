// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusUnitDownload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("Multi/VersusUnitDownload", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "StartAudience", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Error", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_VersusUnitDownload : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (!AssetManager.UseDLC || !GameUtility.Config_UseAssetBundles.Value)
      {
        this.ActivateOutputLinks(100);
      }
      else
      {
        switch (pinID)
        {
          case 0:
            MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
            if ((UnityEngine.Object) pt != (UnityEngine.Object) null)
            {
              List<MyPhoton.MyPlayer> roomPlayerList = pt.GetRoomPlayerList();
              if (roomPlayerList != null && roomPlayerList.Count > 1)
              {
                MyPhoton.MyPlayer myPlayer = roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID != pt.GetMyPlayer().playerID));
                if (myPlayer != null)
                {
                  this.AddAssets(JSON_MyPhotonPlayerParam.Parse(myPlayer.json));
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 1:
            GameManager instance = MonoSingleton<GameManager>.Instance;
            if (instance.AudienceRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(instance.AudienceRoom.json);
              if (myPhotonRoomParam != null)
              {
                for (int index = 0; index < myPhotonRoomParam.players.Length; ++index)
                {
                  if (myPhotonRoomParam.players[index] != null)
                  {
                    myPhotonRoomParam.players[index].SetupUnits();
                    this.AddAssets(myPhotonRoomParam.players[index]);
                  }
                }
                break;
              }
              break;
            }
            break;
        }
        this.StartCoroutine(this.AsyncDownload());
      }
    }

    private void AddAssets(JSON_MyPhotonPlayerParam param)
    {
      if (param == null)
        return;
      AssetManager.PrepareAssets(AssetPath.UnitSkinImage(param.units[0].unit.UnitParam, param.units[0].unit.GetSelectedSkin(-1), param.units[0].unit.CurrentJobId));
    }

    [DebuggerHidden]
    private IEnumerator AsyncDownload()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_VersusUnitDownload.\u003CAsyncDownload\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
