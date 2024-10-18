// Decompiled with JetBrains decompiler
// Type: NativePlugin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
public static class NativePlugin
{
  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern IntPtr DecompressFile(string path, out int size);

  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern void FreePtr(IntPtr ptr);
}
