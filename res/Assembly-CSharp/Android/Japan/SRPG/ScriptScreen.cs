// Decompiled with JetBrains decompiler
// Type: SRPG.ScriptScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SRPG
{
  public class ScriptScreen : MonoBehaviour
  {
    private static Regex regEndTag = new Regex("^\\s*/\\s*([a-zA-Z0-9]+)\\s*");
    private static Regex regColor = new Regex("color=(#?[a-z0-9]+)");
    public float FadeInTime = 1f;
    public float FadeOutTime = 0.5f;
    public float FadeOutInterval = 0.05f;
    public float NewLineInterval = 0.5f;
    public UnityEngine.UI.Text BodyText;
    private bool mSkipFadeOut;
    private ScriptScreen.Character[] mCharacters;
    private int mNumCharacters;
    [NonSerialized]
    public EventAction_Dialog.TextSpeedTypes TextSpeed;
    private float mStartTime;
    private bool mTextNeedsUpdate;
    private ScriptScreen.TextParameter mTextQueue;
    private bool mFadingOut;

    public bool IsPrinting
    {
      get
      {
        if (!this.mFadingOut && this.mTextNeedsUpdate)
          return this.mNumCharacters > 0;
        return false;
      }
    }

    private void OnBackLogButtonClicked()
    {
      if (!this.IsPrinting)
        return;
      this.Skip();
    }

    public void Skip()
    {
      float time = Time.time;
      if (!this.IsPrinting || (double) time - (double) this.mStartTime <= 0.100000001490116)
        return;
      for (int index = 0; index < this.mNumCharacters; ++index)
        this.mCharacters[index].TimeOffset = 0.0f;
      this.mStartTime = time - this.FadeInTime;
      this.mSkipFadeOut = true;
    }

    public void Reset()
    {
      this.mCharacters = (ScriptScreen.Character[]) null;
      this.mNumCharacters = 0;
      this.mSkipFadeOut = false;
      this.mStartTime = 0.0f;
      this.mTextNeedsUpdate = false;
      this.mTextQueue = (ScriptScreen.TextParameter) null;
    }

    private static ScriptScreen.Element[] SplitTags(string s)
    {
      int index1 = 0;
      List<ScriptScreen.Element> elementList = new List<ScriptScreen.Element>();
      while (index1 < s.Length)
      {
        bool flag = false;
        ScriptScreen.Element element = new ScriptScreen.Element();
        elementList.Add(element);
        string empty = string.Empty;
        if (s[index1] == '<')
        {
          flag = true;
          int index2 = index1 + 1;
          while (index2 < s.Length && s[index2] != '>')
            empty += (string) (object) s[index2++];
          index1 = index2 + 1;
        }
        else
        {
          while (index1 < s.Length && s[index1] != '<')
            empty += (string) (object) s[index1++];
        }
        if (flag)
          element.Tag = empty;
        else
          element.Value = empty;
      }
      return elementList.ToArray();
    }

    private void Parse(ScriptScreen.Element[] c, ref int n, string end, ScriptScreen.Ctx ctx)
    {
      while (n < c.Length)
      {
        if (!string.IsNullOrEmpty(c[n].Tag))
        {
          Match match1;
          if ((match1 = ScriptScreen.regEndTag.Match(c[n].Tag)).Success)
          {
            if (match1.Groups[1].Value == end)
            {
              ++n;
              break;
            }
            ++n;
          }
          else
          {
            Match match2;
            if ((match2 = ScriptScreen.regColor.Match(c[n].Tag)).Success)
            {
              ++n;
              Color32 color = ctx.Color;
              ctx.Color = ColorUtility.ParseColor(match2.Groups[1].Value);
              this.Parse(c, ref n, "color", ctx);
              ctx.Color = color;
            }
            else
              ++n;
          }
        }
        else
        {
          this.PushCharacters(c[n].Value, ctx);
          ++n;
        }
      }
    }

    private void PushCharacters(string s, ScriptScreen.Ctx ctx)
    {
      float num = this.mNumCharacters <= 0 ? 0.0f : this.mCharacters[this.mNumCharacters - 1].TimeOffset;
      for (int index = 0; index < s.Length; ++index)
      {
        float interval = ctx.Interval;
        if (s[index] == '\n')
          interval = this.NewLineInterval;
        this.mCharacters[this.mNumCharacters] = new ScriptScreen.Character(s[index], ctx.Color, interval, num + interval);
        num = this.mCharacters[this.mNumCharacters].TimeOffset;
        ++this.mNumCharacters;
      }
    }

    public string ReplaceText(string text)
    {
      text = text.Replace("<br>", "\n");
      return text;
    }

    private void FlushText()
    {
      ScriptScreen.TextParameter mTextQueue = this.mTextQueue;
      this.mTextQueue = (ScriptScreen.TextParameter) null;
      string text;
      object[] objArray;
      if (mTextQueue == null)
      {
        text = string.Empty;
        objArray = (object[]) null;
      }
      else
      {
        text = mTextQueue.text;
        objArray = mTextQueue.args;
      }
      DebugUtility.Log("text : " + text);
      if (this.mCharacters == null || this.mCharacters.Length < text.Length)
        this.mCharacters = new ScriptScreen.Character[text.Length * 2];
      string s = this.ReplaceText(text);
      EventAction_Dialog.TextSpeedTypes speed = EventAction_Dialog.TextSpeedTypes.Normal;
      int n = 0;
      ScriptScreen.Ctx ctx = new ScriptScreen.Ctx();
      ctx.Interval = speed.ToFloat();
      ctx.Color = (Color32) (!((UnityEngine.Object) this.BodyText != (UnityEngine.Object) null) ? Color.black : this.BodyText.color);
      this.mNumCharacters = 0;
      ScriptScreen.Element[] c = ScriptScreen.SplitTags(s);
      if (objArray != null)
      {
        for (int index = 0; index < c.Length; ++index)
        {
          if (c[index] != null)
            c[index].Value = ScriptScreen.StringFormat(c[index].Value, objArray);
        }
      }
      this.Parse(c, ref n, (string) null, ctx);
      if ((UnityEngine.Object) this.BodyText != (UnityEngine.Object) null)
        this.BodyText.text = string.Empty;
      this.mStartTime = Time.time + this.FadeInTime;
      this.mTextNeedsUpdate = this.mNumCharacters > 0;
      this.mFadingOut = false;
    }

    public void SetBody(ScriptScreen.TextParameter param)
    {
      if (this.mTextQueue == null && this.mNumCharacters <= 0)
      {
        this.mTextQueue = param;
        this.FlushText();
      }
      else
      {
        this.BeginFadeOut();
        this.mTextQueue = param;
      }
    }

    private void OnEnable()
    {
      this.mStartTime = Time.time;
    }

    private void BeginFadeOut()
    {
      if (this.mSkipFadeOut)
      {
        for (int index = 0; index < this.mNumCharacters; ++index)
          this.mCharacters[index].TimeOffset = this.FadeOutTime;
      }
      else
      {
        for (int index = 0; index < this.mNumCharacters; ++index)
          this.mCharacters[index].TimeOffset = (float) index * this.FadeOutInterval + this.FadeOutTime;
      }
      this.mSkipFadeOut = false;
      this.mStartTime = Time.time;
      this.mFadingOut = true;
    }

    public bool Finished
    {
      get
      {
        if (!this.mFadingOut)
          return !this.mTextNeedsUpdate;
        return false;
      }
    }

    public void Open()
    {
      RectTransform component = this.GetComponent<RectTransform>();
      if ((double) component.anchorMin.y == 0.0 && (double) component.anchorMax.y == 0.0)
      {
        float num = (float) Screen.height - SetCanvasBounds.GetSafeArea().height;
        if ((double) component.anchoredPosition.y <= (double) num)
          component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
      }
      this.SetVisibility(true);
    }

    public void Close()
    {
      this.SetVisibility(false);
    }

    private void SetVisibility(bool open)
    {
      if (this.enabled)
        return;
      this.enabled = true;
    }

    private void UpdateText()
    {
      if (!this.mFadingOut)
      {
        if (!this.mTextNeedsUpdate)
          return;
        float time = Time.time;
        StringBuilder stringBuilder = new StringBuilder(this.mNumCharacters);
        for (int index = 0; index < this.mNumCharacters; ++index)
        {
          float num = Mathf.Clamp01((float) (1.0 - ((double) this.mStartTime + (double) this.mCharacters[index].TimeOffset - (double) time) / (double) this.FadeInTime));
          if ((double) num > 0.0)
          {
            Color32 color = this.mCharacters[index].Color;
            color.a = (byte) ((double) color.a * (double) num);
            stringBuilder.Append("<color=");
            stringBuilder.Append(color.ToColorValue());
            stringBuilder.Append(">");
            stringBuilder.Append(this.mCharacters[index].Code);
            stringBuilder.Append("</color>");
          }
          else
            break;
        }
        if ((UnityEngine.Object) this.BodyText != (UnityEngine.Object) null)
          this.BodyText.text = stringBuilder.ToString();
        if ((double) this.mStartTime + (double) this.mCharacters[this.mNumCharacters - 1].TimeOffset > (double) time)
          return;
        this.mTextNeedsUpdate = false;
      }
      else
      {
        float time = Time.time;
        StringBuilder stringBuilder = new StringBuilder(this.mNumCharacters);
        for (int index = 0; index < this.mNumCharacters; ++index)
        {
          float num = Mathf.Clamp01((this.mStartTime + this.mCharacters[index].TimeOffset - time) / this.FadeOutTime);
          Color32 color = this.mCharacters[index].Color;
          color.a = (byte) ((double) color.a * (double) num);
          stringBuilder.Append("<color=");
          stringBuilder.Append(color.ToColorValue());
          stringBuilder.Append(">");
          stringBuilder.Append(this.mCharacters[index].Code);
          stringBuilder.Append("</color>");
        }
        if ((UnityEngine.Object) this.BodyText != (UnityEngine.Object) null)
          this.BodyText.text = stringBuilder.ToString();
        if ((double) this.mStartTime + (double) this.mCharacters[this.mNumCharacters - 1].TimeOffset > (double) time)
          return;
        this.mNumCharacters = 0;
      }
    }

    private void Update()
    {
      if (this.mNumCharacters == 0 && this.mTextQueue != null && !string.IsNullOrEmpty(this.mTextQueue.text))
        this.FlushText();
      if (this.mNumCharacters <= 0)
        return;
      this.UpdateText();
    }

    private static string StringFormat(string format, params object[] args)
    {
      try
      {
        return string.Format(format, args);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      return format;
    }

    private struct Character
    {
      public char Code;
      public Color32 Color;
      public float Interval;
      public float TimeOffset;

      public Character(char code, Color32 color, float interval, float timeOffset)
      {
        interval = Mathf.Max(interval, 0.01f);
        this.Code = code;
        this.Color = color;
        this.Interval = interval;
        this.TimeOffset = timeOffset;
      }
    }

    public class TextParameter
    {
      public string text;
      public object[] args;

      public TextParameter(string text, object[] args)
      {
        this.text = text;
        this.args = args;
      }
    }

    private class Element
    {
      public string Tag;
      public string Value;
    }

    private struct Ctx
    {
      public Color32 Color;
      public float Interval;
    }
  }
}
