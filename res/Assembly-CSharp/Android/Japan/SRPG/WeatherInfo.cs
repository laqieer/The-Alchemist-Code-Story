// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class WeatherInfo : MonoBehaviour
  {
    public GameObject GoWeatherInfo;

    private void Start()
    {
      if (!(bool) ((UnityEngine.Object) this.GoWeatherInfo))
        return;
      this.GoWeatherInfo.SetActive(false);
    }

    public void Refresh(WeatherData wd)
    {
      if (!(bool) ((UnityEngine.Object) this.GoWeatherInfo))
        return;
      if (wd != null)
      {
        GameObject goWeatherInfo = this.GoWeatherInfo;
        DataSource component = goWeatherInfo.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component))
          component.Clear();
        DataSource.Bind<WeatherParam>(goWeatherInfo, wd.WeatherParam, false);
        GameParameter.UpdateAll(goWeatherInfo);
      }
      this.GoWeatherInfo.SetActive(wd != null);
    }
  }
}
