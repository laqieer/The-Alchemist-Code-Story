// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/ShopTelop", 32741)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "SetText", FlowNode.PinTypes.Input, 10)]
  public class FlowNode_ShopTelop : FlowNode
  {
    public string Text;
    public string ShopTelopGameObjectID = "ShopTelop";

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ShopTelopGameObjectID);
      ShopTelop component = !Object.op_Equality((Object) gameObject, (Object) null) ? gameObject.GetComponent<ShopTelop>() : (ShopTelop) null;
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetText(LocalizedText.Get(this.Text));
      this.ActivateOutputLinks(1);
    }
  }
}
