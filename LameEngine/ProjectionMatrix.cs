using Silk.NET.Maths;

namespace LameEngine;

public struct ProjectionMatrix
{
    public readonly Matrix4X4<float> Mat;

    public ProjectionMatrix(Vector2D<int> pResolution, float pZoom = 1.0f, float pNearPlane = -1.0f,
        float pFarPlane = 1.0f)
    {
        Matrix4X4<float> orthographic =
            Matrix4X4.CreateOrthographicOffCenter(0, pResolution.X, pResolution.Y, 0, pNearPlane, pFarPlane);
        Matrix4X4<float> zoom = Matrix4X4.CreateScale(new Vector3D<float>(pZoom, pZoom, 1));
        Mat = orthographic * zoom;
    }
}