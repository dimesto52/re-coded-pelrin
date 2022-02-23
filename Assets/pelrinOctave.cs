using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelrinOctave
{

    float[] slicesArray;
    int height, width, slices;
    int seed;
    Vector2 pos;

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
        this.seed = seed;
        this.pos = pos;

    }

    public float getSlices(int x,int y)
    {
        float sqrtMaxInt = Mathf.Sqrt(int.MaxValue);
        float _sqrtMaxInt = sqrtMaxInt - 1;
        float xSlices = (x + pos.x * slices);
        float ySlices = (y + pos.y * slices);

        int posSeed = ((int)((xSlices % _sqrtMaxInt) + (ySlices % _sqrtMaxInt) * sqrtMaxInt));
        Random.InitState(seed | posSeed);


        return Random.Range(0, 1.0f);
    }



    public float getpelrinOctave(int x, int y)
    {

        int sizedividW = (width / (dividBy - 1));
        int sizedividh = (height / (dividBy - 1));


        if (sizedividW < 1) sizedividW = 1;
        if (sizedividh < 1) sizedividh = 1;


        int xbase = x / sizedividW;
        int ybase = y / sizedividh;

        float xfacteur = ((float)x % sizedividW) / sizedividW;
        float yfacteur = ((float)y % sizedividh) / sizedividh;

        //linear
        /*
        float base0_0 = getSlices((xbase + 0), (ybase + 0));
        float base1_0 = getSlices((xbase + 1), (ybase + 0));
        float base0_1 = getSlices((xbase + 0), (ybase + 1));
        float base1_1 = getSlices((xbase + 1), (ybase + 1));


        return biLinearIterpolation(base0_0, base1_0, base0_1, base1_1,
                                         xfacteur, yfacteur);
                                         */

        float base0_0 = getSlices((xbase - 1), (ybase - 1));
        float base1_0 = getSlices((xbase + 0), (ybase - 1));
        float base2_0 = getSlices((xbase + 1), (ybase - 1));
        float base3_0 = getSlices((xbase + 2), (ybase - 1));

        float base0_1 = getSlices((xbase - 1), (ybase + 0));
        float base1_1 = getSlices((xbase + 0), (ybase + 0));
        float base2_1 = getSlices((xbase + 1), (ybase + 0));
        float base3_1 = getSlices((xbase + 2), (ybase + 0));

        float base0_2 = getSlices((xbase - 1), (ybase + 1));
        float base1_2 = getSlices((xbase + 0), (ybase + 1));
        float base2_2 = getSlices((xbase + 1), (ybase + 1));
        float base3_2 = getSlices((xbase + 2), (ybase + 1));

        float base0_3 = getSlices((xbase - 1), (ybase + 2));
        float base1_3 = getSlices((xbase + 0), (ybase + 2));
        float base2_3 = getSlices((xbase + 1), (ybase + 2));
        float base3_3 = getSlices((xbase + 2), (ybase + 2));
        /*
        xfacteur = xfacteur / 2.0f + 0.25f;
        yfacteur = yfacteur / 2.0f + 0.25f;
        //*/
        return biCubicIterpolation(base0_0, base1_0, base2_0, base3_0,
                                  base0_1, base1_1, base2_1, base3_1,
                                  base0_2, base1_2, base2_2, base3_2,
                                  base0_3, base1_3, base2_3, base3_3,
                                           xfacteur, yfacteur);

    }

    public float linearIterpolation(float val1, float val2, float facteur)
    {
        return val1 * (1 - facteur) + val2 * (facteur);
    }
    public float biLinearIterpolation(float base0_0, float base1_0, float base0_1, float base1_1,
                                        float xfacteur, float yfacteur)
    {
        float r1 = linearIterpolation( base0_0, base1_0, xfacteur);
        float r2 = linearIterpolation(base0_1, base1_1, xfacteur);

        return linearIterpolation(r1, r2, yfacteur);
    }

    public float cubicIterpolation(float preval1,float val1, float val2,  float nextval2, float facteur)
    {
        // for better : https://www.paulinternet.nl/?page=bicubic
        return val1 
            + 0.5f * facteur * (val2 - preval1 
            + facteur * (2.0f * preval1 - 5.0f * val1 
            + 4.0f * val2 - nextval2 
            + facteur * (3.0f * (val1 - val2) + nextval2 - preval1)));
    }
    public float biCubicIterpolation(float base0_0, float base1_0, float base2_0, float base3_0,
                                   float base0_1, float base1_1, float base2_1, float base3_1,
                                   float base0_2, float base1_2, float base2_2, float base3_2,
                                   float base0_3, float base1_3, float base2_3, float base3_3,
                                        float xfacteur, float yfacteur)
    {
        float r0 = cubicIterpolation(base0_0, base1_0, base2_0, base3_0, xfacteur);
        float r1 = cubicIterpolation(base0_1, base1_1, base2_1, base3_1, xfacteur);
        float r2 = cubicIterpolation(base0_2, base1_2, base2_2, base3_2, xfacteur);
        float r3 = cubicIterpolation(base0_3, base1_3, base2_3, base3_3, xfacteur);

        return cubicIterpolation(r0, r1, r2, r3, yfacteur);
    }


}
