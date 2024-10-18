// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchCondParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
