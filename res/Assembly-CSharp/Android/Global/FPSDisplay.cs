// Decompiled with JetBrains decompiler
// Type: FPSDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
  [SerializeField]
  private float fpsTimeInterval = 0.1f;
  private StringBuilder stringBuilder = new StringBuilder();
  private const float MBYTE = 1048576f;
  private const string GREY_COLOUR_STRING = "101010FF";
  private const string RED_COLOUR_STRING = "ff0000";
  private const string YELLOW_COLOUR_STRING = "ffff00";
  private const string GREEN_COLOUR_STRING = "00ff00";
  [SerializeField]
  private UnityEngine.UI.Text FPS;
  [SerializeField]
  private UnityEngine.UI.Text FPSMockShadow;
  private float mDeltaTime;
  private float timeTillNextFPSTextUpdate;

  private void Start()
  {
    Object.Destroy((Object) this.gameObject);
  }

  private void Update()
  {
    this.mDeltaTime += (float) (((double) Time.unscaledDeltaTime - (double) this.mDeltaTime) * 0.100000001490116);
    if ((double) this.timeTillNextFPSTextUpdate <= 0.0)
    {
      this.FPS.text = this.GetText(this.mDeltaTime, this.stringBuilder);
      this.stringBuilder.Replace("ff0000", "101010FF", 0, this.stringBuilder.Length);
      this.stringBuilder.Replace("ffff00", "101010FF", 0, this.stringBuilder.Length);
      this.stringBuilder.Replace("00ff00", "101010FF", 0, this.stringBuilder.Length);
      this.FPSMockShadow.text = this.stringBuilder.ToString();
      this.timeTillNextFPSTextUpdate = this.fpsTimeInterval;
    }
    else
      this.timeTillNextFPSTextUpdate -= Time.unscaledDeltaTime;
  }

  private string GetText(float inDeltaTime, StringBuilder inStringBuilder)
  {
    float msec = inDeltaTime * 1000f;
    if (this.stringBuilder == null)
      this.stringBuilder = new StringBuilder();
    else if (this.stringBuilder.Length > 0)
      this.stringBuilder.Remove(0, this.stringBuilder.Length);
    inStringBuilder.Append(string.Format("<color=#{0}>{1:0.00} ms</color>", (object) FPSDisplay.GetColourForMsec(msec), (object) msec));
    inStringBuilder.Append(string.Format("\nAssets:{0}/{1}", (object) AssetManager.GetLoadedAssetNames().Length, (object) AssetManager.GetOpenedAssetBundleNames().Length));
    uint monoUsedSize = Profiler.GetMonoUsedSize();
    uint monoHeapSize = Profiler.GetMonoHeapSize();
    uint totalAllocatedMemory = Profiler.GetTotalAllocatedMemory();
    uint totalReservedMemory = Profiler.GetTotalReservedMemory();
    string colourForMemUsed = FPSDisplay.GetColourForMemUsed(monoHeapSize, totalReservedMemory);
    inStringBuilder.Append(string.Format("\n<color=#{2}>Mono:{0}/{1}MB</color>", (object) ((float) monoUsedSize / 1048576f).ToString("F2"), (object) ((float) monoHeapSize / 1048576f).ToString("F2"), (object) colourForMemUsed));
    inStringBuilder.Append(string.Format("\n<color=#{2}>Unity:{0}/{1}MB</color>", (object) ((float) totalAllocatedMemory / 1048576f).ToString("F2"), (object) ((float) totalReservedMemory / 1048576f).ToString("F2"), (object) colourForMemUsed));
    if (!AssetDownloader.isDone)
      inStringBuilder.Append(string.Format("\n<color=#{2}>Current Download:{0}/{1}MB</color>", (object) AssetDownloader.CurrentDownloadSize.ToString("F2"), (object) AssetDownloader.TotalDownloadSize.ToString("F2"), (object) colourForMemUsed));
    string path = AssetManager.Format.ToPath();
    if (!string.IsNullOrEmpty(path))
      inStringBuilder.Append(string.Format("\n<color=#{1}>AssetsTextureFormat: {0}</color>", (object) path, (object) "00ff00"));
    return inStringBuilder.ToString();
  }

  private static string GetColourForMsec(float msec)
  {
    if ((double) msec >= 50.0)
      return "ff0000";
    return (double) msec >= 33.0 ? "ffff00" : "00ff00";
  }

  private static string GetColourForMemUsed(uint inMonoHeapSize, uint inTotalReservedMemory)
  {
    if (inMonoHeapSize + inTotalReservedMemory > 314572800U)
      return "ff0000";
    return inMonoHeapSize + inTotalReservedMemory > 262144000U ? "ffff00" : "00ff00";
  }
}
