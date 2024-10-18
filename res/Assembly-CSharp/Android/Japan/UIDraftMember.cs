// Decompiled with JetBrains decompiler
// Type: UIDraftMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
