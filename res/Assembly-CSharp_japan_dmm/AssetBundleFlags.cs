// Decompiled with JetBrains decompiler
// Type: AssetBundleFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Flags]
public enum AssetBundleFlags
{
  Compressed = 1,
  RawData = 2,
  Required = 4,
  Scene = 8,
  Tutorial = 16, // 0x00000010
  Multiplay = 32, // 0x00000020
  StreamingAsset = 64, // 0x00000040
  TutorialMovie = 128, // 0x00000080
  Persistent = 256, // 0x00000100
  DiffAsset = 512, // 0x00000200
  iOSRequire = 1024, // 0x00000400
  Home = 2048, // 0x00000800
}
