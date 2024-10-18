// Decompiled with JetBrains decompiler
// Type: SliderSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Audio/Slider Sound")]
public class SliderSound : MonoBehaviour
{
  public string cueID = "0031";
  public float WaitSec = 0.1f;
  private float mValueMin;
  private float mValueMax;
  private float mWait;

  private void Awake()
  {
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if ((double) this.mWait <= 0.0)
      return;
    this.mWait -= Time.unscaledDeltaTime;
  }

  public void OnValueChanged()
  {
    if ((double) this.mWait > 0.0)
      return;
    this.mWait = this.WaitSec;
    Slider component = this.gameObject.GetComponent<Slider>();
    if ((Object) component == (Object) null)
      return;
    if ((double) this.mValueMin != (double) component.minValue || (double) this.mValueMax != (double) component.maxValue)
    {
      this.mValueMin = component.minValue;
      this.mValueMax = component.maxValue;
    }
    else
      this.Play();
  }

  public void Play()
  {
    MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
  }
}
