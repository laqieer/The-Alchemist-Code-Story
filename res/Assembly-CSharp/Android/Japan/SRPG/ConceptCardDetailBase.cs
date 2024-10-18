// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailBase : MonoBehaviour
  {
    protected ConceptCardData mConceptCardData;

    protected GameManager GM
    {
      get
      {
        return MonoSingleton<GameManager>.Instance;
      }
    }

    protected MasterParam Master
    {
      get
      {
        return this.GM.MasterParam;
      }
    }

    public virtual void SetParam(ConceptCardData card_data)
    {
      this.mConceptCardData = card_data;
    }

    public virtual void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
    }

    public virtual void Refresh()
    {
    }

    public void SetText(Text text, string str)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = str;
    }

    public void LoadImage(string path, RawImage image)
    {
      if (!((UnityEngine.Object) image != (UnityEngine.Object) null))
        return;
      string fileName = Path.GetFileName(path);
      if (!(image.mainTexture.name != fileName))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, path);
    }

    public void SwitchObject(bool is_on, GameObject obj, GameObject opposite_obj)
    {
      if ((UnityEngine.Object) obj != (UnityEngine.Object) null)
        obj.SetActive(is_on);
      if (!((UnityEngine.Object) opposite_obj != (UnityEngine.Object) null))
        return;
      opposite_obj.SetActive(!is_on);
    }

    public void SetSprite(Image image, Sprite sprite)
    {
      if (!((UnityEngine.Object) image != (UnityEngine.Object) null))
        return;
      image.sprite = sprite;
    }
  }
}
