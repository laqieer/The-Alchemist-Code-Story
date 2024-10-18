// Decompiled with JetBrains decompiler
// Type: IconLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class IconLoader : MonoBehaviour
{
  private string mPath;
  private LoadRequest mResourceReq;
  private Texture mIcon;

  public string ResourcePath
  {
    set
    {
      if (this.mPath == value && Object.op_Equality((Object) this.IconTexture, (Object) this.mIcon))
        return;
      this.mPath = value;
      this.IconTexture = (Texture) null;
      if (string.IsNullOrEmpty(this.mPath))
      {
        this.mResourceReq = (LoadRequest) null;
        ((Behaviour) this).enabled = false;
      }
      else
      {
        this.mResourceReq = GameUtility.LoadResourceAsyncChecked<Texture2D>(this.mPath);
        ((Behaviour) this).enabled = true;
        if (!((Component) this).gameObject.activeInHierarchy)
          return;
        this.Update();
      }
    }
  }

  private void Update()
  {
    if (this.mResourceReq == null)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      if (!this.mResourceReq.isDone)
        return;
      this.IconTexture = !Object.op_Inequality(this.mResourceReq.asset, (Object) null) ? (Texture) Texture2D.blackTexture : (Texture) (this.mResourceReq.asset as Texture2D);
      this.mResourceReq = (LoadRequest) null;
      ((Behaviour) this).enabled = false;
    }
  }

  private Texture IconTexture
  {
    set
    {
      this.mIcon = value;
      RawImage component = ((Component) this).GetComponent<RawImage>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.texture = value;
    }
    get
    {
      RawImage component = ((Component) this).GetComponent<RawImage>();
      return Object.op_Inequality((Object) component, (Object) null) ? component.texture : (Texture) null;
    }
  }
}
