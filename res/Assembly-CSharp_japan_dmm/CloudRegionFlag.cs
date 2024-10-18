// Decompiled with JetBrains decompiler
// Type: CloudRegionFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Flags]
public enum CloudRegionFlag
{
  eu = 1,
  us = 2,
  asia = 4,
  jp = 8,
  au = 16, // 0x00000010
  usw = 32, // 0x00000020
  sa = 64, // 0x00000040
  cae = 128, // 0x00000080
  kr = 256, // 0x00000100
  @in = 512, // 0x00000200
  ru = 1024, // 0x00000400
  rue = 2048, // 0x00000800
}
