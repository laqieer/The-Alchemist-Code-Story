// Decompiled with JetBrains decompiler
// Type: SRPG.CurrencyBitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class CurrencyBitmapText : BitmapText, ISerializationCallbackReceiver
  {
    private const string mDelimiter = ",";
    private const int mGroupingCount = 3;
    private string mModifiedText;
    private readonly UIVertex[] m_TempVerts = new UIVertex[4];

    public static string CreateFormatedText(string str)
    {
      string formatedText = str;
      if (!string.IsNullOrEmpty(",") && !string.IsNullOrEmpty(str))
      {
        List<object> objectList = new List<object>();
        int num = 3;
        for (int index = str.Length - 1; index >= 0; --index)
        {
          char ch = str[index];
          if (num > 0)
          {
            objectList.Add((object) ch);
          }
          else
          {
            objectList.Add((object) ",");
            objectList.Add((object) ch);
            num = 3;
          }
          --num;
        }
        objectList.Reverse();
        StringBuilder stringBuilder = new StringBuilder();
        foreach (object obj in objectList)
          stringBuilder.Append(obj);
        formatedText = stringBuilder.ToString();
      }
      return formatedText;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
      this.mModifiedText = CurrencyBitmapText.CreateFormatedText(this.m_Text);
    }

    public override string text
    {
      get => base.text;
      set
      {
        this.mModifiedText = CurrencyBitmapText.CreateFormatedText(value);
        base.text = value;
      }
    }

    public string modifiedText
    {
      get => this.mModifiedText;
      set
      {
        this.mModifiedText = value;
        base.text = value;
      }
    }

    protected virtual void OnPopulateMesh(VertexHelper toFill)
    {
      if (Object.op_Equality((Object) this.font, (Object) null))
        return;
      this.m_DisableFontTextureRebuiltCallback = true;
      Rect rect = ((Graphic) this).rectTransform.rect;
      this.cachedTextGenerator.PopulateWithErrors(this.mModifiedText, this.GetGenerationSettings(((Rect) ref rect).size), ((Component) this).gameObject);
      IList<UIVertex> verts = this.cachedTextGenerator.verts;
      float num1 = 1f / this.pixelsPerUnit;
      int num2 = verts.Count - 4;
      if (num2 <= 0)
      {
        toFill.Clear();
      }
      else
      {
        Vector2 vector2_1 = Vector2.op_Multiply(new Vector2(verts[0].position.x, verts[0].position.y), num1);
        Vector2 vector2_2 = Vector2.op_Subtraction(((Graphic) this).PixelAdjustPoint(vector2_1), vector2_1);
        toFill.Clear();
        if (Vector2.op_Inequality(vector2_2, Vector2.zero))
        {
          for (int index1 = 0; index1 < num2; ++index1)
          {
            int index2 = index1 & 3;
            this.m_TempVerts[index2] = verts[index1];
            ref UIVertex local = ref this.m_TempVerts[index2];
            local.position = Vector3.op_Multiply(local.position, num1);
            this.m_TempVerts[index2].position.x += vector2_2.x;
            this.m_TempVerts[index2].position.y += vector2_2.y;
            if (index2 == 3)
              toFill.AddUIVertexQuad(this.m_TempVerts);
          }
        }
        else
        {
          for (int index3 = 0; index3 < num2; ++index3)
          {
            int index4 = index3 & 3;
            this.m_TempVerts[index4] = verts[index3];
            ref UIVertex local = ref this.m_TempVerts[index4];
            local.position = Vector3.op_Multiply(local.position, num1);
            if (index4 == 3)
              toFill.AddUIVertexQuad(this.m_TempVerts);
          }
        }
        this.m_DisableFontTextureRebuiltCallback = false;
      }
    }

    public virtual float preferredWidth
    {
      get
      {
        return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.mModifiedText, this.GetGenerationSettings(Vector2.zero)) / this.pixelsPerUnit;
      }
    }

    public virtual float preferredHeight
    {
      get
      {
        Rect pixelAdjustedRect = ((Graphic) this).GetPixelAdjustedRect();
        return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.mModifiedText, this.GetGenerationSettings(new Vector2(((Rect) ref pixelAdjustedRect).size.x, 0.0f))) / this.pixelsPerUnit;
      }
    }
  }
}
