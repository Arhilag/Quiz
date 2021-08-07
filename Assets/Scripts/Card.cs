using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    private Spawner spawner;
    private CardData data;
    public void Init(CardData _data)
    {
        data = _data;
        GetComponent<SpriteRenderer>().sprite = data.SpriteCard;
    }
    public string Name
    {
        get { return data.NameCard; }
        protected set { }
    }

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        transform.DOShakeScale(1.5f, new Vector3(0.3f,0.3f,0), 1, 0);
    }

    private void OnMouseDown()
    {
        bool d = spawner.Ñomparison(Name);
        if(d == true) transform.DOShakeScale(1.5f, new Vector3(0.3f, 0.3f, 0), 1, 0);
        else transform.DOShakePosition(1.5f, new Vector3(0.3f, 0, 0), 2, 0);
    }
}
