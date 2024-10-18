// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleLocalMapsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class SimpleLocalMapsParam
  {
    public string iname;
    public string[] droplist;

    public bool Deserialize(JSON_SimpleLocalMapsParam json)
    {
      this.iname = json.iname;
      this.droplist = json.droplist;
      return true;
    }
  }
}
