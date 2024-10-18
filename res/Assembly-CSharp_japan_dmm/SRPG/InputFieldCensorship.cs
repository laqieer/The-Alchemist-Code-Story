// Decompiled with JetBrains decompiler
// Type: SRPG.InputFieldCensorship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class InputFieldCensorship : SRPG_InputField
  {
    public bool CheckCharacterLimitAtEnded;
    public int CheckCharacterLimitAtEndedCount;

    public string text
    {
      get => base.text;
      set
      {
        if (this.CheckCharacterLimitAtEnded && value.Length > this.CheckCharacterLimitAtEndedCount)
          base.text = value.Substring(0, this.CheckCharacterLimitAtEndedCount);
        else
          base.text = value;
      }
    }

    protected virtual void Start()
    {
      InputFieldCensorship inputFieldCensorship = this;
      // ISSUE: method pointer
      inputFieldCensorship.onValidateInput = (InputField.OnValidateInput) System.Delegate.Combine((System.Delegate) inputFieldCensorship.onValidateInput, (System.Delegate) new InputField.OnValidateInput((object) this, __methodptr(MyValidate)));
    }

    private char MyValidate(string input, int charIndex, char addedChar)
    {
      GameSettings instance = GameSettings.Instance;
      return UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || Array.IndexOf<char>(instance.ValidInputChars, addedChar) < 0 ? char.MinValue : addedChar;
    }

    public void EndEdit(string text)
    {
      if (text.Length > this.characterLimit)
        text = text.Substring(0, this.characterLimit);
      this.text = text;
    }

    [Serializable]
    public class ValidCodeSegment
    {
      public int Start;
      public int End;
    }
  }
}
