
public class Game_1Controller
{
    public float x;
    public float y;
    public float z;
   
    public float ChangePlayerPos(float x, float y)
    {
        if(x > 0&&x<5)
        {
            if(y<2&&y>0){
                
            }
        }

        if(x > 5&&x<10)
        {
            if(y<5&&y>0){
                z = 15;
            }
        }
 
        if(x > 10&&x<22)
        {
            if(y<6.5&&y>5){
                z = 15;
            }
        }

        if(x > 22&&x<28)
        {
            if(y<7.5&&y>5.7){
                z = 28;
            }
        }

        if(x > 28&&x<41)
        {
            if(y<10&&y>8){
                z = 40;
            }
        }

        if(x > 41&&x<49)
        {
            if(y<12&&y>8.5){
                z = 45;
            }
        }

        if(x > 49&&x<70)
        {
            if(y<15&&y>12){
                z = 28;
            }
        }

        return z;
    }
}
