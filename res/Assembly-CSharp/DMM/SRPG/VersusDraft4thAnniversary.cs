// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraft4thAnniversary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusDraft4thAnniversary : MonoBehaviour
  {
    private static int WinCnt;
    private static int RemainCnt;
    private static bool Enable;
    private static bool IsDefeat;
    private static string BeginAt;
    private static string EndAt;
    private static string URL;
    [SerializeField]
    private ImageArray StatusImage;
    [SerializeField]
    private Text EndAtText;
    [SerializeField]
    private Text WinCountText;
    [SerializeField]
    private Text RemainCountText;
    [SerializeField]
    private Button EntryButton;

    public static void Setup(
      ReqVersusStatus.Draft4thAnniversaryFormData formData)
    {
      VersusDraft4thAnniversary.WinCnt = formData.win;
      VersusDraft4thAnniversary.RemainCnt = formData.remain;
      VersusDraft4thAnniversary.Enable = formData.enable != 0;
      VersusDraft4thAnniversary.IsDefeat = formData.is_defeat != 0;
      VersusDraft4thAnniversary.BeginAt = formData.begin_at;
      VersusDraft4thAnniversary.EndAt = formData.end_at;
      VersusDraft4thAnniversary.URL = formData.url;
    }

    private void Awake()
    {
      DateTime result1;
      DateTime.TryParse(VersusDraft4thAnniversary.BeginAt, out result1);
      DateTime result2;
      DateTime.TryParse(VersusDraft4thAnniversary.EndAt, out result2);
      DateTime serverTime = TimeManager.ServerTime;
      if (serverTime < result1 || result2 < serverTime)
      {
        ((Component) this).gameObject.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EndAtText, (UnityEngine.Object) null))
          this.EndAtText.text = result2.ToString("yyyy年M月d日 HH:mm");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WinCountText, (UnityEngine.Object) null))
          this.WinCountText.text = VersusDraft4thAnniversary.WinCnt.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainCountText, (UnityEngine.Object) null))
          this.RemainCountText.text = VersusDraft4thAnniversary.RemainCnt.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EntryButton, (UnityEngine.Object) null))
        {
          ((Component) this.EntryButton).gameObject.SetActive(VersusDraft4thAnniversary.Enable);
          Button.ButtonClickedEvent onClick = this.EntryButton.onClick;
          // ISSUE: reference to a compiler-generated field
          if (VersusDraft4thAnniversary.\u003C\u003Ef__am\u0024cache0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            VersusDraft4thAnniversary.\u003C\u003Ef__am\u0024cache0 = new UnityAction((object) null, __methodptr(\u003CAwake\u003Em__0));
          }
          // ISSUE: reference to a compiler-generated field
          UnityAction fAmCache0 = VersusDraft4thAnniversary.\u003C\u003Ef__am\u0024cache0;
          ((UnityEvent) onClick).AddListener(fAmCache0);
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StatusImage, (UnityEngine.Object) null))
          return;
        if (VersusDraft4thAnniversary.Enable)
          this.StatusImage.ImageIndex = 1;
        else if (VersusDraft4thAnniversary.IsDefeat)
          this.StatusImage.ImageIndex = 2;
        else
          this.StatusImage.ImageIndex = 0;
      }
    }
  }
}
