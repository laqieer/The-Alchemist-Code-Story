// Decompiled with JetBrains decompiler
// Type: SRPG.GachaPickupIconParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaPickupIconParam : ContentSource.Param
  {
    private GachaDropIconNode mNode;
    private Button mButton;

    public GachaDropData drop_data { get; set; }

    public bool interactable { get; set; }

    public bool select { get; set; }

    public override void OnSetup(ContentNode node)
    {
      base.OnSetup(node);
      this.mButton = ((Component) node).GetComponent<Button>();
      GameUtility.SetButtonIntaractable(this.mButton, this.interactable);
      this.mNode = ((Component) node).GetComponent<GachaDropIconNode>();
      this.Refresh();
    }

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = ((Component) node).GetComponent<GachaDropIconNode>();
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      this.mNode = (GachaDropIconNode) null;
      base.OnDisable(node);
    }

    public void Refresh()
    {
      if (!Object.op_Implicit((Object) this.mNode))
        return;
      this.mNode.Setup(this.drop_data);
      this.mNode.Select(this.select);
    }
  }
}
