//添加敌人每轮说的话，待个性化开发
public class SimpleEnemy : Enemy
{
    public override Round[] GetRounds()
    {
        return new Round[]{
            new Round(){
                gossips=new string[]{"",""},
            },
            new Round(){
            },
            new Round(){
            },
        };
    }
}
