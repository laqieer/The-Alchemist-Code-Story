// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JSON_QuestProgress
  {
    public string i;
    public long s;
    public long e;
    public long n;
    public int c;
    public int t;
    public int m;
    public JSON_QuestCount d;
    public int b;
  }
}
