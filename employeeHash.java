import java.util.ArrayList;
import java.util.List;
import java.util.Arrays;
import java.util.stream.Stream;
import java.util.stream.IntStream;
import java.util.stream.Collector;
import java.util.stream.Collectors;
import java.util.Random;
import java.util.HashMap;
import java.util.Map;

public class Main{
    public static void main(String args[])
    {
        List<String> target = new ArrayList<String>(Arrays.asList("19336", "�؏G��", "19337", "��{�N�G", "69210", "���[�J�C���E�[", "69281", "�F��E��"));
        Map<String, String> userInfo = Stream.iterate(1, i->i+1).limit(target.size()).filter(e -> e % 2 == 1).collect(Collectors.toMap(e -> target.get(e - 1), e -> target.get(e)));
        System.out.println(userInfo);
    }
}
