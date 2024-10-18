// Decompiled with JetBrains decompiler
// Type: SRPG.GachaElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaElement : MonoBehaviour
  {
    public GachaTypes GachaType;
    public GameObject GachaButtonParent;
    public GameObject FreeGacha;
    public GameObject SingularGacha;
    public GameObject MultipleGacha;
    public Button BtnFreeGacha;
    public Button BtnSingularGacha;
    public Button BtnMultipleGacha;

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaButtonParent, (UnityEngine.Object) null))
        return;
      this.GachaButtonParent.SetActive(false);
    }

    public void Reflesh()
    {
      bool flag = false;
      List<GachaParam> gachaParamList = (List<GachaParam>) null;
      switch (this.GachaType)
      {
        case GachaTypes.Normal:
          gachaParamList = MonoSingleton<GameManager>.Instance.GetGachaList("gold");
          break;
        case GachaTypes.Rare:
          gachaParamList = MonoSingleton<GameManager>.Instance.GetGachaList("coin");
          break;
      }
      if (gachaParamList == null || gachaParamList.Count == 0)
        return;
      GachaParam data1 = gachaParamList.Find((Predicate<GachaParam>) (p => p.num == 1));
      if (data1 != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FreeGacha, (UnityEngine.Object) null))
        {
          DataSource.Bind<GachaParam>(this.FreeGacha, data1);
          flag = true;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SingularGacha, (UnityEngine.Object) null))
        {
          DataSource.Bind<GachaParam>(this.SingularGacha, data1);
          flag = true;
        }
      }
      GachaParam data2 = gachaParamList.Find((Predicate<GachaParam>) (p => p.num > 1));
      if (data2 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultipleGacha, (UnityEngine.Object) null))
      {
        DataSource.Bind<GachaParam>(this.MultipleGacha, data2);
        flag = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaButtonParent, (UnityEngine.Object) null))
        this.GachaButtonParent.SetActive(flag);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
