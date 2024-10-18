// Decompiled with JetBrains decompiler
// Type: GachaDetailParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public class GachaDetailParam
{
  public int id;
  public string gname;
  public int type;
  public string text;
  public string image;
  public int width;
  public int height;

  public bool Deserialize(JSON_GachaDetailParam json)
  {
    if (json == null)
      return false;
    this.id = json.fields.id;
    this.gname = json.fields.gname;
    this.type = json.fields.type;
    this.text = json.fields.text;
    this.image = json.fields.image;
    this.width = json.fields.width;
    this.height = json.fields.height;
    return true;
  }

  public enum DetailType
  {
    none,
    text,
    image,
    all,
  }
}
