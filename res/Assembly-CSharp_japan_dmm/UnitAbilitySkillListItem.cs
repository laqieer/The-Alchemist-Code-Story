// Decompiled with JetBrains decompiler
// Type: UnitAbilitySkillListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class UnitAbilitySkillListItem : MonoBehaviour
{
  public GameObject MaxObject;
  public GameObject RemainObject;
  public GameObject LimitObject;
  public GameObject NoLimitObject;
  public GameObject CastSpeedObject;
  public GameObject SpeedObject;

  public void SetSkillCount(int Remaining, int Limit, bool noLimit)
  {
    if (noLimit)
    {
      this.NoLimitObject.SetActive(true);
      this.LimitObject.SetActive(false);
    }
    else
    {
      this.NoLimitObject.SetActive(false);
      Text component1 = this.MaxObject.GetComponent<Text>();
      Text component2 = this.RemainObject.GetComponent<Text>();
      component1.text = Limit.ToString();
      component2.text = Remaining.ToString();
      this.LimitObject.SetActive(true);
    }
  }

  public void SetCastSpeed(OInt Speed)
  {
    if ((int) Speed > 0)
    {
      this.SpeedObject.GetComponent<Text>().text = Speed.ToString();
      this.CastSpeedObject.SetActive(true);
    }
    else
      this.CastSpeedObject.SetActive(false);
  }
}
