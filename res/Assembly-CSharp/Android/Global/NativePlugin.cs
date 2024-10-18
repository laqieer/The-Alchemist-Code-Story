// Decompiled with JetBrains decompiler
// Type: NativePlugin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

public static class NativePlugin
{
  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern IntPtr DecompressFile(string path, out int size);

  [DllImport("NativePlugin", CharSet = CharSet.Ansi)]
  public static extern void FreePtr(IntPtr ptr);

  [DllImport("NativePlugin")]
  public static extern int GetGLExtensions([MarshalAs(UnmanagedType.LPArray)] byte[] dest, int destLen);
}
