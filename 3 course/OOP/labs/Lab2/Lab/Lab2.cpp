#include <iostream>
#include <string>

using namespace std;

class Personality {
    string name;
    string activityYear;
    string description;
public:
    // Конструкторы
    Personality() {
        name = "No name";
        activityYear = "No year";
        description = "No description";
        cout << "Personality: Default constructor" << endl;
    }
    
    Personality(string _name, string _year, string _desc) {
        name = _name;
        activityYear = _year;
        description = _desc;
        cout << "Personality: Constructor with parameters" << endl;
    }
    
    Personality(Personality &p) {
        name = p.name;
        activityYear = p.activityYear;
        description = p.description;
        cout << "Personality: Copy constructor" << endl;
    }
    
    // Деструктор
    ~Personality() {
        cout << "Personality: Destructor" << endl;
    }

    // Методы доступа
    string getName() { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string activityYear) { this->activityYear = activityYear; }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }
};

class Encyclopedia {
    string title;
    int publicationYear;
    Personality* personalities[10];  // массив указателей
    int count;
public:
    // Конструкторы
    Encyclopedia() {
        title = "No title";
        publicationYear = 0;
        count = 0;
        // Инициализируем указатели нулями
        for (int i = 0; i < 10; i++) {
            personalities[i] = nullptr;
        }
        cout << "Encyclopedia: Default constructor" << endl;
    }
    
    Encyclopedia(string _title, int _year) {
        title = _title;
        publicationYear = _year;
        count = 0;
        for (int i = 0; i < 10; i++) {
            personalities[i] = nullptr;
        }
        cout << "Encyclopedia: Constructor with parameters" << endl;
    }
    
    Encyclopedia(Encyclopedia &enc) {
        title = enc.title;
        publicationYear = enc.publicationYear;
        count = enc.count;
        for (int i = 0; i < 10; i++) {
            personalities[i] = nullptr;
        }
        cout << "Encyclopedia: Copy constructor" << endl;
    }
    
    // Деструктор
    ~Encyclopedia() {
        // Удаляем только те объекты, которые были созданы
        for (int i = 0; i < count; i++) {
            if (personalities[i] != nullptr) {
                delete personalities[i];
            }
        }
        cout << "Encyclopedia: Destructor" << endl;
    }

    // Методы доступа
    string getTitle() { return title; }
    void setTitle(string title) { this->title = title; }

    int getPublicationYear() { return publicationYear; }
    void setPublicationYear(int year) { publicationYear = year; }

    void addPersonality(Personality* p) {  // принимаем указатель
        if (count < 10) {
            personalities[count] = p;
            count++;
        }
    }

    Personality* getPersonality(int index) {
        if (index < count) return personalities[index];
        return nullptr;
    }
    
    int getCount() { return count; }
    
    // Новые методы по заданию
    void addNewPersonality(string name, string year, string desc) {
        if (count < 10) {
            Personality* p = new Personality(name, year, desc); // создаем через new
            personalities[count] = p;
            count++;
        }
    }
    
    void addCopiedPersonality(Personality &p, int numCopies) {
        for (int i = 0; i < numCopies && count < 10; i++) {
            Personality* newP = new Personality(p); // конструктор копирования
            personalities[count] = newP;
            count++;
        }
    }
    
    void displayAll() {
        cout << "Encyclopedia: " << title << " (" << publicationYear << ")" << endl;
        cout << "Personalities count: " << count << endl;
        for (int i = 0; i < count; i++) {
            if (personalities[i] != nullptr) {
                cout << i << ": " << personalities[i]->getName() << " - " 
                     << personalities[i]->getActivityYear() << endl;
            }
        }
    }
};

int main() {
    cout << "=== Step 1: Creating encyclopedia objects ===" << endl;
    Encyclopedia enc1;
    Encyclopedia enc2("World History", 2020);
    Encyclopedia enc3(enc2);
    
    cout << "\n=== Step 2: Creating personality objects ===" << endl;
    Personality p1;
    Personality p2("Alexander the Great", "356-323 BC", "Macedonian king and conqueror");
    Personality p3(p2);
    
    cout << "\n=== Step 3: Using new methods ===" << endl;
    enc1.addNewPersonality("Julius Caesar", "100-44 BC", "Roman dictator");
    enc1.addCopiedPersonality(p2, 2);
    
    cout << "\n=== Step 4: Display data ===" << endl;
    enc1.displayAll();
    
    cout << "\n=== End of program ===" << endl;
    return 0;
}