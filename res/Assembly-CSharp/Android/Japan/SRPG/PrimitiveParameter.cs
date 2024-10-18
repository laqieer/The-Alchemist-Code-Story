// Decompiled with JetBrains decompiler
// Type: SRPG.PrimitiveParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PrimitiveParameter : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private PrimitiveParameter.PrimitiveType Type;
    [SerializeField]
    private Text TargetText;

    public void UpdateValue()
    {
      switch (this.Type)
      {
        case PrimitiveParameter.PrimitiveType.Int:
          this.SetText(DataSource.FindDataOfClass<int>(this.gameObject, 0).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Long:
          this.SetText(DataSource.FindDataOfClass<long>(this.gameObject, 0L).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Float:
          this.SetText(DataSource.FindDataOfClass<float>(this.gameObject, 0.0f).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.Double:
          this.SetText(DataSource.FindDataOfClass<double>(this.gameObject, 0.0).ToString());
          break;
        case PrimitiveParameter.PrimitiveType.String:
          this.SetText(DataSource.FindDataOfClass<string>(this.gameObject, (string) null));
          break;
      }
    }

    private void SetText(string text)
    {
      if (!((UnityEngine.Object) this.TargetText != (UnityEngine.Object) null))
        return;
      this.TargetText.text = text;
    }

    public enum PrimitiveType
    {
      Int,
      Long,
      Float,
      Double,
      String,
    }
  }
}
