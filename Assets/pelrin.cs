using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelrin
{

    int height, width, sliceStart, octaves;
    pelrinOctave[] pOctave;

    public pelrin(int height, int width, int sliceStart,int octaves, int seed, Vector2 pos)
    {


        this.sliceStart = sliceStart;
        this.height = height;
        this.width = width;

        pOctave = new pelrinOctave[octaves];

        for (int i = 0; i < octaves; i++)
        {
            int slices2 = (int)(sliceStart * (Mathf.Pow(2.0f, i)));

            pOctave[i] = new pelrinOctave(height, width, slices2, seed, pos);

        }
    }

    public float getpelrin(int x, int y)
    {
        float res = 0.5f;

        for (int i = 0; i < pOctave.Length; i++)
        {
            float factor = (1.0f / Mathf.Pow(2, (i)));

            res += (pOctave[i].getpelrinOctave(x, y) - 0.5f) * factor; 

        }

        return res;
    }
}
