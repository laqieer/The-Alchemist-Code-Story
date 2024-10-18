// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlRewardConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_BtlRewardConceptCard
  {
    public string iname;
    public int num;
    public string get_unit;
    public long get_unit_iid;

    public bool IsGetUnit
    {
      get
      {
        if (!string.IsNullOrEmpty(this.get_unit))
          return this.get_unit_iid != 0L;
        return false;
      }
    }
  }
}
