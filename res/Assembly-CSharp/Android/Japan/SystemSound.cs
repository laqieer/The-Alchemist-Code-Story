// Decompiled with JetBrains decompiler
// Type: SystemSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Audio/System Sound")]
public class SystemSound : MonoBehaviour
{
  public SystemSound.ECue Cue;

  public static void Play(SystemSound.ECue cue)
  {
    string cueID;
    switch (cue)
    {
      case SystemSound.ECue.Tap:
        cueID = SoundSettings.Current.Tap;
        break;
      case SystemSound.ECue.OK:
        cueID = SoundSettings.Current.OK;
        break;
      case SystemSound.ECue.Cancel:
        cueID = SoundSettings.Current.Cancel;
        break;
      case SystemSound.ECue.Select:
        cueID = SoundSettings.Current.Select;
        break;
      case SystemSound.ECue.Buzzer:
        cueID = SoundSettings.Current.Buzzer;
        break;
      case SystemSound.ECue.Swipe:
        cueID = SoundSettings.Current.Swipe;
        break;
      case SystemSound.ECue.ScrollList:
        cueID = SoundSettings.Current.ScrollList;
        break;
      case SystemSound.ECue.WindowPop:
        cueID = SoundSettings.Current.WindowPop;
        break;
      case SystemSound.ECue.WindowClose:
        cueID = SoundSettings.Current.WindowClose;
        break;
      default:
        return;
    }
    MonoSingleton<MySound>.Instance.PlaySEOneShot(cueID, 0.0f);
  }

  public void Play()
  {
    SystemSound.Play(this.Cue);
  }

  public void PlayOnToggleValueChangedToTrue(bool value)
  {
    if (!value)
      return;
    Toggle component = this.gameObject.GetComponent<Toggle>();
    if (!((Object) component != (Object) null) || component.isOn)
      return;
    SystemSound.Play(this.Cue);
  }

  public enum ECue
  {
    Tap,
    OK,
    Cancel,
    Select,
    Buzzer,
    Swipe,
    ScrollList,
    WindowPop,
    WindowClose,
  }
}
