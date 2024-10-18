// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ArenaResult
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
  public class JSON_ArenaResult
  {
    public int rank;
    public int coin;
    public int gold;
    public int ac;
    public string item1;
    public string item2;
    public string item3;
    public string item4;
    public string item5;
    public int num1;
    public int num2;
    public int num3;
    public int num4;
    public int num5;
    public string begin_at;
    public string end_at;
  }
}
