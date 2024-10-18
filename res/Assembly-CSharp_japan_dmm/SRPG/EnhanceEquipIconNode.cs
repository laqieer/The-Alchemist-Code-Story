// Decompiled with JetBrains decompiler
// Type: SRPG.EnhanceEquipIconNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class EnhanceEquipIconNode : ContentNode
  {
    [SerializeField]
    public GameObject Icon;
    [SerializeField]
    public GameObject EmptyObject;

    public void Setup(EnhanceMaterial material)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null) || material == null)
        return;
      DataSource.Bind<EnhanceMaterial>(this.Icon, material);
      this.Icon.gameObject.SetActive(true);
      GameParameter.UpdateAll(this.Icon.gameObject);
    }

    public void Empty(bool is_enmpty)
    {
      if (Object.op_Equality((Object) this.EmptyObject, (Object) null) || Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.gameObject.SetActive(!is_enmpty);
      this.EmptyObject.SetActive(is_enmpty);
    }
  }
}
