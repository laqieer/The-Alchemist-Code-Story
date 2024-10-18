// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Friend
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
  public class Json_Friend
  {
    public string uid;
    public string fuid;
    public string name;
    public string type;
    public int lv;
    public long lastlogin;
    public int is_multi_push;
    public string multi_comment;
    public Json_Unit unit;
    public string created_at;
    public int is_favorite;
    public string award;
    public string wish;
    public string status;
    public JSON_ViewGuild guild;
  }
}
