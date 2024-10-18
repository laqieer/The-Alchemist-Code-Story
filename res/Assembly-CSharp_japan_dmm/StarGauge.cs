// Decompiled with JetBrains decompiler
// Type: StarGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
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
    get => this.mValue;
    set
    {
      value = Mathf.Min(value, this.mValueMax);
      if (this.mValue == value)
        return;
      this.mValue = value;
      ((Behaviour) this).enabled = true;
    }
  }

  public int Max
  {
    get => this.mValueMax;
    set
    {
      value = Mathf.Max(value, 0);
      if (this.mValueMax == value)
        return;
      this.mValueMax = value;
      ((Behaviour) this).enabled = true;
    }
  }

  private void LateUpdate()
  {
    if (this.mStars != null)
    {
      for (int index = 0; index < this.mStars.Length; ++index)
        Object.Destroy((Object) this.mStars[index]);
      this.mStars = (GameObject[]) null;
    }
    if (this.mValueMax > 0)
    {
      this.mStars = new GameObject[this.mValueMax];
      Transform transform = ((Component) this).transform;
      if (Object.op_Inequality((Object) this.StarTemplate, (Object) null))
      {
        for (int index = 0; index < this.mValue; ++index)
        {
          this.mStars[index] = Object.Instantiate<GameObject>(this.StarTemplate);
          this.mStars[index].transform.SetParent(transform, false);
          this.mStars[index].SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.SlotTemplate, (Object) null))
      {
        for (int mValue = this.mValue; mValue < this.mValueMax; ++mValue)
        {
          this.mStars[mValue] = Object.Instantiate<GameObject>(this.SlotTemplate);
          this.mStars[mValue].transform.SetParent(transform, false);
          this.mStars[mValue].SetActive(true);
        }
      }
    }
    ((Behaviour) this).enabled = false;
  }
}
