// Decompiled with JetBrains decompiler
// Type: SRPG.KeyNotFoundException`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class KeyNotFoundException<T> : Exception
  {
    public KeyNotFoundException(string key)
      : base(typeof (T).ToString() + " '" + key + "' doesn't exist.")
    {
    }
  }
}
