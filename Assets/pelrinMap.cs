using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelrinMap
{
    public static Texture2D getpelrinMap(int height, int width, int slices, int octaves)
    {
        Texture2D tx = new Texture2D(height, width);

        float[] octavesumArray = new float[width * height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                octavesumArray[x + y * width] = 0.5f;
            }

        for (int i = 0; i < octaves; i++)
        {
            int slices2 = (int)(slices*(Mathf.Pow(2.0f,i)));

            float[] slicesArray = getpelrinSlices(slices2);
            float[] octaveArray = getpelrinOctave(height, width, slices2, slicesArray);


            octavesumArray = getpelrinAddOctave(height, width, octavesumArray, octaveArray, (1.0f / Mathf.Pow(2, (i))));
        }

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                float c = octavesumArray[x + y * width];
                tx.SetPixel(x, y, new Color(c, c, c));
            }

        tx.Apply();

        return tx;
    }

    public static float[] getpelrinSlices(int slices)
    {
        int dividBy = slices + 1;

        float[] slicesArray = new float[dividBy * dividBy];
        for (int x = 0; x < dividBy; x++)
            for (int y = 0; y < dividBy; y++)
            {
                slicesArray[x + y * dividBy] = Random.Range(0, 1.0f);
            }

        return slicesArray;
    }

    public static float[] getpelrinOctave(int height, int width, int slices, float[] slicesArray)
    {
        int dividBy = slices + 1;

        float[] octaveArray = new float[width * height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int sizedividW = (width / (dividBy - 1));
                int sizedividh = (height / (dividBy - 1));

                int xbase = x / sizedividW;
                int ybase = y / sizedividh;

                float xfacteur = ((float)x % sizedividW) / sizedividW;
                float yfacteur = ((float)y % sizedividh) / sizedividh;

                float base0_0 = slicesArray[(xbase + 0) + (ybase + 0) * dividBy] * (1 - xfacteur) * (1 - yfacteur);
                float base1_0 = slicesArray[(xbase + 1) + (ybase + 0) * dividBy] * (xfacteur) * (1 - yfacteur);
                float base0_1 = slicesArray[(xbase + 0) + (ybase + 1) * dividBy] * (1 - xfacteur) * (yfacteur);
                float base1_1 = slicesArray[(xbase + 1) + (ybase + 1) * dividBy] * (xfacteur) * (yfacteur);

                octaveArray[x + y * width] = (base0_0 + base1_0 + base0_1 + base1_1);
            }

        return octaveArray;
    }
    public static float[] getpelrinAddOctave(int height, int width, float[] sum, float[] octaveArray, float factor)
    {
        float[] octavesumArray = new float[width * height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                octavesumArray[x + y * width] = (sum[x + y * width] + ( (octaveArray[x + y * width]- 0.5f)*factor ));
            }

        return octavesumArray;
    }
}
