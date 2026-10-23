# Formula Typing Race Game

[![C#](https://img.shields.io/badge/Language-C%23%2012-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4.svg)](https://dotnet.microsoft.com/)
[![MonoGame](https://img.shields.io/badge/Framework-MonoGame%203.8-E73C00.svg)](https://monogame.net/)
[![WPF](https://img.shields.io/badge/UI-WPF-0078D4.svg)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)](https://www.microsoft.com/windows)
[![IDE](https://img.shields.io/badge/IDE-Visual%20Studio%202022-purple.svg)](https://visualstudio.microsoft.com/)
[![Course](https://img.shields.io/badge/Course-C%23%20%26%20.NET-orange.svg)](https://fri.uniza.sk)

A 2D arcade typing race game built in **C# (.NET 8.0)** combining a **WPF** desktop launcher with the **MonoGame** 2D game framework. Players race a Formula car against AI opponents by accurately typing words displayed on screen before rival cars cross the finish line.

This project was developed as a university semester project for the **C# and .NET (`Jazyk C# a .NET`)** course at the Faculty of Management Science and Informatics, University of Žilina (**FRI UNIZA**).

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture & Project Structure](#architecture--project-structure)
- [Difficulty Modes](#difficulty-modes)
- [Installation & How to Run](#installation--how-to-run)
- [Controls & Gameplay](#controls--gameplay)
- [Documentation](#documentation)
- [Screenshots](#screenshots)

---

## Overview

The application splits responsibilities between two interconnected .NET projects:

1. **WPF Menu Launcher (`Menu`):** Provides the main menu, difficulty selection, and post-match summary screens (`VYHRAL SI` / `PREHRAL SI`) with race completion times.
2. **MonoGame Racing Engine (`Aplikacia`):** Runs the real-time 2D game loop (`Update` / `Draw`), renders the racetrack, manages the player and 3 AI-controlled Formula cars, processes keyboard input, and tracks player lives and race time.

```plaintext
WPF Main Menu  ──(Select Difficulty)──►  3s Countdown  ──►  Formula Typing Race (MonoGame)  ──(Win / Loss)──►  Post-Game Screen (WPF)
```

Each time the player presses and releases the key matching the currently highlighted letter of the displayed word, the letter turns green, the indicator arrow advances to the next character, and the player's Formula car moves forward toward the finish line (`90%` of screen width). Making a typo deducts one life.

---

## Key Features

- **Hybrid WPF & MonoGame Integration:** Seamless transitions between the native WPF user interface and the hardware-accelerated MonoGame `DesktopGL` window via `INotifyPropertyChanged` state events.
- **Three Difficulty Levels (`Obtiaznost`):** Configurable lives, word dictionaries (`slovaLahke.txt` vs. `slovaTazke.txt`), and randomized AI opponent speeds.
- **Real-Time Typing State Machine:** Tracks key-down and key-up transitions (`Keyboard.GetState()`), highlights correctly typed letters in `LimeGreen`, positions a dynamic tracking arrow under the target character, and loads a new random word as soon as the current one is completed.
- **Resolution-Independent Scaling:** Automatically adapts window dimensions, car sprites, heart icons, lane positions, font scaling, and movement speeds to the host monitor's native resolution (`GraphicsAdapter.DefaultAdapter.CurrentDisplayMode`).
- **Stopwatch & Telemetry:** Includes a 3-second pre-race countdown and a live race timer (`m:ss`), reporting the exact winning time (`m:ss.fff`) in the post-game menu with instant replay (`OPAKOVAŤ`) support.

---

## Architecture & Project Structure

| Component / Class | Namespace / Project | Role in Application |
| :--- | :--- | :--- |
| **`MainWindow`** | `Menu` (WPF) | Manages menu navigation, difficulty selection, `Hra` lifecycle initialization, and end-of-game results display. |
| **`Hra`** | `Aplikacia` | Subclasses `Microsoft.Xna.Framework.Game` and implements `INotifyPropertyChanged`; configures screen resolution and drives the `Update`/`Draw` loop. |
| **`Level`** | `Aplikacia` | Controls match state, 3-second countdown (`Stopwatch`), word dictionary loading, AI opponent spawning, and win/loss evaluation. |
| **`IVykreslovane`** | `Aplikacia.Interface` | Common interface enforcing `Aktualizuj(GameTime)` and `Vykresli(GameTime, SpriteBatch)` across drawable game entities. |
| **`Auto` & `Hrac`** | `Aplikacia.Zavodnici` | Base `Auto` class handles continuous AI movement and sprite scaling; derived `Hrac` overrides movement to advance per valid keystroke (`PocetPosunov * Rychlost`). |
| **`Text` & `Pismeno`** | `Aplikacia.HraTexty` | Manages the active word, keyboard input detection, character coloring, target arrow positioning, and remaining player lives (`Zivoty`). |
| **`Textury` & `Pozadie`** | `Aplikacia` / `PozadieHry` | Loads and caches 2D textures (`hrac`, `protihrac`, `pozadie`, `sipka`, `zivot`) and `SpriteFont` (`font`) via MonoGame's `ContentManager`. |

---

## Difficulty Modes

| Difficulty | UI Label | Player Lives | Word Dictionary | AI Speed Range (per frame) |
| :--- | :--- | :---: | :--- | :--- |
| **Easy** (`Obtiaznost.Lahka`) | `ĽAHKÁ` | **7** | `Content/slovaLahke.txt` (common English words) | `0.5% – 1.8%` of screen width |
| **Medium** (`Obtiaznost.Stredna`) | `STREDNÁ` | **5** | `Content/slovaLahke.txt` (common English words) | `1.0% – 2.3%` of screen width |
| **Hard** (`Obtiaznost.Tazka`) | `ŤAŽKÁ` | **3** | `Content/slovaTazke.txt` (complex English vocabulary) | `1.0% – 2.3%` of screen width |

---

## Installation & How to Run

### Prerequisites

- **Operating System:** Windows 10 / 11 (required for WPF)
- **SDK:** [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **IDE:** [Visual Studio 2022](https://visualstudio.microsoft.com/) with the *.NET desktop development* workload

### Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/lubomirbojda/CS-SEM.git
   cd CS-SEM
   ```

2. **Open the solution:**
   - Open `Semestralna_praca_Solution.sln` in Visual Studio 2022.

3. **Set the Startup Project:**
   - In the **Solution Explorer**, right-click the **`Menu`** project and select **Set as Startup Project**.

4. **Build & Run:**
   - Press `F5` (Start Debugging) or `Ctrl + F5` (Start Without Debugging).

> [!TIP]
> **Troubleshooting (`dotnet-tools.json`):** If a build error occurs when restoring MonoGame Content Builder tools after downloading the repository as a ZIP archive, Windows may have blocked the configuration file. Navigate to `Aplikacia/.config/dotnet-tools.json`, right-click the file &rarr; **Properties** &rarr; check **Unblock** (or run `dotnet tool restore` in the solution directory).

---

## Controls & Gameplay

### 1. Main Menu (WPF)
- **`HRAŤ` (Play):** Opens the difficulty selection screen (`ĽAHKÁ`, `STREDNÁ`, `ŤAŽKÁ`).
- **`⇐SPÄŤ` (Back):** Returns to the main menu.
- **`VYPNÚŤ` (Exit):** Closes the application.

### 2. Racing & Typing Mechanics (MonoGame)
- **Countdown:** Every race begins with a 3-second countdown in the center of the screen.
- **Typing:** Look at the word in the top-left corner. An orange/white arrow (`sipka`) points at the letter you need to type next:
  - **Correct key:** The character turns **green**, the arrow moves to the next character, and your red Formula car advances toward the finish line. Completing a word immediately loads a new random word.
  - **Incorrect key:** One heart icon (`zivot`) is removed from the top-right life bar.
- **Win Condition:** Your Formula car crosses the finish line (`90%` of screen width) before any of the 3 blue AI Formula cars.
- **Loss Conditions:**
  - You lose all lives (`Zivoty == 0`), or
  - Any AI opponent crosses the finish line first.

### 3. Post-Game Screen
- Displays **`VYHRAL SI`** (along with your final race time in `m:ss.fff` format) or **`PREHRAL SI`**.
- Choose **`OPAKOVAŤ`** to immediately replay the same difficulty or **`⇐SPÄŤ`** to pick a different difficulty.

---

## Documentation

The repository includes the project's technical documentation:

- **[Project Documentation (PDF)](https://github.com/lubomirbojda/CS-SEM/blob/master/Dokument%C3%A1cia_Bojda.pdf)**
  - Problem statement & gameplay rules
  - Full **UML Class Diagram** of the `Aplikacia` and `Menu` assemblies
  - Overview of technologies and system requirements
  - User manual and resolution-scaling design notes

> **Note:** Source code identifiers, UI labels, and the accompanying PDF documentation are written in the **Slovak language** (in-game typing dictionaries use English words).

---

## Screenshots

<p align="center">
  <img width="580" height="639" alt="image" src="https://github.com/user-attachments/assets/f0e10050-3fc7-465f-9d09-f393aa404816" />
  <img width="1919" height="1078" alt="image" src="https://github.com/user-attachments/assets/e486660f-29c2-43d1-9da7-0bcc6e69f59f" />

</p>
