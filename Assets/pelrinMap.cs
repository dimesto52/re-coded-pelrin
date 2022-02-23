using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelrinMap
{
    public static Texture2D getpelrinMap(int height, int width, int slices, int octaves,int seed, Vector2 pos)
    {
        Texture2D tx = new Texture2D(height, width);

        pelrin p = new pelrin(height, width, slices, octaves, seed, pos);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                float c = p.getpelrin(x, y);
                tx.SetPixel(x, y, new Color(c, c, c));
            }

        tx.filterMode = FilterMode.Point;
        
        tx.Apply();

        return tx;
    }
}
