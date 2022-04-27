
public class NotePos
{
    private NotePosJson note;
    private int line =0 ;
    private float staffWidth = 0;
    public int scale;
    public float actualX;
    public float actualY;
    public float actualWidth;
    public float actualHeight;
    private float actualStaffWidth;
    private float realX;
    public NotePos(NotePosJson note)
    {
        this.note = note;
    }
    
    //getx
    public int getX()
    {
        return note.x;
    }
    // gety
    public int getY()
    {
        return note.y;
    }
    //setLine
    public void setLine(int line)
    {
        this.line = line;
    }
    //setStaffWidth
    public void setStaffWidth(float staffWidth)
    {
        this.staffWidth = staffWidth;
    }
    
    //getRealX
    // public float getRealX()
    // {
    //     return note.x + (line * staffWidth);
    // }

    public int getLine()
    {
        return line;
    }
    //setScale
    public void setScale(int scale)
    {
        this.scale = scale;
    }
    
    //getRealX with scale
    public float getRealXWithScale()
    {
        return getRealX()/scale;
    }
    
    public float getActualX()
    {
        return actualX;
    }
    public void setActualX(float actualX)
    {
        this.actualX = actualX;
    }
    public float getActualY()
    {
        return actualY;
    }
    public void setActualY(float actualY)
    {
        this.actualY = actualY;
    }
    public float getActualWidth()
    {
        return actualWidth;
    }
    
    //2977  4208
    public void setActualWidth(float actualWidth)
    {
        this.actualWidth = actualWidth;
        setActualX(note.x * actualWidth /35724f);
    }
    public float getActualHeight()
    {
        return actualHeight;

    }
    public void setActualHeight(float actualHeight)
    {
        this.actualHeight = actualHeight;
        setActualY(note.y * actualHeight /50496f);
    }
    //actualStaffWidth
    public float getActualStaffWidth()
    {
        return actualStaffWidth;
    }
    public void setActualStaffWidth(float actualStaffWidth)
    {
        this.actualStaffWidth = actualStaffWidth;
    }
    //realX
    public float getRealX()
    {
        return realX;
    }
    public void setRealX(float realX)
    {
        this.realX = realX;
    }
    
}
