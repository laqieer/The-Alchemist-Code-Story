// Decompiled with JetBrains decompiler
// Type: DeviceKit.HardkeyHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace DeviceKit
{
  internal class HardkeyHandler : MonoBehaviour
  {
    private static IHardkeyListener _listener;

    private static void devicekit_setHardkeyListener(string gameObjectName)
    {
    }

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
  }
}
