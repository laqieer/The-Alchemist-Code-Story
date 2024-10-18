// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchCondParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
