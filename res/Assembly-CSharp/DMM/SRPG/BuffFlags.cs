// Decompiled with JetBrains decompiler
// Type: SRPG.BuffFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public enum BuffFlags
  {
    UpReplenish = 1,
    NoDisabled = 2,
    NoBuffTurn = 4,
    AvoidPerfect = 8,
    AvoidMiss = 16, // 0x00000010
    UpReplenishUseSkillCtr = 32, // 0x00000020
  }
}
