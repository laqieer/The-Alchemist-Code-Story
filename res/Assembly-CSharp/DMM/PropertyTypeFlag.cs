// Decompiled with JetBrains decompiler
// Type: PropertyTypeFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Flags]
public enum PropertyTypeFlag : byte
{
  None = 0,
  Game = 1,
  Actor = 2,
  GameAndActor = Actor | Game, // 0x03
}
