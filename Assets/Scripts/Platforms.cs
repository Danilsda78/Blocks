using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public EcsEntity Entity;
    
    [SerializeField] private GameObject _item;

    private List<Vector3[]> _listPositions = new List<Vector3[]>();

    public EcsEntity Init(EcsWorld world, int rows, int colums)
    {
        _listPositions = InstanPlatform(rows, colums);

        Entity = world.NewEntity();
        Entity.Get<PlatformC>().ListPositions = _listPositions;
        Entity.Get<PlatformC>().Transform = transform;

        return Entity;
    }

    public void Destroy()
    {
        Entity.Destroy();
        GameObject.Destroy(gameObject);
    }

    private List<Vector3[]> InstanPlatform(int rows, int colums)
    {
        var sizeZ = rows + rows * 0.2f;
        var sizeX = colums + colums * 0.2f;

        var list = new List<Vector3[]>()
            {
                Instan(-sizeX, 0),
                Instan(0, sizeZ) ,
                Instan(sizeX, 0) ,
                Instan(0, -sizeZ),
                Instan(0, 0),
            };

        Vector3[] Instan(float sX, float sZ)
        {
            var result = new Vector3[rows * colums];
            var el = 0;

            for (int i = rows; i > 0; i--)
                for (int j = 0; j < colums; j++)
                {
                    float z = i, x = j, offsetX = x * 0.2f, offsetZ = z * 0.2f;
                    var go = GameObject.Instantiate(_item, transform);

                    go.transform.position = new Vector3(sX + x + offsetX, 0, sZ + z + offsetZ);
                    result[el] = go.transform.position;
                    el++;
                }

            return result;
        }

        return list;
    }
}
