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
        cout << "Personality: Default constructor" << endl;
    }
    
    Personality(string _name, string _year, string _desc) {
        name = _name;
        activityYear = _year;
        description = _desc;
        cout << "Personality: Constructor with parameters" << endl;
    }
    
    virtual ~Personality() {
        cout << "Personality: Destructor" << endl;
    }

    string getName() { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string year) { this->activityYear = year; }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }
    
    virtual void print() = 0;
};

class Painter : public Personality {
    string artStyle;
public:
    Painter() : Personality() {
        artStyle = "No style";
        cout << "Painter: Default constructor" << endl;
    }
    
    Painter(string name, string year, string desc, string style) 
        : Personality(name, year, desc) {
        artStyle = style;
        cout << "Painter: Constructor with parameters" << endl;
    }
    
    ~Painter() {
        cout << "Painter: Destructor" << endl;
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
        cout << "Writer: Default constructor" << endl;
    }
    
    Writer(string name, string year, string desc, string genre) 
        : Personality(name, year, desc) {
        literaryGenre = genre;
        cout << "Writer: Constructor with parameters" << endl;
    }
    
    ~Writer() {
        cout << "Writer: Destructor" << endl;
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
    
public:
    // Конструктор
    Encyclopedia() : title("No title"), publicationYear(0), count(0) {
        for (int i = 0; i < 10; i++) {
            personalities[i] = nullptr;
        }
        cout << "Encyclopedia: Default constructor" << endl;
    }
    
    Encyclopedia(string _title, int _year) : title(_title), publicationYear(_year), count(0) {
        for (int i = 0; i < 10; i++) {
            personalities[i] = nullptr;
        }
        cout << "Encyclopedia: Constructor with parameters" << endl;
    }
    
    // УДАЛЯЕМ запрет на копирование и добавляем правильные конструкторы:
    
    // Конструктор копирования (глубокое копирование)
    Encyclopedia(const Encyclopedia& other) : title(other.title), publicationYear(other.publicationYear), count(other.count) {
        cout << "Encyclopedia: Copy constructor" << endl;
        for (int i = 0; i < 10; i++) {
            if (i < count && other.personalities[i] != nullptr) {
                // Создаем новые объекты для копирования
                personalities[i] = new Writer(); // или другой тип, но для простоты используем Writer
                *personalities[i] = *other.personalities[i]; // копируем данные
            } else {
                personalities[i] = nullptr;
            }
        }
    }
    
    // Оператор присваивания
    Encyclopedia& operator=(const Encyclopedia& other) {
        if (this != &other) {
            // Очищаем текущие данные
            for (int i = 0; i < count; i++) {
                if (personalities[i] != nullptr) {
                    delete personalities[i];
                }
            }
            
            // Копируем новые данные
            title = other.title;
            publicationYear = other.publicationYear;
            count = other.count;
            
            for (int i = 0; i < 10; i++) {
                if (i < count && other.personalities[i] != nullptr) {
                    personalities[i] = new Writer();
                    *personalities[i] = *other.personalities[i];
                } else {
                    personalities[i] = nullptr;
                }
            }
        }
        return *this;
    }
    
    // Деструктор
    ~Encyclopedia() {
        cout << "Encyclopedia: Destructor - cleaning up " << count << " personalities" << endl;
        for (int i = 0; i < count; i++) {
            if (personalities[i] != nullptr) {
                cout << "Deleting personality " << i << endl;
                delete personalities[i];
                personalities[i] = nullptr;
            }
        }
        count = 0;
    }

    string getTitle() { return title; }
    void setTitle(string title) { this->title = title; }

    int getPublicationYear() { return publicationYear; }
    void setPublicationYear(int year) { publicationYear = year; }

    // Безопасное добавление личности
    void addPersonality(Personality* p) {
        if (count < 10 && p != nullptr) {
            personalities[count] = p;
            count++;
            cout << "Added personality. Total: " << count << endl;
        } else {
            cout << "Cannot add personality - array full or null pointer" << endl;
            if (p != nullptr) {
                delete p; // важно удалить, если не добавили
            }
        }
    }

    // Оператор + 
    Encyclopedia& operator+(Personality* p) {
        addPersonality(p);
        return *this;
    }
    
    // Префиксный ++
    Encyclopedia& operator++() {
        if (count < 10) {
            addPersonality(new Painter());
        }
        return *this;
    }
    
    // Постфиксный ++ 
    Encyclopedia operator++(int) {
        // Сохраняем только основные данные, не копируем весь массив
        Encyclopedia temp;
        temp.title = this->title;
        temp.publicationYear = this->publicationYear;
        temp.count = this->count;
        
        // Добавляем новую личность в текущий объект
        if (count < 10) {
            addPersonality(new Writer());
        }
        
        return temp;
    }
    
    // Оператор [] с защитой
    Personality* operator[](int index) {
        if (index >= 0 && index < count) {
            return personalities[index];
        }
        cout << "Warning: Index " << index << " out of bounds!" << endl;
        return nullptr;
    }

    int getCount() const { return count; }

    void showAll() {
        cout << "\n=== ENCYCLOPEDIA: " << title << " (" << publicationYear << ") ===" << endl;
        cout << "Total personalities: " << count << endl;
        
        for (int i = 0; i < count; i++) {
            cout << i << ": ";
            
            if (personalities[i] != nullptr) {
                try {
                    personalities[i]->print();
                } catch (...) {
                    cout << "ERROR: Cannot print personality " << i << endl;
                }
            } else {
                cout << "ERROR: Personality " << i << " is null!" << endl;
            }
        }
        cout << "=====================================" << endl;
    }
};

// Глобальный оператор <<
ostream& operator<<(ostream& os, Encyclopedia& enc) {
    os << "Encyclopedia: \"" << enc.getTitle() << "\" (" << enc.getPublicationYear() 
       << "), Persons: " << enc.getCount();
    return os;
}

int main() {
    cout << "=== SAFE Demonstration of overloaded operators ===" << endl;
    
    Encyclopedia enc("Great Personalities", 2023);
    
    Painter* p1 = new Painter("Leonardo da Vinci", "1452-1519", "Renaissance genius", "Renaissance");
    Writer* w1 = new Writer("William Shakespeare", "1564-1616", "English playwright", "Drama");
    
    cout << "\n=== Using operator+ ===" << endl;
    enc + p1;
    enc + w1;
    enc.showAll();
    
    cout << "\n=== Using prefix operator++ ===" << endl;
    ++enc;
    enc.showAll();
    
    cout << "\n=== Using postfix operator++ ===" << endl;
    enc++;
    enc.showAll();
    
    cout << "\n=== Using operator<< ===" << endl;
    cout << enc << endl;
    
    cout << "\n=== End of program ===" << endl;
    
    return 0;
}