// Decompiled with JetBrains decompiler
// Type: SkillAnimationToggleController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillAnimationToggleController : MonoBehaviour
{
  [SerializeField]
  private Button skillAnimationOff;
  [SerializeField]
  private Button skillAnimationOn;

  private void Start()
  {
    this.skillAnimationOn.onClick.AddListener(new UnityAction(this.TurnOffSkillAnimation));
    this.skillAnimationOff.onClick.AddListener(new UnityAction(this.TurnOnSkillAnimation));
    this.updateToggleButtons(GameUtility.Config_SkillAnimation);
  }

  protected void updateToggleButtons(bool withSkillAnimation)
  {
    this.skillAnimationOn.gameObject.SetActive(withSkillAnimation);
    this.skillAnimationOff.gameObject.SetActive(!withSkillAnimation);
  }

  protected void TurnOffSkillAnimation()
  {
    GameUtility.Config_SkillAnimation = false;
    this.updateToggleButtons(false);
  }

  protected void TurnOnSkillAnimation()
  {
    GameUtility.Config_SkillAnimation = true;
    this.updateToggleButtons(true);
  }
}
