// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Utils.ThreadSafeRandom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace CodeStage.AntiCheat.Utils
{
  public class ThreadSafeRandom
  {
    private static readonly Random Global = new Random();
    [ThreadStatic]
    private static Random local;

    public static int Next(int minInclusive, int maxExclusive)
    {
      Random local = ThreadSafeRandom.local;
      if (local != null)
        return local.Next(minInclusive, maxExclusive);
      int Seed;
      lock ((object) ThreadSafeRandom.Global)
        Seed = ThreadSafeRandom.Global.Next();
      return (ThreadSafeRandom.local = new Random(Seed)).Next(minInclusive, maxExclusive);
    }

    public static int Next() => ThreadSafeRandom.Next(1, int.MaxValue);

    public static int Next(int maxExclusive) => ThreadSafeRandom.Next(1, maxExclusive);
  }
}
