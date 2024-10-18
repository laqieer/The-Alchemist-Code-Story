// Decompiled with JetBrains decompiler
// Type: SRPG.InputFieldCensorship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

namespace SRPG
{
  public class InputFieldCensorship : SRPG_InputField
  {
    public bool CheckCharacterLimitAtEnded;
    public int CheckCharacterLimitAtEndedCount;

    public new string text
    {
      get
      {
        return base.text;
      }
      set
      {
        if (this.CheckCharacterLimitAtEnded && value.Length > this.CheckCharacterLimitAtEndedCount)
          base.text = value.Substring(0, this.CheckCharacterLimitAtEndedCount);
        else
          base.text = value;
      }
    }

    protected override void Start()
    {
      InputFieldCensorship inputFieldCensorship = this;
      inputFieldCensorship.onValidateInput = inputFieldCensorship.onValidateInput + new InputField.OnValidateInput(this.MyValidate);
    }

    private char MyValidate(string input, int charIndex, char addedChar)
    {
      GameSettings instance = GameSettings.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null || Array.IndexOf<char>(instance.ValidInputChars, addedChar) < 0)
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
