// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusRankParam
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
  public class JSON_VersusRankParam
  {
    public int id;
    public int btl_mode;
    public string name;
    public int limit;
    public string begin_at;
    public string end_at;
    public int win_pt_base;
    public int lose_pt_base;
    public string[] disabledate;
    public string hurl;
  }
}
