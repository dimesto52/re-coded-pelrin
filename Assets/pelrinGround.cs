using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class pelrinGround : MonoBehaviour
{
    Dictionary<string, chunkHeightData> data = new Dictionary<string, chunkHeightData>();
    public chunkHeightData getData(Vector3 pos)
    {
        if(!data.ContainsKey(pos.ToString()))
        {
            data.Add(pos.ToString(), new chunkHeightData(chunkSize, pos));
        }

        //Debug.Log(pos.ToString());
        return data[pos.ToString()];
    }

    public int chunkSize = 64;
    public int chunkView = 8;

    public float midleChunk
    {
        get
        {
            return chunkSize / 2.0f;
        }
    }

    public Vector3 cameraPosition
    {
        get
        {
            return Camera.main.gameObject.transform.position;
        }
    }

    public Vector2 CameraChunk
    {
        get
        {
            return new Vector2((int)(cameraPosition.x / chunkSize), (int)(cameraPosition.z / chunkSize));
        }
    }
    public Vector2 cameraPositionInChunk
    {
        get
        {
            return new Vector2(cameraPosition.x % chunkSize, cameraPosition.z % chunkSize);
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        data.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for(int x= -chunkView; x <= chunkView; x++)
        {
            for (int z = -chunkView; z <= chunkView; z++)
            {
                debugViewChunk(new Vector2(x + CameraChunk.x,z+CameraChunk.y));
            }
        }
    }
    void debugViewChunk(Vector2 pos)
    {
        Vector3 pos0_0 = new Vector3(-midleChunk + pos.x * chunkSize, 0, -midleChunk + pos.y * chunkSize);
        Vector3 pos1_0 = new Vector3(midleChunk + pos.x * chunkSize, 0, -midleChunk + pos.y * chunkSize);
        Vector3 pos1_1 = new Vector3(midleChunk + pos.x * chunkSize, 0, midleChunk + pos.y * chunkSize);
        Vector3 pos0_1 = new Vector3(-midleChunk + pos.x * chunkSize, 0, midleChunk + pos.y * chunkSize);

        Debug.DrawLine(pos0_0, pos1_0, Color.yellow);
        Debug.DrawLine(pos1_0, pos1_1, Color.yellow);
        Debug.DrawLine(pos1_1, pos0_1, Color.yellow);
        Debug.DrawLine(pos0_1, pos0_0, Color.yellow);

        debugViewinChunk(pos);
    }
    void debugViewinChunk(Vector2 pos)
    {
        chunkHeightData cdata = getData(pos);

        for (int x = 0; x < chunkSize; x++)
            for (int y = 0; y < chunkSize; y++)
            {
                float val0_0 = cdata.getData(x + 0, y + 0);
                float val1_0 = cdata.getData(x + 1, y + 0);
                float val1_1 = cdata.getData(x + 1, y + 1);
                float val0_1 = cdata.getData(x + 0, y + 1);

                Vector3 basePosition = new Vector3(-midleChunk + pos.x * chunkSize + 1, 0, -midleChunk + pos.y * chunkSize+1);


                Vector3 pos0_0 = basePosition + new Vector3(x - 1, 0, y - 1) + Vector3.up * val0_0 * chunkSize - Vector3.up * chunkSize/2.0f;
                Vector3 pos1_0 = basePosition + new Vector3(x - 0, 0, y - 1) + Vector3.up * val1_0 * chunkSize - Vector3.up * chunkSize / 2.0f;
                Vector3 pos1_1 = basePosition + new Vector3(x - 0, 0, y - 0) + Vector3.up * val1_1 * chunkSize - Vector3.up * chunkSize / 2.0f;
                Vector3 pos0_1 = basePosition + new Vector3(x - 1, 0, y - 0) + Vector3.up * val0_1 * chunkSize - Vector3.up * chunkSize / 2.0f;

                Debug.DrawLine(pos0_0, pos1_0, Color.green);
                Debug.DrawLine(pos1_0, pos1_1, Color.green);
                Debug.DrawLine(pos1_1, pos0_1, Color.green);
                Debug.DrawLine(pos0_1, pos0_0, Color.green);
            }
    }
}
public class chunkHeightData
{

    public int chunkSize = 64;

    public float midleChunk
    {
        get
        {
            return chunkSize / 2.0f;
        }
    }

    float[] data;

    public float getData(int x, int y)
    {
        if (x > (chunkSize + 1) || y > (chunkSize + 1)) return 0;
        return data[x + (chunkSize + 1) * y];
    }
    public void setData(int x, int y, float dataIn)
    {
        data[x + (chunkSize + 1) * y] = dataIn;
    }

    public chunkHeightData(int chunkSize, Vector2 pos)
    {
        this.chunkSize = chunkSize;

        data = new float[(chunkSize+1) * (chunkSize + 1)];

        generate(pos);
    }

    void generate(Vector2 pos)
    {

        pelrin p = new pelrin(chunkSize, chunkSize, 1, 3, 0, pos);

        for (int x = 0; x < chunkSize+1; x++)
            for (int y = 0; y < chunkSize+1; y++)
            {
                setData(x, y, p.getpelrin(x, y));
            }
    }
}