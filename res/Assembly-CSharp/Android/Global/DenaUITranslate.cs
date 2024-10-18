// Decompiled with JetBrains decompiler
// Type: DenaUITranslate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class DenaUITranslate : MonoBehaviour
{
  public Vector2 vecoffsetMin = new Vector2(44f, 0.0f);
  public Vector2 vecoffsetMax = new Vector2(-44f, 0.0f);
  public Vector3 vecScale = new Vector3(1.2f, 1.2f, 1f);
  public bool AutoAdapt;
  [Space]
  public bool doTranslate;
  public Vector3 vecTrans;
  public bool doScale;
  [Space]
  public bool OnlyShowInIphoneX;
  public GameObject go;

  private void Start()
  {
    if ((Object) this.GetComponent<DisplayUGUI>() != (Object) null)
      return;
    float num = (float) Screen.width / (float) Screen.height;
    if ((double) num >= 2.0 && this.doScale)
      this.gameObject.transform.localScale = this.vecScale;
    if ((double) num < 2.0699999332428)
      return;
    if (this.AutoAdapt)
    {
      RectTransform component = this.gameObject.GetComponent<RectTransform>();
      if ((Object) component != (Object) null)
      {
        component.offsetMin += this.vecoffsetMin;
        component.offsetMax += this.vecoffsetMax;
      }
    }
    if (this.doTranslate)
      this.gameObject.transform.Translate(this.vecTrans);
    if (!this.OnlyShowInIphoneX || !((Object) this.go != (Object) null))
      return;
    this.go.SetActive(true);
  }
}
