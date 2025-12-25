## 約束式生成演算法

## 專案簡介

每次遊玩都是一次新的探索：系統會自動拼接地形與關卡片段，產生無限可能的地圖

## 技術棧

- 語言：C#
- 平台：.NET.NET 8
- 遊戲框架：MonoGame 3.8.4
- 主要演算法：波函數坍縮
- 開發環境：Visual Studio 2026

## 安裝與啟動

先決條件（在 Windows 上）：

- 安裝 .NET 8 SDK（確保 `dotnet --version`）
- 可選：安裝 MonoGame 開發套件

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
│  ├── Content.mgcb            # MonoGame 內容管線檔案
│  ├── DefaultFont.spritefont  # 字型定義，設計與排版
│  ├── Grass.png               # 素材：草地
│  └── PurpleChapels.png       # 素材：場景貼圖
├── Game1.cs                   # 遊戲主類別
├── Program.cs                 # 程式進入點
└── readme.md

```

