﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkinUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("UI/SkinUnlockWindow", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SkinUnlockWindow : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ItemParam> showItems = new List<ItemParam>();
      ItemData[] array = instance.Player.Items.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index].IsNewSkin)
          showItems.Add(array[index].Param);
      }
      if (showItems.Count >= 1)
        this.StartCoroutine(this.OnOpenAsync(showItems));
      else
        this.ActivateOutputLinks(1);
    }

    [DebuggerHidden]
    private IEnumerator OnOpenAsync(List<ItemParam> showItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_SkinUnlockWindow.\u003COnOpenAsync\u003Ec__Iterator0() { showItems = showItems, \u0024this = this };
    }
  }
}
