// Decompiled with JetBrains decompiler
// Type: SRPG.ResultOrdeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ResultOrdeal : MonoBehaviour
  {
    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.ResultData == null)
        return;
      DataSource.Bind<BattleCore.Record>(((Component) this).gameObject, instance.ResultData.Record);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
