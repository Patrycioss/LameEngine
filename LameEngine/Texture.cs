using Silk.NET.OpenGL;
using StbImageSharp;

namespace LameEngine;

public class Texture
{
    public struct Settings()
    {
        public int LevelOfDetail = 0;

        // public TextureWrapMode WrapSMode = TextureWrapMode.Repeat;
        // public TextureWrapMode WrapTMode = TextureWrapMode.Repeat;
        public TextureWrapMode WrapSMode = TextureWrapMode.Repeat;
        public TextureWrapMode WrapTMode = TextureWrapMode.Repeat;

        public Color BorderColor = Color.Black;

        // public TextureMinFilter MinFilter = TextureMinFilter.Nearest;
        // public TextureMagFilter MagFilter = TextureMagFilter.Linear;
        // public TextureMinFilter MipMapMinFilter = TextureMinFilter.LinearMipmapLinear;
        // public TextureMagFilter MipMapMagFilter = TextureMagFilter.Linear;

        public TextureMinFilter MinFilter = TextureMinFilter.Nearest;
        public TextureMagFilter MagFilter = TextureMagFilter.Linear;
        public TextureMinFilter MipMapMinFilter = TextureMinFilter.LinearMipmapLinear;
        public TextureMagFilter MipMapMagFilter = TextureMagFilter.Linear;
    }

    private uint handle;
    private static GL GL = WindowManager.GL;


    static Texture()
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
    }

    public Texture(string pImagePath, Settings pSettings = default)
    {
        handle = GL.GenTexture();

        Use();

        // Texture wrapping
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)pSettings.WrapSMode);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)pSettings.WrapTMode);

        // Texture filtering
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)pSettings.MinFilter);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)pSettings.MagFilter);

        ImageResult image = ImageResult.FromStream(File.OpenRead(pImagePath), ColorComponents.RedGreenBlueAlpha);
        Span<byte> data = new Span<byte>(image.Data);

        GL.TexImage2D(
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

        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)pSettings.MipMapMinFilter
        );
        
        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)pSettings.MipMapMagFilter
        );
   
        GL.GenerateMipmap(TextureTarget.Texture2D);
    }

    public void Use()
    {
        GL.BindTexture(TextureTarget.Texture2D, handle);
    }
}