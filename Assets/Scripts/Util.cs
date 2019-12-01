using UnityEngine;

public static class Util
{
    /// <summary>
    /// Cosine of an angle IN DEGREES
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static float DCos(float degrees)
    {
        float rads = (degrees * Mathf.PI) / 180f;
        return Mathf.Cos(rads);
    }
    
    
    /// <summary>
    /// Sine of an angle IN DEGREES
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static float DSin(float degrees)
    {
        float rads = (degrees * Mathf.PI) / 180f;
        return Mathf.Sin(rads);
    }
    
    
    
    
    /// <summary>
    /// ArcCosine IN DEGREES
    /// </summary>
    /// <param name="cos"></param>
    /// <returns>The angle in degrees</returns>
    public static float DArcCos(float cos)
    {
        float rads = Mathf.Acos(cos);
        return (rads * 180f) / Mathf.PI;
    }
    
    
    /// <summary>
    /// ArcSine IN DEGREES
    /// </summary>
    /// <param name="sin"></param>
    /// <returns>The angle in degrees</returns>
    public static float DArcSin(float sin)
    {
        float rads = Mathf.Asin(sin);
        return (rads * 180f) / Mathf.PI;
    }
    
    
    /// <summary>
    /// ArcTan IN DEGREES
    /// </summary>
    /// <param name="tan"></param>
    /// <returns>The angle in degrees</returns>
    public static float DArcTan(float tan)
    {
        float rads = Mathf.Atan(tan);
        return (rads * 180f) / Mathf.PI;
    }
    
    public static float DArcTan(float y, float x)
    {
        float rads = Mathf.Atan2(y, x);
        return (rads * 180f) / Mathf.PI;
    }
    
    
    
    
    /// <summary>
    /// The angle in degrees of a vector.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float Direction(Vector2 v)
    {
        return Vector2.Angle(Vector2.zero, v);
    }
    
    
    
    
    /// <summary>
    /// Performs a bilinear interpolation between four points
    /// </summary>
    /// <param name="v00"></param>
    /// <param name="v10"></param>
    /// <param name="v01"></param>
    /// <param name="v11"></param>
    /// <param name="xPercent"></param>
    /// <param name="yPercent"></param>
    /// <returns></returns>
    public static float Blerp(float v00, float v10, float v01, float v11, float xPercent, float yPercent) {
        return Lerp(
                Lerp(v00, v10, xPercent), 
                Lerp(v01, v11, xPercent), 
                yPercent
            );
    }
    
    /// <summary>
    /// Interpolates two numbers
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static float Lerp(float a, float b, float percent) {
        return a + (b - a) * percent;
    }
    
    /// <summary>
    /// Interpolates two colors
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static Color Lerp(Color a, Color b, float percent)
    {
        return new Color(
            Lerp(a.r, b.r, percent), 
            Lerp(a.g, b.g, percent), 
            Lerp(a.b, b.b, percent), 
            Lerp(a.a, b.a, percent)
            );
    }
}
