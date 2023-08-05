#include <iostream>
#include <fstream>
#include <vector>
#include <map>
#include <string>
#include <ctime>
#include <iomanip>
#include <algorithm>
#include <regex>
#include <stdexcept>
#include <sstream>

using namespace std;

class Wallet {
public:
    Wallet(string name, double balance) : name(name), balance(balance) {}
    string getName() const { return name; }
    double getBalance() const { return balance; }
    void deposit(double amount) { balance += amount; }
    void withdraw(double amount) {
        if (balance >= amount) {
            balance -= amount;
        } else {
            throw runtime_error("Insufficient balance in the wallet.");
        }
    }

private:
    string name;
    double balance;
};

class Card {
public:
    Card(string name, double limit, double balance) : name(name), limit(limit), balance(balance) {}
    string getName() const { return name; }
    double getBalance() const { return balance; }
    double getLimit() const { return limit; }
    void deposit(double amount) { balance += amount; }
    void withdraw(double amount) {
        double availableBalance = balance + limit;
        if (availableBalance >= amount) {
            balance -= amount;
        } else {
            throw runtime_error("Insufficient balance and credit limit in the card.");
        }
    }

private:
    string name;
    double limit;
    double balance;
};

class Category {
public:
    Category(string name) : name(name) {}
    string getName() const { return name; }

private:
    string name;
};

class Expense {
public:
    Expense(string description, double amount, Category category) : description(description), amount(amount), category(category) {}
    string getDescription() const { return description; }
    double getAmount() const { return amount; }
    Category getCategory() const { return category; }

private:
    string description;
    double amount;
    Category category;
};

class FinanceManager {
public:
    FinanceManager();
    void run();

private:
    void displayMainMenu() const;
    void displayWalletsAndCards() const;
    void displayCategories() const;
    void displayReports() const;
    void displayRankings() const;

    void createWalletOrCard();
    void createCategory();
    void addExpense();
    void generateReports();
    void generateRankings();

    void saveDataToFile() const;
    void loadDataFromFile();

private:
    vector<Wallet> wallets;
    vector<Card> cards;
    vector<Category> categories;
    vector<Expense> expenses;
};

FinanceManager::FinanceManager() {
    loadDataFromFile();
}

void FinanceManager::run() {
    while (true) {
        displayMainMenu();
        int choice;
        cin >> choice;
        cin.ignore();

        switch (choice) {
            case 1:
                createWalletOrCard();
                break;
            case 2:
                createCategory();
                break;
            case 3:
                addExpense();
                break;
            case 4:
                generateReports();
                break;
            case 5:
                generateRankings();
                break;
            case 0:
                saveDataToFile();
                cout << "Exiting the Finance Manager. Goodbye!\n";
                return;
            default:
                cout << "Invalid choice. Please try again.\n";
        }
    }
}

void FinanceManager::displayMainMenu() const {
    cout << "----------------------------------------\n";
    cout << "Personal Finance Management System\n";
    cout << "----------------------------------------\n";
    cout << "1. Create Wallet or Card\n";
    cout << "2. Create Category\n";
    cout << "3. Add Expense\n";
    cout << "4. Generate Reports\n";
    cout << "5. Generate Rankings\n";
    cout << "0. Exit\n";
    cout << "----------------------------------------\n";
    cout << "Select option: ";
}

void FinanceManager::displayWalletsAndCards() const {
    cout << "----------------------------------------\n";
    cout << "Wallets and Cards\n";
    cout << "----------------------------------------\n";
    cout << "Wallets:\n";
    for (const Wallet& wallet : wallets) {
        cout << " - " << wallet.getName() << ": $" << fixed << setprecision(2) << wallet.getBalance() << endl;
    }
    cout << "Cards:\n";
    for (const Card& card : cards) {
        cout << " - " << card.getName() << ": $" << fixed << setprecision(2) << card.getBalance()
             << " (Limit: $" << card.getLimit() << ")\n";
    }
    cout << "----------------------------------------\n";
}

void FinanceManager::displayCategories() const {
    cout << "----------------------------------------\n";
    cout << "Expense Categories\n";
    cout << "----------------------------------------\n";
    for (const Category& category : categories) {
        cout << " - " << category.getName() << endl;
    }
    cout << "----------------------------------------\n";
}

