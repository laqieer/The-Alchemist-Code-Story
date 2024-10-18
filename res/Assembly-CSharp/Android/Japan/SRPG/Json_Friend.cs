// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Friend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
