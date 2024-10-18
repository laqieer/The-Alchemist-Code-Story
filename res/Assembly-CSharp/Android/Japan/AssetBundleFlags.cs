// Decompiled with JetBrains decompiler
// Type: AssetBundleFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
  iOSRequire = 1024, // 0x00000400
}
