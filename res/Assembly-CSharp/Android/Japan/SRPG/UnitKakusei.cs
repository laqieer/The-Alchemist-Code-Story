﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakusei
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.JobUnlock != (UnityEngine.Object) null)
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
            DataSource.Bind<JobParam>(this.JobUnlocks[index], this.UnlockJobParams[index], false);
            this.JobUnlocks[index].SetActive(true);
          }
        }
      }
      if ((UnityEngine.Object) this.LevelValue != (UnityEngine.Object) null && this.UpdateLevelValue > 0)
        this.LevelValue.text = "+" + this.UpdateLevelValue.ToString();
      if (!((UnityEngine.Object) this.CombValue != (UnityEngine.Object) null) || this.UpdateCombValue <= 0)
        return;
      this.CombValue.text = "+" + this.UpdateCombValue.ToString();
    }
  }
}
