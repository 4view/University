package Lab4;

import javax.swing.*;
import java.awt.*;

public class Lab4 extends JFrame {

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            JFrame frame = new JFrame("Лабораторная работа №4");
            frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
            frame.setSize(330, 270);

            /// TOP PANEL
            JPanel topPanel = new JPanel();
            topPanel.setBorder(BorderFactory.createEmptyBorder(2,2,2,2));

            JLabel label1 = new JLabel("Метка 1");
            JSlider slider = new JSlider(0, 100, 50);
            JLabel label2 = new JLabel("Метка 2");

            topPanel.add(label1, BorderLayout.WEST);
            topPanel.add(slider, BorderLayout.CENTER);
            topPanel.add(label2, BorderLayout.EAST);

            /// CENTER PANEL
            JPanel centerPanel = new JPanel();
            centerPanel.setLayout(new BoxLayout(centerPanel, BoxLayout.X_AXIS));  
            centerPanel.setBorder(null);
            
            JTextField textField = new JTextField();
            textField.setFont(new Font("Arial", Font.PLAIN, 14));
            textField.setMaximumSize(new Dimension(330, 20));

            centerPanel.add(textField, BorderLayout.CENTER);

            ///BUTTON PANEL
            JPanel buttonPanel = new JPanel(new GridLayout(4, 5, 5, 5));

            String[] buttonLabels = {
                "1", "2", "3", "", "+",
                "4", "5", "6", "", "-",
                "7", "8", "9", "", "=",
                "", "0", "", ""
            };

            for (String label : buttonLabels)
            {
                if (label.isEmpty())
                    buttonPanel.add(new Label());
                else
                {
                    JButton button = new JButton(label);
                    button.setFont(new Font("Arial", Font.BOLD, 14));
                    button.setPreferredSize(new Dimension(60,40));
                    buttonPanel.add(button);
                }
            }

            frame.add(topPanel, BorderLayout.NORTH);
            frame.add(centerPanel, BorderLayout.CENTER);
            frame.add(buttonPanel, BorderLayout.SOUTH);

            frame.setVisible(true);
        });
    }
}