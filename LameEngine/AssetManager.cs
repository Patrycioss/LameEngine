namespace LameEngine;

public static class AssetManager
{
    private struct TextureID(string pPath, Texture.Settings pSettings)
    {
        public readonly string Path = pPath;
        public readonly Texture.Settings Settings = pSettings;
    }

    private static readonly Dictionary<TextureID, Texture> textures = new Dictionary<TextureID, Texture>();

    public static Texture LoadTexture(string pPath, Texture.Settings pSettings)
    {
        TextureID textureID = new TextureID(pPath, pSettings);

        if (textures.TryGetValue(textureID, out Texture texture))
        {
            return texture;
        }

        Texture newTexture = new Texture(pPath, pSettings);
        Console.WriteLine($"[Asset Manager]: Loaded Texture '{pPath}'");
        textures.Add(textureID, newTexture);
        return newTexture;
    }
}