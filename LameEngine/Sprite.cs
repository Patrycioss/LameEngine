using Silk.NET.Maths;

namespace LameEngine;

public class SpriteSettings
{
    public Matrix4X4<float> ModelMatrix;
    public Color Color;
    public Texture.Settings TextureSettings;
    public Vector2D<int> Resolution;
    public readonly string TexturePath;
    
    public SpriteSettings(string pTexturePath)
    {
        ModelMatrix = Matrix4X4<float>.Identity;
        Color = Color.White;
        TextureSettings = new Texture.Settings();
        Resolution = new Vector2D<int>(-1, -1);
        TexturePath = pTexturePath;
    }

    public void ReCalculateModelMatrix(Transform pTransform)
    {
        ReCalculateModelMatrix(pTransform.Angle, pTransform.Scale, pTransform.Position);
    }

    public void ReCalculateModelMatrix(float pRotation, Vector2D<float> pScale, Vector2D<float> pTranslation)
    {
        Matrix4X4<float> modelMatrix = Matrix4X4<float>.Identity;
        Matrix4X4<float> rotationMatrix = Matrix4X4.CreateFromAxisAngle(new Vector3D<float>(0, 0, 1), pRotation);
        Matrix4X4<float> scaleMatrix =
            Matrix4X4.CreateScale(Resolution.X * pScale.X, Resolution.Y * pScale.Y, 1);
        Matrix4X4<float> translationMatrix = Matrix4X4.CreateTranslation(pTranslation.X, pTranslation.Y, 0);

        ModelMatrix = modelMatrix * rotationMatrix * scaleMatrix * translationMatrix;
    }

    public override string ToString()
    {
        return
            $"[ModelMatrix: {ModelMatrix}, Color: {Color}, Settings: {TextureSettings}, Resolution: {Resolution}, TexturePath: {TexturePath}";
    }
}

public class Sprite : Component
{
    private SpriteSettings spriteSettings;

    public Sprite(SpriteSettings pSpriteSettings)
    {
        spriteSettings = pSpriteSettings;
    }

    protected override void Start()
    {
        spriteSettings.ReCalculateModelMatrix(transform);
    }

    protected override void Update()
    {
        if (transform.Dirty)
        {
            spriteSettings.ReCalculateModelMatrix(gameObject.Transform);
        }
    }

    protected override void Render()
    {
        Draw.DrawSprite(gameObject.Target, spriteSettings);
    }
}