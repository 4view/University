#include <iostream>
#include <string>

using namespace std;

class Personality
{
    // свойства класса
    string name;
    string activityYear;
    string description;

public:
    // методы set устанавливающие свойства и get для получения значений
    string getName() { return name; }
    void setName(string name) { this->name = name; }

    string getActivityYear() { return activityYear; }
    void setActivityYear(string activityYear) { this->activityYear = activityYear; }

    string getDescription() { return description; }
    void setDescription(string description) { this->description = description; }

    //метод заполняющий свойства через консольный ввод
    void setProperties()
    {
        string str;
        cout << "Enter name:" << endl;
        getline(cin, str);
        setName(str);
        cout << "Enter year of activity:" << endl;
        getline(cin, str);
        setActivityYear(str);
        cout << "Enter description:" << endl;
        getline(cin, str);
        setDescription(str);
    }

    //Метод заполняющий свойства через переданные в параметры значения
    void setProperties(string name, string year, string desc)
    {
        setName(name);
        setActivityYear(year);
        setDescription(desc);
    }
};

class Encyclopedia
{
    // свойства класса
    string title;
    int publicationYear;
    Personality personalities[10];

public:
    // методы set устанавливающие свойства и get для получения значений 
    string getTitle() { return title; } 
    void setTitle(string title) { this->title = title; }

    int getPublicationYear() { return publicationYear; }
    void setPublicationYear(int year) { publicationYear = year; }

    void addPersonality(Personality p, int index)
    {
        personalities[index] = p;
    }

    Personality getPersonality(int index)
    {
        return personalities[index];
    }
};

int main()
{
    Encyclopedia enc;
    string str;
    int year;

    cout << "Enter encyclopedia title:" << endl;
    getline(cin, str);
    enc.setTitle(str);

    cout << "Enter publication year:" << endl;
    cin >> year;
    enc.setPublicationYear(year);
    cin.ignore();

    for (int i = 0; i < 3; i++)
    {
        cout << "Enter data for personality " << i << ":" << endl;
        Personality p;
        if (i % 2 == 0)
        {
            p.setProperties(); // ввод с клавиатуры
        }
        else
        {
            p.setProperties("Default Name", "Default Year", "Default Description"); // Устанавливаем через параметры
        }
        enc.addPersonality(p, i);
    }

    // Вывод данных
    cout << "\n=== Encyclopedia ===" << endl;
    cout << "Title: " << enc.getTitle() << endl;
    cout << "Publication Year: " << enc.getPublicationYear() << endl;
    cout << "\n=== Personalities ===" << endl;
    for (int i = 0; i < 3; i++)
    {
        Personality p = enc.getPersonality(i);
        cout << "Name: " << p.getName() << endl;
        cout << "Year of Activity: " << p.getActivityYear() << endl;
        cout << "Description: " << p.getDescription() << endl;
        cout << "---------------------" << endl;
    }

    return 0;
}