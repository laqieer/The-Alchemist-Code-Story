﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItemInfoArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class ShopTimeOutItemInfoArray
  {
    public ShopTimeOutItemInfo[] Infos;

    public ShopTimeOutItemInfoArray()
    {
    }

    public ShopTimeOutItemInfoArray(ShopTimeOutItemInfo[] infos) => this.Infos = infos;
  }
}
