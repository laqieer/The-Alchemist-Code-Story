// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_InputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/InputField (SRPG)")]
  public class SRPG_InputField : InputField
  {
    private bool m_IsPointerOutofRange;
    private Event m_EventWork = new Event();
    private static bool NowInput = false;
    private static readonly char[] Separators = new char[6]
    {
      ' ',
      '.',
      ',',
      '\t',
      '\r',
      '\n'
    };
    private CanvasRenderer m_Renderer;
    private RectTransform m_RectTrans;

    public static bool IsFocus => SRPG_InputField.NowInput;

    public static void ResetInput() => SRPG_InputField.NowInput = false;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerEnter(eventData);
      this.m_IsPointerOutofRange = false;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
      ((Selectable) this).OnPointerExit(eventData);
      this.m_IsPointerOutofRange = true;
    }

    public virtual void OnUpdateSelected(BaseEventData eventData)
    {
      if (!this.isFocused)
        return;
      if (this.m_IsPointerOutofRange && this.GetMouseButtonDown())
      {
        Event @event = new Event();
        do
          ;
        while (Event.PopEvent(@event));
        this.UpdateLabel();
        ((AbstractEventData) eventData).Use();
      }
      else
      {
        bool flag = false;
        while (Event.PopEvent(this.m_EventWork))
        {
          if (this.m_EventWork.rawType == 4)
          {
            flag = true;
            if (this.KeyPressedForWin(this.m_EventWork) == 1)
            {
              this.DeactivateInputField();
              break;
            }
          }
          else if (this.m_EventWork.rawType == 5 && this.m_EventWork.keyCode == 8)
            flag = true;
          EventType type = this.m_EventWork.type;
          if (type == 13 || type == 14)
          {
            switch (this.m_EventWork.commandName)
            {
              case "SelectAll":
                this.SelectAll();
                flag = true;
                continue;
              default:
                continue;
            }
          }
        }
        if (flag)
          this.UpdateLabel();
        ((AbstractEventData) eventData).Use();
      }
    }

    private bool GetMouseButtonDown()
    {
      return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }

    public virtual void OnSelect(BaseEventData eventData) => base.OnSelect(eventData);

    public virtual void OnDeselect(BaseEventData eventData) => base.OnDeselect(eventData);

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
        ((UnityEvent<string>) this.onValueChanged).Invoke(text);
      this.UpdateLabel();
    }

    protected virtual void OnDestroy()
    {
      if (this.m_Keyboard != null)
        this.m_Keyboard.active = false;
      ((UIBehaviour) this).OnDestroy();
    }

    private static string clipboard
    {
      get => GUIUtility.systemCopyBuffer;
      set => GUIUtility.systemCopyBuffer = value;
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
        ((UnityEvent<string>) this.onValueChanged).Invoke(this.text);
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
      return line + 1 < gen.lines.Count ? gen.lines[line + 1].startCharIdx - 1 : gen.characterCountVisible;
    }

    private int DetermineCharacterLineForWin(int charPos, TextGenerator generator)
    {
      for (int characterLineForWin = 0; characterLineForWin < generator.lineCount - 1; ++characterLineForWin)
      {
        if (generator.lines[characterLineForWin + 1].startCharIdx > charPos)
          return characterLineForWin;
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
        return goToFirstChar ? 0 : originalPos;
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
        return goToLastChar ? this.text.Length : originalPos;
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
        case '\u007F':
          return false;
        default:
          return this.m_TextComponent.font.HasCharacter(c);
      }
    }

    private bool InPlaceEditingForWin() => !TouchScreenKeyboard.isSupported;

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
      ((UnityEvent<string>) this.onValueChanged).Invoke(this.text);
    }

    protected virtual void Append(char input)
    {
      if (this.readOnly || !this.InPlaceEditingForWin())
        return;
      if (this.onValidateInput != null)
        input = this.onValidateInput.Invoke(this.text, this.caretPositionInternal, input);
      else if (this.characterValidation != null)
        input = this.Validate(this.text, this.caretPositionInternal, input);
      if (input == char.MinValue)
        return;
      this.InsertForWin(input);
    }

    protected InputField.EditState KeyPressedForWin(Event evt)
    {
      EventModifiers modifiers = evt.modifiers;
      RuntimePlatform platform = Application.platform;
      bool ctrl = platform != null && platform != 1 ? (modifiers & 2) != 0 : (modifiers & 8) != 0;
      bool shift = (modifiers & 1) != 0;
      bool flag1 = (modifiers & 4) != 0;
      bool flag2 = ctrl && !flag1 && !shift;
      KeyCode keyCode = evt.keyCode;
      switch (keyCode - 271)
      {
        case 0:
label_23:
          if (this.lineType != 2)
            return (InputField.EditState) 1;
          break;
        case 2:
          this.MoveUp(shift, true);
          return (InputField.EditState) 0;
        case 3:
          this.MoveDown(shift, true);
          return (InputField.EditState) 0;
        case 4:
          this.MoveRight(shift, ctrl);
          return (InputField.EditState) 0;
        case 5:
          this.MoveLeft(shift, ctrl);
          return (InputField.EditState) 0;
        case 7:
          this.MoveTextStart(shift);
          return (InputField.EditState) 0;
        case 8:
          this.MoveTextEnd(shift);
          return (InputField.EditState) 0;
        default:
          switch (keyCode - 97)
          {
            case 0:
              if (flag2)
              {
                this.SelectAll();
                return (InputField.EditState) 0;
              }
              break;
            case 2:
              if (flag2)
              {
                SRPG_InputField.clipboard = this.inputType == 2 ? string.Empty : this.GetSelectedStringForWin();
                return (InputField.EditState) 0;
              }
              break;
            default:
              switch (keyCode - 118)
              {
                case 0:
                  if (flag2)
                  {
                    this.Append(SRPG_InputField.clipboard);
                    return (InputField.EditState) 0;
                  }
                  break;
                case 2:
                  if (flag2)
                  {
                    SRPG_InputField.clipboard = this.inputType == 2 ? string.Empty : this.GetSelectedStringForWin();
                    this.DeleteForWin();
                    this.SendOnValueChangedAndUpdateLabelForWin();
                    return (InputField.EditState) 0;
                  }
                  break;
                default:
                  if (keyCode != 8)
                  {
                    if (keyCode != 13)
                    {
                      if (keyCode == 27)
                        return this.KeyPressed(evt);
                      if (keyCode == (int) sbyte.MaxValue)
                      {
                        this.ForwardSpaceForWin();
                        return (InputField.EditState) 0;
                      }
                      break;
                    }
                    goto label_23;
                  }
                  else
                  {
                    this.BackspaceForWin();
                    return (InputField.EditState) 0;
                  }
              }
              break;
          }
          break;
      }
      char c = evt.character;
      if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
        return (InputField.EditState) 0;
      if (c == '\r' || c == '\u0003')
        c = '\n';
      if (this.IsValidCharForWin(c))
        base.Append(c);
      if (c == char.MinValue && Input.compositionString.Length > 0)
        this.UpdateLabel();
      return (InputField.EditState) 0;
    }

    public virtual void Rebuild(CanvasUpdate update)
    {
      if (update != 4)
        return;
      this.UpdateGeometryForWin();
    }

    private void UpdateGeometryForWin()
    {
      if (!this.shouldHideMobileInput)
        return;
      if (Object.op_Equality((Object) this.m_Renderer, (Object) null) && Object.op_Inequality((Object) this.m_TextComponent, (Object) null))
      {
        GameObject gameObject = new GameObject(((Object) ((Component) this).transform).name + " Input Caret");
        ((Object) gameObject).hideFlags = (HideFlags) 52;
        gameObject.transform.SetParent(((Component) this.m_TextComponent).transform.parent);
        gameObject.transform.SetAsFirstSibling();
        gameObject.layer = ((Component) this).gameObject.layer;
        this.m_RectTrans = gameObject.AddComponent<RectTransform>();
        this.m_Renderer = gameObject.AddComponent<CanvasRenderer>();
        this.m_Renderer.SetMaterial(Graphic.defaultGraphicMaterial, (Texture) Texture2D.whiteTexture);
        gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
        this.AssignPositioningIfNeeded();
      }
      if (Object.op_Equality((Object) this.m_Renderer, (Object) null))
        return;
      this.OnFillVBO(this.mesh);
      this.m_Renderer.SetMesh(this.mesh);
    }

    private void AssignPositioningIfNeeded()
    {
      if (!Object.op_Inequality((Object) this.m_TextComponent, (Object) null) || !Object.op_Inequality((Object) this.m_RectTrans, (Object) null) || !Vector3.op_Inequality(((Transform) this.m_RectTrans).localPosition, ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localPosition) && !Quaternion.op_Inequality(((Transform) this.m_RectTrans).localRotation, ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localRotation) && !Vector3.op_Inequality(((Transform) this.m_RectTrans).localScale, ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localScale) && !Vector2.op_Inequality(this.m_RectTrans.anchorMin, ((Graphic) this.m_TextComponent).rectTransform.anchorMin) && !Vector2.op_Inequality(this.m_RectTrans.anchorMax, ((Graphic) this.m_TextComponent).rectTransform.anchorMax) && !Vector2.op_Inequality(this.m_RectTrans.anchoredPosition, ((Graphic) this.m_TextComponent).rectTransform.anchoredPosition) && !Vector2.op_Inequality(this.m_RectTrans.sizeDelta, ((Graphic) this.m_TextComponent).rectTransform.sizeDelta) && !Vector2.op_Inequality(this.m_RectTrans.pivot, ((Graphic) this.m_TextComponent).rectTransform.pivot))
        return;
      ((Transform) this.m_RectTrans).localPosition = ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localPosition;
      ((Transform) this.m_RectTrans).localRotation = ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localRotation;
      ((Transform) this.m_RectTrans).localScale = ((Transform) ((Graphic) this.m_TextComponent).rectTransform).localScale;
      this.m_RectTrans.anchorMin = ((Graphic) this.m_TextComponent).rectTransform.anchorMin;
      this.m_RectTrans.anchorMax = ((Graphic) this.m_TextComponent).rectTransform.anchorMax;
      this.m_RectTrans.anchoredPosition = ((Graphic) this.m_TextComponent).rectTransform.anchoredPosition;
      this.m_RectTrans.sizeDelta = ((Graphic) this.m_TextComponent).rectTransform.sizeDelta;
      this.m_RectTrans.pivot = ((Graphic) this.m_TextComponent).rectTransform.pivot;
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
          Rect rect = ((Graphic) this.m_TextComponent).rectTransform.rect;
          Vector2 size = ((Rect) ref rect).size;
          Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_TextComponent.alignment);
          Vector2 zero = Vector2.zero;
          zero.x = Mathf.Lerp(((Rect) ref rect).xMin, ((Rect) ref rect).xMax, textAnchorPivot.x);
          zero.y = Mathf.Lerp(((Rect) ref rect).yMin, ((Rect) ref rect).yMax, textAnchorPivot.y);
          Vector2 roundingOffset = Vector2.op_Addition(Vector2.op_Subtraction(((Graphic) this.m_TextComponent).PixelAdjustPoint(zero), zero), Vector2.Scale(size, textAnchorPivot));
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
      int num1 = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
      TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
      if (cachedTextGenerator == null || cachedTextGenerator.lineCount == 0)
        return;
      Vector2 zero = Vector2.zero;
      if (num1 < cachedTextGenerator.characters.Count)
      {
        UICharInfo character = cachedTextGenerator.characters[num1];
        zero.x = character.cursorPos.x;
      }
      zero.x /= this.m_TextComponent.pixelsPerUnit;
      double x = (double) zero.x;
      Rect rect1 = ((Graphic) this.m_TextComponent).rectTransform.rect;
      double xMax1 = (double) ((Rect) ref rect1).xMax;
      if (x > xMax1)
      {
        ref Vector2 local = ref zero;
        Rect rect2 = ((Graphic) this.m_TextComponent).rectTransform.rect;
        double xMax2 = (double) ((Rect) ref rect2).xMax;
        local.x = (float) xMax2;
      }
      int characterLineForWin = this.DetermineCharacterLineForWin(num1, cachedTextGenerator);
      zero.y = cachedTextGenerator.lines[characterLineForWin].topY / this.m_TextComponent.pixelsPerUnit;
      float num2 = (float) cachedTextGenerator.lines[characterLineForWin].height / this.m_TextComponent.pixelsPerUnit;
      for (int index = 0; index < this.m_CursorVerts.Length; ++index)
        this.m_CursorVerts[index].color = Color32.op_Implicit(this.caretColor);
      this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num2, 0.0f);
      this.m_CursorVerts[1].position = new Vector3(zero.x + caretWidth, zero.y - num2, 0.0f);
      this.m_CursorVerts[2].position = new Vector3(zero.x + caretWidth, zero.y, 0.0f);
      this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0.0f);
      if (Vector2.op_Inequality(roundingOffset, Vector2.zero))
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
      int num3 = Mathf.Max(0, num1 - this.m_DrawStart);
      int num4 = Mathf.Max(0, num2 - this.m_DrawStart);
      if (num3 > num4)
      {
        int num5 = num3;
        num3 = num4;
        num4 = num5;
      }
      int num6 = num4 - 1;
      TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
      if (cachedTextGenerator.lineCount <= 0)
        return;
      int characterLineForWin = this.DetermineCharacterLineForWin(num3, cachedTextGenerator);
      int endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
      UIVertex simpleVert = UIVertex.simpleVert;
      simpleVert.uv0 = Vector2.zero;
      simpleVert.color = Color32.op_Implicit(this.selectionColor);
      for (int index = num3; index <= num6 && index < cachedTextGenerator.characterCount; ++index)
      {
        if (index == endPositionForWin || index == num6)
        {
          UICharInfo character1 = cachedTextGenerator.characters[num3];
          UICharInfo character2 = cachedTextGenerator.characters[index];
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector(character1.cursorPos.x / this.m_TextComponent.pixelsPerUnit, cachedTextGenerator.lines[characterLineForWin].topY / this.m_TextComponent.pixelsPerUnit);
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((character2.cursorPos.x + character2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector2_1.y - (float) cachedTextGenerator.lines[characterLineForWin].height / this.m_TextComponent.pixelsPerUnit);
          double x1 = (double) vector2_2.x;
          Rect rect1 = ((Graphic) this.m_TextComponent).rectTransform.rect;
          double xMax1 = (double) ((Rect) ref rect1).xMax;
          if (x1 <= xMax1)
          {
            double x2 = (double) vector2_2.x;
            Rect rect2 = ((Graphic) this.m_TextComponent).rectTransform.rect;
            double xMin = (double) ((Rect) ref rect2).xMin;
            if (x2 >= xMin)
              goto label_13;
          }
          ref Vector2 local = ref vector2_2;
          Rect rect3 = ((Graphic) this.m_TextComponent).rectTransform.rect;
          double xMax2 = (double) ((Rect) ref rect3).xMax;
          local.x = (float) xMax2;
label_13:
          int currentVertCount = vbo.currentVertCount;
          simpleVert.position = Vector3.op_Addition(new Vector3(vector2_1.x, vector2_2.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = Vector3.op_Addition(new Vector3(vector2_2.x, vector2_2.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = Vector3.op_Addition(new Vector3(vector2_2.x, vector2_1.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          simpleVert.position = Vector3.op_Addition(new Vector3(vector2_1.x, vector2_1.y, 0.0f), Vector2.op_Implicit(roundingOffset));
          vbo.AddVert(simpleVert);
          vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
          vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
          num3 = index + 1;
          ++characterLineForWin;
          endPositionForWin = SRPG_InputField.GetLineEndPositionForWin(cachedTextGenerator, characterLineForWin);
        }
      }
    }
  }
}
