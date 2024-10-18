// Decompiled with JetBrains decompiler
// Type: IconLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class IconLoader : MonoBehaviour
{
  private string mPath;
  private LoadRequest mResourceReq;
  private Texture mIcon;

  public string ResourcePath
  {
    set
    {
      if (this.mPath == value && (Object) this.IconTexture == (Object) this.mIcon)
        return;
      this.mPath = value;
      this.IconTexture = (Texture) null;
      if (string.IsNullOrEmpty(this.mPath))
      {
        this.mResourceReq = (LoadRequest) null;
        this.enabled = false;
      }
      else
      {
        this.mResourceReq = GameUtility.LoadResourceAsyncChecked<Texture2D>(this.mPath);
        this.enabled = true;
        if (!this.gameObject.activeInHierarchy)
          return;
        this.Update();
      }
    }
  }

  private void Update()
  {
    if (this.mResourceReq == null)
    {
      this.enabled = false;
    }
    else
    {
      if (!this.mResourceReq.isDone)
        return;
      this.IconTexture = !(this.mResourceReq.asset != (Object) null) ? (Texture) Texture2D.blackTexture : (Texture) (this.mResourceReq.asset as Texture2D);
      this.mResourceReq = (LoadRequest) null;
      this.enabled = false;
    }
  }

  private Texture IconTexture
  {
    set
    {
      this.mIcon = value;
      RawImage component = this.GetComponent<RawImage>();
      if (!((Object) component != (Object) null))
        return;
      component.texture = value;
    }
    get
    {
      RawImage component = this.GetComponent<RawImage>();
      if ((Object) component != (Object) null)
        return component.texture;
      return (Texture) null;
    }
  }
}
