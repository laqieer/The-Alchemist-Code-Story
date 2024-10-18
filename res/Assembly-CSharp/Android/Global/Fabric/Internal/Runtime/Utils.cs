// Decompiled with JetBrains decompiler
// Type: Fabric.Internal.Runtime.Utils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Internal.Runtime
{
  public static class Utils
  {
    public static void Log(string kit, string message)
    {
      new AndroidJavaClass("android.util.Log").CallStatic<int>("d", (object) kit, (object) message);
    }
  }
}
