// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardDetailBase : MonoBehaviour
  {
    protected ConceptCardData mConceptCardData;

    protected GameManager GM => MonoSingleton<GameManager>.Instance;

    protected MasterParam Master => this.GM.MasterParam;

    public virtual void SetParam(ConceptCardData card_data) => this.mConceptCardData = card_data;

    public virtual void SetParam(
      ConceptCardData card_data,
      int addExp,
      int addTrust,
      int addAwakeLv)
    {
    }

    public virtual void Refresh()
    {
    }

    public void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = str;
    }

    public void LoadImage(string path, RawImage image)
    {
      if (!Object.op_Inequality((Object) image, (Object) null))
        return;
      string fileName = Path.GetFileName(path);
      if (!(((Object) ((Graphic) image).mainTexture).name != fileName))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, path);
    }

    public void SwitchObject(bool is_on, GameObject obj, GameObject opposite_obj)
    {
      if (Object.op_Inequality((Object) obj, (Object) null))
        obj.SetActive(is_on);
      if (!Object.op_Inequality((Object) opposite_obj, (Object) null))
        return;
      opposite_obj.SetActive(!is_on);
    }

    public void SetSprite(Image image, Sprite sprite)
    {
      if (!Object.op_Inequality((Object) image, (Object) null))
        return;
      image.sprite = sprite;
    }
  }
}
