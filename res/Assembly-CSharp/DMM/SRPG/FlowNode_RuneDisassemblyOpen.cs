// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RuneDisassemblyOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Rune/RuneDisassemblyOpen", 32741)]
  [FlowNode.Pin(10, "ルーン分解ウィンドウを開く", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "output", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_RuneDisassemblyOpen : FlowNode
  {
    private const int INPUT_RUNE_DISASSEMBLY_WINDOW_OPEN = 10;
    private const int PIN_OUTPUT = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID == 10)
      {
        if (Object.op_Inequality((Object) RuneManager.Instance, (Object) null))
        {
          RuneManager.Instance.OpenDisassembly();
        }
        else
        {
          RuneDisassemblyWindow popupWindow = RuneManager.CreatePopupWindow<RuneDisassemblyWindow>("UI/Rune/RuneDisassemblyWindow");
          if (Object.op_Equality((Object) popupWindow, (Object) null))
            return;
          List<BindRuneData> rune_list = new List<BindRuneData>();
          foreach (KeyValuePair<long, RuneData> rune in MonoSingleton<GameManager>.Instance.Player.Runes)
            rune_list.Add(new BindRuneData((long) rune.Value.UniqueID)
            {
              is_owner_disable = true
            });
          popupWindow.Setup((RuneManager) null, rune_list);
        }
      }
      this.ActivateOutputLinks(100);
    }
  }
}
