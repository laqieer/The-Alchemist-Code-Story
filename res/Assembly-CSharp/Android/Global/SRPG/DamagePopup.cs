// Decompiled with JetBrains decompiler
// Type: SRPG.DamagePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class DamagePopup : MonoBehaviour
  {
    public float Spacing = 32f;
    public float PopMin = 0.1f;
    public float PopMax = 0.5f;
    public float Gravity = -10f;
    public float Resititution = 0.3f;
    public float KeepVisible = 0.5f;
    public float FadeTime = 1f;
    public Color DigitColor = new Color(1f, 1f, 1f, 1f);
    public Sprite[] DigitSprites;
    public int Value;
    private DamagePopup.Digit[] mDigits;
    private float mFadeTime;

    private void Start()
    {
      int length = 1;
      int num1 = this.Value;
      while (num1 >= 10)
      {
        ++length;
        num1 /= 10;
      }
      this.mDigits = new DamagePopup.Digit[length];
      float num2 = (float) length * this.Spacing;
      int num3 = this.Value;
      for (int index = 0; index < length; ++index)
      {
        GameObject gameObject = new GameObject("Number", new System.Type[2]
        {
          typeof (RectTransform),
          typeof (Image)
        });
        Sprite digitSprite = this.DigitSprites[num3 % 10];
        this.mDigits[index].Position = new Vector2((float) ((double) num2 * 0.5 - (double) this.Spacing * ((double) index + 0.5)), UnityEngine.Random.Range(this.PopMin, this.PopMax));
        this.mDigits[index].Transform = gameObject.transform as RectTransform;
        this.mDigits[index].Transform.SetParent(this.transform, false);
        this.mDigits[index].Transform.sizeDelta = new Vector2(digitSprite.textureRect.width, digitSprite.textureRect.height);
        this.mDigits[index].Transform.anchoredPosition = this.mDigits[index].Position;
        this.mDigits[index].Image = gameObject.GetComponent<Image>();
        this.mDigits[index].Image.sprite = digitSprite;
        this.mDigits[index].Image.color = this.DigitColor;
        num3 /= 10;
      }
    }

    private void Update()
    {
      bool flag = true;
      for (int index = 0; index < this.mDigits.Length; ++index)
      {
        this.mDigits[index].Velocity += this.Gravity * Time.deltaTime;
        this.mDigits[index].Position.y += this.mDigits[index].Velocity * Time.deltaTime;
        if ((double) this.mDigits[index].Position.y <= 0.0)
        {
          this.mDigits[index].Position.y = 0.0f;
          this.mDigits[index].Velocity = -this.mDigits[index].Velocity * this.Resititution;
          if ((double) Mathf.Abs(this.mDigits[index].Velocity) <= 0.00999999977648258)
            this.mDigits[index].Velocity = 0.0f;
        }
        else
          flag = false;
        this.mDigits[index].Transform.anchoredPosition = this.mDigits[index].Position;
      }
      if (!flag)
        return;
      if ((double) this.KeepVisible > 0.0)
      {
        this.KeepVisible -= Time.deltaTime;
      }
      else
      {
        this.mFadeTime += Time.deltaTime;
        if ((double) this.mFadeTime >= (double) this.FadeTime)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
        }
        else
        {
          float num = (float) (1.0 - (double) this.mFadeTime / (double) this.FadeTime);
          for (int index = 0; index < this.mDigits.Length; ++index)
          {
            Color digitColor = this.DigitColor;
            digitColor.a = num;
            this.mDigits[index].Image.color = digitColor;
          }
        }
      }
    }

    private struct Digit
    {
      public RectTransform Transform;
      public Image Image;
      public Vector2 Position;
      public float Velocity;
    }
  }
}
