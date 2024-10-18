// Decompiled with JetBrains decompiler
// Type: SRPG.AreaMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (CanvasGroup))]
  public class AreaMapController : MonoBehaviour
  {
    public string MapID;
    public RawImage_Transparent[] Images = new RawImage_Transparent[0];
    public string[] ImageNames = new string[0];
    private CanvasGroup mCanvasGroup;

    private void Awake() => this.SetVisible(false);

    private void Start() => this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();

    public void SetOpacity(float opacity)
    {
      opacity = Mathf.Clamp01(opacity);
      this.SetVisible((double) opacity > 0.0);
      if (!Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup.alpha = Mathf.Clamp01(opacity);
    }

    private void SetVisible(bool visible) => ((Component) this).gameObject.SetActive(visible);

    private void OnEnable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && Object.op_Inequality((Object) this.Images[index], (Object) null) && !string.IsNullOrEmpty(this.ImageNames[index]))
          this.Images[index].texture = (Texture) AssetManager.Load<Texture2D>(this.ImageNames[index]);
      }
    }

    private void OnDisable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && Object.op_Inequality((Object) this.Images[index], (Object) null))
          this.Images[index].texture = (Texture) null;
      }
    }
  }
}
