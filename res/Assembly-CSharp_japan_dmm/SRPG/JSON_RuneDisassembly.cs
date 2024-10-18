// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RuneDisassembly
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
  public class JSON_RuneDisassembly
  {
    public string success_item;
    public int success_num;
    public string great_item;
    public int great_num;
    public string super_item;
    public int super_num;
  }
}
