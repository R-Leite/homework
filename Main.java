import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.awt.Point;

public class Main {
    public static class Player {
        HashMap<String, Integer> MOVEMENT_X = new HashMap<String, Integer>(){{
            put("7", -1); put("8", 0); put("9", 1);
            put("4", -1); put("5", 0); put("6", 1);
            put("1", -1); put("2", 0); put("3", 1);
        }};

        HashMap<String, Integer> MOVEMENT_Y = new HashMap<String, Integer>(){{
            put("7",  1); put("8",  1); put("9",  1);
            put("4",  0); put("5",  0); put("6",  0);
            put("1", -1); put("2", -1); put("3", -1);
        }};

        Point point = null;
        Map map = null;

        public Player() {
            point = new Point(0, 0);
            map = new Map();
        }

        public boolean walk(String moveData) {
            // Playerの移動
            if (MOVEMENT_X.containsKey(moveData)) {
                point.move(point.x + MOVEMENT_X.get(moveData), point.y + MOVEMENT_Y.get(moveData));
            } else {
                point.move(point.x + MOVEMENT_X.get("5"), point.y + MOVEMENT_Y.get("5"));
            }

            // 移動した結果、街に留まっているかチェック
            // 街からはみ出したらはみ出ないようにする。
            if (point.x < map.getMinX()) {
                point.move(map.getMinX(), point.y);
            } else if (point.getX() > map.getMaxX()) {
                point.move(map.getMaxX(), point.y);
            }

            if (point.getY() < map.getMinY()) {
                point.move(point.x, map.getMinY());
            } else if (point.getY() > map.getMaxY()) {
                point.move(point.x, map.getMaxY());
            }

            return true;
        }

        public Point getPosition() {
            return point;
        }
    }

    public static class Map {
        private int MAX_X = 10;
        private int MIN_X = 0;
        private int MAX_Y = 10;
        private int MIN_Y = 0;

        public Map() {
        }

        public int getMaxX() {
            return MAX_X;
        }

        public int getMinX() {
            return MIN_X;
        }

        public int getMaxY() {
            return MAX_Y;
        }

        public int getMinY() {
            return MIN_Y;
        }
    }

    public static void main(String[] args) throws Exception {
        Player player = new Player();

        while(true) {
            // 標準入力の設定
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(System.in));
            System.out.print("入力してください:");

            // 移動データの取り込み
            String moveData = bufferedReader.readLine();

            // 移動データのチェック
            // 数字以外の場合は終了
            if (!checkMoveData(moveData)) {
                System.out.println("ゲームを終了します");
                break;
            }

            // walkメソッド呼び出し
            player.walk(moveData);

            // 位置情報の取得
            Point point = player.getPosition();

            // printする内容を作成
            String printData = String.format("(x, y) = (%d, %d)", point.x, point.y);

            // 結果出力
            System.out.println(printData);
            System.out.println();
        }
    }

    // 移動データのチェックメソッド
    public static boolean checkMoveData(String moveData) {
        // 数字の場合はtrue
        // 数字以外の場合はfalse
        try {
            Integer.parseInt(moveData);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }
} 
