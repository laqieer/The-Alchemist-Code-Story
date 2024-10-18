// Decompiled with JetBrains decompiler
// Type: PropertyTypeFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

[Flags]
public enum PropertyTypeFlag : byte
{
  None = 0,
  Game = 1,
  Actor = 2,
  GameAndActor = Actor | Game, // 0x03
}
