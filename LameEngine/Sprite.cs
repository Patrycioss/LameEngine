namespace LameEngine;

public struct SpriteSettings()
{
    public Color Color = Color.White;
    public Texture.Settings TextureSettings = new Texture.Settings();
}

public class Sprite : Component
{
    public readonly SpriteSettings Settings;
    
    private readonly Texture texture;

    public Sprite(string pPath)
    {
        Settings = new SpriteSettings();
        texture = AssetManager.LoadTexture(pPath, Settings.TextureSettings);
    }

    public Sprite(string pPath, SpriteSettings pSpriteSettings)
    {
        Settings = pSpriteSettings;
        texture = AssetManager.LoadTexture(pPath, Settings.TextureSettings);
    }

    public override void Render()
    {
        Rectangle.Draw(texture, Settings.Color);
    }
}