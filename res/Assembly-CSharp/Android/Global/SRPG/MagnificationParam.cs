// Decompiled with JetBrains decompiler
// Type: SRPG.MagnificationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class MagnificationParam
  {
    public string iname;
    public int[] atkMagnifications;

    public void Deserialize(JSON_MagnificationParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.atkMagnifications = json.atk;
    }
  }
}
