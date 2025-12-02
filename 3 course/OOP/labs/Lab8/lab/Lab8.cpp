#include <iostream>
#include <string>
#include <stdexcept>

using namespace std;

// Пользовательские классы исключений
class PersonalityException : public invalid_argument {
public:
    PersonalityException(const string& message) : invalid_argument(message) {}
};

class EncyclopediaException : public runtime_error {
public:
    EncyclopediaException(const string& message) : runtime_error(message) {}
};

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
        if (_name.empty()) {
            throw PersonalityException("Personality name cannot be empty");
        }
        if (_year.empty()) {
            throw PersonalityException("Activity year cannot be empty");
        }
        name = _name;
        activityYear = _year;
        description = _desc;
    }
    
    virtual ~Personality() {}

    string getName() { return name; }
    void setName(string name) { 
        if (name.empty()) {
            throw PersonalityException("Name cannot be empty");
        }
        this->name = name; 
    }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string year) { 
        if (year.empty()) {
            throw PersonalityException("Activity year cannot be empty");
        }
        this->activityYear = year; 
    }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }
    
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
    
    void print() override {
        cout << "PAINTER || " << getName() << " | " << getActivityYear() 
             << " | Style: " << artStyle << endl;
    }
};

template<class T>
class MyArray {
    T* arr;
    int count;
    int size;
public:
    MyArray(int n) {
        if (n <= 0) {
            throw invalid_argument("Array size must be positive");
        }
        arr = new T[n];
        size = n;
        count = 0;
    }
    
    ~MyArray() {
        delete[] arr;
    }
    
    void addItem(T obj) {
        try {
            if (obj == nullptr) {
                throw PersonalityException("Cannot add null object to array");
            }
            if (count >= size) {
                throw out_of_range("Array is full");
            }
            arr[count] = obj;
            count++;
        }
        catch (const PersonalityException& e) {
            cout << "Error in addItem: " << e.what() << endl;
            throw; // перевыбрасываем исключение
        }
    }
    
    T getItem(int index) {
        if (index < 0 || index >= count) {
            throw out_of_range("Index " + to_string(index) + " is out of range");
        }
        return arr[index];
    }
    
    void printAll() {
        cout << "Array contents (" << count << " items):" << endl;
        for (int i = 0; i < count; i++) {
            cout << "  [" << i << "]: ";
            arr[i]->print();
        }
    }
};

void demonstrateExceptions() {
    cout << "\n=== 1. Exception with primitive types ===" << endl;
    try {
        int x;
        cout << "Enter a number (0 for exception): ";
        cin >> x;
        
        if (x == 0) {
            throw string("Division by zero attempted");
        }
        if (x < 0) {
            throw x; // бросаем int
        }
        
        cout << "100 / " << x << " = " << (100 / x) << endl;
    }
    catch (const string& str) {
        cout << "Caught string exception: " << str << endl;
    }
    catch (int i) {
        cout << "Caught int exception: " << i << endl;
    }
    catch (...) {
        cout << "Caught unknown exception" << endl;
    }
    
    cout << "\n=== 2. Exception with standard types ===" << endl;
    try {
        MyArray<Personality*> array(-5); // неверный размер
    }
    catch (const invalid_argument& e) {
        cout << "Invalid argument: " << e.what() << endl;
    }
    
    cout << "\n=== 3. Exception with custom types ===" << endl;
    try {
        // Попытка создать личность с пустым именем
        Painter* p = new Painter("", "1900-2000", "Test", "Test");
    }
    catch (const PersonalityException& e) {
        cout << "Personality error: " << e.what() << endl;
    }
    
    cout << "\n=== 4. Nested exception handling ===" << endl;
    try {
        MyArray<Personality*> personArray(3);
        
        Painter* p1 = new Painter("Van Gogh", "1853-1890", "Painter", "Post-Impressionism");
        Painter* p2 = nullptr; // null pointer
        
        personArray.addItem(p1);
        personArray.addItem(p2); // вызовет исключение
        
        personArray.printAll();
        
        delete p1;
    }
    catch (const exception& e) {
        cout << "Standard exception caught: " << e.what() << endl;
    }
    
    cout << "\n=== 5. Local exception handling ===" << endl;
    MyArray<Personality*> localArray(2);
    try {
        Painter* p1 = new Painter("Monet", "1840-1926", "Painter", "Impressionism");
        localArray.addItem(p1);
        
        // Попытка добавить слишком много элементов
        Painter* p2 = new Painter("Renoir", "1841-1919", "Painter", "Impressionism");
        localArray.addItem(p2);
        
        Painter* p3 = new Painter("Degas", "1834-1917", "Painter", "Impressionism");
        localArray.addItem(p3); // вызовет исключение
    }
    catch (const out_of_range& e) {
        cout << "Local handling: " << e.what() << endl;
        // Обрабатываем локально, не передаем выше
    }
    
    localArray.printAll();
}

int main() {
    cout << "=== Demonstration of exception handling ===" << endl;
    
    try {
        demonstrateExceptions();
    }
    catch (const PersonalityException& e) {
        cout << "Main caught PersonalityException: " << e.what() << endl;
    }
    catch (const EncyclopediaException& e) {
        cout << "Main caught EncyclopediaException: " << e.what() << endl;
    }
    catch (const exception& e) {
        cout << "Main caught standard exception: " << e.what() << endl;
    }
    catch (...) {
        cout << "Main caught unknown exception" << endl;
    }
    
    cout << "\n=== Program completed successfully ===" << endl;
    return 0;
}