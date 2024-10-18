// Decompiled with JetBrains decompiler
// Type: SRPG.RuneIconNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneIconNode : ContentNode
  {
    [SerializeField]
    public RuneIcon Icon;

    public void Setup(BindRuneData rune_data)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || rune_data == null)
        return;
      this.Icon.Setup(rune_data);
      ((Component) this.Icon).gameObject.SetActive(true);
    }
  }
}
