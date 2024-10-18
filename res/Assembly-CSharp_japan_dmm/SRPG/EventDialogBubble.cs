// Decompiled with JetBrains decompiler
// Type: SRPG.EventDialogBubble
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
  public class EventDialogBubble : MonoBehaviour
  {
    public static List<EventDialogBubble> Instances = new List<EventDialogBubble>();
    public const float TopMargin = 30f;
    public const float BottomMargin = 20f;
    public const float LeftMargin = 20f;
    public const float RightMargin = 20f;
    public RawImage PortraitFront;
    public UnityEngine.UI.Text NameText;
    public UnityEngine.UI.Text BodyText;
    [NonSerialized]
    public Texture2D CustomEmotion;
    private PortraitSet mPortraitSet;
    private PortraitSet.EmotionTypes mDesiredEmotion;
    private PortraitSet.EmotionTypes mCurrentEmotion;
    public string VisibilityBoolName = "open";
    public Animator BubbleAnimator;
    public string OpenedStateName = "opened";
    public string ClosedStateName = "closed";
    [NonSerialized]
    public string BubbleID;
    private bool mCloseAndDestroy;
    private MySound.Voice mVoice;
    private readonly float VoiceFadeOutSec = 0.1f;
    private bool mSkipFadeOut;
    public float FadeInTime = 1f;
    public float FadeOutTime = 0.5f;
    public float FadeOutInterval = 0.05f;
    private EventDialogBubble.Character[] mCharacters;
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
    private EventDialogBubble.Anchors mAnchor;

    public PortraitSet PortraitSet
    {
      set => this.mPortraitSet = value;
      get => this.mPortraitSet;
    }

    public PortraitSet.EmotionTypes Emotion
    {
      set
      {
        this.mDesiredEmotion = value;
        if (this.mTextQueue != null)
          return;
        this.mCurrentEmotion = this.mDesiredEmotion;
      }
      get => this.mDesiredEmotion;
    }

    public string VoiceSheetName { get; set; }

    public string VoiceCueName { get; set; }

    public bool IsVoicePlaying => this.mVoice != null && this.mVoice.IsPlaying;

    public static EventDialogBubble Find(string id)
    {
      for (int index = EventDialogBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (EventDialogBubble.Instances[index].BubbleID == id)
          return EventDialogBubble.Instances[index];
      }
      return (EventDialogBubble) null;
    }

    public static void DiscardAll()
    {
      EventScript.ActiveButtons(false);
      for (int index = EventDialogBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (!((Component) EventDialogBubble.Instances[index]).gameObject.activeInHierarchy)
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) EventDialogBubble.Instances[index]).gameObject);
        else
          EventDialogBubble.Instances[index].mCloseAndDestroy = true;
      }
      EventDialogBubble.Instances.Clear();
    }

    private void UpdatePortrait()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PortraitFront, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.PortraitSet, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CustomEmotion, (UnityEngine.Object) null))
        this.PortraitFront.texture = (Texture) this.PortraitSet.GetEmotionImage(this.mCurrentEmotion);
      else
        this.PortraitFront.texture = (Texture) this.CustomEmotion;
    }

    private void Awake()
    {
      EventDialogBubble.Instances.Add(this);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
      {
        RectTransform transform1 = ((Component) this).transform as RectTransform;
        RectTransform transform2 = ((Component) this.BodyText).transform as RectTransform;
        Rect rect1 = transform1.rect;
        double width1 = (double) ((Rect) ref rect1).width;
        Rect rect2 = transform2.rect;
        double width2 = (double) ((Rect) ref rect2).width;
        this.mBaseWidth = (float) (width1 - width2);
      }
      EventScript.OnBackLogButtonClicked += new EventScript.OnBackLogButtonClick(this.OnBackLogButtonClicked);
    }

    private void OnDestroy()
    {
      EventScript.OnBackLogButtonClicked -= new EventScript.OnBackLogButtonClick(this.OnBackLogButtonClicked);
      this.FadeOutVoice();
      EventDialogBubble.Instances.Remove(this);
    }

    private void FadeOutVoice()
    {
      if (this.mVoice == null)
        return;
      this.mVoice.StopAll(this.VoiceFadeOutSec);
      this.mVoice.Cleanup();
      this.mVoice = (MySound.Voice) null;
    }

    public void StopVoice()
    {
      if (this.mVoice == null)
        return;
      this.mVoice.StopAll(0.0f);
      this.mVoice = (MySound.Voice) null;
    }

    public bool IsPrinting => !this.mFadingOut && this.mTextNeedsUpdate && this.mNumCharacters > 0;

    public void Skip()
    {
      float time = Time.time;
      if (!this.IsPrinting || (double) time - (double) this.mStartTime <= 0.10000000149011612)
        return;
      for (int index = 0; index < this.mNumCharacters; ++index)
        this.mCharacters[index].TimeOffset = 0.0f;
      EventScript.BackLogCanOpen = true;
      this.mStartTime = time - this.FadeInTime;
      this.mSkipFadeOut = true;
    }

    public void AdjustWidth(string bodyText)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null) || !this.AutoExpandWidth)
        return;
      EventDialogBubble.Element[] elementArray = EventDialogBubble.SplitTags(bodyText);
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

    public string Name
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NameText, (UnityEngine.Object) null) ? this.NameText.text : string.Empty;
      }
    }

    public string Text
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null) ? this.BodyText.text : string.Empty;
      }
    }

    private void OnBackLogButtonClicked()
    {
      if (!this.IsPrinting)
        return;
      this.Skip();
    }

    private static EventDialogBubble.Element[] SplitTags(string s)
    {
      int index1 = 0;
      List<EventDialogBubble.Element> elementList = new List<EventDialogBubble.Element>();
      while (index1 < s.Length)
      {
        bool flag = false;
        EventDialogBubble.Element element = new EventDialogBubble.Element();
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
      EventDialogBubble.Element[] c,
      ref int n,
      string end,
      EventDialogBubble.Ctx ctx)
    {
      while (n < c.Length)
      {
        if (!string.IsNullOrEmpty(c[n].Tag))
        {
          Match match1;
          if ((match1 = EventDialogBubble.regEndTag.Match(c[n].Tag)).Success)
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
            if ((match2 = EventDialogBubble.regColor.Match(c[n].Tag)).Success)
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

    private void PushCharacters(string s, EventDialogBubble.Ctx ctx)
    {
      float num = this.mNumCharacters <= 0 ? 0.0f : this.mCharacters[this.mNumCharacters - 1].TimeOffset;
      for (int index = 0; index < s.Length; ++index)
      {
        float interval = ctx.Interval;
        if (s[index] == '\n')
          interval = this.NewLineInterval;
        this.mCharacters[this.mNumCharacters] = new EventDialogBubble.Character(s[index], ctx.Color, interval, num + interval);
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
      string mTextQueue = this.mTextQueue;
      this.mTextQueue = (string) null;
      if (this.mCharacters == null || this.mCharacters.Length < mTextQueue.Length)
        this.mCharacters = new EventDialogBubble.Character[mTextQueue.Length * 2];
      string s = this.ReplaceText(mTextQueue);
      EventAction_Dialog.TextSpeedTypes speed = EventAction_Dialog.TextSpeedTypes.Normal;
      int n = 0;
      EventDialogBubble.Ctx ctx = new EventDialogBubble.Ctx();
      ctx.Interval = speed.ToFloat();
      ctx.Color = Color32.op_Implicit(!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null) ? Color.black : ((Graphic) this.BodyText).color);
      this.mNumCharacters = 0;
      this.Parse(EventDialogBubble.SplitTags(s), ref n, (string) null, ctx);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BodyText, (UnityEngine.Object) null))
        this.BodyText.text = string.Empty;
      this.mStartTime = Time.time + this.FadeInTime;
      this.mTextNeedsUpdate = this.mNumCharacters > 0;
      this.mFadingOut = false;
      this.mCurrentEmotion = this.Emotion;
      if (string.IsNullOrEmpty(this.VoiceSheetName) || string.IsNullOrEmpty(this.VoiceCueName))
      {
        this.FadeOutVoice();
      }
      else
      {
        this.mVoice = new MySound.Voice(this.VoiceSheetName, (string) null, (string) null, EventAction.IsUnManagedAssets(this.VoiceSheetName));
        this.mVoice.Play(this.VoiceCueName);
        this.VoiceCueName = (string) null;
      }
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

    public void Open()
    {
      this.SetVisibility(true);
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      if ((double) component.anchorMin.y == 0.0 && (double) component.anchorMax.y == 0.0)
      {
        Rect safeArea = SetCanvasBounds.GetSafeArea();
        float num = (float) Screen.height - ((Rect) ref safeArea).height;
        if ((double) component.anchoredPosition.y <= (double) num)
          component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
      }
      EventScript.BackLogCanOpen = false;
      EventScript.SetButtonFront();
    }

    public void Close()
    {
      this.FadeOutVoice();
      this.SetVisibility(false);
    }

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
      EventScript.BackLogCanOpen = false;
      this.FadeOutVoice();
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
        EventScript.BackLogCanOpen = true;
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
      this.UpdatePortrait();
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

    public EventDialogBubble.Anchors Anchor
    {
      set
      {
        if (this.mAnchor == value)
          return;
        RectTransform component = ((Component) this).GetComponent<RectTransform>();
        this.mAnchor = value;
        switch (this.mAnchor)
        {
          case EventDialogBubble.Anchors.TopLeft:
            RectTransform rectTransform1 = component;
            Vector2 vector2_1 = new Vector2(0.0f, 1f);
            component.anchorMax = vector2_1;
            Vector2 vector2_2 = vector2_1;
            rectTransform1.anchorMin = vector2_2;
            component.pivot = new Vector2(0.0f, 1f);
            component.anchoredPosition = new Vector2(20f, -30f);
            break;
          case EventDialogBubble.Anchors.TopCenter:
            RectTransform rectTransform2 = component;
            Vector2 vector2_3 = new Vector2(0.5f, 1f);
            component.anchorMax = vector2_3;
            Vector2 vector2_4 = vector2_3;
            rectTransform2.anchorMin = vector2_4;
            component.pivot = new Vector2(0.5f, 1f);
            component.anchoredPosition = new Vector2(0.0f, -30f);
            break;
          case EventDialogBubble.Anchors.TopRight:
            RectTransform rectTransform3 = component;
            Vector2 vector2_5 = new Vector2(1f, 1f);
            component.anchorMax = vector2_5;
            Vector2 vector2_6 = vector2_5;
            rectTransform3.anchorMin = vector2_6;
            component.pivot = new Vector2(1f, 1f);
            component.anchoredPosition = new Vector2(-20f, -30f);
            break;
          case EventDialogBubble.Anchors.MiddleLeft:
            RectTransform rectTransform4 = component;
            Vector2 vector2_7 = new Vector2(0.0f, 0.5f);
            component.anchorMax = vector2_7;
            Vector2 vector2_8 = vector2_7;
            rectTransform4.anchorMin = vector2_8;
            component.pivot = new Vector2(0.0f, 0.5f);
            component.anchoredPosition = new Vector2(20f, 0.0f);
            break;
          case EventDialogBubble.Anchors.MiddleRight:
            RectTransform rectTransform5 = component;
            Vector2 vector2_9 = new Vector2(1f, 0.5f);
            component.anchorMax = vector2_9;
            Vector2 vector2_10 = vector2_9;
            rectTransform5.anchorMin = vector2_10;
            component.pivot = new Vector2(1f, 0.5f);
            component.anchoredPosition = new Vector2(-20f, 0.0f);
            break;
          case EventDialogBubble.Anchors.BottomLeft:
            RectTransform rectTransform6 = component;
            Vector2 vector2_11 = new Vector2(0.0f, 0.0f);
            component.anchorMax = vector2_11;
            Vector2 vector2_12 = vector2_11;
            rectTransform6.anchorMin = vector2_12;
            component.pivot = new Vector2(0.0f, 0.0f);
            component.anchoredPosition = new Vector2(20f, 20f);
            break;
          case EventDialogBubble.Anchors.BottomCenter:
            RectTransform rectTransform7 = component;
            Vector2 vector2_13 = new Vector2(0.5f, 0.0f);
            component.anchorMax = vector2_13;
            Vector2 vector2_14 = vector2_13;
            rectTransform7.anchorMin = vector2_14;
            component.pivot = new Vector2(0.5f, 0.0f);
            component.anchoredPosition = new Vector2(0.0f, 20f);
            break;
          case EventDialogBubble.Anchors.BottomRight:
            RectTransform rectTransform8 = component;
            Vector2 vector2_15 = new Vector2(1f, 0.0f);
            component.anchorMax = vector2_15;
            Vector2 vector2_16 = vector2_15;
            rectTransform8.anchorMin = vector2_16;
            component.pivot = new Vector2(1f, 0.0f);
            component.anchoredPosition = new Vector2(-20f, 20f);
            break;
        }
      }
      get => this.mAnchor;
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

    public enum Anchors
    {
      None,
      TopLeft,
      TopCenter,
      TopRight,
      MiddleLeft,
      Center,
      MiddleRight,
      BottomLeft,
      BottomCenter,
      BottomRight,
    }
  }
}
