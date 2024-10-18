﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BundleParamResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class BundleParamResponse
  {
    public List<BundleParam> bundles = new List<BundleParam>();
    public List<BundleParam> bundles_all = new List<BundleParam>();

    public bool Deserialize(JSON_BundleParamResponse json)
    {
      if (json == null || json.bundles == null)
        return true;
      this.bundles.Clear();
      for (int index = 0; index < json.bundles.Length; ++index)
      {
        BundleParam bundleParam = new BundleParam();
        if (!bundleParam.Deserialize(json.bundles[index]))
          return false;
        this.bundles.Add(bundleParam);
      }
      this.bundles_all.Clear();
      foreach (JSON_BundleParam json1 in json.bundles_all)
      {
        BundleParam bundleParam = new BundleParam();
        if (!bundleParam.Deserialize(json1))
          return false;
        this.bundles_all.Add(bundleParam);
      }
      MonoSingleton<GameManager>.Instance.Player.SetBundleParam(this.bundles);
      return true;
    }
  }
}
