package Lab5;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class Lab5 extends JFrame implements ActionListener {

    private JTextField textField;
    private JButton plusButton, minusButton, equalsButton;
    private JButton[] numberButtons;
    private JSlider slider;

    private Integer firstNumber = 0;
    private String operation = "";
    private boolean isNewCalculation = true;

    public Lab5() {
        setTitle("Лабораторная работа №5");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(350, 270);

        numberButtons = new JButton[10];

        // TOP PANEL
        JPanel topPanel = new JPanel();
        topPanel.setBorder(BorderFactory.createEmptyBorder(2, 2, 2, 2));


        JLabel label1 = new JLabel("Метка 1");
        slider = new JSlider(0, 100, 50);
        slider.addChangeListener(e -> {
            int value = slider.getValue();
            label1.setText("Метка:" + value);
        });
        JLabel label2 = new JLabel("Метка 2");

        topPanel.add(label1);
        topPanel.add(slider);
        topPanel.add(label2);

        

        // CENTER PANEL
        JPanel centerPanel = new JPanel();
        centerPanel.setLayout(new BoxLayout(centerPanel, BoxLayout.Y_AXIS));

        textField = new JTextField("0");
        textField.setFont(new Font("Arial", Font.BOLD, 14));
        textField.setHorizontalAlignment(JTextField.RIGHT);
        textField.setMaximumSize(new Dimension(330, 20));

        centerPanel.add(topPanel);
        centerPanel.add(textField);

        // BUTTON PANEL
        JPanel buttonPanel = new JPanel(new GridLayout(4, 5, 5, 5));

        String[] buttonLabels = {
                "1", "2", "3", "", "+",
                "4", "5", "6", "", "-",
                "7", "8", "9", "", "=",
                "", "0", "", ""
        };

        for (String label : buttonLabels) {
            if (label.isEmpty()) {
                buttonPanel.add(new Label());
            } else {
                JButton button = new JButton(label);
                button.setFont(new Font("Arial", Font.BOLD, 14));
                button.setPreferredSize(new Dimension(60, 40));
                button.addActionListener(this);

                if (label.equals("+")) {
                    plusButton = button;
                } else if (label.equals("-")) {
                    minusButton = button;
                } else if (label.equals("=")) {
                    equalsButton = button;
                } else if (label.matches("\\d")) {
                    int digit = Integer.parseInt(label);
                    numberButtons[digit] = button;
                    if (digit == 0) {
                        numberButtons[0] = button;
                    }
                }

                buttonPanel.add(button);
            }
        }

        add(centerPanel, BorderLayout.NORTH);
        add(buttonPanel, BorderLayout.CENTER);

        setupKeyboardListener();
        setVisible(true);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        Object source = e.getSource();

        for (int i = 0; i <= 9; i++) {
            if (source == numberButtons[i]) {
                if (isNewCalculation || textField.getText().equals("0")) {
                    textField.setText(String.valueOf(i));
                    isNewCalculation = false;
                } else {
                    textField.setText(textField.getText() + i);
                }
                return;
            }
        }

        if (source == plusButton) {
            performOperation("+");
        } else if (source == minusButton) {
            performOperation("-");
        } else if (source == equalsButton) {
            calculateResult();
        }
    }

    private void performOperation(String op) {
        try {
            firstNumber = Integer.parseInt(textField.getText());
            operation = op;
            isNewCalculation = true;
        } catch (NumberFormatException ex) {
            textField.setText("Ошибка");
            isNewCalculation = true;
        }
    }

    private void calculateResult() {
        if (operation.isEmpty())
            return;

        try {
            Integer secondNumber = Integer.parseInt(textField.getText());
            Integer result = 0;

            switch (operation) {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
            }

            textField.setText(String.valueOf(result));
            operation = "";
            isNewCalculation = true;

        } catch (NumberFormatException ex) {
            textField.setText("Ошибка");
            isNewCalculation = true;
        }
    }

    private void setupKeyboardListener() {
        textField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                if (e.getKeyCode() == KeyEvent.VK_ENTER) {
                    calculateResult();
                } else if (e.getKeyCode() == KeyEvent.VK_ESCAPE) {
                    textField.setText("0");
                    firstNumber = 0;
                    operation = "";
                    isNewCalculation = true;
                }
            }
        });

        textField.requestFocusInWindow();
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            Lab5 window = new Lab5();
            window.setLocationRelativeTo(null);
        });
    }
}