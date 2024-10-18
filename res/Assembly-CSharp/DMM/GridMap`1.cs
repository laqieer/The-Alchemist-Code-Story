// Decompiled with JetBrains decompiler
// Type: GridMap`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class GridMap<T>
{
  private T[] _data;
  private int _w;
  private int _h;

  public GridMap(int wSize, int hSize)
  {
    this._data = new T[wSize * hSize];
    this._w = wSize;
    this._h = hSize;
  }

  protected GridMap()
  {
  }

  public int w => this._w;

  public int h => this._h;

  public T[] data => this._data;

  public bool isValid(int x, int y) => 0 <= x && 0 <= y && x < this._w && y < this._h;

  public T get(int x, int y) => this._data[x + y * this._w];

  public T get(int x, int y, T defaultValue)
  {
    return !this.isValid(x, y) ? defaultValue : this._data[x + y * this._w];
  }

  public void set(int x, int y, T src) => this._data[x + y * this._w] = src;

  public void set(int idx, T src)
  {
    if (this._data == null || idx < 0 || idx >= this._data.Length)
      return;
    this._data[idx] = src;
  }

  public void resize(int cx, int cy)
  {
    this._w = cx;
    this._h = cy;
    this._data = new T[cx * cy];
  }

  public void fill(T value)
  {
    for (int index = this._w * this._h - 1; index >= 0; --index)
      this._data[index] = value;
  }

  public GridMap<T> clone()
  {
    return new GridMap<T>()
    {
      _w = this._w,
      _h = this._h,
      _data = (T[]) this._data.Clone()
    };
  }

  public void RotateRight()
  {
    int w = this._w;
    int h = this._h;
    this._w = h;
    this._h = w;
    T[] objArray = new T[this._data.Length];
    int num = 0;
    for (int index1 = w - 1; index1 >= 0; --index1)
    {
      for (int index2 = 0; index2 < h; ++index2)
      {
        int index3 = index2 * w + index1;
        objArray[num++] = this._data[index3];
      }
    }
    this._data = objArray;
  }

  public void RotateLeft()
  {
    int w = this._w;
    int h = this._h;
    this._w = h;
    this._h = w;
    T[] objArray = new T[this._data.Length];
    int num = 0;
    for (int index1 = 0; index1 < w; ++index1)
    {
      for (int index2 = h - 1; index2 >= 0; --index2)
      {
        int index3 = index2 * w + index1;
        objArray[num++] = this._data[index3];
      }
    }
    this._data = objArray;
  }
}
