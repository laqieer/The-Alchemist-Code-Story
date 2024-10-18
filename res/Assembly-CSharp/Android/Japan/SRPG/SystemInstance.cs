// Decompiled with JetBrains decompiler
// Type: SRPG.SystemInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/System/SystemInstance")]
  public class SystemInstance : MonoSingleton<SystemInstance>
  {
    private static System.Type[] SYSTEM_INSTANCE = new System.Type[3]{ typeof (GameManager), typeof (TimeManager), typeof (Network) };

    protected override void Initialize()
    {
      DeployGateSDK.Install();
      int length = SystemInstance.SYSTEM_INSTANCE.Length;
      for (int index = 0; index < length; ++index)
      {
        System.Type componentType = SystemInstance.SYSTEM_INSTANCE[index];
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = this.transform;
        gameObject.transform.name = componentType.Name;
        gameObject.AddComponent(componentType);
      }
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }
  }
}
