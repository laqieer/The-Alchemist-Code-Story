// Decompiled with JetBrains decompiler
// Type: LocalNotificationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public class LocalNotificationInfo
{
  public int id;
  public int push_flg;
  public string trophy_iname;
  public string push_word;

  public bool Deserialize(JSON_LocalNotificationInfo json)
  {
    if (json == null)
      return false;
    this.id = json.fields.id;
    this.trophy_iname = json.fields.trophy_iname;
    this.push_flg = json.fields.push_flg;
    this.push_word = json.fields.push_word;
    return true;
  }
}
