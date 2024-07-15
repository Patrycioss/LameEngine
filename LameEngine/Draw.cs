namespace LameEngine;

public static class Draw
{
   
    
    private static readonly Dictionary<Guid, SpriteRenderer> spriteRenderers = new();

    public static void DrawSprite(Window pTarget, SpriteSettings pSpriteSettings)
    {
        if (!spriteRenderers.TryGetValue(pTarget.ID, out SpriteRenderer spriteRenderer))
        {
            spriteRenderer = new SpriteRenderer(pTarget.GL);
            spriteRenderers.Add(pTarget.ID, spriteRenderer);
        }

        Texture texture =
            AssetManager.LoadTexture(pTarget, pSpriteSettings.TexturePath, pSpriteSettings.TextureSettings);

        spriteRenderer.Render(new SpriteRenderer.Settings(texture, pSpriteSettings.ModelMatrix,
            pTarget.ProjectionMatrix, pSpriteSettings.Color));
    }
}