﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_DrawCardRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_DrawCardRewardParam
  {
    public string iname;
    public JSON_DrawCardRewardParam.Data[] rewards;

    [MessagePackObject(true)]
    [Serializable]
    public class Data
    {
      public int weight;
      public int item_type;
      public string item_iname;
      public int item_num;
    }
  }
}
