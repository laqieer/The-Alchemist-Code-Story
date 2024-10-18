// Decompiled with JetBrains decompiler
// Type: LightFilterVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("")]
[ExecuteInEditMode]
public class LightFilterVolume : MonoBehaviour
{
  private void Awake()
  {
    ((Component) this).tag = "EditorOnly";
    ((Object) this).hideFlags = (HideFlags) 2;
    ((Component) this).transform.localScale = Vector3.op_Multiply(Vector3.one, 3f);
    ((Object) ((Component) this).gameObject).hideFlags = (HideFlags) 52;
  }

  public Bounds Bounds
  {
    get
    {
      return new Bounds(((Component) this).transform.position, ((Component) this).transform.localScale);
    }
  }
}
