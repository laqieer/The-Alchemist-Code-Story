// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DropItemNode : ContentNode
  {
    [SerializeField]
    private DropItemIcon mDropItemIcon;

    public DropItemIcon DropItemIcon => this.mDropItemIcon;

    public override void Initialize(ContentController controller) => base.Initialize(controller);

    public override void Release() => base.Release();
  }
}
