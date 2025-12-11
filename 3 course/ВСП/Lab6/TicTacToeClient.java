import java.io.*;
import java.net.*;
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class TicTacToeClient {
    private Socket socket;
    private PrintWriter out;
    private BufferedReader in;
    private char mySymbol;
    private JFrame frame;
    private JButton[][] buttons = new JButton[3][3];

    public TicTacToeClient(String host, int port) {
        try {
            socket = new Socket(host, port);
            out = new PrintWriter(socket.getOutputStream(), true);
            in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            String welcome = in.readLine();
            if (welcome.startsWith("WELCOME")) {
                mySymbol = welcome.split(" ")[1].charAt(0);
                System.out.println("Вы играете за: " + mySymbol);
            }
            createGUI();
            new Thread(new ServerListener()).start();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void createGUI() {
        frame = new JFrame("Крестики-нолики (" + mySymbol + ")");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(300, 300);
        frame.setLayout(new GridLayout(3, 3));
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                buttons[i][j] = new JButton("-");
                final int row = i, col = j;
                buttons[i][j].addActionListener(e -> {
                    out.println("MOVE " + row + " " + col);
                    buttons[row][col].setEnabled(false);
                });
                frame.add(buttons[i][j]);
            }
        }
        frame.setVisible(true);
    }

    private class ServerListener implements Runnable {
        @Override
        public void run() {
            try {
                String message;
                while ((message = in.readLine()) != null) {
                    if (message.equals("VALID_MOVE")) {
                        System.out.println("Ход принят.");
                    } else if (message.equals("INVALID_MOVE")) {
                        JOptionPane.showMessageDialog(frame, "Неверный ход!");
                    } else if (message.startsWith("BOARD")) {
                        StringBuilder board = new StringBuilder();
                        for (int i = 0; i < 3; i++) {
                            board.append(in.readLine()).append("\n");
                        }
                        updateBoard(board.toString());
                    } else if (message.startsWith("WINNER")) {
                        JOptionPane.showMessageDialog(frame, "Победитель: " + message.charAt(7));
                        frame.dispose();
                        break;
                    } else if (message.equals("DRAW")) {
                        JOptionPane.showMessageDialog(frame, "Ничья!");
                        frame.dispose();
                        break;
                    } else if (message.equals("TURN")) {
                        enableAllButtons();
                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    private void updateBoard(String boardStr) {
        String[] lines = boardStr.split("\n");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                buttons[i][j].setText(String.valueOf(lines[i].charAt(j)));
            }
        }
    }

    private void enableAllButtons() {
        for (JButton[] row : buttons) {
            for (JButton btn : row) {
                btn.setEnabled(btn.getText().equals("-"));
            }
        }
    }

    public static void main(String[] args) {
        String host = args.length > 0 ? args[0] : "localhost";
        int port = args.length > 1 ? Integer.parseInt(args[1]) : 5555;
        new TicTacToeClient(host, port);
    }
}