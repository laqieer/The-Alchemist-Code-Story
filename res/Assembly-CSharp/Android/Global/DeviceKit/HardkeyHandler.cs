// Decompiled with JetBrains decompiler
// Type: DeviceKit.HardkeyHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;
using UnityEngine;

namespace DeviceKit
{
  internal class HardkeyHandler : MonoBehaviour
  {
    private static IHardkeyListener _listener;

    [DllImport("devicekit")]
    private static extern void devicekit_setHardkeyListener(string gameObjectName);

    public static void Init(GameObject serviceNode = null)
    {
      if ((Object) serviceNode == (Object) null)
      {
        serviceNode = new GameObject(nameof (HardkeyHandler));
        serviceNode.hideFlags |= HideFlags.HideInHierarchy;
        Object.DontDestroyOnLoad((Object) serviceNode);
      }
      if (!((Object) serviceNode.GetComponent<HardkeyHandler>() == (Object) null))
        return;
      HardkeyHandler.devicekit_setHardkeyListener(serviceNode.AddComponent<HardkeyHandler>().gameObject.name);
    }

    public static void SetListener(IHardkeyListener listener)
    {
      HardkeyHandler._listener = listener;
    }

    private void Hardkey_OnBackKey(string msg)
    {
      if (HardkeyHandler._listener == null)
        return;
      HardkeyHandler._listener.OnBackKey();
    }

    private void Update()
    {
      if (!Input.GetKey(KeyCode.Escape))
        return;
      this.Hardkey_OnBackKey((string) null);
    }
  }
}
