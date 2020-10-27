using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardController : MonoBehaviour
{
    [Serializable]
    public class CardSetting
    {
        public CardType CardType;
        public AttackType AttackType;
        public DefenceType DefenceType;
        public int Power;
    }
    public enum CardType
    {
        Attack,
        Defence
    }

    public enum AttackType
    {
        Highkick,
        Lowkick,
        Middlekick,
        Overhand,
        None // should be always last
    }

    public enum DefenceType
    {
        HighDefence,
        MiddleDefence,
        LowDefence,
        TakeDownDefence,
        None // should be always last
    }

    public CardSetting Configuration { get; set; }

    public TextMeshProUGUI ActionLabel;

    public Image Image;

    public Color AttackColor;
    public Color DefenceColor;

    public void SetCard(CardSetting settings)
    {
        if (settings != null)
            Configuration = settings;
        ActionLabel.text = Configuration.CardType == CardType.Attack ? Configuration.AttackType.ToString() : Configuration.DefenceType.ToString();
        Image = GetComponent<Image>();
        if (Image != null)
            Image.color = Configuration.CardType == CardType.Attack ? AttackColor : DefenceColor;
    }
}