void FinanceManager::displayReports() const {
    cout << "----------------------------------------\n";
    cout << "Generate Reports\n";
    cout << "----------------------------------------\n";
    cout << "1. Daily Report\n";
    cout << "2. Weekly Report\n";
    cout << "3. Monthly Report\n";
    cout << "0. Back to Main Menu\n";
    cout << "----------------------------------------\n";
    cout << "Select option: ";
}

void FinanceManager::displayRankings() const {
    cout << "----------------------------------------\n";
    cout << "Generate Rankings\n";
    cout << "----------------------------------------\n";
    cout << "1. Top Expenses in Week\n";
    cout << "2. Top Expenses in Month\n";
    cout << "3. Top Categories in Week\n";
    cout << "4. Top Categories in Month\n";
    cout << "0. Back to Main Menu\n";
    cout << "----------------------------------------\n";
    cout << "Select option: ";
}

void FinanceManager::createWalletOrCard() {
    string name;
    double initialBalance;
    int type;

    cout << "Enter the name of the wallet or card: ";
    getline(cin, name);

    cout << "Enter the initial balance: $";
    cin >> initialBalance;

    cout << "Select the type (1 - Wallet, 2 - Card): ";
    cin >> type;
    cin.ignore();

    if (type == 1) {
        wallets.push_back(Wallet(name, initialBalance));
        cout << "Wallet created successfully.\n";
    } else if (type == 2) {
        double limit;
        cout << "Enter the credit limit: $";
        cin >> limit;
        cin.ignore();
        cards.push_back(Card(name, limit, initialBalance));
        cout << "Card created successfully.\n";
    } else {
        cout << "Invalid type. Please try again.\n";
    }
}

void FinanceManager::createCategory() {
    string name;
    cout << "Enter the name of the category: ";
    getline(cin, name);
    categories.push_back(Category(name));
    cout << "Category created successfully.\n";
}

void FinanceManager::addExpense() {
    string description;
    double amount;
    int categoryIndex;

    if (categories.empty()) {
        cout << "No categories available. Please create a category first.\n";
        return;
    }

    cout << "Enter the description of the expense: ";
    getline(cin, description);

    cout << "Enter the amount of the expense: $";
    cin >> amount;

    cout << "Select the category:\n";
    displayCategories();
    cin >> categoryIndex;
    cin.ignore();

    if (categoryIndex >= 1 && categoryIndex <= categories.size()) {
        expenses.push_back(Expense(description, amount, categories[categoryIndex - 1]));
        cout << "Expense added successfully.\n";
    } else {
        cout << "Invalid category index. Please try again.\n";
    }
}

void FinanceManager::generateReports() {
    int choice;
    displayReports();
    cin >> choice;
    cin.ignore();

    time_t now = time(0);
    tm* timeinfo = localtime(&now);
    int currentDay = timeinfo->tm_mday;
    int currentWeek = timeinfo->tm_yday / 7;
    int currentMonth = timeinfo->tm_mon + 1;

    cout << "----------------------------------------\n";
    switch (choice) {
        case 1:
            cout << "Daily Report\n";
            cout << "Date: " << currentMonth << "/" << currentDay << endl;
            break;
        case 2:
            cout << "Weekly Report\n";
            cout << "Week: " << currentWeek << endl;
            break;
        case 3:
            cout << "Monthly Report\n";
            cout << "Month: " << currentMonth << endl;
            break;
        default:
            cout << "Invalid choice. Please try again.\n";
            return;
    }
    cout << "----------------------------------------\n";
    double totalExpenses = 0.0;

    for (const Expense& expense : expenses) {
        int expenseDay = timeinfo->tm_mday;
        int expenseWeek = timeinfo->tm_yday / 7;
        int expenseMonth = timeinfo->tm_mon + 1;

        if ((choice == 1 && expenseDay == currentDay) ||
            (choice == 2 && expenseWeek == currentWeek) ||
            (choice == 3 && expenseMonth == currentMonth)) {
            cout << "Category: " << expense.getCategory().getName() << ", "
                 << "Description: " << expense.getDescription() << ", "
                 << "Amount: $" << fixed << setprecision(2) << expense.getAmount() << endl;
            totalExpenses += expense.getAmount();
        }
    }
    cout << "----------------------------------------\n";
    cout << "Total Expenses: $" << fixed << setprecision(2) << totalExpenses << endl;
}

