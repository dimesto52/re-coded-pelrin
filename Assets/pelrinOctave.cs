using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelrinOctave
{

    float[] slicesArray;
    int height, width, slices;

    int dividBy
    {
        get
        {
            return slices + 1;
        }
    }


    public pelrinOctave(int height, int width, int slices, int seed, Vector2 pos)
    {


        this.slices = slices;
        this.height = height;
        this.width = width;
        slicesArray = getpelrinSlices(seed, pos);

    }

    public float[] getpelrinSlices( int seed, Vector2 pos)
    {

        float[] slicesArray = new float[dividBy * dividBy];
        for (int x = 0; x < dividBy; x++)
            for (int y = 0; y < dividBy; y++)
            {
                float sqrtMaxInt = Mathf.Sqrt(int.MaxValue);
                int posSeed = ((int)(((x + pos.x * slices) % sqrtMaxInt - 1) + ((y + pos.y * slices) % sqrtMaxInt - 1) * sqrtMaxInt));
                //Debug.Log((seed | posSeed).ToString());
                Random.InitState(seed | posSeed);


                slicesArray[x + y * dividBy] = Random.Range(0, 1.0f);
            }

        return slicesArray;
    }


    public float getpelrinOctave(int x, int y)//for better : https://en.wikipedia.org/wiki/Bilinear_interpolation#/media/File:BilinearInterpolation.svg
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

        float res = (base0_0 + base1_0 + base0_1 + base1_1);

        return res;
    }
}
