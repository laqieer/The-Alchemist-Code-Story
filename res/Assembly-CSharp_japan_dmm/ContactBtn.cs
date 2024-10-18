// Decompiled with JetBrains decompiler
// Type: ContactBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ContactBtn : MonoBehaviour
{
  public ContactBtn.SubjectType Subject = ContactBtn.SubjectType.DataRestore;

  private void Start()
  {
    Button component = ((Component) this).gameObject.GetComponent<Button>();
    if (Object.op_Equality((Object) component, (Object) null))
    {
      Debug.Log((object) "@@@[Menu]Button is Null Object!");
    }
    else
    {
      // ISSUE: method pointer
      ((UnityEvent) component.onClick).AddListener(new UnityAction((object) this, __methodptr(OpenMailer)));
    }
  }

  private void OpenMailer()
  {
    string mailto = LocalizedText.Get("contact.CONTACT_ADDRESS");
    string str1 = MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode;
    if (string.IsNullOrEmpty(str1))
    {
      string configOkyakusamaCode = GameUtility.Config_OkyakusamaCode;
      if (!string.IsNullOrEmpty(configOkyakusamaCode))
        str1 = configOkyakusamaCode;
    }
    int subject1 = (int) this.Subject;
    string subject2 = string.Format(LocalizedText.Get("contact.CONTACT_SUBJECT_" + subject1.ToString("d2")), (object) str1);
    if (string.IsNullOrEmpty(subject2))
      subject2 = LocalizedText.Get("contact.CONTACT_SUBJECT");
    string str2 = LocalizedText.Get("contact.CONTACT_BODY_OPTION_" + subject1.ToString("d2"));
    string str3 = "CONTACT_BODY_OPTION_" + subject1.ToString("d2");
    string str4 = LocalizedText.Get("contact.CONTACT_BODY_TEMPLATE", (object) (!(str2 == str3) ? str2 : string.Empty));
    string name = MonoSingleton<GameManager>.Instance.Player.Name;
    string version = Application.version;
    string str5 = AssetManager.AssetRevision.ToString();
    string str6 = SystemInfo.deviceModel + " / " + SystemInfo.processorType + " / " + (object) SystemInfo.systemMemorySize;
    string operatingSystem = SystemInfo.operatingSystem;
    string body = str4 + LocalizedText.Get("contact.CONTACT_PLAYER_DATA", (object) str1, (object) version, (object) str5, (object) name, (object) str6, (object) operatingSystem);
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
