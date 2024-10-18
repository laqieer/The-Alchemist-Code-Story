// Decompiled with JetBrains decompiler
// Type: SRPG.RaidStagePoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RaidStagePoint : MonoBehaviour
  {
    [SerializeField]
    private List<Transform> mStageList;
    [SerializeField]
    private Transform mBossStage;

    public List<Transform> StageList => this.mStageList;

    public Transform BossStage => this.mBossStage;
  }
}
