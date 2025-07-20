using Leopotam.Ecs;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private List<GameObject> list;
    public EcsEntity Entity;
    private EcsWorld _world;

    public EcsEntity Init(EcsWorld world)
    {
        _world = world;
        Entity = world.NewEntity();
        ref var c = ref Entity.Get<CubeC>();
        c.Transform = transform;

        if (list.Count != 0)
        {
            var count = Random.Range(0, list.Count);
            var item = list[count];

            if (item != null)
            {
                item.isStatic = true;
                Instantiate(item, transform).transform.localPosition = new Vector2(0, 0.2f);
                cube.SetActive(false);
            }
        }

        if (TryGetComponent<Animator>(out var animator))
            animator.Play("CubeStart");

        return Entity;
    }

    public async void Destroy()
    {
        gameObject.tag = "Untagged";
        Entity.Destroy();

        if (TryGetComponent<Animator>(out var animator))
            animator.Play("Destroy");

        await Task.Delay(700);
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Cube" && other.tag == "Cube")
            Entity.Get<ECubeOnTriggerEnterC>().other = other;
    }

    private void Handler_EndAnimate()
    {
        _world.NewEntity().Get<ECubeEndAnimationStartC>();
    }
}
