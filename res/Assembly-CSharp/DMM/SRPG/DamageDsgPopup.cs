// Decompiled with JetBrains decompiler
// Type: SRPG.DamageDsgPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class DamageDsgPopup : MonoBehaviour
  {
    public Sprite[] DigitSprites;
    public Sprite BillionSprite;
    public Sprite TrillionSprite;
    public GameObject GoDispDigit;
    public float Spacing = 48f;
    public float SpacingUnit = 90f;
    public float DispTime = 1f;
    public float FadeTime = 0.3f;
    public float DelayTime = 0.03f;
    [SerializeField]
    private int mValue;
    [SerializeField]
    private Color mDigitColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    private eDamageDispType mDamageDispType;
    private List<DamageDsgPopup.Digit> mDigitLists = new List<DamageDsgPopup.Digit>();
    private DamageDsgPopup.Digit mNumUnit;
    private float mPassedTime;
    private float mPassedFadeTime;
    private bool mIsInitialized;

    public void Setup(int value, Color color, eDamageDispType damage_disp_type)
    {
      if (!Object.op_Implicit((Object) this.GoDispDigit))
        return;
      this.GoDispDigit.SetActive(false);
      this.mValue = value;
      this.mDigitColor = color;
      this.mDamageDispType = damage_disp_type;
      int num1 = 1;
      for (int mValue = this.mValue; mValue >= 10; mValue /= 10)
        ++num1;
      this.mDigitLists.Clear();
      float num2 = (float) num1 * this.Spacing;
      Sprite sprite = (Sprite) null;
      switch (this.mDamageDispType)
      {
        case eDamageDispType.Billion:
          sprite = this.BillionSprite;
          break;
        case eDamageDispType.Trillion:
          sprite = this.TrillionSprite;
          break;
      }
      DamageDsgPopup.Digit digit1 = (DamageDsgPopup.Digit) null;
      if (Object.op_Inequality((Object) sprite, (Object) null))
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.GoDispDigit);
        if (Object.op_Implicit((Object) gameObject))
        {
          digit1 = new DamageDsgPopup.Digit();
          digit1.mPosition = new Vector2(num2 * 0.5f, 0.0f);
          digit1.mTransform = gameObject.transform as RectTransform;
          ((Transform) digit1.mTransform).SetParent(((Component) this).transform, false);
          RectTransform mTransform = digit1.mTransform;
          Rect textureRect1 = sprite.textureRect;
          double width = (double) ((Rect) ref textureRect1).width;
          Rect textureRect2 = sprite.textureRect;
          double height = (double) ((Rect) ref textureRect2).height;
          Vector2 vector2 = new Vector2((float) width, (float) height);
          mTransform.sizeDelta = vector2;
          digit1.mTransform.anchoredPosition = digit1.mPosition;
          digit1.mImage = gameObject.GetComponent<Image>();
          if (Object.op_Implicit((Object) digit1.mImage))
            digit1.mImage.sprite = sprite;
          gameObject.SetActive(true);
        }
      }
      List<DamageDsgPopup.Digit> digitList = new List<DamageDsgPopup.Digit>();
      int mValue1 = this.mValue;
      for (int index = 0; index < num1; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.GoDispDigit);
        if (Object.op_Implicit((Object) gameObject))
        {
          Sprite digitSprite = this.DigitSprites[mValue1 % 10];
          DamageDsgPopup.Digit digit2 = new DamageDsgPopup.Digit();
          digit2.mPosition = new Vector2((float) ((double) num2 * 0.5 - (double) this.SpacingUnit * 0.5 - (double) this.Spacing * ((double) index + 0.5)), 0.0f);
          digit2.mTransform = gameObject.transform as RectTransform;
          ((Transform) digit2.mTransform).SetParent(((Component) this).transform, false);
          RectTransform mTransform = digit2.mTransform;
          Rect textureRect3 = digitSprite.textureRect;
          double width = (double) ((Rect) ref textureRect3).width;
          Rect textureRect4 = digitSprite.textureRect;
          double height = (double) ((Rect) ref textureRect4).height;
          Vector2 vector2 = new Vector2((float) width, (float) height);
          mTransform.sizeDelta = vector2;
          digit2.mTransform.anchoredPosition = digit2.mPosition;
          digit2.mImage = gameObject.GetComponent<Image>();
          if (Object.op_Implicit((Object) digit2.mImage))
          {
            digit2.mImage.sprite = digitSprite;
            ((Graphic) digit2.mImage).color = this.mDigitColor;
          }
          digitList.Add(digit2);
          gameObject.SetActive(true);
        }
        mValue1 /= 10;
      }
      for (int index = digitList.Count - 1; index >= 0; --index)
        this.mDigitLists.Add(digitList[index]);
      if (digit1 != null)
        this.mDigitLists.Add(digit1);
      this.mPassedTime = 0.0f;
      this.mPassedFadeTime = 0.0f;
      this.mIsInitialized = true;
      if (this.mDigitLists.Count == 0)
        return;
      foreach (DamageDsgPopup.Digit mDigitList in this.mDigitLists)
      {
        Animator component = ((Component) mDigitList.mImage).gameObject.GetComponent<Animator>();
        if (Object.op_Implicit((Object) component))
          ((Behaviour) component).enabled = false;
      }
      this.StartCoroutine(this.startDelayAnm());
    }

    [DebuggerHidden]
    private IEnumerator startDelayAnm()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DamageDsgPopup.\u003CstartDelayAnm\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Update()
    {
      if (!this.mIsInitialized)
        return;
      this.mPassedTime += Time.deltaTime;
      if ((double) this.mPassedTime < (double) this.DispTime)
        return;
      this.mPassedFadeTime += Time.deltaTime;
      if ((double) this.mPassedFadeTime >= (double) this.FadeTime)
      {
        Object.Destroy((Object) ((Component) this).gameObject);
      }
      else
      {
        float num = (float) (1.0 - (double) this.mPassedFadeTime / (double) this.FadeTime);
        for (int index = 0; index < this.mDigitLists.Count; ++index)
        {
          Color mDigitColor = this.mDigitColor;
          mDigitColor.a = num;
          ((Graphic) this.mDigitLists[index].mImage).color = mDigitColor;
        }
      }
    }

    private class Digit
    {
      public RectTransform mTransform;
      public Image mImage;
      public Vector2 mPosition;
    }
  }
}
