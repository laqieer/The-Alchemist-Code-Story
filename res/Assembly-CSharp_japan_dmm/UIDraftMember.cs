// Decompiled with JetBrains decompiler
// Type: UIDraftMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (RectTransform))]
[ExecuteInEditMode]
[AddComponentMenu("UI/Expose")]
public class UIDraftMember : MonoBehaviour
{
  [SerializeField]
  private bool mSearchGraphicComponent = true;
  public bool UseGraphic;

  private void Awake()
  {
    if (!Application.isEditor)
      return;
    ((Component) this).tag = "EditorOnly";
    if (!this.mSearchGraphicComponent)
      return;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<Text>(), (Object) null))
      this.UseGraphic = true;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<RawImage>(), (Object) null))
      this.UseGraphic = true;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<Image>(), (Object) null))
      this.UseGraphic = true;
    this.mSearchGraphicComponent = false;
  }
}
