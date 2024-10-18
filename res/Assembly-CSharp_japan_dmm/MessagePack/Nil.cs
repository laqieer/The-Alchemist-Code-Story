// Decompiled with JetBrains decompiler
// Type: MessagePack.Nil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace MessagePack
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct Nil : IEquatable<Nil>
  {
    public static readonly Nil Default = new Nil();

    public override bool Equals(object obj) => obj is Nil;

    public bool Equals(Nil other) => true;

    public override int GetHashCode() => 0;

    public override string ToString() => "()";
  }
}
