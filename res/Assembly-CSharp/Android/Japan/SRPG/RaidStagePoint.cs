// Decompiled with JetBrains decompiler
// Type: SRPG.RaidStagePoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class RaidStagePoint : MonoBehaviour
  {
    [SerializeField]
    private List<Transform> mStageList;
    [SerializeField]
    private Transform mBossStage;

    public List<Transform> StageList
    {
      get
      {
        return this.mStageList;
      }
    }

    public Transform BossStage
    {
      get
      {
        return this.mBossStage;
      }
    }
  }
}
