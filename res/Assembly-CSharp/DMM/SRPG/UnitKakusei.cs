﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakusei
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitKakusei : MonoBehaviour
  {
    public GameObject JobUnlock;
    public GameObject[] JobUnlocks;
    public JobParam UnlockJobParam;
    public JobParam[] UnlockJobParams;
    public Text LevelValue;
    public Text CombValue;

    public int UpdateLevelValue { get; set; }

    public int UpdateCombValue { get; set; }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.JobUnlock, (Object) null))
        this.JobUnlock.SetActive(false);
      if (this.JobUnlocks == null || this.JobUnlocks.Length <= 0)
        return;
      for (int index = 0; index < this.JobUnlocks.Length; ++index)
        this.JobUnlocks[index].SetActive(false);
    }

    private void Start()
    {
      if (this.UnlockJobParams != null && this.UnlockJobParams.Length > 0)
      {
        for (int index = 0; index < this.UnlockJobParams.Length; ++index)
        {
          if (index < this.JobUnlocks.Length && this.UnlockJobParams[index] != null)
          {
            DataSource.Bind<JobParam>(this.JobUnlocks[index], this.UnlockJobParams[index]);
            this.JobUnlocks[index].SetActive(true);
          }
        }
      }
      if (Object.op_Inequality((Object) this.LevelValue, (Object) null) && this.UpdateLevelValue > 0)
        this.LevelValue.text = "+" + this.UpdateLevelValue.ToString();
      if (!Object.op_Inequality((Object) this.CombValue, (Object) null) || this.UpdateCombValue <= 0)
        return;
      this.CombValue.text = "+" + this.UpdateCombValue.ToString();
    }
  }
}
