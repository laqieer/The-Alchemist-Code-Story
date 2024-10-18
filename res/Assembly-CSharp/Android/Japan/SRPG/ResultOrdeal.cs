// Decompiled with JetBrains decompiler
// Type: SRPG.ResultOrdeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ResultOrdeal : MonoBehaviour
  {
    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance) || instance.ResultData == null)
        return;
      DataSource.Bind<BattleCore.Record>(this.gameObject, instance.ResultData.Record, false);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
