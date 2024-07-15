using System.Text;
using Silk.NET.Maths;

namespace LameEngine;

public static class DebugHelper
{
    private static readonly HashSet<string> done = new HashSet<string>();

    // Matrix4X4<float> newModel = Matrix4X4<float>.Identity;
    // Matrix4X4<float> newT = Matrix4X4.CreateTranslation(new Vector3D<float>(200,200,0));
    // Matrix4X4<float> newR = Matrix4X4.CreateFromAxisAngle(new Vector3D<float>(0, 0, 1), 0);
    // Matrix4X4<float> newS = Matrix4X4.CreateScale(new Vector3D<float>(200, 200, 1));

    // newModel *= newR;
    // newModel *= newS;
    // newModel *= newT;

    public static void LogMatrix(this Matrix4X4<float> pMatrix, string pName)
    {
        Console.Write(pMatrix.MatrixToString(pName));
        // Console.WriteLine($"------ {pName} ------");
        // Console.WriteLine($"{pMatrix.M11}, {pMatrix.M21}, {pMatrix.M31}, {pMatrix.M41}");
        // Console.WriteLine($"{pMatrix.M12}, {pMatrix.M22}, {pMatrix.M32}, {pMatrix.M42}");
        // Console.WriteLine($"{pMatrix.M13}, {pMatrix.M23}, {pMatrix.M33}, {pMatrix.M43}");
        // Console.WriteLine($"{pMatrix.M14}, {pMatrix.M24}, {pMatrix.M34}, {pMatrix.M44}");
        // Console.WriteLine($"------ {pName} ------");
    }

    public static string MatrixToString(this Matrix4X4<float> pMatrix, string pName)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"------ {pName} ------");
        stringBuilder.AppendLine($"{pMatrix.M11}, {pMatrix.M21}, {pMatrix.M31}, {pMatrix.M41}");
        stringBuilder.AppendLine($"{pMatrix.M12}, {pMatrix.M22}, {pMatrix.M32}, {pMatrix.M42}");
        stringBuilder.AppendLine($"{pMatrix.M13}, {pMatrix.M23}, {pMatrix.M33}, {pMatrix.M43}");
        stringBuilder.AppendLine($"{pMatrix.M14}, {pMatrix.M24}, {pMatrix.M34}, {pMatrix.M44}");
        stringBuilder.AppendLine($"------ {pName} ------");
        return stringBuilder.ToString();
    }
    
    public static void LogMatrixOnce(this Matrix4X4<float> pMatrix, string pName)
    {
        if (done.Add(pName))
        {
           LogMatrix(pMatrix,pName);
        }
    }
}