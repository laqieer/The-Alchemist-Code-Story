// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterLimitButton
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
  [AddComponentMenu("CharacterLimitButton")]
  public class CharacterLimitButton : MonoBehaviour
  {
    [SerializeField]
    public CharacterLimitButton.InputfieldSet[] target_list;
    [SerializeField]
    public Button target_button;

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.target_button, (UnityEngine.Object) null) || this.target_list == null)
        return;
      foreach (CharacterLimitButton.InputfieldSet target in this.target_list)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) target.input, (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent<string>) target.input.onValueChanged).AddListener(new UnityAction<string>((object) this, __methodptr(OnInputFieldChange)));
          this.OnInputFieldChange(string.Empty);
        }
      }
    }

    public void OnInputFieldChange(string value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.target_button, (UnityEngine.Object) null))
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
      ((Selectable) this.target_button).interactable = flag;
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
