using Silk.NET.OpenGL;
using StbImageSharp;

namespace LameEngine;

public class Texture
{
    public struct Settings()
    {
        public int LevelOfDetail = 0;
        public TextureWrapMode WrapSMode = TextureWrapMode.Repeat;
        public TextureWrapMode WrapTMode = TextureWrapMode.Repeat;
        public Color BorderColor = Color.Black;
        public TextureMinFilter MinFilter = TextureMinFilter.Nearest;
        public TextureMagFilter MagFilter = TextureMagFilter.Linear;
        public TextureMinFilter MipMapMinFilter = TextureMinFilter.LinearMipmapLinear;
        public TextureMagFilter MipMapMagFilter = TextureMagFilter.Linear;
    }

    public readonly int Width;
    public readonly int Height;
    
    private readonly uint handle;
    private static GL gl;

    public Texture(string pImagePath, Settings pSettings = default)
    {
        handle = gl.GenTexture();

        Use();

        // Texture wrapping
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)pSettings.WrapSMode);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)pSettings.WrapTMode);

        // Texture filtering
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)pSettings.MinFilter);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)pSettings.MagFilter);

        ImageResult image = ImageResult.FromStream(File.OpenRead(pImagePath), ColorComponents.RedGreenBlueAlpha);
        Span<byte> data = new Span<byte>(image.Data);

        Width = image.Width;
        Height = image.Height;

        gl.TexImage2D(
            TextureTarget.Texture2D,
            pSettings.LevelOfDetail,
            InternalFormat.Rgba,
            (uint)image.Width,
            (uint)image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            data.GetPinnableReference()
        );

        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)pSettings.MipMapMinFilter
        );

        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)pSettings.MipMapMagFilter
        );

        gl.GenerateMipmap(TextureTarget.Texture2D);
    }

    public void Use()
    {
        gl.BindTexture(TextureTarget.Texture2D, handle);
    }
    
    internal static void Initialize(GL pGL)
    {
        gl = pGL;
        StbImage.stbi_set_flip_vertically_on_load(1);
    }
}