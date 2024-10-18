// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/ShopTelop", 32741)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "SetText", FlowNode.PinTypes.Input, 10)]
  public class FlowNode_ShopTelop : FlowNode
  {
    public string ShopTelopGameObjectID = "ShopTelop";
    public string Text;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ShopTelopGameObjectID);
      ShopTelop shopTelop = !((UnityEngine.Object) gameObject == (UnityEngine.Object) null) ? gameObject.GetComponent<ShopTelop>() : (ShopTelop) null;
      if ((UnityEngine.Object) shopTelop != (UnityEngine.Object) null)
        shopTelop.SetText(LocalizedText.Get(this.Text));
      this.ActivateOutputLinks(1);
    }
  }
}
