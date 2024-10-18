// Decompiled with JetBrains decompiler
// Type: StarGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("UI/Star Gauge")]
public class StarGauge : MonoBehaviour
{
  [Description("明るい星の雛形となるゲームオブジェクト")]
  public GameObject StarTemplate;
  [Description("暗い星の雛形となるゲームオブジェクト")]
  public GameObject SlotTemplate;
  private int mValue;
  private int mValueMax;
  private GameObject[] mStars;

  public int Value
  {
    get
    {
      return this.mValue;
    }
    set
    {
      value = Mathf.Min(value, this.mValueMax);
      if (this.mValue == value)
        return;
      this.mValue = value;
      this.enabled = true;
    }
  }

  public int Max
  {
    get
    {
      return this.mValueMax;
    }
    set
    {
      value = Mathf.Max(value, 0);
      if (this.mValueMax == value)
        return;
      this.mValueMax = value;
      this.enabled = true;
    }
  }

  private void LateUpdate()
  {
    if (this.mStars != null)
    {
      for (int index = 0; index < this.mStars.Length; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mStars[index]);
      this.mStars = (GameObject[]) null;
    }
    if (this.mValueMax > 0)
    {
      this.mStars = new GameObject[this.mValueMax];
      Transform transform = this.transform;
      if ((UnityEngine.Object) this.StarTemplate != (UnityEngine.Object) null)
      {
        for (int index = 0; index < this.mValue; ++index)
        {
          this.mStars[index] = UnityEngine.Object.Instantiate<GameObject>(this.StarTemplate);
          this.mStars[index].transform.SetParent(transform, false);
          this.mStars[index].SetActive(true);
        }
      }
      if ((UnityEngine.Object) this.SlotTemplate != (UnityEngine.Object) null)
      {
        for (int mValue = this.mValue; mValue < this.mValueMax; ++mValue)
        {
          this.mStars[mValue] = UnityEngine.Object.Instantiate<GameObject>(this.SlotTemplate);
          this.mStars[mValue].transform.SetParent(transform, false);
          this.mStars[mValue].SetActive(true);
        }
      }
    }
    this.enabled = false;
  }
}
