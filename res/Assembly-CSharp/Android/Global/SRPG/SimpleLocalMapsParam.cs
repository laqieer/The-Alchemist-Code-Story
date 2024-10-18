// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleLocalMapsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
