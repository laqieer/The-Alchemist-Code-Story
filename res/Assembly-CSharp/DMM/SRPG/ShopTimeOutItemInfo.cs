﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class ShopTimeOutItemInfo
  {
    public string ShopId;
    public int ItemId;
    public long End;

    public ShopTimeOutItemInfo(string shopId, int itemId, long end)
    {
      this.ShopId = shopId;
      this.ItemId = itemId;
      this.End = end;
    }
  }
}