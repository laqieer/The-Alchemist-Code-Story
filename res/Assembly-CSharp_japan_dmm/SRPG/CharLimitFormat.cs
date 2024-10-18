// Decompiled with JetBrains decompiler
// Type: SRPG.CharLimitFormat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class CharLimitFormat : MonoBehaviour
  {
    private readonly string SEPARATOR_STRING = "  ";
    [SerializeField]
    private bool display_brackets;
    [SerializeField]
    private bool force_override_limit;
    [SerializeField]
    private int force_override_limit_value;
    [SerializeField]
    private bool append_char_type;
    [SerializeField]
    private CharLimitFormat.EditType edit_type;
    [SerializeField]
    private CharLimitFormat.FormatType format_type;
    [SerializeField]
    private InputField input_field;
    [SerializeField]
    private bool check_update;
    private bool is_finish_edit;
    private Text text;

    private void Awake()
    {
      this.text = ((Component) this).GetComponent<Text>();
      if (this.check_update)
        return;
      this.Edit();
    }

    private void Update()
    {
      if (!this.check_update)
        return;
      this.EditForUpdate();
    }

    private void EditForUpdate()
    {
      if (this.is_finish_edit || this.text.text.StartsWith("sys."))
        return;
      this.Edit();
    }

    private void Edit()
    {
      if (Object.op_Equality((Object) this.text, (Object) null) || !this.force_override_limit && Object.op_Equality((Object) this.input_field, (Object) null))
        return;
      string stringFormat = this.GetStringFormat(this.format_type);
      string empty = string.Empty;
      int num = !this.force_override_limit ? this.input_field.characterLimit : this.force_override_limit_value;
      if (num > 0)
        empty = num.ToString();
      string str1 = string.Empty;
      if (this.append_char_type && Object.op_Inequality((Object) this.input_field, (Object) null))
        str1 = this.GetCharTypeText(this.input_field);
      string str2 = string.Format(stringFormat, (object) empty, (object) str1);
      if (this.display_brackets)
        str2 = "(" + str2 + ")";
      switch (this.edit_type)
      {
        case CharLimitFormat.EditType.Append:
          this.text.text += str2;
          break;
        case CharLimitFormat.EditType.Replace:
          this.text.text = str2;
          break;
      }
      this.is_finish_edit = true;
    }

    private string GetStringFormat(CharLimitFormat.FormatType _format_type)
    {
      switch (_format_type)
      {
        case CharLimitFormat.FormatType.Simple:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_SIMPLE");
        case CharLimitFormat.FormatType.Append_Saidai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_SAIDAI");
        case CharLimitFormat.FormatType.Append_Inai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_INAI");
        case CharLimitFormat.FormatType.Navi_Saidai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_NAVI_SAIDAI");
        default:
          return string.Empty;
      }
    }

    private string GetCharTypeText(InputField _input_field)
    {
      string separatorString = this.SEPARATOR_STRING;
      InputField.ContentType contentType = _input_field.contentType;
      switch (contentType - 2)
      {
        case 0:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER");
        case 2:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC");
        default:
          return contentType == 9 ? this.GetCharTypeTextByTypeCustom(_input_field) : string.Empty;
      }
    }

    private string GetCharTypeTextByTypeCustom(InputField _input_field)
    {
      string separatorString = this.SEPARATOR_STRING;
      InputField.CharacterValidation characterValidation = _input_field.characterValidation;
      if (characterValidation == 1)
        return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER");
      return characterValidation == 3 ? separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC") : string.Empty;
    }

    private enum EditType
    {
      Append,
      Replace,
    }

    private enum FormatType
    {
      Simple,
      Append_Saidai,
      Append_Inai,
      Navi_Saidai,
    }
  }
}
