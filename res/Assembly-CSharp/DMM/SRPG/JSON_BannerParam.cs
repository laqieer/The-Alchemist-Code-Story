// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_BannerParam
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
  public class JSON_BannerParam
  {
    public string iname;
    public string type;
    public string sval;
    public string banr;
    public string banr_sprite;
    public string begin_at;
    public string end_at;
    public int priority;
    public string message;
    public int is_not_home;
    public int tab_type;
  }
}
