// Decompiled with JetBrains decompiler
// Type: LightmapLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class LightmapLayout : MonoBehaviour
{
  public int Index;
  public Vector4 Position;
  public bool Lock;
  public float Scale = 1f;
  public Texture2D Tex;
  private MaterialPropertyBlock mMaterialProperty;

  private void Awake() => this.ApplyLayout();

  private void UpdateMaterialBlock()
  {
    if (Object.op_Inequality((Object) this.Tex, (Object) null))
    {
      if (this.mMaterialProperty == null)
        this.mMaterialProperty = new MaterialPropertyBlock();
      this.mMaterialProperty.SetTexture("_Lightmap", (Texture) this.Tex);
      this.mMaterialProperty.SetVector("_Lightmap_ST", this.Position);
    }
    else
      this.mMaterialProperty = (MaterialPropertyBlock) null;
    Renderer component = ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.SetPropertyBlock(this.mMaterialProperty);
  }

  private void OnEnable() => this.UpdateMaterialBlock();

  private void OnDisable()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    Renderer component = ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.SetPropertyBlock((MaterialPropertyBlock) null);
  }

  public void ApplyLayout()
  {
    Renderer component = ((Component) this).GetComponent<Renderer>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.lightmapIndex = this.Index;
      component.lightmapScaleOffset = this.Position;
    }
    this.UpdateMaterialBlock();
  }

  public Vector4 lightmapTilingOffset => this.Position;

  public int lightmapIndex => this.Index;
}
