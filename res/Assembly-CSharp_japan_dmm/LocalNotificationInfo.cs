// Decompiled with JetBrains decompiler
// Type: LocalNotificationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
