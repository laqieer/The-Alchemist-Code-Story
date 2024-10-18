// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleCharacterFadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleCharacterFadeColor : AnimEvent
  {
    [SerializeField]
    private Color FadeColor = Color.white;
    private Color StartColor = Color.white;
    private UnitController mUnitController;
    private GeneratedCharacter mGeneratedCharacter;

    public override void OnStart(GameObject go)
    {
      this.mGeneratedCharacter = go.GetComponent<GeneratedCharacter>();
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        this.StartColor = this.mGeneratedCharacter.GetColor();
      }
      else
      {
        this.mUnitController = go.GetComponentInParent<UnitController>();
        if (!((UnityEngine.Object) this.mUnitController == (UnityEngine.Object) null))
          return;
        this.StartColor = this.mUnitController.GetColor();
      }
    }

    public override void OnEnd(GameObject go)
    {
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        this.mGeneratedCharacter.SetColor(this.FadeColor);
      }
      else
      {
        if (!((UnityEngine.Object) this.mUnitController != (UnityEngine.Object) null))
          return;
        this.mUnitController.SetColor(this.FadeColor);
      }
    }

    public override void OnTick(GameObject go, float ratio)
    {
      this.SetRenderMode(ratio);
    }

    private void SetRenderMode(float ratio)
    {
      Color color = Color.Lerp(this.StartColor, this.FadeColor, ratio);
      if ((UnityEngine.Object) this.mGeneratedCharacter != (UnityEngine.Object) null)
      {
        this.mGeneratedCharacter.SetColor(color);
      }
      else
      {
        if (!((UnityEngine.Object) this.mUnitController != (UnityEngine.Object) null))
          return;
        this.mUnitController.SetColor(color);
      }
    }
  }
}
