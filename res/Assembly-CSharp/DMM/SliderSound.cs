// Decompiled with JetBrains decompiler
// Type: SliderSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    Slider component = ((Component) this).gameObject.GetComponent<Slider>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if ((double) this.mValueMin != (double) component.minValue || (double) this.mValueMax != (double) component.maxValue)
    {
      this.mValueMin = component.minValue;
      this.mValueMax = component.maxValue;
    }
    else
      this.Play();
  }

  public void Play() => MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID);
}
