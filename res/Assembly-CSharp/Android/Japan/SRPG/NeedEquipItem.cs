// Decompiled with JetBrains decompiler
// Type: SRPG.NeedEquipItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class NeedEquipItem
  {
    private ItemParam param;
    private int need_picec_num;

    public NeedEquipItem(ItemParam item_param, int need_picec)
    {
      this.param = item_param;
      this.need_picec_num = need_picec;
    }

    public string Iname
    {
      get
      {
        return this.param.iname;
      }
    }

    public int CommonType
    {
      get
      {
        return (int) this.param.cmn_type;
      }
    }

    public int NeedPiece
    {
      get
      {
        return this.need_picec_num;
      }
    }

    public ItemParam Param
    {
      get
      {
        return this.param;
      }
    }
  }
}
