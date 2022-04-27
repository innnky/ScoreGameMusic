
public class NotePos
{
    private NotePosJson note;
    private int line =0 ;
    private float staffWidth = 0;
    public int scale;
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
    public float getRealX()
    {
        return note.x + (line * staffWidth);
    }

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
    
    
}
