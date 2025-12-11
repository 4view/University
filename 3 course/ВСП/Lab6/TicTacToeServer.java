import java.io.*;
import java.net.*;
import java.util.*;

public class TicTacToeServer {
    private static final int PORT = 5555;
    private static Set<ClientHandler> clients = new HashSet<>();
    private static GameState game = new GameState();

    public static void main(String[] args) {
        System.out.println("Сервер запущен на порту " + PORT);
        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            while (clients.size() < 2) {
                Socket clientSocket = serverSocket.accept();
                ClientHandler handler = new ClientHandler(clientSocket, clients.size() + 1, game);
                clients.add(handler);
                new Thread(handler).start();
                System.out.println("Игрок " + handler.playerSymbol + " подключился.");
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    static class GameState {
        private char[][] board = new char[3][3];
        private int currentPlayer = 1; // 1 - X, 2 - O

        public GameState() {
            for (char[] row : board) Arrays.fill(row, '-');
        }

        public synchronized boolean makeMove(int player, int row, int col) {
            if (board[row][col] == '-' && player == currentPlayer) {
                board[row][col] = (player == 1) ? 'X' : 'O';
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
                return true;
            }
            return false;
        }

        public synchronized char checkWinner() {
            // Проверка строк, столбцов, диагоналей
            for (int i = 0; i < 3; i++) {
                if (board[i][0] != '-' && board[i][0] == board[i][1] && board[i][1] == board[i][2])
                    return board[i][0];
                if (board[0][i] != '-' && board[0][i] == board[1][i] && board[1][i] == board[2][i])
                    return board[0][i];
            }
            if (board[0][0] != '-' && board[0][0] == board[1][1] && board[1][1] == board[2][2])
                return board[0][0];
            if (board[0][2] != '-' && board[0][2] == board[1][1] && board[1][1] == board[2][0])
                return board[0][2];
            // Ничья
            for (char[] row : board)
                for (char c : row)
                    if (c == '-') return '-';
            return 'D'; // Draw
        }

        public synchronized String getBoardString() {
            StringBuilder sb = new StringBuilder();
            for (char[] row : board) {
                sb.append(new String(row)).append("\n");
            }
            return sb.toString();
        }
    }

    static class ClientHandler implements Runnable {
        private Socket socket;
        private PrintWriter out;
        private BufferedReader in;
        private int playerNumber;
        private char playerSymbol;
        private GameState game;

        public ClientHandler(Socket socket, int playerNumber, GameState game) {
            this.socket = socket;
            this.playerNumber = playerNumber;
            this.playerSymbol = (playerNumber == 1) ? 'X' : 'O';
            this.game = game;
        }

        @Override
        public void run() {
            try {
                out = new PrintWriter(socket.getOutputStream(), true);
                in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                out.println("WELCOME " + playerSymbol);
                out.println("BOARD\n" + game.getBoardString());

                while (true) {
                    String input = in.readLine();
                    if (input == null) break;
                    if (input.startsWith("MOVE")) {
                        String[] parts = input.split(" ");
                        int row = Integer.parseInt(parts[1]);
                        int col = Integer.parseInt(parts[2]);
                        boolean valid = game.makeMove(playerNumber, row, col);
                        if (valid) {
                            out.println("VALID_MOVE");
                            out.println("BOARD\n" + game.getBoardString());
                            char winner = game.checkWinner();
                            if (winner != '-') {
                                if (winner == 'D') {
                                    out.println("DRAW");
                                } else {
                                    out.println("WINNER " + winner);
                                }
                                break;
                            } else {
                                out.println("TURN");
                            }
                        } else {
                            out.println("INVALID_MOVE");
                        }
                    }
                }
                socket.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}