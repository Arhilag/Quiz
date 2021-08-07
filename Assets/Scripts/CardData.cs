using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Standart Card", fileName = "New Card")]
public class CardData : ScriptableObject
{
    [Tooltip("���")]
    [SerializeField] private string nameCard;
    public string NameCard
    {
        get { return nameCard; }
        protected set { }
    }

    [Tooltip("������")]
    [SerializeField] private Sprite spriteCard;
    public Sprite SpriteCard
    {
        get { return spriteCard; }
        protected set { }
    }
}
