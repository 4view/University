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

    // добавлены const методы
    string getName() const { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() const { return activityYear; }
    void setActivityYear(string year) { this->activityYear = year; }

    string getDescription() const { return description; }
    void setDescription(string description) { this->description = description; }
    
    virtual void print() const = 0;  // : const чисто виртуальный метод
    
    // Операторы сравнения должны быть const
    bool operator==(const Personality& other) const {
        return name == other.name;
    }
    
    bool operator<(const Personality& other) const {
        return name < other.name;
    }
    
    bool operator>(const Personality& other) const {
        return name > other.name;
    }
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
    
    // const методы
    string getArtStyle() const { return artStyle; }
    void setArtStyle(string style) { artStyle = style; }
    
    void print() const override {
        cout << "PAINTER || " << getName() << " | " << getActivityYear() 
             << " | Style: " << artStyle << endl;
    }
    
    // операторы сравнения для Painter
    bool operator<(const Painter& other) const {
        return getName() < other.getName();
    }
    
    bool operator>(const Painter& other) const {
        return getName() > other.getName();
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
    
    // const методы
    string getLiteraryGenre() const { return literaryGenre; }
    void setLiteraryGenre(string genre) { literaryGenre = genre; }
    
    void print() const override {
        cout << "WRITER  || " << getName() << " | " << getActivityYear() 
             << " | Genre: " << literaryGenre << endl;
    }
};

// Шаблон класса MyArray
template<class T>
class MyArray {
    T* arr;
    int count;
    int size;
public:
    // Конструктор с параметром размера
    MyArray(int n) {
        arr = new T[n];
        size = n;
        count = 0;
        cout << "MyArray created with size: " << size << endl;
    }
    
    // Деструктор
    ~MyArray() {
        delete[] arr;
        cout << "MyArray destroyed" << endl;
    }
    
    // Метод добавления элемента
    void addItem(T obj) {
        if (count < size) {
            arr[count] = obj;
            count++;
        } else {
            cout << "Array is full!" << endl;
        }
    }
    
    // Метод получения элемента по индексу
    T getItem(int index) const {
        if (index >= 0 && index < count) {
            return arr[index];
        }
        throw out_of_range("Index out of range");
    }
    
    // Метод поиска элемента
    int findItem(const T& obj) const {
        for (int i = 0; i < count; i++) {
            if (arr[i] == obj) {
                return i;
            }
        }
        return -1;
    }
    
    // Функция поиска минимального элемента
    T min() const {
        if (count == 0) throw runtime_error("Array is empty");
        
        T minVal = arr[0];
        for (int i = 1; i < count; i++) {
            if (arr[i] < minVal) {
                minVal = arr[i];
            }
        }
        return minVal;
    }
    
    // Функция поиска максимального элемента
    T max() const {
        if (count == 0) throw runtime_error("Array is empty");
        
        T maxVal = arr[0];
        for (int i = 1; i < count; i++) {
            if (arr[i] > maxVal) {
                maxVal = arr[i];
            }
        }
        return maxVal;
    }
    
    void printAll() const {
        cout << "Array contents (" << count << " items):" << endl;
        for (int i = 0; i < count; i++) {
            cout << "  [" << i << "]: ";
            if constexpr (is_pointer<T>::value) {
                // Для указателей вызываем print()
                arr[i]->print();
            } else {
                // Для значений используем оператор <<
                cout << arr[i] << endl;
            }
        }
    }
};

int main()
{
    cout << "=== Demonstration of class templates ===" << endl;
    
    cout << "\n=== 1. MyArray with int ===" << endl;
    MyArray<int> intArray(5);
    intArray.addItem(10);
    intArray.addItem(5);
    intArray.addItem(20);
    intArray.addItem(15);
    intArray.printAll();
    cout << "Min: " << intArray.min() << ", Max: " << intArray.max() << endl;
    
    cout << "\n=== 2. MyArray with char ===" << endl;
    MyArray<char> charArray(4);
    charArray.addItem('d');
    charArray.addItem('a');
    charArray.addItem('c');
    charArray.addItem('b');
    charArray.printAll();
    cout << "Min: " << charArray.min() << ", Max: " << charArray.max() << endl;
    
    cout << "\n=== 3. MyArray with Personality pointers ===" << endl;
    MyArray<Personality*> personArray(4);
    
    Painter* p1 = new Painter("Leonardo", "1452-1519", "Renaissance", "Renaissance");
    Painter* p2 = new Painter("Vincent", "1853-1890", "Post-impressionist", "Post-Impressionism");
    Painter* p3 = new Painter("Pablo", "1881-1973", "Cubist", "Cubism");
    Painter* p4 = new Painter("Claude", "1840-1926", "Impressionist", "Impressionism");
    
    personArray.addItem(p1);
    personArray.addItem(p2);
    personArray.addItem(p3);
    personArray.addItem(p4);
    
    personArray.printAll();
    
    // Поиск элемента
    int index = personArray.findItem(p2);
    cout << "Found p2 at index: " << index << endl;
    
    // Очистка памяти
    delete p1;
    delete p2;
    delete p3;
    delete p4;
    
    return 0;
}