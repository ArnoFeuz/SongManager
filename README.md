# 🎵 Song Manager

A lightweight, efficient C# console application designed to manage and organize your personal music collection, songs, artists, genres, and instruments.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Platform](https://img.shields.io/badge/platform-Windows-blue)

---

## ✨ Features

* **Song Management:** Effortlessly add, view, update, and delete songs in your library.
* **Smart Search & Filtering:** Filter songs by title, artist, genre, difficulty level, or target instrument.
* **Data Persistence:** Automatic JSON data saving upon application exit, plus a manual save option at any time to prevent data loss.
* **Console UI:** Clean, structured, and user-friendly command-line interface.

---

## 🚀 How to Run

You do **not** need Visual Studio or .NET SDK installed to run this application!

1. Go to the **[Releases](../../releases)** section on the right side of this repository.
2. Download the latest `SongManager-v1.0.0.zip` archive.
3. Extract the ZIP file to any folder on your computer.
4. Double-click **`SongManager.exe`** to launch the app.

---

## 📁 Project Structure

* `Program.cs` – Main entry point of the application.
* `ConsoleUI.cs` – Handles user interactions, menus, and console output.
* `DataRepository.cs` – In-memory management of songs, genres, and artists.
* `JsonStorage.cs` – Manages saving and loading data to/from JSON files.
