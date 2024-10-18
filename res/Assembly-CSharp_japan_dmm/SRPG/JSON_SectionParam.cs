// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_SectionParam
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
  public class JSON_SectionParam
  {
    public string iname;
    public string name;
    public string expr;
    public long start;
    public long end;
    public int hide;
    public string home;
    public string unit;
    public string item;
    public string shop;
    public string inn;
    public string bar;
    public string bgm;
    public int story_part;
    public string release_key_quest;
    public string message_sys_id;
  }
}
