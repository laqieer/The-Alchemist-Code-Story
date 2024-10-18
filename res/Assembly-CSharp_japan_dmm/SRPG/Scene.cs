// Decompiled with JetBrains decompiler
// Type: SRPG.Scene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public abstract class Scene : MonoBehaviour
  {
    protected bool IsLoaded { get; set; }

    protected void Awake()
    {
      MonoSingleton<SystemInstance>.Instance.Ensure();
      GameUtility.RemoveDuplicatedMainCamera();
    }
  }
}
