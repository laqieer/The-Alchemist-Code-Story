// Decompiled with JetBrains decompiler
// Type: SRPG.DamageDsgPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class DamageDsgPopup : MonoBehaviour
  {
    public float Spacing = 48f;
    public float SpacingUnit = 90f;
    public float DispTime = 1f;
    public float FadeTime = 0.3f;
    public float DelayTime = 0.03f;
    [SerializeField]
    private Color mDigitColor = new Color(1f, 1f, 1f, 1f);
    private List<DamageDsgPopup.Digit> mDigitLists = new List<DamageDsgPopup.Digit>();
    public Sprite[] DigitSprites;
    public Sprite BillionSprite;
    public Sprite TrillionSprite;
    public GameObject GoDispDigit;
    [SerializeField]
    private int mValue;
    [SerializeField]
    private eDamageDispType mDamageDispType;
    private DamageDsgPopup.Digit mNumUnit;
    private float mPassedTime;
    private float mPassedFadeTime;
    private bool mIsInitialized;

    public void Setup(int value, Color color, eDamageDispType damage_disp_type)
    {
      if (!(bool) ((UnityEngine.Object) this.GoDispDigit))
        return;
      this.GoDispDigit.SetActive(false);
      this.mValue = value;
      this.mDigitColor = color;
      this.mDamageDispType = damage_disp_type;
      int num1 = 1;
      int mValue1 = this.mValue;
      while (mValue1 >= 10)
      {
        ++num1;
        mValue1 /= 10;
      }
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
      if ((UnityEngine.Object) sprite != (UnityEngine.Object) null)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoDispDigit);
        if ((bool) ((UnityEngine.Object) gameObject))
        {
          digit1 = new DamageDsgPopup.Digit();
          digit1.mPosition = new Vector2(num2 * 0.5f, 0.0f);
          digit1.mTransform = gameObject.transform as RectTransform;
          digit1.mTransform.SetParent(this.transform, false);
          digit1.mTransform.sizeDelta = new Vector2(sprite.textureRect.width, sprite.textureRect.height);
          digit1.mTransform.anchoredPosition = digit1.mPosition;
          digit1.mImage = gameObject.GetComponent<Image>();
          if ((bool) ((UnityEngine.Object) digit1.mImage))
            digit1.mImage.sprite = sprite;
          gameObject.SetActive(true);
        }
      }
      List<DamageDsgPopup.Digit> digitList = new List<DamageDsgPopup.Digit>();
      int mValue2 = this.mValue;
      for (int index = 0; index < num1; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoDispDigit);
        if ((bool) ((UnityEngine.Object) gameObject))
        {
          Sprite digitSprite = this.DigitSprites[mValue2 % 10];
          DamageDsgPopup.Digit digit2 = new DamageDsgPopup.Digit();
          digit2.mPosition = new Vector2((float) ((double) num2 * 0.5 - (double) this.SpacingUnit * 0.5 - (double) this.Spacing * ((double) index + 0.5)), 0.0f);
          digit2.mTransform = gameObject.transform as RectTransform;
          digit2.mTransform.SetParent(this.transform, false);
          digit2.mTransform.sizeDelta = new Vector2(digitSprite.textureRect.width, digitSprite.textureRect.height);
          digit2.mTransform.anchoredPosition = digit2.mPosition;
          digit2.mImage = gameObject.GetComponent<Image>();
          if ((bool) ((UnityEngine.Object) digit2.mImage))
          {
            digit2.mImage.sprite = digitSprite;
            digit2.mImage.color = this.mDigitColor;
          }
          digitList.Add(digit2);
          gameObject.SetActive(true);
        }
        mValue2 /= 10;
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
        Animator component = mDigitList.mImage.gameObject.GetComponent<Animator>();
        if ((bool) ((UnityEngine.Object) component))
          component.enabled = false;
      }
      this.StartCoroutine(this.startDelayAnm());
    }

    [DebuggerHidden]
    private IEnumerator startDelayAnm()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DamageDsgPopup.\u003CstartDelayAnm\u003Ec__Iterator0() { \u0024this = this };
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
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
      else
      {
        float num = (float) (1.0 - (double) this.mPassedFadeTime / (double) this.FadeTime);
        for (int index = 0; index < this.mDigitLists.Count; ++index)
        {
          Color mDigitColor = this.mDigitColor;
          mDigitColor.a = num;
          this.mDigitLists[index].mImage.color = mDigitColor;
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
