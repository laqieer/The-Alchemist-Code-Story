// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResultContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AbilityPowerUpResultContent : MonoBehaviour
  {
    public void SetData(AbilityPowerUpResultContent.Param param)
    {
      DataSource.Bind<AbilityParam>(((Component) this).gameObject, param.data);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public class Param
    {
      public AbilityParam data;
    }
  }
}
