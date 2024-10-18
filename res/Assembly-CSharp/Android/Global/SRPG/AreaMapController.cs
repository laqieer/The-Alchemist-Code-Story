// Decompiled with JetBrains decompiler
// Type: SRPG.AreaMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [RequireComponent(typeof (CanvasGroup))]
  public class AreaMapController : MonoBehaviour
  {
    public RawImage_Transparent[] Images = new RawImage_Transparent[0];
    public string[] ImageNames = new string[0];
    public string MapID;
    private CanvasGroup mCanvasGroup;

    private void Awake()
    {
      this.SetVisible(false);
    }

    private void Start()
    {
      this.mCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void SetOpacity(float opacity)
    {
      opacity = Mathf.Clamp01(opacity);
      this.SetVisible((double) opacity > 0.0);
      if (!((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null))
        return;
      this.mCanvasGroup.alpha = Mathf.Clamp01(opacity);
    }

    private void SetVisible(bool visible)
    {
      this.gameObject.SetActive(visible);
    }

    private void OnEnable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && (UnityEngine.Object) this.Images[index] != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.ImageNames[index]))
          this.Images[index].texture = (Texture) AssetManager.Load<Texture2D>(this.ImageNames[index]);
      }
    }

    private void OnDisable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && (UnityEngine.Object) this.Images[index] != (UnityEngine.Object) null)
          this.Images[index].texture = (Texture) null;
      }
    }
  }
}
