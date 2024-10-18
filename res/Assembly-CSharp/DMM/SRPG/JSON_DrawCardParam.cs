// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_DrawCardParam
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
  public class JSON_DrawCardParam
  {
    public string iname;
    public string begin_at;
    public string end_at;
    public JSON_DrawCardParam.DrawInfo[] draw_infos;

    [MessagePackObject(true)]
    [Serializable]
    public class DrawInfo
    {
      public int card_num;
      public int miss_num;
      public string dc_reward_id;
    }
  }
}
