﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class VersusPlayerInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;

    private void Start()
    {
      if ((UnityEngine.Object) this.template == (UnityEngine.Object) null)
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      if (multiPlayerParam == null)
        return;
      for (int index = 0; index < multiPlayerParam.units.Length; ++index)
      {
        if (multiPlayerParam.units[index] != null)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.template);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
            DataSource.Bind<UnitData>(gameObject, multiPlayerParam.units[index].unit);
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.parent.transform, false);
        }
      }
    }
  }
}
