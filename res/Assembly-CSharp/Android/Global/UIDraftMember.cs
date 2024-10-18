// Decompiled with JetBrains decompiler
// Type: UIDraftMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Expose")]
[ExecuteInEditMode]
[RequireComponent(typeof (RectTransform))]
public class UIDraftMember : MonoBehaviour
{
  [SerializeField]
  private bool mSearchGraphicComponent = true;
  public bool UseGraphic;

  private void Awake()
  {
    if (!Application.isEditor)
      return;
    this.tag = "EditorOnly";
    if (!this.mSearchGraphicComponent)
      return;
    if ((Object) this.GetComponent<Text>() != (Object) null)
      this.UseGraphic = true;
    if ((Object) this.GetComponent<RawImage>() != (Object) null)
      this.UseGraphic = true;
    if ((Object) this.GetComponent<Image>() != (Object) null)
      this.UseGraphic = true;
    this.mSearchGraphicComponent = false;
  }
}
