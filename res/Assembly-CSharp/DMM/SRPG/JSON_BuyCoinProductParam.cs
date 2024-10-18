// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_BuyCoinProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_BuyCoinProductParam
  {
    public string iname;
    public string product_id;
    public string shop_id;
    public int type;
    public int val;
    public int is_platform_common;
    public string reward;
    public string title;
    public string description;
    public int badge;
    public int img_array_idx;
    public int unlock_lv;
    public string temp_name;
  }
}
