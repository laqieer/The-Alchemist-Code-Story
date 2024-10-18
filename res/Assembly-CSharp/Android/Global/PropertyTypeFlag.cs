// Decompiled with JetBrains decompiler
// Type: PropertyTypeFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

[Flags]
public enum PropertyTypeFlag : byte
{
  None = 0,
  Game = 1,
  Actor = 2,
  GameAndActor = Actor | Game, // 0x03
}
