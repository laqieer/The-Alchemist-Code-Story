// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ArchiveParam
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
  public class JSON_ArchiveParam
  {
    public string iname;
    public string area_iname;
    public string area_iname_multi;
    public int type;
    public string begin_at;
    public string end_at;
    public string keyitem1;
    public int keynum1;
    public int keytime;
    public string unit1;
    public string unit2;
    public JSON_ArchiveItemsParam[] items;
  }
}
