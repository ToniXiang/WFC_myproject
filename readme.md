<div align="center">
  <h1>WFC_myproject</h1>
  <p>隨機地圖的縫合師</p>
</div>

---
## 目錄
- [目錄](#目錄)
- [專案簡介](#專案簡介)
- [技術棧](#技術棧)
- [安裝與啟動](#安裝與啟動)
- [資料夾結構](#資料夾結構)

---

## 專案簡介

在《隨機地圖的縫合師》中，每次遊玩都是一次新的探索：系統會自動拼接地形與關卡片段，產生無限可能的地圖與挑戰。輕鬆上手，深度可玩 — 適合喜歡探索、拼圖與程序生成驚喜的玩家。現在就啟動，看看下一張地圖會把你帶到哪裡！

## 技術棧

- 語言：C#
- 平台：.NET 6.0 (net6.0)
- IDE：Visual Studio 2022
- 遊戲框架：MonoGame
- 演算法：波函數坍縮

## 安裝與啟動

先決條件（在 Windows 上）：

- 安裝 .NET 6 SDK（確保 `dotnet --version` 回傳 6.x）。
- 可選：安裝 MonoGame 開發套件（若要建立新專案或需工具支援）。

基本啟動步驟（在 PowerShell 中執行）：

1. 開啟專案資料夾內的 solution

2. 使用 dotnet 建置並執行（或 Debug F5）：

```powershell
dotnet build
dotnet run --project .
```


## 資料夾結構

```
WFC_myproject/
│── Content/
│  ├── Content.mgcb            #MonoGame 內容管線檔案
│  ├── DefaultFont.spritefont  #字型定義，設計與排版
│  ├── Grass.png               #素材：草地
│  └── PurpleChapels.png       #素材：場景貼圖
├── Game1.cs                  #遊戲主類別
├── Program.cs                #程式進入點
└── readme.md

```

