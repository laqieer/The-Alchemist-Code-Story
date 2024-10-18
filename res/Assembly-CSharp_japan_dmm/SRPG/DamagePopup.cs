// Decompiled with JetBrains decompiler
// Type: SRPG.DamagePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class DamagePopup : MonoBehaviour
  {
    public Sprite[] DigitSprites;
    public int Value;
    public float Spacing = 32f;
    public float PopMin = 0.1f;
    public float PopMax = 0.5f;
    public float Gravity = -10f;
    public float Resititution = 0.3f;
    public float KeepVisible = 0.5f;
    public float FadeTime = 1f;
    public Color DigitColor = new Color(1f, 1f, 1f, 1f);
    private DamagePopup.Digit[] mDigits;
    private float mFadeTime;

    private void Start()
    {
      int length = 1;
      for (int index = this.Value; index >= 10; index /= 10)
        ++length;
      this.mDigits = new DamagePopup.Digit[length];
      float num1 = (float) length * this.Spacing;
      int num2 = this.Value;
      for (int index = 0; index < length; ++index)
      {
        GameObject gameObject = new GameObject("Number", new System.Type[2]
        {
          typeof (RectTransform),
          typeof (Image)
        });
        Sprite digitSprite = this.DigitSprites[num2 % 10];
        this.mDigits[index].Position = new Vector2((float) ((double) num1 * 0.5 - (double) this.Spacing * ((double) index + 0.5)), Random.Range(this.PopMin, this.PopMax));
        this.mDigits[index].Transform = gameObject.transform as RectTransform;
        ((Transform) this.mDigits[index].Transform).SetParent(((Component) this).transform, false);
        RectTransform transform = this.mDigits[index].Transform;
        Rect textureRect1 = digitSprite.textureRect;
        double width = (double) ((Rect) ref textureRect1).width;
        Rect textureRect2 = digitSprite.textureRect;
        double height = (double) ((Rect) ref textureRect2).height;
        Vector2 vector2 = new Vector2((float) width, (float) height);
        transform.sizeDelta = vector2;
        this.mDigits[index].Transform.anchoredPosition = this.mDigits[index].Position;
        this.mDigits[index].Image = gameObject.GetComponent<Image>();
        this.mDigits[index].Image.sprite = digitSprite;
        ((Graphic) this.mDigits[index].Image).color = this.DigitColor;
        num2 /= 10;
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
          if ((double) Mathf.Abs(this.mDigits[index].Velocity) <= 0.0099999997764825821)
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
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).gameObject);
        }
        else
        {
          float num = (float) (1.0 - (double) this.mFadeTime / (double) this.FadeTime);
          for (int index = 0; index < this.mDigits.Length; ++index)
          {
            Color digitColor = this.DigitColor;
            digitColor.a = num;
            ((Graphic) this.mDigits[index].Image).color = digitColor;
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
