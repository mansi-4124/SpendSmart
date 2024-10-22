ExpenseTracker

Project Overview
ExpenseTracker is a simple and intuitive web application designed to help users track their daily income and expenses. With features like adding transactions, categorizing expenses, setting budgets, and reviewing spending history, ExpenseTracker enables users to manage their finances efficiently.

Features
Add Transactions: Easily log income and expenses with details such as category, amount, date, and description.
Category Management: Organize transactions into custom categories like 'Food', 'Transportation', and more.
Budget Tracking: Set a monthly budget for specific categories and monitor your spending.
User Authentication: Secure login and registration system to ensure data privacy.
Responsive Design: Fully responsive interface to support desktop, tablet, and mobile views.
User Dashboard: Overview of recent transactions, total expenses, and income summaries.

Technologies Used
Frontend: HTML, CSS, Razor Views
Backend: ASP.NET Core MVC
Database: SQL Server with Entity Framework Core
Authentication: ASP.NET Identity
Additional Libraries: Bootstrap, FontAwesome
IDE: Visual Studio

Getting Started
Prerequisites
To run this project locally, you will need the following software installed:
.NET SDK (6.0 or higher)
SQL Server for the database
Visual Studio (Recommended) or any C# IDE
Git for version control (optional)
Installation Instructions
Clone the Repository
Clone this project to your local machine using the following command:
bash
Copy code
git clone https://github.com/your-username/ExpenseTracker.git

Set Up the Database
Open appsettings.json located in the project folder and update the database connection string with your SQL Server instance details:
json
Copy code
"ConnectionStrings": {
   "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ExpenseTrackerDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Run Database Migrations
Open the terminal/command line in the project root and run the following commands to set up the database schema:
bash
Copy code
dotnet ef database update

Run the Application
Now you're ready to run the project. In Visual Studio, simply press F5 or use the following command in your terminal:
bash
Copy code
dotnet run
Access the Web App
Once the application is running, open your browser and navigate to:
arduino
Copy code
https://localhost:5001

Usage Instructions
Login or Register to access the app. Only logged-in users can add transactions and access their financial data.
Add New Transactions by specifying the type (income or expense), category, amount, and date.
Create New Categories to better organize your transactions.
Set Budgets for specific categories and monitor your spending as you log transactions.
View Transaction History and filter your data based on date and category.

Project Structure
Controllers: Handles the logic and routes for managing transactions, categories, and user authentication.
Views: Contains the Razor Pages that handle the user interface (UI) for displaying data.
Models: Represents the database structure and handles business logic.
Migrations: Contains the Entity Framework Core migrations for database version control.

Future Enhancements
Adding charts and visualizations for better expense tracking.
Email notifications for budget threshold alerts.
Multi-user support for family/shared accounts.

Contributing
If you'd like to contribute to this project, feel free to fork the repository and submit a pull request. Please make sure your changes are well-documented and follow the coding standards.

Contact
For any inquiries or issues, please reach out to:
Name: Mansi Shintre
Email: shintrems4@gmail.com
