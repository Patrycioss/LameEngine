namespace LameEngine;

public struct Color
{
    public float R;
    public float G;
    public float B;
    public float A;

    public Color(float pR, float pG, float pB, float pA = 1.0f)
    {
        R = pR;
        G = pG;
        B = pB;
        A = pA;
    }

    public static Color White => new Color(1.0f, 1.0f, 1.0f);
    public static Color Black => new Color(0, 0, 0);
    public static Color Red => new Color(1.0f, 0, 0);
    public static Color Green => new Color(0, 1.0f, 0);
    public static Color Blue => new Color(0, 0, 1.0f);
}