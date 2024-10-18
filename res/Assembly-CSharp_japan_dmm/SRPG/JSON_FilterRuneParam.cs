// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_FilterRuneParam
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
  public class JSON_FilterRuneParam
  {
    public string iname;
    public string tab_name;
    public string name;
    public int filter_type;
    public JSON_FilterRuneConditionParam[] cnds;
  }
}
