# The-Alchemist-Code-Story

Story archive of [The Alchemist Code / 誰ガ為のアルケミスト / 為了誰的鍊金術師](https://al.fg-games.co.jp/)

## Story Archive

- [Story Replay (ストーリー回想)](https://laqieer.github.io/The-Alchemist-Code-Story/replay.html)
- [All Quests](https://laqieer.github.io/The-Alchemist-Code-Story/quests.html)
- [All Texts](https://laqieer.github.io/The-Alchemist-Code-Story/all.html)

## Special Thanks

- [Game data & Japanese texts](https://github.com/K0lb3/The-Alchemist-Code---asset-downloader-and-extractor)
- [Traditional Chinese texts](https://gitlab.com/the-alchemist-codes/taiwan/-/tree/master/Loc/japanese)
- [English texts](https://gitlab.com/the-alchemist-codes/taiwan/-/tree/master/Loc/japanese)

# The Alchemist Code - asset downloader & extractor

A small project that downloads all assets as well as the data of THe Alchemist Code and extracts them while it's at it.

The script updates the assets and even its own parameters required for downloading the assets on its own,
so all you have to do is execute the ``update_assets.py`` script after every update to get the latest files.

## Script Requirements

- Python 3.6+

- UnityPy 1.7.10
- pycryptodome
- msgpack
- pillow

```cmd
pip install UnityPy==1.7.10
pip install pycryptodome
pip install msgpack
pip install pillow
```

### Whitelisting

If you only want to download a subset of the assets,
then you can create a file named ``whitelist.txt`` next to ``update_assets.py``.
Each line of this file will be interpreted as regex pattern and matched against the paths of the assets.
If no match is found, then the asset won't be downloaded.

e.g. if you only want to download the ``Data`` and ``loc`` files, following whitelist can be used
```
Data/.*
Loc/.*
```
