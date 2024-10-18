// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ItemParam
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
  public class JSON_ItemParam
  {
    public string iname;
    public string name;
    public string expr;
    public string flavor;
    public int type;
    public int tabtype;
    public int rare;
    public int cap;
    public int invcap;
    public int eqlv;
    public int coin;
    public int tc;
    public int ac;
    public int ec;
    public int mc;
    public int pp;
    public int buy;
    public int sell;
    public int encost;
    public int enpt;
    public int facilitypt;
    public int val;
    public string icon;
    public string skill;
    public string recipe;
    public int is_valuables;
    public int is_cmn;
    public byte cmn_type;
    public int gallery_view;
    public string begin_at;
    public string end_at;
  }
}
