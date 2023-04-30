//自定义WordZone库，增加数据成员
namespace WordZone
{
    public class Tone
    {
        public static Tone Runtime = new Tone();

        public int v = int.MaxValue;            //说话速度
        public bool Wobble = false;             //是否震荡
        public static bool Instant = false;     //备用
    }
}
