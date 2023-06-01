using UnityEngine;

public static class PuzzleUtil
{
    const float DEGREE = 90f;
    public static Texture2D RotateTexture(Texture2D originalTexture, PiecePosition piecePosition) => RotateTexture(originalTexture, DEGREE * (int)piecePosition);// Function to rotate a texture by a specified degree with high-quality interpolation
    public static Texture2D RotateTexture(Texture2D originalTexture, float degree)
    {
        int width = originalTexture.width;
        int height = originalTexture.height;

        Texture2D rotatedTexture = new Texture2D(width, height);

        // Calculate the center pivot point
        float pivotX = 0.5f * (width - 1);
        float pivotY = 0.5f * (height - 1);

        // Convert degree to radians
        float radians = Mathf.Deg2Rad * degree;

        // Calculate sine and cosine of the rotation angle
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Iterate over each pixel of the rotated texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the relative coordinates of the pixel with respect to the pivot point
                float offsetX = x - pivotX;
                float offsetY = y - pivotY;

                // Apply rotation transformation
                float rotatedX = (offsetX * cos) - (offsetY * sin) + pivotX;
                float rotatedY = (offsetX * sin) + (offsetY * cos) + pivotY;

                // Sample the color using bilinear interpolation
                Color sampledColor = SampleTextureBilinear(originalTexture, rotatedX, rotatedY);

                // Set the color in the rotated texture
                rotatedTexture.SetPixel(x, y, sampledColor);
            }
        }

        // Apply the changes to the rotated texture
        rotatedTexture.Apply();

        return rotatedTexture;
    }

    // Function to perform bilinear interpolation for sampling the texture
    private static Color SampleTextureBilinear(Texture2D texture, float x, float y)
    {
        int x1 = Mathf.FloorToInt(x);
        int x2 = Mathf.CeilToInt(x);
        int y1 = Mathf.FloorToInt(y);
        int y2 = Mathf.CeilToInt(y);

        Color color1 = texture.GetPixel(x1, y1);
        Color color2 = texture.GetPixel(x2, y1);
        Color color3 = texture.GetPixel(x1, y2);
        Color color4 = texture.GetPixel(x2, y2);

        float weightX = x - x1;
        float weightY = y - y1;

        Color interpolatedColor = Color.Lerp(Color.Lerp(color1, color2, weightX), Color.Lerp(color3, color4, weightX), weightY);

        return interpolatedColor;
    }

    // Method to calculate the GCD (Greatest Common Divisor) using Euclidean algorithm
    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;
        }
        return a;
    }

    // Method to calculate the LCM (Least Common Multiple)
    public static int LCM(int a, int b)
    {
        int gcd = GCD(a, b);
        return (a * b) / gcd;
    }
}
