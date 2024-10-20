﻿// Decompiled with JetBrains decompiler
// Type: LightmapLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class LightmapLayout : MonoBehaviour
{
  public float Scale = 1f;
  public int Index;
  public Vector4 Position;
  public bool Lock;
  public Texture2D Tex;
  private MaterialPropertyBlock mMaterialProperty;

  private void Awake()
  {
    this.ApplyLayout();
  }

  private void UpdateMaterialBlock()
  {
    if ((Object) this.Tex != (Object) null)
    {
      if (this.mMaterialProperty == null)
        this.mMaterialProperty = new MaterialPropertyBlock();
      this.mMaterialProperty.SetTexture("_Lightmap", (Texture) this.Tex);
      this.mMaterialProperty.SetVector("_Lightmap_ST", this.Position);
    }
    else
      this.mMaterialProperty = (MaterialPropertyBlock) null;
    Renderer component = this.GetComponent<Renderer>();
    if (!((Object) component != (Object) null))
      return;
    component.SetPropertyBlock(this.mMaterialProperty);
  }

  private void OnEnable()
  {
    this.UpdateMaterialBlock();
  }

  private void OnDisable()
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    Renderer component = this.GetComponent<Renderer>();
    if (!((Object) component != (Object) null))
      return;
    component.SetPropertyBlock((MaterialPropertyBlock) null);
  }

  public void ApplyLayout()
  {
    Renderer component = this.GetComponent<Renderer>();
    if ((Object) component != (Object) null)
    {
      component.lightmapIndex = this.Index;
      component.lightmapScaleOffset = this.Position;
    }
    this.UpdateMaterialBlock();
  }

  public Vector4 lightmapTilingOffset
  {
    get
    {
      return this.Position;
    }
  }

  public int lightmapIndex
  {
    get
    {
      return this.Index;
    }
  }
}