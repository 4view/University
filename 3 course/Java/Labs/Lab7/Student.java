import java.io.Serializable;

public class Student implements Serializable {
    private static final long serialVersionUID = 1L;
    
    private String name;
    private int age;
    private String address;
    
    public Student(String name, int age, String address) {
        this.name = name;
        this.age = age;
        this.address = address;
    }
    
    public String getName() { return name; }
    public int getAge() { return age; }
    public String getAddress() { return address; }
    
    public String toFileString() {
        return name + "|" + age + "|" + address;
    }
    
    public static Student fromFileString(String line) {
        String[] parts = line.split("\\|", 3);
        if (parts.length == 3) {
            String name = parts[0];
            int age = Integer.parseInt(parts[1]);
            String address = parts[2];
            return new Student(name, age, address);
        }
        return null;
    }
    
    @Override
    public String toString() {
        return name + " (" + age + " лет) - " + address;
    }
}