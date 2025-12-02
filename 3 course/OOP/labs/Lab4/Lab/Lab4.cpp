#include <iostream>
#include <string>

using namespace std;

class Personality {
    string name;
    string activityYear;
    string description;
public:
    Personality() {
        name = "No name";
        activityYear = "No year";
        description = "No description";
    }
    
    Personality(string _name, string _year, string _desc) {
        name = _name;
        activityYear = _year;
        description = _desc;
    }
    
    virtual ~Personality() {}

    string getName() { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string year) { this->activityYear = year; }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }
    
    // Чисто виртуальный метод - делаем класс абстрактным
    virtual void print() = 0;
};

class Painter : public Personality {
    string artStyle;
public:
    Painter() : Personality() {
        artStyle = "No style";
    }
    
    Painter(string name, string year, string desc, string style) 
        : Personality(name, year, desc) {
        artStyle = style;
    }
    
    string getArtStyle() { return artStyle; }
    void setArtStyle(string style) { artStyle = style; }
    
    void print() override { // Базовый метод, реализация которого переписана в классе наследнике
        cout << "PAINTER || Name: " << getName() << " | Years: " << getActivityYear() 
             << " | Style: " << artStyle << " | Description: " << getDescription() << endl;
    }
};

class Writer : public Personality {
    string literaryGenre;
public:
    Writer() : Personality() {
        literaryGenre = "No genre";
    }
    
    Writer(string name, string year, string desc, string genre) 
        : Personality(name, year, desc) {
        literaryGenre = genre;
    }
    
    string getLiteraryGenre() { return literaryGenre; }
    void setLiteraryGenre(string genre) { literaryGenre = genre; }
    
    void print() override {// Базовый метод, реализация которого переписана в классе наследнике
        cout << "WRITER  || Name: " << getName() << " | Years: " << getActivityYear() 
             << " | Genre: " << literaryGenre << " | Description: " << getDescription() << endl;
    }
};

class Encyclopedia {
    string title;
    int publicationYear;
    Personality* personalities[10];
    int count;
public:
    Encyclopedia() {
        title = "No title";
        publicationYear = 0;
        count = 0;
    }
    
    Encyclopedia(string _title, int _year) {
        title = _title;
        publicationYear = _year;
        count = 0;
    }
    
    ~Encyclopedia() {
        for (int i = 0; i < count; i++) {
            delete personalities[i];
        }
    }

    string getTitle() { return title; }
    void setTitle(string title) { this->title = title; }

    int getPublicationYear() { return publicationYear; }
    void setPublicationYear(int year) { publicationYear = year; }

    void addPersonality(Personality* p) {
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
    
    void showAll() {
        cout << "\n=== ENCYCLOPEDIA: " << title << " (" << publicationYear << ") ===" << endl;
        for (int i = 0; i < count; i++) {
            personalities[i]->print();
        }
        cout << "=====================================" << endl;
    }
};

int main() {
    cout << "=== Step 1: Create objects of derived classes ===" << endl;
    Painter* p1 = new Painter("Leonardo da Vinci", "1452-1519", 
                             "Renaissance genius, painter of Mona Lisa", "Renaissance");
    Painter* p2 = new Painter("Vincent van Gogh", "1853-1890", 
                             "Post-impressionist painter", "Post-Impressionism");
    Writer* w1 = new Writer("William Shakespeare", "1564-1616", 
                           "English playwright and poet", "Drama");
    Writer* w2 = new Writer("Leo Tolstoy", "1828-1910", 
                           "Russian writer, author of War and Peace", "Realism");

    cout << "\n=== Step 2: Array of base class pointers ===" << endl;
    Personality* arr[4];
    arr[0] = p1;
    arr[1] = p2;
    arr[2] = w1;
    arr[3] = w2;

    for (int i = 0; i < 4; i++) {
        arr[i]->print(); // Полиморфный вызов!
    }

    cout << "\n=== Step 3: Working with Encyclopedia class ===" << endl;
    Encyclopedia enc("Great Personalities", 2023);
    enc.addPersonality(p1);
    enc.addPersonality(p2);
    enc.addPersonality(w1);
    enc.addPersonality(w2);
    
    enc.showAll();

    // Не удаляем объекты, так как они удаляются в деструкторе Encyclopedia
    return 0;
}