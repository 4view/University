#include <iostream>
#include <string>

using namespace std;

class Personality {
    string name;
    string activityYear;
    string description;
    static int countPersonality; // статический счетчик
public:
    Personality() {
        name = "No name";
        activityYear = "No year";
        description = "No description";
        countPersonality++;
        cout << "Personality created. Total: " << countPersonality << endl;
    }
    
    Personality(string _name, string _year, string _desc) {
        name = _name;
        activityYear = _year;
        description = _desc;
        countPersonality++;//Инкрементируем счетчик
        cout << "Personality created. Total: " << countPersonality << endl;
    }
    
    virtual ~Personality() {
        countPersonality--;//Декрементируем счетчик
        cout << "Personality destroyed. Total: " << countPersonality << endl;
    }

    string getName() { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string year) { this->activityYear = year; }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }
    
    virtual void print() = 0;
    
    // Статический метод
    static int getCount() {
        return countPersonality;
    }
};

// Инициализация статической переменной
int Personality::countPersonality = 0;

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
    
    void print() override {
        cout << "PAINTER || " << getName() << " | " << getActivityYear() 
             << " | Style: " << artStyle << endl;
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
    
    void print() override {
        cout << "WRITER  || " << getName() << " | " << getActivityYear() 
             << " | Genre: " << literaryGenre << endl;
    }
};

class Encyclopedia {
    string title;
    int publicationYear;
    Personality* personalities[10];
    int count;
    static int countInArrays; // статический счетчик в массивах
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
            countInArrays--;
        }
    }

    void addPersonality(Personality* p) {
        if (count < 10) {
            personalities[count] = p;
            count++;
            countInArrays++;
        }
    }

    void showAll() {
        cout << "\n=== ENCYCLOPEDIA: " << title << " (" << publicationYear << ") ===" << endl;
        for (int i = 0; i < count; i++) {
            personalities[i]->print();
        }
        cout << "=====================================" << endl;
    }
    
    // Статический метод
    static int getCountInArrays() {
        return countInArrays;
    }
};

// Инициализация статической переменной
int Encyclopedia::countInArrays = 0;

int main() {
    cout << "=== Step 1: Check counts before creating objects ===" << endl;
    cout << "Total personalities: " << Personality::getCount() << endl;
    cout << "Personalities in arrays: " << Encyclopedia::getCountInArrays() << endl;

    cout << "\n=== Step 2: Create objects and add to encyclopedia ===" << endl;
    Encyclopedia enc("Great Personalities", 2023);
    
    enc.addPersonality(new Painter("Leonardo da Vinci", "1452-1519", 
                                  "Renaissance genius", "Renaissance"));
    enc.addPersonality(new Painter("Vincent van Gogh", "1853-1890", 
                                  "Post-impressionist", "Post-Impressionism"));
    enc.addPersonality(new Writer("William Shakespeare", "1564-1616", 
                                 "English playwright", "Drama"));
    enc.addPersonality(new Writer("Leo Tolstoy", "1828-1910", 
                                 "Russian writer", "Realism"));

    cout << "\n=== Step 3: Check counts after creating objects ===" << endl;
    cout << "Total personalities: " << Personality::getCount() << endl;
    cout << "Personalities in arrays: " << Encyclopedia::getCountInArrays() << endl;

    enc.showAll();

    return 0;
}