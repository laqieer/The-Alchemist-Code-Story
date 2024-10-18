// Decompiled with JetBrains decompiler
// Type: GachaTopPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class GachaTopPopup : MonoBehaviour
{
  public GameObject TextTemplate;
  public GameObject ImageTemplate;
  public GameObject Contents;
  public GameObject Title;
  private static readonly string HOST_URL = string.Empty;
  private static readonly string GACHA_DETAIL_TITLE = "sys.TITLE_POPUP_GACHA_DETAIL";
  private static readonly string GACHA_DESCRIPTION_TITLE = "sys.TITLE_POPUP_GACHA_DESCRIPTION";
  private GachaTopPopup.PopupType popupType;
  private GachaTopParam mCurrentGachaTopParam;
  private string mCurrentGachaIname;

  private void Start()
  {
    if (Object.op_Inequality((Object) this.TextTemplate, (Object) null))
      this.TextTemplate.SetActive(false);
    if (Object.op_Inequality((Object) this.ImageTemplate, (Object) null))
      this.ImageTemplate.SetActive(false);
    if (Object.op_Equality((Object) this.Contents, (Object) null) || Object.op_Equality((Object) this.Title, (Object) null))
      return;
    Text component = ((Component) this.Title.transform.Find("Text")).GetComponent<Text>();
    this.popupType = (GachaTopPopup.PopupType) int.Parse(FlowNode_Variable.Get(nameof (GachaTopPopup)));
    string key = this.popupType != GachaTopPopup.PopupType.DETAIL ? GachaTopPopup.GACHA_DESCRIPTION_TITLE : GachaTopPopup.GACHA_DETAIL_TITLE;
    if (Object.op_Inequality((Object) component, (Object) null))
      component.text = LocalizedText.Get(key);
    if (this.popupType == GachaTopPopup.PopupType.DETAIL)
    {
      this.mCurrentGachaIname = FlowNode_Variable.Get("GachaDetailSelectIname");
      if (string.IsNullOrEmpty(this.mCurrentGachaIname))
        return;
    }
    this.CreateContents();
  }

  public List<GachaDetailParam> GetGachaDetailData()
  {
    List<GachaDetailParam> gachaDetailData = new List<GachaDetailParam>();
    string empty = string.Empty;
    foreach (JSON_GachaDetailParam json in JSONParser.parseJSONArray<JSON_GachaDetailParam>(this.popupType != GachaTopPopup.PopupType.DETAIL ? AssetManager.LoadTextData("Gachas/gacha_description") : AssetManager.LoadTextData("Gachas/gacha_detail")))
    {
      GachaDetailParam gachaDetailParam = new GachaDetailParam();
      if (gachaDetailParam.Deserialize(json))
        gachaDetailData.Add(gachaDetailParam);
    }
    return gachaDetailData;
  }

  private void CreateContents()
  {
    List<GachaDetailParam> gachaDetailData = this.GetGachaDetailData();
    if (gachaDetailData == null)
      return;
    foreach (GachaDetailParam gachaDetailParam in gachaDetailData)
    {
      if (this.popupType != GachaTopPopup.PopupType.DETAIL || !(this.mCurrentGachaIname != gachaDetailParam.gname))
      {
        if (gachaDetailParam.type == 1)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.TextTemplate);
          gameObject.transform.SetParent(this.Contents.transform, false);
          Text component = ((Component) gameObject.transform.Find("Text")).GetComponent<Text>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.text = LocalizedText.Get(gachaDetailParam.text);
          gameObject.SetActive(true);
        }
        if (gachaDetailParam.type == 2)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ImageTemplate);
          gameObject.transform.SetParent(this.Contents.transform, false);
          RawImage component = ((Component) gameObject.transform.Find("Image")).GetComponent<RawImage>();
          string url = GachaTopPopup.HOST_URL + "/images/gacha/" + gachaDetailParam.image;
          if (Object.op_Inequality((Object) component, (Object) null))
            this.StartCoroutine(this.GetWWWImage(((Component) component).gameObject, url, gachaDetailParam.width, gachaDetailParam.height));
          gameObject.SetActive(true);
        }
      }
    }
  }

  [DebuggerHidden]
  private IEnumerator GetWWWImage(
    GameObject image,
    string url,
    int continue_count = 0,
    int height = 0,
    int width = 0)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new GachaTopPopup.\u003CGetWWWImage\u003Ec__Iterator0()
    {
      url = url,
      image = image
    };
  }

  public enum PopupType
  {
    DETAIL,
    DESCRIPTION,
    ALL,
  }
}
