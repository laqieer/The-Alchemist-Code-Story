// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLimitButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("CharacterLimitButton")]
  public class CharacterLimitButton : MonoBehaviour
  {
    [SerializeField]
    public CharacterLimitButton.InputfieldSet[] target_list;
    [SerializeField]
    public Button target_button;

    private void Start()
    {
      if ((UnityEngine.Object) this.target_button == (UnityEngine.Object) null || this.target_list == null)
        return;
      foreach (CharacterLimitButton.InputfieldSet target in this.target_list)
      {
        if ((UnityEngine.Object) target.input != (UnityEngine.Object) null)
        {
          target.input.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldChange));
          this.OnInputFieldChange(string.Empty);
        }
      }
    }

    public void OnInputFieldChange(string value)
    {
      if ((UnityEngine.Object) this.target_button == (UnityEngine.Object) null)
        return;
      bool flag = true;
      foreach (CharacterLimitButton.InputfieldSet target in this.target_list)
      {
        int length = target.input.text.Length;
        if (target.min_length > length || length > target.max_length)
        {
          flag = false;
          break;
        }
      }
      this.target_button.interactable = flag;
    }

    [Serializable]
    public class InputfieldSet
    {
      public InputField input;
      public int min_length;
      public int max_length;
    }
  }
}