void FinanceManager::generateRankings() {
    int choice;
    displayRankings();
    cin >> choice;
    cin.ignore();

    int currentWeek = time(0) / (7 * 24 * 60 * 60);
    int currentMonth = time(0) / (30 * 24 * 60 * 60);

    cout << "----------------------------------------\n";
    switch (choice) {
        case 1:
            cout << "Top Expenses in Week " << currentWeek << endl;
            break;
        case 2:
            cout << "Top Expenses in Month " << currentMonth << endl;
            break;
        case 3:
            cout << "Top Categories in Week " << currentWeek << endl;
            break;
        case 4:
            cout << "Top Categories in Month " << currentMonth << endl;
            break;
        default:
            cout << "Invalid choice. Please try again.\n";
            return;
    }
    cout << "----------------------------------------\n";

    map<string, double> categoryExpenses;

    for (const Expense& expense : expenses) {
        int expenseWeek = time(0) / (7 * 24 * 60 * 60);
        int expenseMonth = time(0) / (30 * 24 * 60 * 60);

        if ((choice == 1 && expenseWeek == currentWeek) ||
            (choice == 2 && expenseMonth == currentMonth)) {
            categoryExpenses[expense.getCategory().getName()] += expense.getAmount();
        }
    }

    vector<pair<string, double>> sortedExpenses(categoryExpenses.begin(), categoryExpenses.end());
    sort(sortedExpenses.begin(), sortedExpenses.end(),
         [](const pair<string, double>& a, const pair<string, double>& b) {
             return a.second > b.second;
         });

    int count = 0;
    for (const auto& item : sortedExpenses) {
        if (count >= 3) {
            break;
        }
        cout << count + 1 << ". " << item.first << ": $" << fixed << setprecision(2) << item.second << endl;
        count++;
    }
    cout << "----------------------------------------\n";
}

void FinanceManager::saveDataToFile() const {
    ofstream file("finance_data.csv");

    if (!file) {
        cout << "Error saving data to file.\n";
        return;
    }

    for (const Wallet& wallet : wallets) {
        file << "W," << wallet.getName() << "," << wallet.getBalance() << "\n";
    }
    for (const Card& card : cards) {
        file << "C," << card.getName() << "," << card.getLimit() << "," << card.getBalance() << "\n";
    }
    for (const Category& category : categories) {
        file << "T," << category.getName() << "\n";
    }
    for (const Expense& expense : expenses) {
        file << "E," << expense.getDescription() << "," << expense.getAmount() << "," << expense.getCategory().getName() << "\n";
    }

    file.close();
    cout << "Data saved to file successfully.\n";
}

void FinanceManager::loadDataFromFile() {
    ifstream file("finance_data.csv");

    if (!file) {
        cout << "Data file not found. Starting with an empty database.\n";
        return;
    }

    wallets.clear();
    cards.clear();
    categories.clear();
    expenses.clear();

    string line;
    while (getline(file, line)) {
        stringstream ss(line);
        string type;
        getline(ss, type, ',');

        if (type == "W") { // Wallet
            string name;
            double balance;
            getline(ss, name, ',');
            ss >> balance;
            wallets.push_back(Wallet(name, balance));
        } else if (type == "C") { // Card
            string name;
            double limit, balance;
            getline(ss, name, ',');
            ss >> limit >> balance;
            cards.push_back(Card(name, limit, balance));
        } else if (type == "T") { // Category
            string name;
            getline(ss, name, ',');
            categories.push_back(Category(name));
        } else if (type == "E") { // Expense
            string description, category;
            double amount;
            getline(ss, description, ',');
            ss >> amount;
            getline(ss, category, ',');
            Category* foundCategory = nullptr;
            for (Category& cat : categories) {
                if (cat.getName() == category) {
                    foundCategory = &cat;
                    break;
                }
            }
            if (foundCategory != nullptr) {
                expenses.push_back(Expense(description, amount, *foundCategory));
            }
        }
    }

    file.close();
    cout << "Data loaded from file successfully.\n";
}

int main() {
    FinanceManager financeManager;
    financeManager.run();
    return 0;
}
