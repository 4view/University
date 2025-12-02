#include <iostream>
#include <string>

using namespace std;

class Personality {
protected:  //protected для доступа в наследниках
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
    
    void print() { // метод вывода информации на консоль
        cout << "Personality: " << name << " (" << activityYear << ") - " << description << endl;
    }
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
    
    //Методы получения и установки значения 
    string getArtStyle() { return artStyle; }
    void setArtStyle(string style) { artStyle = style; }
    
    void print() { 
        cout << "Painter: " << getName() << " (" << getActivityYear() << ") - " 
             << getDescription() << " - Style: " << artStyle << endl;
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
    
    //Методы получения и установки значения 
    string getLiteraryGenre() { return literaryGenre; }
    void setLiteraryGenre(string genre) { literaryGenre = genre; }
    
    void print() {  
        cout << "Writer: " << getName() << " (" << getActivityYear() << ") - " 
             << getDescription() << " - Genre: " << literaryGenre << endl;
    }
};

int main() {
    cout << "========== Step 1: Create objects ==========" << endl;
    Personality p1("Leonardo da Vinci", "1452-1519", "Renaissance genius");
    Painter p2("Vincent van Gogh", "1853-1890", "Post-impressionist painter", "Post-Impressionism");
    Writer p3("William Shakespeare", "1564-1616", "English playwright", "Drama");
    
    cout << "\n========== Step 2: Array of base class POINTERS ==========" << endl;
    //массив указателей для избежания срезки
    Personality* arr1[3];
    arr1[0] = &p1;
    arr1[1] = &p2;  // Указатель на Painter
    arr1[2] = &p3;  // Указатель на Writer
    
    for (int i = 0; i < 3; i++) {
        arr1[i]->print();
    }
    
    cout << "\n========== Step 3: Array of derived class ==========" << endl;
    Painter painters[2] = {
        Painter("Pablo Picasso", "1881-1973", "Spanish painter", "Cubism"),
        Painter("Claude Monet", "1840-1926", "French painter", "Impressionism")
    };
    
    for (int i = 0; i < 2; i++) {
        painters[i].print();
    }
    
    cout << "\n========== End of program ==========" << endl;
    return 0;
}