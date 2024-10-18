// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeCountResetCompleteWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChallengeCountResetCompleteWindow : MonoBehaviour
  {
    [SerializeField]
    private Text ChallengeCountResetNum;
    [SerializeField]
    private Text ChallengeCountResetMax;
    [SerializeField]
    private Button DecideButton;
    public ChallengeCountResetCompleteWindow.DecideButtonEvent DecideEvent;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickEvent)));
    }

    private void OnClickEvent()
    {
      if (this.DecideEvent == null)
        return;
      this.DecideEvent(((Component) this).gameObject);
    }

    public void Setup(
      int reset_num,
      int reset_max,
      ChallengeCountResetCompleteWindow.DecideButtonEvent okEvnet)
    {
      if (Object.op_Inequality((Object) this.ChallengeCountResetNum, (Object) null))
        this.ChallengeCountResetNum.text = reset_num.ToString();
      if (Object.op_Inequality((Object) this.ChallengeCountResetMax, (Object) null))
        this.ChallengeCountResetMax.text = reset_max.ToString();
      this.DecideEvent = okEvnet;
    }

    public delegate void DecideButtonEvent(GameObject dialog);
  }
}
