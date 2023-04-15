
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
