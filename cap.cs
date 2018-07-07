public struct Capacity
{
    public float min;
    public float max;
    public float target;

    public Capacity(float x, float y)
    {
        min = x;
        max = y;
        target = y;
    }

    public Capacity(float y)
    {
        min = 0;
        max = y;
        target = y;
    }

    public Capacity(float x, float y, float z)
    {
        min = x;
        max = y;
        target = z;
    }

    public Capacity(float y, float z)
    {
        min = 0;
        max = y;
        target = z;
    }
}