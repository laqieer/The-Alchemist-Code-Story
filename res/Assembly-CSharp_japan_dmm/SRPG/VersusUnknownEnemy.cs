﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusUnknownEnemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Unknown", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Reset", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Finish", FlowNode.PinTypes.Output, 3)]
  public class VersusUnknownEnemy : MonoBehaviour, IFlowInterface
  {
    public RawImage_Transparent EnemyImage;
    public GameObject UnknownObj;

    private void RefreshUnknow()
    {
      if (Object.op_Inequality((Object) this.EnemyImage, (Object) null))
        ((Graphic) this.EnemyImage).color = new Color(0.0f, 0.0f, 0.0f, 1f);
      if (!Object.op_Inequality((Object) this.UnknownObj, (Object) null))
        return;
      this.UnknownObj.SetActive(true);
    }

    private void RefreshReset()
    {
      if (Object.op_Inequality((Object) this.EnemyImage, (Object) null))
        ((Graphic) this.EnemyImage).color = new Color(1f, 1f, 1f, 1f);
      if (!Object.op_Inequality((Object) this.UnknownObj, (Object) null))
        return;
      this.UnknownObj.SetActive(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.RefreshUnknow();
          break;
        case 2:
          this.RefreshReset();
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
