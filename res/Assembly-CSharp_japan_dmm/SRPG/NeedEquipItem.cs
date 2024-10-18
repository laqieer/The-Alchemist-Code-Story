// Decompiled with JetBrains decompiler
// Type: SRPG.NeedEquipItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public string Iname => this.param.iname;

    public int CommonType => (int) this.param.cmn_type;

    public int NeedPiece => this.need_picec_num;

    public ItemParam Param => this.param;
  }
}
