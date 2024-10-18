// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GuildMember
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
  public class JSON_GuildMember
  {
    public long gid;
    public string uid;
    public int role_id;
    public string name;
    public int lv;
    public string award_id;
    public Json_Unit units;
    public long applied_at;
    public long joined_at;
    public long leave_at;
    public long lastlogin;
  }
}
