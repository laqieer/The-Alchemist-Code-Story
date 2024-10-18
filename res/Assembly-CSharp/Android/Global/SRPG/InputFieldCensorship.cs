// Decompiled with JetBrains decompiler
// Type: SRPG.InputFieldCensorship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

namespace SRPG
{
  public class InputFieldCensorship : SRPG_InputField
  {
    protected override void Start()
    {
      InputFieldCensorship inputFieldCensorship = this;
      inputFieldCensorship.onValidateInput = inputFieldCensorship.onValidateInput + new InputField.OnValidateInput(this.MyValidate);
    }

    private char MyValidate(string input, int charIndex, char addedChar)
    {
      GameSettings instance = GameSettings.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || Array.IndexOf<char>(instance.ValidInputChars, addedChar) < 0 || this.characterValidation == InputField.CharacterValidation.Name && addedChar == ' ')
        return char.MinValue;
      return addedChar;
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
