// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_InputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/InputField (SRPG)")]
  public class SRPG_InputField : InputField
  {
    private static bool NowInput = false;
    private static readonly char[] Separators = new char[6]{ ' ', '.', ',', '\t', '\r', '\n' };
    private UnityEngine.Event m_EventWork = new UnityEngine.Event();
    private bool m_IsPointerOutofRange;
    private CanvasRenderer m_Renderer;
    private RectTransform m_RectTrans;

    public static bool IsFocus
    {
      get
      {
        return SRPG_InputField.NowInput;
      }
    }

    public static void ResetInput()
    {
      SRPG_InputField.NowInput = false;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
      base.OnPointerEnter(eventData);
      this.m_IsPointerOutofRange = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
      base.OnPointerExit(eventData);
      this.m_IsPointerOutofRange = true;
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
      if (!this.isFocused)
        return;
      if (this.m_IsPointerOutofRange && this.GetMouseButtonDown())
      {
        UnityEngine.Event outEvent = new UnityEngine.Event();
        do
          ;
        while (UnityEngine.Event.PopEvent(outEvent));
        this.UpdateLabel();
        eventData.Use();
      }
      else
      {
        bool flag = false;
        while (UnityEngine.Event.PopEvent(this.m_EventWork))
        {
          if (this.m_EventWork.rawType == UnityEngine.EventType.KeyDown)
          {
            flag = true;
            if (this.KeyPressedForWin(this.m_EventWork) == InputField.EditState.Finish)
            {
              this.DeactivateInputField();
              break;
            }
          }
          else if (this.m_EventWork.rawType == UnityEngine.EventType.KeyUp && this.m_EventWork.keyCode == KeyCode.Backspace)
            flag = true;
          switch (this.m_EventWork.type)
          {
            case UnityEngine.EventType.ValidateCommand:
            case UnityEngine.EventType.ExecuteCommand:
              switch (this.m_EventWork.commandName)
              {
                case "SelectAll":
                  this.SelectAll();
                  flag = true;
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
        if (flag)
          this.UpdateLabel();
        eventData.Use();
      }
    }

    private bool GetMouseButtonDown()
    {
      if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        return Input.GetMouseButtonDown(2);
      return true;
    }

    public override void OnSelect(BaseEventData eventData)
    {
      base.OnSelect(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
      base.OnDeselect(eventData);
    }

    public virtual void ForceSetText(string text)
    {
      if (this.characterLimit > 0 && text.Length > this.characterLimit)
        text = text.Substring(0, this.characterLimit);
      this.m_Text = text;
      if (this.m_Keyboard != null)
        this.m_Keyboard.text = this.m_Text;
      if (this.m_CaretPosition > this.m_Text.Length)
        this.m_CaretPosition = this.m_CaretSelectPosition = this.m_Text.Length;
      if (this.onValueChanged != null)
        this.onValueChanged.Invoke(text);
      this.UpdateLabel();
    }

    protected override void OnDestroy()
    {
      if (this.m_Keyboard != null)
        this.m_Keyboard.active = false;
      base.OnDestroy();
    }

    private static string clipboard
    {
      get
      {
        return GUIUtility.systemCopyBuffer;
      }
      set
      {
        GUIUtility.systemCopyBuffer = value;
      }
    }

    private void DeleteForWin()
    {
      if (this.readOnly || this.caretPositionInternal == this.caretSelectPositionInternal)
        return;
      int num1 = this.caretPositionInternal - Input.compositionString.Length;
      int num2 = this.caretSelectPositionInternal - Input.compositionString.Length;
      if (num1 < num2)
      {
        this.m_Text = this.text.Substring(0, num1) + this.text.Substring(num2, this.text.Length - num2);
        this.caretSelectPositionInternal = this.caretPositionInternal;
      }
      else
      {
        this.m_Text = this.text.Substring(0, num2) + this.text.Substring(num1, this.text.Length - num1);
        this.caretPositionInternal = this.caretSelectPositionInternal;
      }
    }

    private void SendOnValueChangedAndUpdateLabelForWin()
    {
      if (this.onValueChanged != null)
        this.onValueChanged.Invoke(this.text);
      this.UpdateLabel();
    }

    private string GetSelectedStringForWin()
    {
      if (this.caretPositionInternal == this.caretSelectPositionInternal)
        return string.Empty;
      int startIndex = this.caretPositionInternal;
      int num1 = this.caretSelectPositionInternal;
      if (startIndex > num1)
      {
        int num2 = startIndex;
        startIndex = num1;
        num1 = num2;
      }
      return this.text.Substring(startIndex, num1 - startIndex);
    }

    private int FindtPrevWordBegin()
    {
      if (this.caretSelectPositionInternal - 2 < 0)
        return 0;
      int num = this.text.LastIndexOfAny(SRPG_InputField.Separators, this.caretSelectPositionInternal - 2);
      return num != -1 ? num + 1 : 0;
    }

    private int FindtNextWordBeginForWin()
    {
      if (this.caretSelectPositionInternal + 1 >= this.text.Length)
        return this.text.Length;
      int num = this.text.IndexOfAny(SRPG_InputField.Separators, this.caretSelectPositionInternal + 1);
      return num != -1 ? num + 1 : this.text.Length;
    }

    private void MoveLeft(bool shift, bool ctrl)
    {
      if (this.caretPositionInternal != this.caretSelectPositionInternal && !shift)
      {
        int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
        this.caretSelectPositionInternal = num;
        this.caretPositionInternal = num;
      }
      else
      {
        int num1 = !ctrl ? this.caretSelectPositionInternal - 1 : this.FindtPrevWordBegin();
        if (shift)
        {
          this.caretSelectPositionInternal = num1;
        }
        else
        {
          int num2 = num1;
          this.caretPositionInternal = num2;
          this.caretSelectPositionInternal = num2;
        }
      }
    }

    private void MoveRight(bool shift, bool ctrl)
    {
      if (this.caretPositionInternal != this.caretSelectPositionInternal && !shift)
      {
        int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
        this.caretSelectPositionInternal = num;
        this.caretPositionInternal = num;
      }
      else
      {
        int num1 = !ctrl ? this.caretSelectPositionInternal + 1 : this.FindtNextWordBeginForWin();
        if (shift)
        {
          this.caretSelectPositionInternal = num1;
        }
        else
        {
          int num2 = num1;
          this.caretPositionInternal = num2;
          this.caretSelectPositionInternal = num2;
        }
      }
    }

    private static int GetLineEndPositionForWin(TextGenerator gen, int line)
    {
      line = Mathf.Max(line, 0);
      if (line + 1 < gen.lines.Count)
        return gen.lines[line + 1].startCharIdx - 1;
      return gen.characterCountVisible;
    }

    private int DetermineCharacterLineForWin(int charPos, TextGenerator generator)
    {
      for (int index = 0; index < generator.lineCount - 1; ++index)
      {
        if (generator.lines[index + 1].startCharIdx > charPos)
          return index;
      }
      return generator.lineCount - 1;
    }

    private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
    {
      if (originalPos > this.cachedInputTextGenerator.characterCountVisible)
        return 0;
      UICharInfo character = this.cachedInputTextGenerator.characters[originalPos];
      int characterLineForWin = this.DetermineCharacterLineForWin(originalPos, this.cachedInputTextGenerator);
      if (characterLineForWin <= 0)
      {
        if (goToFirstChar)
          return 0;
        return originalPos;
      }
      int num = this.cachedInputTextGenerator.lines[characterLineForWin].startCharIdx - 1;
      for (int startCharIdx = this.cachedInputTextGenerator.lines[characterLineForWin - 1].startCharIdx; startCharIdx < num; ++startCharIdx)
      {
        if ((double) this.cachedInputTextGenerator.characters[startCharIdx].cursorPos.x >= (double) character.cursorPos.x)
          return startCharIdx;
      }
      return num;
    }

    private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
    {
      if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
        return this.text.Length;
      UICharInfo character = this.cachedInputTextGenerator.characters[originalPos];
      int characterLineForWin = this.DetermineCharacterLineForWin(originalPos, this.cachedInputTextGenerator);
      if (characterLineForWin + 1 >= this.cachedInputTextGenerator.lineCount)
      {
        if (goToLastChar)
          return this.text.Length;
        return originalPos;
      }
      int endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(this.cachedInputTextGenerator, characterLineForWin + 1);
      for (int startCharIdx = this.cachedInputTextGenerator.lines[characterLineForWin + 1].startCharIdx; startCharIdx < endPositionForWin; ++startCharIdx)
      {
        if ((double) this.cachedInputTextGenerator.characters[startCharIdx].cursorPos.x >= (double) character.cursorPos.x)
          return startCharIdx;
      }
      return endPositionForWin;
    }

    private void MoveDown(bool shift, bool goToLastChar)
    {
      if (this.caretPositionInternal != this.caretSelectPositionInternal && !shift)
      {
        int num = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal);
        this.caretSelectPositionInternal = num;
        this.caretPositionInternal = num;
      }
      int num1 = !this.multiLine ? this.text.Length : this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar);
      if (shift)
      {
        this.caretSelectPositionInternal = num1;
      }
      else
      {
        int num2 = num1;
        this.caretSelectPositionInternal = num2;
        this.caretPositionInternal = num2;
      }
    }

    private void MoveUp(bool shift, bool goToFirstChar)
    {
      if (this.caretPositionInternal != this.caretSelectPositionInternal && !shift)
      {
        int num = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal);
        this.caretSelectPositionInternal = num;
        this.caretPositionInternal = num;
      }
      int num1 = !this.multiLine ? 0 : this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar);
      if (shift)
      {
        this.caretSelectPositionInternal = num1;
      }
      else
      {
        int num2 = num1;
        this.caretPositionInternal = num2;
        this.caretSelectPositionInternal = num2;
      }
    }

    private void ForwardSpaceForWin()
    {
      if (this.readOnly)
        return;
      if (this.caretPositionInternal != this.caretSelectPositionInternal)
      {
        this.DeleteForWin();
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
      else
      {
        if (this.caretPositionInternal >= this.text.Length)
          return;
        this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
    }

    private void BackspaceForWin()
    {
      if (this.readOnly)
        return;
      if (this.caretPositionInternal != this.caretSelectPositionInternal)
      {
        this.DeleteForWin();
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
      else
      {
        if (this.caretPositionInternal <= 0)
          return;
        this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
        this.caretSelectPositionInternal = --this.caretPositionInternal;
        this.SendOnValueChangedAndUpdateLabelForWin();
      }
    }

    private bool IsValidCharForWin(char c)
    {
      switch (c)
      {
        case '\t':
        case '\n':
          return true;
        case '\x007F':
          return false;
        default:
          return this.m_TextComponent.font.HasCharacter(c);
      }
    }

    private bool InPlaceEditingForWin()
    {
      return !TouchScreenKeyboard.isSupported;
    }

    private void InsertForWin(char c)
    {
      if (this.readOnly)
        return;
      string str = c.ToString();
      this.DeleteForWin();
      if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
        return;
      this.m_Text = this.text.Insert(this.m_CaretPosition, str);
      this.caretSelectPositionInternal = (this.caretPositionInternal += str.Length);
      if (this.onValueChanged == null)
        return;
      this.onValueChanged.Invoke(this.text);
    }

    protected override void Append(char input)
    {
      if (this.readOnly || !this.InPlaceEditingForWin())
        return;
      if (this.onValidateInput != null)
        input = this.onValidateInput(this.text, this.caretPositionInternal, input);
      else if (this.characterValidation != InputField.CharacterValidation.None)
        input = this.Validate(this.text, this.caretPositionInternal, input);
      if (input == char.MinValue)
        return;
      this.InsertForWin(input);
    }

    protected InputField.EditState KeyPressedForWin(UnityEngine.Event evt)
    {
      EventModifiers modifiers = evt.modifiers;
      RuntimePlatform platform = Application.platform;
      bool ctrl = platform != RuntimePlatform.OSXEditor && platform != RuntimePlatform.OSXPlayer ? (modifiers & EventModifiers.Control) != EventModifiers.None : (modifiers & EventModifiers.Command) != EventModifiers.None;
      bool shift = (modifiers & EventModifiers.Shift) != EventModifiers.None;
      bool flag1 = (modifiers & EventModifiers.Alt) != EventModifiers.None;
      bool flag2 = ctrl && !flag1 && !shift;
      KeyCode keyCode = evt.keyCode;
      switch (keyCode)
      {
        case KeyCode.KeypadEnter:
label_23:
          if (this.lineType != InputField.LineType.MultiLineNewline)
            return InputField.EditState.Finish;
          break;
        case KeyCode.UpArrow:
          this.MoveUp(shift, true);
          return InputField.EditState.Continue;
        case KeyCode.DownArrow:
          this.MoveDown(shift, true);
          return InputField.EditState.Continue;
        case KeyCode.RightArrow:
          this.MoveRight(shift, ctrl);
          return InputField.EditState.Continue;
        case KeyCode.LeftArrow:
          this.MoveLeft(shift, ctrl);
          return InputField.EditState.Continue;
        case KeyCode.Home:
          this.MoveTextStart(shift);
          return InputField.EditState.Continue;
        case KeyCode.End:
          this.MoveTextEnd(shift);
          return InputField.EditState.Continue;
        default:
          switch (keyCode - 97)
          {
            case KeyCode.None:
              if (flag2)
              {
                this.SelectAll();
                return InputField.EditState.Continue;
              }
              break;
            case (KeyCode) 2:
              if (flag2)
              {
                SRPG_InputField.clipboard = this.inputType == InputField.InputType.Password ? string.Empty : this.GetSelectedStringForWin();
                return InputField.EditState.Continue;
              }
              break;
            default:
              switch (keyCode - 118)
              {
                case KeyCode.None:
                  if (flag2)
                  {
                    this.Append(SRPG_InputField.clipboard);
                    return InputField.EditState.Continue;
                  }
                  break;
                case (KeyCode) 2:
                  if (flag2)
                  {
                    SRPG_InputField.clipboard = this.inputType == InputField.InputType.Password ? string.Empty : this.GetSelectedStringForWin();
                    this.DeleteForWin();
                    this.SendOnValueChangedAndUpdateLabelForWin();
                    return InputField.EditState.Continue;
                  }
                  break;
                default:
                  if (keyCode != KeyCode.Backspace)
                  {
                    if (keyCode != KeyCode.Return)
                    {
                      if (keyCode == KeyCode.Escape)
                        return this.KeyPressed(evt);
                      if (keyCode == KeyCode.Delete)
                      {
                        this.ForwardSpaceForWin();
                        return InputField.EditState.Continue;
                      }
                      break;
                    }
                    goto label_23;
                  }
                  else
                  {
                    this.BackspaceForWin();
                    return InputField.EditState.Continue;
                  }
              }
          }
      }
      char ch = evt.character;
      if (!this.multiLine && (ch == '\t' || ch == '\r' || ch == '\n'))
        return InputField.EditState.Continue;
      if (ch == '\r' || ch == '\x0003')
        ch = '\n';
      if (this.IsValidCharForWin(ch))
        this.Append(ch);
      if (ch == char.MinValue && Input.compositionString.Length > 0)
        this.UpdateLabel();
      return InputField.EditState.Continue;
    }

    public override void Rebuild(CanvasUpdate update)
    {
      if (update != CanvasUpdate.LatePreRender)
        return;
      this.UpdateGeometryForWin();
    }

    private void UpdateGeometryForWin()
    {
      if (!this.shouldHideMobileInput)
        return;
      if ((UnityEngine.Object) this.m_Renderer == (UnityEngine.Object) null && (UnityEngine.Object) this.m_TextComponent != (UnityEngine.Object) null)
      {
        GameObject gameObject = new GameObject(this.transform.name + " Input Caret");
        gameObject.hideFlags = HideFlags.DontSave;
        gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
        gameObject.transform.SetAsFirstSibling();
        gameObject.layer = this.gameObject.layer;
        this.m_RectTrans = gameObject.AddComponent<RectTransform>();
        this.m_Renderer = gameObject.AddComponent<CanvasRenderer>();
        this.m_Renderer.SetMaterial(Graphic.defaultGraphicMaterial, (Texture) Texture2D.whiteTexture);
        gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
        this.AssignPositioningIfNeeded();
      }
      if ((UnityEngine.Object) this.m_Renderer == (UnityEngine.Object) null)
        return;
      this.OnFillVBO(this.mesh);
      this.m_Renderer.SetMesh(this.mesh);
    }

    private void AssignPositioningIfNeeded()
    {
      if (!((UnityEngine.Object) this.m_TextComponent != (UnityEngine.Object) null) || !((UnityEngine.Object) this.m_RectTrans != (UnityEngine.Object) null) || !(this.m_RectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition) && !(this.m_RectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation) && (!(this.m_RectTrans.localScale != this.m_TextComponent.rectTransform.localScale) && !(this.m_RectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin)) && (!(this.m_RectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax) && !(this.m_RectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition) && (!(this.m_RectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta) && !(this.m_RectTrans.pivot != this.m_TextComponent.rectTransform.pivot))))
        return;
      this.m_RectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
      this.m_RectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
      this.m_RectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
      this.m_RectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
      this.m_RectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
      this.m_RectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
      this.m_RectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
      this.m_RectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
    }

    private void OnFillVBO(Mesh vbo)
    {
      using (VertexHelper vbo1 = new VertexHelper())
      {
        if (!this.isFocused)
        {
          vbo1.FillMesh(vbo);
        }
        else
        {
          Rect rect = this.m_TextComponent.rectTransform.rect;
          Vector2 size = rect.size;
          Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_TextComponent.alignment);
          Vector2 zero = Vector2.zero;
          zero.x = Mathf.Lerp(rect.xMin, rect.xMax, textAnchorPivot.x);
          zero.y = Mathf.Lerp(rect.yMin, rect.yMax, textAnchorPivot.y);
          Vector2 roundingOffset = this.m_TextComponent.PixelAdjustPoint(zero) - zero + Vector2.Scale(size, textAnchorPivot);
          roundingOffset.x -= Mathf.Floor(0.5f + roundingOffset.x);
          roundingOffset.y -= Mathf.Floor(0.5f + roundingOffset.y);
          if (this.caretPositionInternal == this.caretSelectPositionInternal)
            this.GenerateCaret(vbo1, roundingOffset);
          else
            this.GenerateHightlight(vbo1, roundingOffset);
          vbo1.FillMesh(vbo);
        }
      }
    }

    private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
    {
      if (!this.m_CaretVisible)
        return;
      if (this.m_CursorVerts == null)
        this.CreateCursorVerts();
      float caretWidth = (float) this.caretWidth;
      int charPos = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
      TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
      if (cachedTextGenerator == null || cachedTextGenerator.lineCount == 0)
        return;
      Vector2 zero = Vector2.zero;
      if (charPos < cachedTextGenerator.characters.Count)
      {
        UICharInfo character = cachedTextGenerator.characters[charPos];
        zero.x = character.cursorPos.x;
      }
      zero.x /= this.m_TextComponent.pixelsPerUnit;
      if ((double) zero.x > (double) this.m_TextComponent.rectTransform.rect.xMax)
        zero.x = this.m_TextComponent.rectTransform.rect.xMax;
      int characterLineForWin = this.DetermineCharacterLineForWin(charPos, cachedTextGenerator);
      zero.y = cachedTextGenerator.lines[characterLineForWin].topY / this.m_TextComponent.pixelsPerUnit;
      float num = (float) cachedTextGenerator.lines[characterLineForWin].height / this.m_TextComponent.pixelsPerUnit;
      for (int index = 0; index < this.m_CursorVerts.Length; ++index)
        this.m_CursorVerts[index].color = (Color32) this.caretColor;
      this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num, 0.0f);
      this.m_CursorVerts[1].position = new Vector3(zero.x + caretWidth, zero.y - num, 0.0f);
      this.m_CursorVerts[2].position = new Vector3(zero.x + caretWidth, zero.y, 0.0f);
      this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0.0f);
      if (roundingOffset != Vector2.zero)
      {
        for (int index = 0; index < this.m_CursorVerts.Length; ++index)
        {
          UIVertex cursorVert = this.m_CursorVerts[index];
          cursorVert.position.x += roundingOffset.x;
          cursorVert.position.y += roundingOffset.y;
        }
      }
      vbo.AddUIVertexQuad(this.m_CursorVerts);
      int height = Screen.height;
      zero.y = (float) height - zero.y;
      Input.compositionCursorPos = zero;
    }

    private void CreateCursorVerts()
    {
      this.m_CursorVerts = new UIVertex[4];
      for (int index = 0; index < this.m_CursorVerts.Length; ++index)
      {
        this.m_CursorVerts[index] = UIVertex.simpleVert;
        this.m_CursorVerts[index].uv0 = Vector2.zero;
      }
    }

    private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
    {
      int num1 = this.caretPositionInternal;
      int num2 = this.caretSelectPositionInternal;
      if (this.m_Text.Length == Mathf.Abs(this.m_CaretPosition - this.m_CaretSelectPosition))
      {
        int length = Input.compositionString.Length;
        num1 -= length;
        num2 -= length;
      }
      else if (this.m_CaretPosition > this.m_CaretSelectPosition)
      {
        num1 = this.m_CaretSelectPosition;
        num2 = this.m_CaretPosition;
      }
      int charPos = Mathf.Max(0, num1 - this.m_DrawStart);
      int num3 = Mathf.Max(0, num2 - this.m_DrawStart);
      if (charPos > num3)
      {
        int num4 = charPos;
        charPos = num3;
        num3 = num4;
      }
      int num5 = num3 - 1;
      TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
      if (cachedTextGenerator.lineCount <= 0)
        return;
      int characterLineForWin = this.DetermineCharacterLineForWin(charPos, cachedTextGenerator);
      int endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
      UIVertex simpleVert = UIVertex.simpleVert;
      simpleVert.uv0 = Vector2.zero;
      simpleVert.color = (Color32) this.selectionColor;
      for (int index = charPos; index <= num5 && index < cachedTextGenerator.characterCount; ++index)
      {
        if (index == endPositionForWin || index == num5)
        {
          UICharInfo character1 = cachedTextGenerator.characters[charPos];
          UICharInfo character2 = cachedTextGenerator.characters[index];
          Vector2 vector2_1 = new Vector2(character1.cursorPos.x / this.m_TextComponent.pixelsPerUnit, cachedTextGenerator.lines[characterLineForWin].topY / this.m_TextComponent.pixelsPerUnit);
          Vector2 vector2_2 = new Vector2((character2.cursorPos.x + character2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector2_1.y - (float) cachedTextGenerator.lines[characterLineForWin].height / this.m_TextComponent.pixelsPerUnit);
          if ((double) vector2_2.x > (double) this.m_TextComponent.rectTransform.rect.xMax || (double) vector2_2.x < (double) this.m_TextComponent.rectTransform.rect.xMin)
            vector2_2.x = this.m_TextComponent.rectTransform.rect.xMax;
          int currentVertCount = vbo.currentVertCount;
          simpleVert.position = new Vector3(vector2_1.x, vector2_2.y, 0.0f) + (Vector3) roundingOffset;
          vbo.AddVert(simpleVert);
          simpleVert.position = new Vector3(vector2_2.x, vector2_2.y, 0.0f) + (Vector3) roundingOffset;
          vbo.AddVert(simpleVert);
          simpleVert.position = new Vector3(vector2_2.x, vector2_1.y, 0.0f) + (Vector3) roundingOffset;
          vbo.AddVert(simpleVert);
          simpleVert.position = new Vector3(vector2_1.x, vector2_1.y, 0.0f) + (Vector3) roundingOffset;
          vbo.AddVert(simpleVert);
          vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
          vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
          charPos = index + 1;
          ++characterLineForWin;
          endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
        }
      }
    }
  }
}
