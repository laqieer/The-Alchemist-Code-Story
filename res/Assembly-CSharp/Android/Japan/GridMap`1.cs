﻿// Decompiled with JetBrains decompiler
// Type: GridMap`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

  public int w
  {
    get
    {
      return this._w;
    }
  }

  public int h
  {
    get
    {
      return this._h;
    }
  }

  public T[] data
  {
    get
    {
      return this._data;
    }
  }

  public bool isValid(int x, int y)
  {
    if (0 <= x && 0 <= y && x < this._w)
      return y < this._h;
    return false;
  }

  public T get(int x, int y)
  {
    return this._data[x + y * this._w];
  }

  public T get(int x, int y, T defaultValue)
  {
    if (!this.isValid(x, y))
      return defaultValue;
    return this._data[x + y * this._w];
  }

  public void set(int x, int y, T src)
  {
    this._data[x + y * this._w] = src;
  }

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
}
