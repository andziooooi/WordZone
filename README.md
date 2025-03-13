# WordZone

**WordZone** is a desktop application built with WPF that helps users learn English. With WordZone, you can create custom word packs to study and then use interactive quizzes or flashcards to effectively memorize new words. The application also allows editing existing word packs, making it easy to tailor your learning experience to your needs.

## Technologies

- **WPF (Windows Presentation Foundation)**  
- **Entity Framework Core**  
- **SQLite**  
- **MVVM (Model-View-ViewModel)**  

## Features

✔️ **Create custom word packs** – Add words you want to learn and organize them into packets.  
🎓 **Learning Mode – Quiz** – Test your knowledge with quizzes.  
📖 **Learning Mode – Flashcards** – Use flashcards to reinforce word meanings and boost retention.  
✏️ **Edit word packs** – Add, modify, or delete words in your existing packs.  
💾 **Save your progress** – Your word packs are stored in a SQLite database, allowing you to revisit them anytime.  

## System Requirements

- **.NET 6.0+**  
- **SQLite**  
- **Windows 10/11**  

## Installation

1. **Clone the repository**  
   ```sh
   git clone https://github.com/andziooooi/WordZone.git
   cd WordZone
   ```
2. **Install dependencies**  
   If you're using Entity Framework Core, make sure you have the necessary packages installed.
   ```sh
   dotnet restore
   ```
3. **Set up the database**  
   Run migrations to prepare the SQLite database:
   ```sh
   dotnet ef database update --project WordZone
   ```
4. **Run the application**  
   ```sh
   dotnet run
   ```

## Planned Updates 🚀

🎨 **UI Improvements** – Enhancing the user interface and user experience for better usability.  
📊 **Learning Statistics** – Track learning progress and analyze quiz results.  
📌 **Import and Export Word Packs** – Users will be able to export and import word packs.    


