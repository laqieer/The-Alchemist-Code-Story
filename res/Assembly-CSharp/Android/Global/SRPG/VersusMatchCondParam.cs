// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchCondParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusMatchCondParam
  {
    public OInt Floor;
    public OInt LvRange;
    public OInt FloorRange;

    public void Deserialize(JSON_VersusMatchCondParam json)
    {
      if (json == null)
        return;
      this.Floor = (OInt) json.floor;
      this.LvRange = (OInt) json.lvrang;
      this.FloorRange = (OInt) json.floorrange;
    }
  }
}
