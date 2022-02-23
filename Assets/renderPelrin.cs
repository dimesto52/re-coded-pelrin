using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class renderPelrin : MonoBehaviour
{
    public int seed = 0;
    public int octaves = 5;
    public int slices = 2;
    public Vector2 pos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        int size = 256;

        Texture2D tx = pelrinMap.getpelrinMap(size, size, slices, octaves, seed,pos);

        Sprite sp = Sprite.Create(tx,new Rect(0,0, size, size),Vector2.one/2.0f,size);

        this.GetComponent<SpriteRenderer>().sprite = sp;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
