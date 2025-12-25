## 約束式生成演算法

## 專案簡介
- 透過 Tile 模組建立約束關係表（Adjacency Constraints）
- 以機率驅動 Collapse 選擇，保留多樣性
- 內部以圖與矩陣結構維護狀態與合法解空間
- 生成結果保證滿足所有拼接約束
- 每次執行皆產生不同但合法的內容輸出

## 技術棧
- 語言：C#
- 平台：.NET 8
- 遊戲框架：MonoGame 3.8.2
- 主要演算法：波函數坍縮 (Wave Function Collapse)
- 開發環境：Visual Studio 2022

## WFC 理論基礎

| 理論 | 在 WFC 的角色 | 程式碼中的關鍵線索 |
|------|---------------|-------------------|
| **機率統計** | Collapse 選擇 Tile 的機率、權重、隨機取樣 | `tileWeights`、`getRandom()`、`adjustedWeights`、加權隨機選擇 |
| **線性代數** | 用矩陣/向量表示 Tile adjacency、狀態壓縮與約束表 | `tileRows` (2D array)、`tileRules` (adjacency matrix)、狀態向量 |
| **圖論** | Tile 之間的鄰接關係本質是圖 (節點=Tile，邊=可連接) | `neighbours` (adjacency list)、`NORTH/EAST/SOUTH/WEST`、圖遍歷 (BFS/DFS) |
| **離散數學** | 狀態是離散集合，Propagation 是集合刪除與交集運算 | `possibilities` (set)、`Constrain()`、集合過濾與約簡 |
| **資訊理論** | Entropy 衡量不確定性，低 entropy 優先 collapse | `entropy` 計算、`GetTileLowestEntropy()`、資訊熵最小化 |

## 核心演算法流程

### 1. 初始化階段
```
- 建立 Tile 網格，所有格子包含所有可能狀態
- 初始化鄰接關係圖 (上下左右四個方向)
- 設定每個 Tile 類型的約束規則與權重
```

### 2. 迭代生成階段
```
While (存在未解決的 Tile):
    a. 尋找 entropy 最低的 Tile
    b. 根據權重與鄰接匹配度進行 Collapse
    c. 使用 Stack 進行約束傳播 (Constraint Propagation)
    d. 偵測矛盾 (Contradiction)，若發生則重新開始
```

### 3. 約束傳播機制
```
Stack-based propagation:
1. 將剛 collapse 的 Tile 推入 stack
2. Pop 出 Tile，檢查所有鄰居
3. 根據當前 Tile 的狀態約束鄰居的 possibilities
4. 若鄰居的 possibilities 減少，將其推入 stack
5. 重複直到 stack 為空
```

## 執行流程概覽
1. 載入 Tile 模組與約束表
2. 建立可能狀態網格（Wave）
3. 計算每格 entropy（資訊不確定性）
4. 選擇最低 entropy 位置進行 collapse
5. 依據約束傳播（constraint propagation）
6. 偵測矛盾並自動重新生成
7. 重複直到完成合法生成結果

## 遊戲操作
- **Space / Enter** - 在標題畫面開始遊戲
- **T** - 在遊戲中重新開始生成
- **Esc** - 退出遊戲

## 安裝與啟動

### 先決條件（在 Windows 上）
- 安裝 .NET 8 SDK（確保 `dotnet --version` 顯示 8.x）
- 可選：安裝 Visual Studio 2022 with MonoGame 擴充套件

### 基本啟動步驟（在 PowerShell 中執行）

```powershell
dotnet build
dotnet run --project .
```

或在 Visual Studio 中直接按 **F5** 進行偵錯執行。

## 專案架構說明

### 核心類別

| 類別 | 職責 | 關鍵方法/屬性 |
|------|------|--------------|
| `Game1` | 遊戲主迴圈、狀態管理、畫面切換 | `Update()`, `Draw()`, `GameState` |
| `World` | 管理整個 Tile 網格、執行 WFC 演算法 | `WaveFunctionCollapse()`, `GetTileLowestEntropy()` |
| `Tile` | 單一格子的狀態、可能性集合、約束 | `Collapse()`, `Constrain()`, `entropy` |
| `TileDef` | Tile 類型定義、規則表、權重配置 | `tileRules`, `tileWeights`, `tileSprites` |
| `WorldRenderer` | 渲染 World 到 RenderTarget | `RenderTo()` |
| `InputManager` | 輸入處理與按鍵偵測 | `IsKeyPressedOnce()` |

### 狀態表示

```csharp
// Tile 狀態
- entropy > 0  : 未解決 (有多個可能性)
- entropy = 0  : 已塌縮 (單一確定狀態)
- entropy = -1 : 矛盾 (無可能性，需要重新生成)

// Tile 類型範例
- TILE_WATER: 水域
- TILE_GRASS: 草地
- TILE_FOREST: 森林
- TILE_COAST_*: 海岸線變化
- TILE_ROAD_*: 道路系統
- TILE_HOUSE: 建築物
```

## 資料夾結構

```
WFC_myproject/
├── Content/
│   ├── Content.mgcb            # MonoGame 內容管線檔案
│   ├── DefaultFont.spritefont  # 字型定義，用於 UI 與數字顯示
│   ├── Grass.png               # 素材：草地與地形貼圖
│   └── PurpleChapels.png       # 素材：建築物與覆蓋層貼圖
├── Game1.cs                    # 遊戲主類別 (主迴圈、狀態機)
├── World.cs                    # World 管理 (WFC 演算法核心)
├── Tile.cs                     # Tile 類別 (狀態與約束)
├── TileDef.cs                  # Tile 定義 (規則表、權重、sprite)
├── WorldRenderer.cs            # 渲染邏輯
├── InputManager.cs             # 輸入管理
├── Program.cs                  # 程式進入點
├── WFC.csproj                  # 專案檔
└── readme.md                   # 本文件
```

## 技術細節與最佳化

### Entropy 計算策略
- 使用曼哈頓距離從中心向外擴展
- 相同 entropy 時優先處理靠近中心的 Tile
- 避免邊緣效應導致的不自然結果

### 權重調整機制
```csharp
// 基礎權重 × 匹配倍率
adjustedWeight = baseWeight × (1 + 3 × matchCount)

// 特殊規則：
- 水域：matchCount 倍率提高到 6 (增強連續性)
- 道路：強制檢查連接性，不連接則權重為 0
```

### 矛盾處理
- 即時偵測 `possibilities.Count == 0`
- 設置 `entropy = -1` 作為矛盾標記
- 自動重新生成新的 World