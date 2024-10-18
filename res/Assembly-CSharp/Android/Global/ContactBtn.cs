// Decompiled with JetBrains decompiler
// Type: ContactBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContactBtn : MonoBehaviour
{
  public ContactBtn.SubjectType Subject = ContactBtn.SubjectType.DataRestore;

  private void Start()
  {
    Button component = this.gameObject.GetComponent<Button>();
    if ((UnityEngine.Object) component == (UnityEngine.Object) null)
      Debug.Log((object) "@@@[Menu]Button is Null Object!");
    else
      component.onClick.AddListener(new UnityAction(this.OpenMailer));
  }

  private void OpenMailer()
  {
    string mailto = LocalizedText.Get("contact.CONTACT_ADDRESS");
    int subject1 = (int) this.Subject;
    string subject2 = LocalizedText.Get("contact.CONTACT_SUBJECT_" + subject1.ToString("d2"));
    if (string.IsNullOrEmpty(subject2))
      subject2 = LocalizedText.Get("contact.CONTACT_SUBJECT");
    string str1 = LocalizedText.Get("contact.CONTACT_BODY_OPTION_" + subject1.ToString("d2"));
    string str2 = "CONTACT_BODY_OPTION_" + subject1.ToString("d2");
    string str3 = LocalizedText.Get("contact.CONTACT_BODY_TEMPLATE", new object[1]
    {
      (object) (!(str1 == str2) ? str1 : string.Empty)
    });
    string str4 = MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode;
    if (string.IsNullOrEmpty(str4))
    {
      string configOkyakusamaCode = GameUtility.Config_OkyakusamaCode;
      if (!string.IsNullOrEmpty(configOkyakusamaCode))
        str4 = configOkyakusamaCode;
    }
    string name = MonoSingleton<GameManager>.Instance.Player.Name;
    string bundleVersion = gu3.Device.Application.GetBundleVersion();
    string str5 = AssetManager.AssetRevision.ToString();
    string deviceModel = SystemInfo.deviceModel;
    string operatingSystem = SystemInfo.operatingSystem;
    string body = str3 + LocalizedText.Get("contact.CONTACT_PLAYER_DATA", (object) str4, (object) bundleVersion, (object) str5, (object) name, (object) deviceModel, (object) operatingSystem);
    MailerUtility.Launch(mailto, subject2, body);
  }

  public enum SubjectType : byte
  {
    DataRestore = 1,
    BuyCoin = 2,
    BugReport = 3,
    FgGID = 4,
    CommentRequest = 5,
    Other = 6,
  }
}
