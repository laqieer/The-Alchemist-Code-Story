// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResultContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class AbilityPowerUpResultContent : MonoBehaviour
  {
    public void SetData(AbilityPowerUpResultContent.Param param)
    {
      DataSource.Bind<AbilityParam>(this.gameObject, param.data, false);
      GameParameter.UpdateAll(this.gameObject);
    }

    public class Param
    {
      public AbilityParam data;
    }
  }
}
