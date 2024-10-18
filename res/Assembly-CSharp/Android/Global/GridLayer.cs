// Decompiled with JetBrains decompiler
// Type: GridLayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
[DisallowMultipleComponent]
public class GridLayer : MonoBehaviour
{
  public int LayerID;
  [Multiline]
  public string Preview;
  private int mXSize;
  private int mYSize;
  private Texture2D mTex;
  private Color32[] mPixels;
  private float mOpacity;
  private float mTransitTime;
  private float mDesiredOpacity;
  private float mCurrentOpacity;

  private void Start()
  {
    if (string.IsNullOrEmpty(this.Preview))
      return;
    string[] strArray = this.Preview.Split('\n');
    GridMap<Color32> grid = new GridMap<Color32>(strArray[0].Length, strArray.Length);
    Color32[] color32Array = new Color32[7]
    {
      new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 0),
      new Color32(byte.MaxValue, (byte) 64, (byte) 64, byte.MaxValue),
      new Color32((byte) 64, byte.MaxValue, (byte) 64, byte.MaxValue),
      new Color32((byte) 64, (byte) 64, byte.MaxValue, byte.MaxValue),
      new Color32(byte.MaxValue, byte.MaxValue, (byte) 64, byte.MaxValue),
      new Color32(byte.MaxValue, (byte) 64, byte.MaxValue, byte.MaxValue),
      new Color32((byte) 64, byte.MaxValue, byte.MaxValue, byte.MaxValue)
    };
    for (int y = 0; y < strArray.Length; ++y)
    {
      for (int index = 0; index < strArray[0].Length && index < strArray[y].Length; ++index)
      {
        int result;
        int.TryParse(strArray[y].Substring(index, 1), out result);
        grid.set(index, y, color32Array[result % color32Array.Length]);
      }
    }
    this.UpdateGrid(grid);
  }

  public void UpdateGrid(GridMap<Color32> grid)
  {
    if (grid.w != this.mXSize || grid.h != this.mYSize)
      this.InitTexture(grid.w, grid.h);
    int width = this.mTex.width;
    Color32[] mPixels = this.mPixels;
    for (int y = 0; y < grid.h; ++y)
    {
      for (int x = 0; x < grid.w; ++x)
        mPixels[x + 1 + (y + 1) * width] = grid.get(x, y);
    }
    this.mTex.SetPixels32(mPixels);
    this.mTex.Apply();
  }

  private void Awake()
  {
    this.GetComponent<Renderer>().material = Resources.Load<Material>("BG/GridMaterial");
  }

  private void Update()
  {
    float num = 2f * Time.deltaTime;
    if ((double) this.mDesiredOpacity < (double) this.mCurrentOpacity)
      num = -num;
    this.mCurrentOpacity = Mathf.Clamp01(this.mCurrentOpacity + num);
    if ((double) this.mDesiredOpacity > 0.0 || (double) this.mCurrentOpacity > 0.0)
      return;
    this.gameObject.SetActive(false);
  }

  private void OnWillRenderObject()
  {
    this.GetComponent<Renderer>().material.SetFloat("_opacity", this.mCurrentOpacity);
  }

  private void InitTexture(int w, int h)
  {
    this.mXSize = w;
    this.mYSize = h;
    w += 2;
    h += 2;
    Object.DestroyImmediate((Object) this.mTex);
    if (!Mathf.IsPowerOfTwo(w))
      w = Mathf.NextPowerOfTwo(w);
    if (!Mathf.IsPowerOfTwo(h))
      h = Mathf.NextPowerOfTwo(h);
    this.mTex = new Texture2D(w, h, TextureFormat.RGBA32, false);
    this.mTex.wrapMode = TextureWrapMode.Clamp;
    this.mTex.filterMode = FilterMode.Point;
    Vector2 vector2 = new Vector2();
    vector2.x = (float) this.mXSize / (float) this.mTex.width / (float) this.mXSize;
    vector2.y = (float) this.mYSize / (float) this.mTex.height / (float) this.mYSize;
    Material material = this.GetComponent<Renderer>().material;
    material.SetTexture("_indexTex", (Texture) this.mTex);
    material.SetTextureScale("_indexTex", vector2);
    material.SetTextureOffset("_indexTex", vector2);
    if (!string.IsNullOrEmpty(SystemInfo.deviceModel))
    {
      string[] strArray = new string[70]
      {
        "FJL21",
        "FJL22",
        "HW-01E",
        "HW-03E",
        "016SH",
        "106SH",
        "107SH",
        "200SH",
        "205SH",
        "206SH",
        "302SH",
        "303SH",
        "304SH",
        "201K",
        "202F",
        "L-01F",
        "SC-01D",
        "SC-03D",
        "SC-05D",
        "SC-06D",
        "SHL21",
        "SHL22",
        "SHL23",
        "SHL24",
        "SHL25",
        "N-04D",
        "N-05D",
        "N-07D",
        "N-08D",
        "F-02E",
        "F-03E",
        "F-04E",
        "F-06E",
        "F-05D",
        "F-11D",
        "SH-01E",
        "SH-04E",
        "SH-06E",
        "SH-07E",
        "SH-07D",
        "SH-09D",
        "SH-10D",
        "SH-02F",
        "SH-05F",
        "WX05SH",
        "P-02E",
        "P-03E",
        "ISW11F",
        "ISW13F",
        "IS12S",
        "IS15SH",
        "IS17SH",
        "SO-02E",
        "SO-03E",
        "SO-04E",
        "SO-03D",
        "SO-04D",
        "SO-05D",
        "SO-01F",
        "SOL21",
        "SOL22",
        "SOL23",
        "LGL21",
        "LGL22",
        "LGL23",
        "KYY04",
        "KYY21",
        "KYY22",
        "KYY23",
        "KYY24"
      };
      foreach (string str in strArray)
      {
        if (SystemInfo.deviceModel.Contains(str))
        {
          material.SetFloat("_offset", 0.0f);
          material.SetFloat("_offsetZ", -1f / 1000f);
          break;
        }
      }
    }
    this.mPixels = new Color32[this.mTex.width * this.mTex.height];
  }

  private void OnDestroy()
  {
    if ((Object) this.mTex != (Object) null)
    {
      Object.DestroyImmediate((Object) this.mTex);
      this.mTex = (Texture2D) null;
    }
    this.mPixels = (Color32[]) null;
  }

  public void Show()
  {
    if ((double) this.mDesiredOpacity >= 1.0)
      return;
    this.mCurrentOpacity = 0.0f;
    this.mDesiredOpacity = 1f;
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    if ((double) this.mDesiredOpacity <= 0.0)
      return;
    this.mDesiredOpacity = 0.0f;
  }

  public void SetMask(bool enable)
  {
    Material material = this.GetComponent<Renderer>().material;
    if (enable)
      material.shaderKeywords = new string[1]{ "WITH_MASK" };
    else
      material.shaderKeywords = new string[1]
      {
        "WITHOUT_MASK"
      };
  }

  public void ChangeMaterial(string path)
  {
    if (string.IsNullOrEmpty(path))
      return;
    Material material = Resources.Load<Material>(path);
    if (!(bool) ((Object) material))
      return;
    Renderer component = this.GetComponent<Renderer>();
    if (!(bool) ((Object) component))
      return;
    component.material = material;
  }
}
