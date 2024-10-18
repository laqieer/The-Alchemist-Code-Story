// Decompiled with JetBrains decompiler
// Type: AssetBundleFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
  IsLanguage = 4096, // 0x00001000
  IsCombined = 8192, // 0x00002000
  IsFolder = 16384, // 0x00004000
}
