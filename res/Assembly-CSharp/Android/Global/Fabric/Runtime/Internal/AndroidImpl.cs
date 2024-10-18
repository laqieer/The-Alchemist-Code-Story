// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.AndroidImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Runtime.Internal
{
  internal class AndroidImpl : Impl
  {
    private static readonly AndroidJavaClass FabricInitializer = new AndroidJavaClass("io.fabric.unity.android.FabricInitializer");

    public override string Initialize()
    {
      return AndroidImpl.FabricInitializer.CallStatic<string>("JNI_InitializeFabric");
    }
  }
}
