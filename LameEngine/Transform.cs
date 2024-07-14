using Silk.NET.Maths;

namespace LameEngine;

/// <summary>
/// Used for storing the Position, Rotation and Scale of a <see cref="GameObject"/>.
/// </summary>
public class Transform
{
    /// <summary>
    /// Determines whether any values have changed this frame.
    /// </summary>
    public bool Dirty { get; private set; }

    /// <summary>
    /// The position of a GameObject.
    /// </summary>
    public Vector2D<float> Position
    {
        get => position;
        set
        {
            position = value;
            Dirty = true;
        }
    }
    
    /// <summary>
    /// The rotation of a GameObject in radians.
    /// </summary>
    public float Angle
    {
        get => angle;
        set
        {
            angle = value;
            Dirty = true;
        }
    }
    
    /// <summary>
    /// The scale of a GameObject.
    /// </summary>
    public Vector2D<float> Scale
    {
        get => scale;
        set
        {
            scale = value;
            Dirty = true;
        }
    }

    private Vector2D<float> position;
    private float angle = 0;
    private Vector2D<float> scale = new Vector2D<float>(1, 1);

    /// <summary>
    /// Constructs a <see cref="Transform"/>.
    /// </summary>
    /// <param name="pPosition">Initial position of the Transform.</param>
    /// <param name="pAngle">Initial rotation of the Transform in radians.</param>
    public Transform(Vector2D<float> pPosition, float pAngle = 0)
    {
        position = pPosition;
        angle = pAngle;
    }
    
    /// <summary>
    /// Constructs a <see cref="Transform"/>.
    /// </summary>
    /// <param name="pPosition">Initial position of the Transform.</param>
    /// <param name="pScale">Initial scale of the Transform.</param>
    /// <param name="pAngle">Initial rotation of the Transform in radians.</param>
    public Transform(Vector2D<float> pPosition, Vector2D<float> pScale, float pAngle = 0)
    {
        position = pPosition;
        scale = pScale;
        angle = pAngle;
    }

    /// <summary>
    /// Sets <see cref="Dirty"/> to false. Should be called at the end of <see cref="GameObject"/>'s Update./>
    /// </summary>
    internal void Clean()
    {
        Dirty = false;
    }
}