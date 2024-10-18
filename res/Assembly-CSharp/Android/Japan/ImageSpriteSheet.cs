// Decompiled with JetBrains decompiler
// Type: ImageSpriteSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ImageSpriteSheet (透明)")]
public class ImageSpriteSheet : Image
{
  [HeaderBar("▼スプライトシートのパス")]
  [SerializeField]
  [StringIsResourcePath(typeof (SpriteSheet))]
  private string m_SpriteSheetPath;
  [SerializeField]
  private string m_DefaultKey;
  private SpriteSheet m_SpriteSheet;

  protected override void Start()
  {
    base.Start();
    if (!Application.isPlaying)
      return;
    if (!string.IsNullOrEmpty(this.m_SpriteSheetPath))
      this.m_SpriteSheet = AssetManager.Load<SpriteSheet>(this.m_SpriteSheetPath);
    this.SetSprite(this.m_DefaultKey);
  }

  public string DefaultKey
  {
    get
    {
      return this.m_DefaultKey;
    }
  }

  public void SetSprite(string key)
  {
    this.sprite = this.GetSprite(key);
  }

  public Sprite GetSprite(string key)
  {
    if ((UnityEngine.Object) this.m_SpriteSheet != (UnityEngine.Object) null)
      return this.m_SpriteSheet.GetSprite(key);
    return (Sprite) null;
  }

  protected override void OnPopulateMesh(VertexHelper toFill)
  {
    if ((UnityEngine.Object) this.sprite != (UnityEngine.Object) null)
      base.OnPopulateMesh(toFill);
    else
      toFill.Clear();
  }
}
