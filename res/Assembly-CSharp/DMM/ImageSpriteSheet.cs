// Decompiled with JetBrains decompiler
// Type: ImageSpriteSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
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
  private bool m_IsInitialized;
  private string m_BeforeInitializationSetKey = string.Empty;

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    if (!Application.isPlaying)
      return;
    this.m_IsInitialized = true;
    this.ForceLoad();
    if (string.IsNullOrEmpty(this.m_BeforeInitializationSetKey))
      return;
    this.SetSprite(this.m_BeforeInitializationSetKey);
  }

  public void ForceLoad()
  {
    if (string.IsNullOrEmpty(this.m_SpriteSheetPath) || !Object.op_Equality((Object) this.m_SpriteSheet, (Object) null))
      return;
    this.m_SpriteSheet = AssetManager.Load<SpriteSheet>(this.m_SpriteSheetPath);
    this.SetSprite(this.m_DefaultKey);
  }

  public string DefaultKey => this.m_DefaultKey;

  public void SetSprite(string key)
  {
    this.sprite = this.GetSprite(key);
    if (this.m_IsInitialized)
      return;
    this.m_BeforeInitializationSetKey = key;
  }

  public Sprite GetSprite(string key)
  {
    return Object.op_Inequality((Object) this.m_SpriteSheet, (Object) null) ? this.m_SpriteSheet.GetSprite(key) : (Sprite) null;
  }

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    if (Object.op_Inequality((Object) this.sprite, (Object) null))
      base.OnPopulateMesh(toFill);
    else
      toFill.Clear();
  }
}
