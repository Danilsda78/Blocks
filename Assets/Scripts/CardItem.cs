using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Animator _animatorBorder;

    public int Id;
    public Image Image;
    public TMP_Text TextName;
    public TMP_Text TextCost;
    public Button Button;
    public Animator Animator;
    public EcsEntity Entity;
    [Header("Cube")]
    public GameObject Cube;

    public EcsEntity Init(CardData cardData, EcsWorld world)
    {
        Entity = world.NewEntity();
        ref var c = ref Entity.Get<CardC>();

        c.Info.Cost = cardData.Cost;
        c.CardItem = this;
        c.Info.Id = Id = cardData.Id;

        TextName.text = cardData.Name;
        TextCost.text = cardData.Cost.ToString();

        var cube = Instantiate(cardData.Material, this.Cube.transform);
        cube.layer = 20;
        cube.transform.localPosition = new Vector2(0, -0.5f);

        Button.onClick.AddListener(() =>
        {
            world.NewEntity().Get<EBuyCardC>().Entity = Entity;
        });

        return Entity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.Animator.Play("CubeStayRotation");
    }
}
