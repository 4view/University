import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.util.HashMap;

public class Lab7 extends JFrame {
    private DefaultListModel<String> listModel;
    private JList<String> studentList;
    private JTextField nameField, ageField, addressField;
    private JButton addButton, removeButton, clearButton, saveButton, loadButton;
    private HashMap<String, Student> studentMap;
    private static final String FILE_NAME = "students.txt";

    public Lab7() {
        super("Список группы студентов");
        studentMap = new HashMap<>();
        initComponents();
        loadFromFile();
    }

    private void initComponents() {
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLayout(new BorderLayout());

        // Панель списка студентов
        listModel = new DefaultListModel<>();
        studentList = new JList<>(listModel);
        JScrollPane scrollPane = new JScrollPane(studentList);
        scrollPane.setPreferredSize(new Dimension(200, 300));

        // Панель информации о студенте
        JPanel infoPanel = new JPanel(new GridLayout(3, 2, 5, 5));
        infoPanel.setBorder(BorderFactory.createTitledBorder("Данные студента"));

        infoPanel.add(new JLabel("Имя:"));
        nameField = new JTextField();
        infoPanel.add(nameField);

        infoPanel.add(new JLabel("Возраст:"));
        ageField = new JTextField();
        infoPanel.add(ageField);

        infoPanel.add(new JLabel("Адрес:"));
        addressField = new JTextField();
        infoPanel.add(addressField);

        // Панель кнопок
        JPanel buttonPanel = new JPanel(new FlowLayout());

        addButton = new JButton("Добавить");
        addButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                addStudent();
            }
        });

        removeButton = new JButton("Удалить");
        removeButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                removeStudent();
            }
        });

        clearButton = new JButton("Очистить");
        clearButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                clearList();
            }
        });

        saveButton = new JButton("Сохранить в файл");
        saveButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                saveToFile();
            }
        });

        loadButton = new JButton("Загрузить из файла");
        loadButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                loadFromFile();
            }
        });

        buttonPanel.add(addButton);
        buttonPanel.add(removeButton);
        buttonPanel.add(clearButton);
        buttonPanel.add(saveButton);
        buttonPanel.add(loadButton);

        studentList.addListSelectionListener(new ListSelectionListener() {
            @Override
            public void valueChanged(ListSelectionEvent e) {
                if (!e.getValueIsAdjusting()) {
                    displayStudentInfo();
                }
            }
        });

        add(scrollPane, BorderLayout.WEST);

        JPanel centerPanel = new JPanel(new BorderLayout());
        centerPanel.add(infoPanel, BorderLayout.NORTH);

        add(buttonPanel, BorderLayout.NORTH);
        add(centerPanel, BorderLayout.CENTER);

        pack();
        setLocationRelativeTo(null);
    }

    private void addStudent() {
        String name = nameField.getText().trim();
        String ageStr = ageField.getText().trim();
        String address = addressField.getText().trim();

        if (name.isEmpty() || ageStr.isEmpty() || address.isEmpty()) {
            JOptionPane.showMessageDialog(this, "Заполните все поля!", "Ошибка", JOptionPane.ERROR_MESSAGE);
            return;
        }

        try {
            int age = Integer.parseInt(ageStr);
            Student student = new Student(name, age, address);

            studentMap.put(name, student);

            if (!listModel.contains(name)) {
                listModel.addElement(name);
            }

            clearFields();

        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(this, "Возраст должен быть числом!", "Ошибка", JOptionPane.ERROR_MESSAGE);
        }
    }

    private void removeStudent() {
        String selected = studentList.getSelectedValue();
        if (selected != null) {
            studentMap.remove(selected);

            listModel.removeElement(selected);

            clearFields();
        }
    }

    private void clearList() {
        int confirm = JOptionPane.showConfirmDialog(this,
                "Очистить весь список?", "Подтверждение",
                JOptionPane.YES_NO_OPTION);

        if (confirm == JOptionPane.YES_OPTION) {
            studentMap.clear();
            listModel.clear();
            clearFields();
        }
    }

    private void displayStudentInfo() {
        String selected = studentList.getSelectedValue();
        if (selected != null) {
            Student student = studentMap.get(selected);
            if (student != null) {
                nameField.setText(student.getName());
                ageField.setText(String.valueOf(student.getAge()));
                addressField.setText(student.getAddress());
            }
        }
    }

    private void clearFields() {
        nameField.setText("");
        ageField.setText("");
        addressField.setText("");
    }

    private void saveToFile() {
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(FILE_NAME))) {
            for (Student student : studentMap.values()) {
                writer.write(student.toFileString());
                writer.newLine(); 
            }
            JOptionPane.showMessageDialog(this,
                    "Данные сохранены в файл!",
                    "Успех",
                    JOptionPane.INFORMATION_MESSAGE);
        } catch (IOException e) {
            JOptionPane.showMessageDialog(this,
                    "Ошибка сохранения: " + e.getMessage(),
                    "Ошибка",
                    JOptionPane.ERROR_MESSAGE);
        }
    }

    private void loadFromFile() {
        File file = new File(FILE_NAME);
        if (!file.exists()) {
            createSampleData();
            return;
        }

        try (BufferedReader reader = new BufferedReader(new FileReader(FILE_NAME))) {
            studentMap.clear();
            listModel.clear();

            String line;
            int count = 0;

            while ((line = reader.readLine()) != null) {
                if (!line.trim().isEmpty()) { // Пропускаем пустые строки
                    Student student = Student.fromFileString(line);
                    if (student != null) {
                        studentMap.put(student.getName(), student);
                        listModel.addElement(student.getName());
                        count++;
                    }
                }
            }

            if (count > 0) {
                JOptionPane.showMessageDialog(this,
                        "Загружено " + count + " студентов из файла!",
                        "Успех",
                        JOptionPane.INFORMATION_MESSAGE);
            } else {
                JOptionPane.showMessageDialog(this,
                        "Файл пуст! Созданы тестовые данные.",
                        "Информация",
                        JOptionPane.INFORMATION_MESSAGE);
                createSampleData();
            }

        } catch (IOException e) {
            JOptionPane.showMessageDialog(this,
                    "Ошибка загрузки: " + e.getMessage(),
                    "Ошибка",
                    JOptionPane.ERROR_MESSAGE);
        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(this,
                    "Ошибка в формате данных: некорректный возраст",
                    "Ошибка",
                    JOptionPane.ERROR_MESSAGE);
        }
    }

    private void createSampleData() {
        Student[] sampleStudents = {
                new Student("Иванов Иван", 20, "ул. Ленина, д. 10, г. Москва"),
                new Student("Петрова Анна", 21, "пр. Мира, д. 25, г. Санкт-Петербург"),
                new Student("Сидоров Алексей", 22, "ул. Гагарина, д. 5, г. Казань"),
                new Student("Кузнецова Мария", 19, "ул. Пушкина, д. 15, г. Екатеринбург"),
                new Student("Смирнов Дмитрий", 23, "ул. Чехова, д. 30, г. Новосибирск")
        };

        for (Student student : sampleStudents) {
            studentMap.put(student.getName(), student);
            listModel.addElement(student.getName());
        }
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            @Override
            public void run() {
                new Lab7().setVisible(true);
            }
        });
    }
}