using Silk.NET.Maths;

namespace LameEngine;

public struct SpriteSettings()
{
    public Color Color = Color.White;
    public Texture.Settings TextureSettings = new Texture.Settings();
    public Vector2D<int> Resolution = new Vector2D<int>(-1, -1);
}

public class Sprite : Component
{
    private Rectangle.RenderSettings renderSettings;
    private readonly Texture texture;
    private Vector2D<int> resolution;

    public Sprite(string pPath)
    {
        texture = AssetManager.LoadTexture(pPath, new Texture.Settings());
        renderSettings = new Rectangle.RenderSettings();
        resolution = new Vector2D<int>(texture.Width, texture.Height);
    }

    public Sprite(string pPath, SpriteSettings pSpriteSettings)
    {
        texture = AssetManager.LoadTexture(pPath, pSpriteSettings.TextureSettings);
        renderSettings = new Rectangle.RenderSettings()
        {
            Color = pSpriteSettings.Color,
        };
        resolution = pSpriteSettings.Resolution;
    }

    protected override void Start()
    {
        ReCalculateModelMatrix();
    }

    protected override void Update()
    {
        if (transform.Dirty)
        {
            ReCalculateModelMatrix();
        }
    }

    protected override void Render()
    {
        Rectangle.Draw(renderSettings, texture);
    }

    // Matrix4X4<float> modelMatrix = Matrix4X4<float>.Identity;
    // Matrix4X4<float> rotationMatrix = Matrix4X4.CreateFromAxisAngle(new Vector3D<float>(0, 0, 1), Angle);
    // Matrix4X4<float> scaleMatrix = Matrix4X4.CreateScale(Scale.X, Scale.Y, 1);
    // Matrix4X4<float> translationMatrix = Matrix4X4.CreateTranslation(Position.X, Position.Y, 0);

    // modelMatrix * translationMatrix * rotationMatrix * scaleMatrix;

    private void ReCalculateModelMatrix()
    {
        // Console.WriteLine($"Scale: {spriteTransform.TransformData.Scale}");

        Matrix4X4<float> modelMatrix = Matrix4X4<float>.Identity;
        Matrix4X4<float> rotationMatrix = Matrix4X4.CreateFromAxisAngle(new Vector3D<float>(0, 0, 1), transform.Angle);
        Matrix4X4<float> scaleMatrix =
            Matrix4X4.CreateScale(resolution.X * transform.Scale.X, resolution.Y * transform.Scale.Y, 1);
        Matrix4X4<float> translationMatrix = Matrix4X4.CreateTranslation(transform.Position.X, transform.Position.Y, 0);


        renderSettings.ModelMatrix = modelMatrix * rotationMatrix * scaleMatrix * translationMatrix;
        // Console.WriteLine($"{renderSettings.ModelMatrix.Row1}");
        // Console.WriteLine($"{renderSettings.ModelMatrix.Row2}");
        // Console.WriteLine($"{renderSettings.ModelMatrix.Row3}");
        // Console.WriteLine($"{renderSettings.ModelMatrix.Row4}");
    }
}