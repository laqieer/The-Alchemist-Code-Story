// Decompiled with JetBrains decompiler
// Type: SRPG.Json_DropInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_DropInfo
  {
    public string iname = string.Empty;
    public int num;
    public string iname_origin = string.Empty;
    public string type = string.Empty;
    public int is_new;
    public int rare = -1;
    public string get_unit = string.Empty;
    public int is_gift;
    public int is_feature_item;
    public int ch_piece_coin_num;
    public int is_pickup;
  }
}
