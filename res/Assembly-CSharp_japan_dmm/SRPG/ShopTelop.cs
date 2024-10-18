// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ShopTelop : MonoBehaviour
  {
    public Text BodyText;
    public float FadeInSec = 1f;
    public float FadeInInterval = 0.05f;
    public float FadeOutSec = 0.1f;
    public float FadeOutInterval = 0.005f;
    public GameObject WindowAnimator;
    public string WindowOpenProperty = "close:false";
    public string WindowOpeningState = "open";
    public string WindowCloseProperty = "close:true";
    public string WindowClosingState = "close";
    public string WindowClosedState;
    public string WindowLoopState = "loop";
    public CanvasGroup WindowAlphaCanvasGroup;
    private List<ShopTelop.TextChar> chList = new List<ShopTelop.TextChar>();
    private float mPastSec;
    private ShopTelop.EState mState;
    private string mNextText;
    private bool mNextTextUpdated;
    private bool mFadeOut;

    public void SetText(string text)
    {
      this.mNextText = text;
      this.mNextTextUpdated = true;
      if (string.IsNullOrEmpty(this.mNextText))
        return;
      ((Component) this).gameObject.SetActive(true);
    }

    private void Awake() => ((Component) this).gameObject.SetActive(false);

    private void StartTextAnim()
    {
      this.chList.Clear();
      if (this.mNextText != null)
      {
        int num = 0;
        foreach (char ch in this.mNextText)
          this.chList.Add(new ShopTelop.TextChar()
          {
            index = num++,
            ch = ch,
            alpha = 0.0f
          });
      }
      this.mPastSec = 0.0f;
      this.mFadeOut = false;
      this.mNextText = (string) null;
      this.mNextTextUpdated = false;
    }

    private void StartWindowAnim(string property)
    {
      Animator component = !Object.op_Equality((Object) this.WindowAnimator, (Object) null) ? this.WindowAnimator.GetComponent<Animator>() : (Animator) null;
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      string[] strArray = property.Split(':');
      bool flag = true;
      if (strArray.Length > 1)
        flag = strArray[1].Equals("true");
      component.SetBool(strArray[0], flag);
    }

    private bool IsWindowState(string state)
    {
      if (string.IsNullOrEmpty(state))
        return false;
      Animator component = !Object.op_Equality((Object) this.WindowAnimator, (Object) null) ? this.WindowAnimator.GetComponent<Animator>() : (Animator) null;
      if (Object.op_Equality((Object) component, (Object) null))
        return true;
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) ref animatorStateInfo).IsName(state);
    }

    private bool IsCanvasGroupAlphaZero()
    {
      return !Object.op_Equality((Object) this.WindowAlphaCanvasGroup, (Object) null) && (double) this.WindowAlphaCanvasGroup.alpha <= 0.0;
    }

    private bool UpdateWindowState()
    {
      if (string.IsNullOrEmpty(this.mNextText))
      {
        if (this.IsWindowState(this.WindowClosedState) || this.IsCanvasGroupAlphaZero())
          ((Component) this).gameObject.SetActive(false);
        else if (this.IsWindowState(this.WindowOpeningState) || this.IsWindowState(this.WindowLoopState))
          this.StartWindowAnim(this.WindowCloseProperty);
        return false;
      }
      if (this.IsWindowState(this.WindowClosingState) || this.IsWindowState(this.WindowClosedState))
        this.StartWindowAnim(this.WindowOpenProperty);
      return this.IsWindowState(this.WindowLoopState);
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.BodyText, (Object) null))
        return;
      if (this.mState == ShopTelop.EState.NOP)
      {
        this.BodyText.text = string.Empty;
        if (!this.UpdateWindowState())
          return;
        this.StartTextAnim();
        this.mState = ShopTelop.EState.ACTIVE;
      }
      if (!this.mNextTextUpdated)
      {
        this.UpdateTextAnim();
      }
      else
      {
        if (!this.UpdateTextOut())
          return;
        if (!string.IsNullOrEmpty(this.mNextText))
        {
          this.StartTextAnim();
        }
        else
        {
          this.StartWindowAnim(this.WindowCloseProperty);
          this.mState = ShopTelop.EState.NOP;
        }
      }
    }

    private void UpdateTextAnim()
    {
      this.mPastSec += Time.deltaTime;
      foreach (ShopTelop.TextChar ch in this.chList)
      {
        float num1 = (float) ch.index * this.FadeInInterval;
        float num2 = num1 + this.FadeInSec;
        ch.alpha = (double) this.mPastSec > (double) num1 ? ((double) num2 <= (double) this.mPastSec || (double) this.FadeInSec <= 0.0 ? 1f : (this.mPastSec - num1) / this.FadeInSec) : 0.0f;
      }
      this.UpdateTextString();
    }

    private void UpdateTextString()
    {
      if (Object.op_Equality((Object) this.BodyText, (Object) null))
        return;
      Color color = ((Graphic) this.BodyText).color;
      string str1 = string.Empty;
      foreach (ShopTelop.TextChar ch1 in this.chList)
      {
        char ch2 = ch1.ch;
        byte num = (byte) ((double) byte.MaxValue * (double) ch1.alpha);
        string str2 = string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>", (object) (byte) ((double) byte.MaxValue * (double) color.r), (object) (byte) ((double) byte.MaxValue * (double) color.g), (object) (byte) ((double) byte.MaxValue * (double) color.b), (object) num);
        str1 = str1 + str2 + ch2.ToString() + "</color>";
      }
      this.BodyText.text = str1;
    }

    private bool UpdateTextOut()
    {
      if (!this.mFadeOut)
      {
        this.mFadeOut = true;
        this.mPastSec = 0.0f;
      }
      this.mPastSec += Time.deltaTime;
      bool flag = true;
      foreach (ShopTelop.TextChar ch in this.chList)
      {
        float num1 = (float) ch.index * this.FadeOutInterval;
        float num2 = num1 + this.FadeOutSec;
        float num3 = (double) this.mPastSec > (double) num1 ? ((double) num2 <= (double) this.mPastSec || (double) this.FadeOutSec <= 0.0 ? 0.0f : (num2 - this.mPastSec) / this.FadeOutSec) : 1f;
        ch.alpha *= num3;
        flag &= (double) num3 <= 0.0;
      }
      this.UpdateTextString();
      return flag;
    }

    private class TextChar
    {
      public int index;
      public char ch;
      public float alpha;
    }

    private enum EState
    {
      NOP,
      ACTIVE,
    }
  }
}
