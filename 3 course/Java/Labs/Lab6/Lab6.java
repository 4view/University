package Lab6;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class Lab6 extends JFrame {

    private DrawingPanel drawingPanel;
    private JSlider sizeSlider;

    private String currentShape = "NONE";
    private Color currentColor = Color.BLACK;
    private int shapeSize = 50;
    private int lineThickness = 2;
    private boolean isFilled = false;

    public Lab6() {
        setTitle("Лабораторная работа №6");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(800, 600);

        createMenuBar();

        JPanel controlPanel = new JPanel(new GridBagLayout());
        controlPanel.setBorder(BorderFactory.createTitledBorder("Управление"));
        controlPanel.setBackground(new Color(240, 240, 240));

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.insets = new Insets(5, 5, 5, 5);
        gbc.fill = GridBagConstraints.HORIZONTAL;

        gbc.gridx = 1;
        gbc.gridy = 0;
        controlPanel.add(new JLabel("Размер:"), gbc);

        gbc.gridx = 2;
        gbc.gridy = 0;
        gbc.gridwidth = 4;
        sizeSlider = new JSlider(10, 200, 50);
        sizeSlider.setMajorTickSpacing(50);
        sizeSlider.setMinorTickSpacing(10);
        sizeSlider.setPaintTicks(true);
        sizeSlider.setPaintLabels(true);
        sizeSlider.addChangeListener(e -> {
            shapeSize = sizeSlider.getValue();
            drawingPanel.repaint();
        });
        controlPanel.add(sizeSlider, gbc);

        gbc.gridx = 6;
        gbc.gridy = 0;
        gbc.gridwidth = 1;
        controlPanel.add(new JLabel("Толщина:"), gbc);

        gbc.gridx = 7;
        gbc.gridy = 0;
        JSlider thicknessSlider = new JSlider(1, 20, 2);
        thicknessSlider.setPaintTicks(true);
        thicknessSlider.setPaintLabels(true);
        thicknessSlider.addChangeListener(e -> {
            lineThickness = thicknessSlider.getValue();
            drawingPanel.repaint();
        });
        controlPanel.add(thicknessSlider, gbc);

        gbc.gridx = 0;
        gbc.gridy = 1;
        gbc.gridwidth = 2;
        JButton lineButton = new JButton("Линия");
        lineButton.addActionListener(e -> {
            currentShape = "LINE";
            drawingPanel.repaint();
        });
        controlPanel.add(lineButton, gbc);

        gbc.gridx = 2;
        gbc.gridy = 1;
        JButton rectButton = new JButton("Прямоугольник");
        rectButton.addActionListener(e -> {
            currentShape = "RECT";
            drawingPanel.repaint();
        });
        controlPanel.add(rectButton, gbc);

        gbc.gridx = 4;
        gbc.gridy = 1;
        JButton ovalButton = new JButton("Овал");
        ovalButton.addActionListener(e -> {
            currentShape = "OVAL";
            drawingPanel.repaint();
        });
        controlPanel.add(ovalButton, gbc);

        gbc.gridx = 6;
        gbc.gridy = 1;
        JButton triangleButton = new JButton("Треугольник");
        triangleButton.addActionListener(e -> {
            currentShape = "TRIANGLE";
            drawingPanel.repaint();
        });
        controlPanel.add(triangleButton, gbc);

        gbc.gridx = 10;
        gbc.gridy = 1;
        JButton fillButton = new JButton("Заливка");
        fillButton.addActionListener(e -> {
            isFilled = !isFilled;
            fillButton.setText(isFilled ? "Контур" : "Заливка");
            drawingPanel.repaint();
        });
        controlPanel.add(fillButton, gbc);

        gbc.gridx = 8;
        gbc.gridy = 1;
        JButton clearButton = new JButton("Очистить");
        clearButton.addActionListener(e -> drawingPanel.clearDrawing());
        controlPanel.add(clearButton, gbc);

        drawingPanel = new DrawingPanel();
        drawingPanel.setBorder(BorderFactory.createTitledBorder("Область для рисования"));
        drawingPanel.setBackground(Color.WHITE);

        add(controlPanel, BorderLayout.NORTH);
        add(drawingPanel, BorderLayout.CENTER);

        setVisible(true);
    }

    private void createMenuBar() {
        JMenuBar menuBar = new JMenuBar();

        JMenu shapesMenu = new JMenu("Фигуры");

        JMenuItem lineItem = new JMenuItem("Линия");
        lineItem.addActionListener(e -> {
            currentShape = "LINE";
            drawingPanel.repaint();
        });
        shapesMenu.add(lineItem);

        JMenuItem rectItem = new JMenuItem("Прямоугольник");
        rectItem.addActionListener(e -> {
            currentShape = "RECT";
            drawingPanel.repaint();
        });
        shapesMenu.add(rectItem);

        JMenuItem ovalItem = new JMenuItem("Овал");
        ovalItem.addActionListener(e -> {
            currentShape = "OVAL";
            drawingPanel.repaint();
        });
        shapesMenu.add(ovalItem);

        JMenuItem triangleItem = new JMenuItem("Треугольник");
        triangleItem.addActionListener(e -> {
            currentShape = "TRIANGLE";
            drawingPanel.repaint();
        });
        shapesMenu.add(triangleItem);

        JMenuItem roundRectItem = new JMenuItem("Скругленный прямоугольник");
        roundRectItem.addActionListener(e -> {
            currentShape = "ROUND_RECT";
            drawingPanel.repaint();
        });
        shapesMenu.add(roundRectItem);

        JMenu colorsMenu = new JMenu("Цвета");

        JMenuItem blackItem = new JMenuItem("Черный");
        blackItem.addActionListener(e -> {
            currentColor = Color.BLACK;
            drawingPanel.repaint();
        });
        colorsMenu.add(blackItem);

        JMenuItem redItem = new JMenuItem("Красный");
        redItem.addActionListener(e -> {
            currentColor = Color.RED;
            drawingPanel.repaint();
        });
        colorsMenu.add(redItem);

        JMenuItem greenItem = new JMenuItem("Зеленый");
        greenItem.addActionListener(e -> {
            currentColor = Color.GREEN;
            drawingPanel.repaint();
        });
        colorsMenu.add(greenItem);

        JMenuItem blueItem = new JMenuItem("Синий");
        blueItem.addActionListener(e -> {
            currentColor = Color.BLUE;
            drawingPanel.repaint();
        });
        colorsMenu.add(blueItem);

        JMenuItem yellowItem = new JMenuItem("Желтый");
        yellowItem.addActionListener(e -> {
            currentColor = Color.YELLOW;
            drawingPanel.repaint();
        });
        colorsMenu.add(yellowItem);

        JMenu styleMenu = new JMenu("Стиль");

        JMenuItem solidItem = new JMenuItem("Сплошная");
        solidItem.addActionListener(e -> {
            drawingPanel.setLineStyle("SOLID");
            drawingPanel.repaint();
        });
        styleMenu.add(solidItem);

        JMenuItem dashItem = new JMenuItem("Пунктир");
        dashItem.addActionListener(e -> {
            drawingPanel.setLineStyle("DASH");
            drawingPanel.repaint();
        });
        styleMenu.add(dashItem);

        JMenuItem dotItem = new JMenuItem("Точечная");
        dotItem.addActionListener(e -> {
            drawingPanel.setLineStyle("DOT");
            drawingPanel.repaint();
        });
        styleMenu.add(dotItem);

        JMenu nameMenu = new JMenu("Фамилия");

        JMenuItem drawNameItem = new JMenuItem("Нарисовать фамилию");
        drawNameItem.addActionListener(e -> {
            drawingPanel.setDrawName(true);
            drawingPanel.repaint();
        });

        JMenuItem clearNameItem = new JMenuItem("Стереть фамилию");
        clearNameItem.addActionListener(e -> {
            drawingPanel.setDrawName(false);
            drawingPanel.repaint();
        });

        nameMenu.add(drawNameItem);
        nameMenu.add(clearNameItem);

        menuBar.add(shapesMenu);
        menuBar.add(colorsMenu);
        menuBar.add(styleMenu);
        menuBar.add(nameMenu);

        setJMenuBar(menuBar);
    }

    class DrawingPanel extends JPanel {
        private boolean isDrawing = false;
        private Point startPoint = null;
        private Point endPoint = null;
        private boolean drawName = false;
        private String lineStyle = "SOLID";

        public DrawingPanel() {
            setPreferredSize(new Dimension(700, 400));

            addMouseListener(new MouseAdapter() {
                @Override
                public void mousePressed(MouseEvent e) {
                    if (currentShape.equals("LINE")) {
                        startPoint = e.getPoint();
                        endPoint = e.getPoint();
                        isDrawing = true;
                        repaint();
                    }
                }

                @Override
                public void mouseReleased(MouseEvent e) {
                    if (currentShape.equals("LINE") && isDrawing) {
                        endPoint = e.getPoint();
                        isDrawing = false;
                        repaint();
                    }
                }
            });

            addMouseMotionListener(new MouseMotionAdapter() {
                @Override
                public void mouseDragged(MouseEvent e) {
                    if (currentShape.equals("LINE") && isDrawing) {
                        endPoint = e.getPoint();
                        repaint();
                    }
                }
            });
        }

        @Override
        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            Graphics2D g2d = (Graphics2D) g;

            int centerX = getWidth() / 2;
            int centerY = getHeight() / 2;

            if (!currentShape.equals("NONE")) {

                BasicStroke stroke;
                switch (lineStyle) {
                    case "DASH":
                        float[] dashPattern = { 10, 5 };
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_ROUND,
                                BasicStroke.JOIN_ROUND, 1.0f, dashPattern, 0);
                        break;
                    case "DOT":
                        float[] dotPattern = { 2, 2 };
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_ROUND,
                                BasicStroke.JOIN_ROUND, 1.0f, dotPattern, 0);
                        break;
                    default:
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_SQUARE,
                                BasicStroke.JOIN_ROUND);
                }
                g2d.setStroke(stroke);

                int x = centerX - shapeSize / 2;
                int y = centerY - shapeSize / 2;

                switch (currentShape) {
                    case "RECT":
                        if (isFilled) {
                            g2d.fillRect(x, y, shapeSize, shapeSize);
                        } else {
                            g2d.drawRect(x, y, shapeSize, shapeSize);
                        }
                        break;

                    case "OVAL":
                        if (isFilled) {
                            g2d.fillOval(x, y, shapeSize, shapeSize);
                        } else {
                            g2d.drawOval(x, y, shapeSize, shapeSize);
                        }
                        break;

                    case "ROUND_RECT":
                        if (isFilled) {
                            g2d.fillRoundRect(x, y, shapeSize, shapeSize, 20, 20);
                        } else {
                            g2d.drawRoundRect(x, y, shapeSize, shapeSize, 20, 20);
                        }
                        break;

                    case "TRIANGLE":
                        int[] xPoints = { centerX, centerX - shapeSize / 2, centerX + shapeSize / 2 };
                        int[] yPoints = { centerY - shapeSize / 2, centerY + shapeSize / 2, centerY + shapeSize / 2 };
                        if (isFilled) {
                            g2d.fillPolygon(xPoints, yPoints, 3);
                        } else {
                            g2d.drawPolygon(xPoints, yPoints, 3);
                        }
                        break;
                }
            }

            if (currentShape.equals("LINE") && startPoint != null && endPoint != null) {
                g2d.setColor(currentColor);

                BasicStroke stroke;
                switch (lineStyle) {
                    case "DASH":
                        float[] dashPattern = { 10, 5 };
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_ROUND,
                                BasicStroke.JOIN_ROUND, 1.0f, dashPattern, 0);
                        break;
                    case "DOT":
                        float[] dotPattern = { 2, 2 };
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_ROUND,
                                BasicStroke.JOIN_ROUND, 1.0f, dotPattern, 0);
                        break;
                    default:
                        stroke = new BasicStroke(lineThickness, BasicStroke.CAP_ROUND,
                                BasicStroke.JOIN_ROUND);
                }
                g2d.setStroke(stroke);
                g2d.drawLine()

                g2d.drawLine(startPoint.x, startPoint.y, endPoint.x, endPoint.y);
            }

            if (drawName) {
                drawStudentName(g2d);
            }

            if (currentShape.equals("NONE") && !drawName) {
                g2d.setColor(Color.GRAY);
                g2d.setFont(new Font("Arial", Font.ITALIC, 18));
                String message = "Выберите фигуру в меню или нарисуйте фамилию";
                FontMetrics fm = g2d.getFontMetrics();
                int textWidth = fm.stringWidth(message);
                g2d.drawString(message, centerX - textWidth / 2, centerY);
            }
        }

        private void drawStudentName(Graphics2D g2d) {
            String name = "КОЧЕРГИН";
            int centerX = getWidth() / 2;
            int nameY = getHeight() - 100;

            g2d.setColor(currentColor);
            

            int x = centerX - 150;
            for (int i = 0; i < name.length(); i++) {
                char letter = name.charAt(i);

                int thickness = 1 + i;
                g2d.setStroke(new BasicStroke(thickness));

                int fontSize = 20 + i * 5;
                Font font = new Font("Arial", Font.BOLD, fontSize);
                g2d.setFont(font);

                FontMetrics fm = g2d.getFontMetrics();
                g2d.drawString(String.valueOf(letter), x, nameY);

                x += fm.charWidth(letter) + 10;
            }
        }

        public void clearDrawing() {
            startPoint = null;
            endPoint = null;
            currentShape = "NONE";
            drawName = false;
            repaint();
        }

        public void setFilled(boolean filled) {
            Lab6.this.isFilled = filled;
        }

        public void setLineStyle(String style) {
            this.lineStyle = style;
        }

        public void setDrawName(boolean drawName) {
            this.drawName = drawName;
        }
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            Lab6 window = new Lab6();
            window.setLocationRelativeTo(null);
        });
    }
}