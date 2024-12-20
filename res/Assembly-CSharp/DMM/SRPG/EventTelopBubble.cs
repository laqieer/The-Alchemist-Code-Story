﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventTelopBubble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventTelopBubble : MonoBehaviour
  {
    public static List<EventTelopBubble> Instances = new List<EventTelopBubble>();
    public RawImage PortraitFront;
    public RawImage PortraitBack;
    public UnityEngine.UI.Text NameText;
    public UnityEngine.UI.Text BodyText;
    public float PortraitTransitionTime = 0.5f;
    public bool TextColor;
    public Event2dAction_Telop.TextPositionTypes TextPosition;
    private bool mPortraitInitialized;
    public Texture2D CustomEmotion;
    private PortraitSet mPortraitSet;
    [NonSerialized]
    public PortraitSet.EmotionTypes Emotion;
    private PortraitSet.EmotionTypes mCurrentEmotion;
    private float mPortraitTransition;
    public string VisibilityBoolName = "open";
    public Animator BubbleAnimator;
    public string OpenedStateName = "opened";
    public string ClosedStateName = "closed";
    [NonSerialized]
    public string BubbleID;
    private bool mCloseAndDestroy;
    private bool mSkipFadeOut;
    public float FadeInTime = 1f;
    public float FadeOutTime = 0.5f;
    public float FadeOutInterval = 0.05f;
    private EventTelopBubble.Character[] mCharacters;
    private int mNumCharacters;
    public float NewLineInterval = 0.5f;
    [NonSerialized]
    public EventAction_Dialog.TextSpeedTypes TextSpeed;
    public bool AutoExpandWidth = true;
    public float MaxBodyTextWidth = 800f;
    private float mBaseWidth;
    private float mStartTime;
    private bool mTextNeedsUpdate;
    private string mTextQueue;
    private static Regex regEndTag = new Regex("^\\s*/\\s*([a-zA-Z0-9]+)\\s*");
    private static Regex regColor = new Regex("color=(#?[a-z0-9]+)");
    private bool mFadingOut;
    private bool mShouldOpen;

    public PortraitSet PortraitSet
    {
      set => this.mPortraitSet = value;
      get => this.mPortraitSet;
    }

    public static EventTelopBubble Find(string id)
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (EventTelopBubble.Instances[index].BubbleID == id)
          return EventTelopBubble.Instances[index];
      }
      return (EventTelopBubble) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventTelopBubble.Instances[index]).gameObject.activeInHierarchy)
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) EventTelopBubble.Instances[index]).gameObject);
        else
          EventTelopBubble.Instances[index].mCloseAndDestroy = true;
      }
      EventTelopBubble.Instances.Clear();
    }

    private void UpdatePortrait()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PortraitFront, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PortraitSet, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null))
        return;
      if (this.mPortraitInitialized && this.Emotion == this.mCurrentEmotion && (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PortraitFront.texture, (UnityEngine.Object) this.CustomEmotion)))
        this.mPortraitTransition = 0.0f;
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PortraitBack, (UnityEngine.Object) null) && this.mPortraitInitialized)
      {
        this.mPortraitTransition += Time.deltaTime;
        if ((double) this.mPortraitTransition < (double) this.PortraitTransitionTime)
        {
          float num = Mathf.Clamp01(this.mPortraitTransition / this.PortraitTransitionTime);
          ((Graphic) this.PortraitFront).color = new Color(1f, 1f, 1f, num);
          this.PortraitBack.texture = this.PortraitFront.texture;
          this.PortraitFront.texture = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null) ? (Texture) this.CustomEmotion : (Texture) this.PortraitSet.GetEmotionImage(this.Emotion);
          ((Graphic) this.PortraitBack).color = new Color(1f, 1f, 1f, 1f - num);
          ((Component) this.PortraitBack).gameObject.SetActive(true);
        }
        else
        {
          this.mCurrentEmotion = this.Emotion;
          ((Graphic) this.PortraitFront).color = new Color(1f, 1f, 1f, 1f);
          ((Component) this.PortraitBack).gameObject.SetActive(false);
        }
      }
      else
      {
        this.PortraitFront.texture = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null) ? (Texture) this.CustomEmotion : (Texture) this.PortraitSet.GetEmotionImage(this.mCurrentEmotion);
        this.mPortraitInitialized = true;
      }
    }

    private void Awake()
    {
      EventTelopBubble.Instances.Add(this);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
        return;
      RectTransform transform1 = ((Component) this).transform as RectTransform;
      RectTransform transform2 = ((Component) this.BodyText).transform as RectTransform;
      Rect rect1 = transform1.rect;
      double width1 = (double) ((Rect) ref rect1).width;
      Rect rect2 = transform2.rect;
      double width2 = (double) ((Rect) ref rect2).width;
      this.mBaseWidth = (float) (width1 - width2);
    }

    private void OnDestroy() => EventTelopBubble.Instances.Remove(this);

    public bool IsPrinting => !this.mFadingOut && this.mTextNeedsUpdate && this.mNumCharacters > 0;

    public void Skip()
    {
      float time = Time.time;
      if (!this.IsPrinting || (double) time - (double) this.mStartTime <= 0.10000000149011612)
        return;
      for (int index = 0; index < this.mNumCharacters; ++index)
        this.mCharacters[index].TimeOffset = 0.0f;
      this.mStartTime = time - this.FadeInTime;
      this.mSkipFadeOut = true;
    }

    public void AdjustWidth(string bodyText)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null) || !this.AutoExpandWidth)
        return;
      EventTelopBubble.Element[] elementArray = EventTelopBubble.SplitTags(bodyText);
      StringBuilder stringBuilder = new StringBuilder(elementArray.Length);
      for (int index = 0; index < elementArray.Length; ++index)
      {
        if (!string.IsNullOrEmpty(elementArray[index].Value))
          stringBuilder.Append(elementArray[index].Value);
      }
      float num = Mathf.Min(this.BodyText.cachedTextGeneratorForLayout.GetPreferredWidth(stringBuilder.ToString(), this.BodyText.GetGenerationSettings(Vector2.zero)) / this.BodyText.pixelsPerUnit, this.MaxBodyTextWidth) + this.mBaseWidth;
      RectTransform transform = ((Component) this).transform as RectTransform;
      Vector2 sizeDelta = transform.sizeDelta;
      sizeDelta.x = Mathf.Max(sizeDelta.x, num);
      transform.sizeDelta = sizeDelta;
    }

    public void SetName(string name)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NameText, (UnityEngine.Object) null))
        return;
      this.NameText.text = name;
    }

    private static EventTelopBubble.Element[] SplitTags(string s)
    {
      int index1 = 0;
      List<EventTelopBubble.Element> elementList = new List<EventTelopBubble.Element>();
      while (index1 < s.Length)
      {
        bool flag = false;
        EventTelopBubble.Element element = new EventTelopBubble.Element();
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

    private void Parse(
      EventTelopBubble.Element[] c,
      ref int n,
      string end,
      EventTelopBubble.Ctx ctx)
    {
      while (n < c.Length)
      {
        if (!string.IsNullOrEmpty(c[n].Tag))
        {
          Match match1;
          if ((match1 = EventTelopBubble.regEndTag.Match(c[n].Tag)).Success)
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
            if ((match2 = EventTelopBubble.regColor.Match(c[n].Tag)).Success)
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

    private void PushCharacters(string s, EventTelopBubble.Ctx ctx)
    {
      float num = this.mNumCharacters <= 0 ? 0.0f : this.mCharacters[this.mNumCharacters - 1].TimeOffset;
      for (int index = 0; index < s.Length; ++index)
      {
        float interval = ctx.Interval;
        if (s[index] == '\n')
          interval = this.NewLineInterval;
        this.mCharacters[this.mNumCharacters] = new EventTelopBubble.Character(s[index], ctx.Color, interval, num + interval);
        num = this.mCharacters[this.mNumCharacters].TimeOffset;
        ++this.mNumCharacters;
      }
    }

    private void FlushText()
    {
      string mTextQueue = this.mTextQueue;
      this.mTextQueue = (string) null;
      if (this.mCharacters == null || this.mCharacters.Length < mTextQueue.Length)
        this.mCharacters = new EventTelopBubble.Character[mTextQueue.Length * 2];
      string s = mTextQueue.Replace("<br>", "\n");
      EventAction_Dialog.TextSpeedTypes speed = EventAction_Dialog.TextSpeedTypes.Normal;
      int n = 0;
      EventTelopBubble.Ctx ctx = new EventTelopBubble.Ctx();
      ctx.Interval = speed.ToFloat();
      ctx.Color = Color32.op_Implicit(!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null) ? Color.black : ((Graphic) this.BodyText).color);
      if (this.TextColor)
        ctx.Color = Color32.op_Implicit(Color.white);
      this.BodyText.alignment = this.TextPosition != Event2dAction_Telop.TextPositionTypes.Center ? (this.TextPosition != Event2dAction_Telop.TextPositionTypes.Right ? (TextAnchor) 3 : (TextAnchor) 5) : (TextAnchor) 4;
      this.mNumCharacters = 0;
      this.Parse(EventTelopBubble.SplitTags(s), ref n, (string) null, ctx);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
        this.BodyText.text = string.Empty;
      this.mStartTime = Time.time + this.FadeInTime;
      this.mTextNeedsUpdate = this.mNumCharacters > 0;
      this.mFadingOut = false;
    }

    public void SetBody(string text)
    {
      if (this.mTextQueue == null && this.mNumCharacters <= 0)
      {
        this.mTextQueue = text;
        this.FlushText();
      }
      else
      {
        this.BeginFadeOut();
        this.mTextQueue = text;
      }
    }

    private void OnEnable() => this.mStartTime = Time.time;

    private void Start() => this.mShouldOpen = true;

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

    public bool Finished => !this.mFadingOut && !this.mTextNeedsUpdate;

    public void Open() => this.SetVisibility(true);

    public void Close() => this.SetVisibility(false);

    private void SetVisibility(bool open)
    {
      this.mShouldOpen = open;
      if (((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.UpdateStateBool();
    }

    public void Forward()
    {
      if (!this.Finished)
        ;
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
          this.BodyText.text = stringBuilder.ToString();
        if ((double) this.mStartTime + (double) this.mCharacters[this.mNumCharacters - 1].TimeOffset > (double) time)
          return;
        this.mNumCharacters = 0;
      }
    }

    private void UpdateStateBool()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BubbleAnimator, (UnityEngine.Object) null))
        return;
      this.BubbleAnimator.SetBool(this.VisibilityBoolName, this.mShouldOpen);
    }

    private void Update()
    {
      if (this.mCloseAndDestroy)
      {
        this.mShouldOpen = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BubbleAnimator, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.ClosedStateName))
        {
          this.UpdateStateBool();
          AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
          if (!((AnimatorStateInfo) ref animatorStateInfo).IsName(this.ClosedStateName))
            return;
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).gameObject);
        }
        else
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).gameObject);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BubbleAnimator, (UnityEngine.Object) null))
        {
          this.UpdateStateBool();
          if (!this.mShouldOpen && !string.IsNullOrEmpty(this.ClosedStateName))
          {
            AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
            if (((AnimatorStateInfo) ref animatorStateInfo).IsName(this.ClosedStateName))
              this.mNumCharacters = 0;
          }
          if (!string.IsNullOrEmpty(this.OpenedStateName))
          {
            AnimatorStateInfo animatorStateInfo = this.BubbleAnimator.GetCurrentAnimatorStateInfo(0);
            if (!((AnimatorStateInfo) ref animatorStateInfo).IsName(this.OpenedStateName))
            {
              this.mStartTime = Time.time;
              return;
            }
          }
        }
        if (this.mNumCharacters == 0 && !string.IsNullOrEmpty(this.mTextQueue))
          this.FlushText();
        if (this.mNumCharacters <= 0)
          return;
        this.UpdateText();
      }
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
