using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [Serializable]
    public class Fighter
    {
        public string Name;
        public List<CardController> AttackCards;
        public List<CardController> DefenceCards;
        public int Health;
        public FighterStatistics FighterStatistic;

    }

    [Serializable]
    public class FighterStatistics
    {
        public int TotalFightsFought;
        public int TotalWins;
        public int TotalLoss;
        public int TotalDraws;
    }

    public static FightManager Instance;

    public enum FightType
    {
        regular,
        champion
    }

    private int regularFightLength = 3;//rounds
    private int championFightLength = 5;//rounds

    public static int MOVECOUNT = 5;
    public int CurrentRound { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("FightManager already exists");
    }

    public FightType CurrentFightType;

    public Fighter PlayerOne;
    public Fighter PlayerTwo;

    public bool FightInProgress { get; private set;}

    public void SetFight(FightType fightType, Fighter userFighter, Fighter npcFighter)
    {
        if (userFighter != null)
        {
            CurrentFightType = fightType;
            PlayerOne = userFighter;
            if (npcFighter != null)
                PlayerTwo = npcFighter;
            SetCardsToFighter(PlayerOne);
            SetCardsToFighter(PlayerTwo);

            CurrentRound = 1;
            FightInProgress = true;
        }
    }

    private void SetCardsToFighter(Fighter fighter)
    {
        fighter.AttackCards = GetCards(CardController.CardType.Attack, MOVECOUNT, null, null);
        fighter.DefenceCards = GetCards(CardController.CardType.Defence, MOVECOUNT, null, null);
    }

    private List<CardController> GetCards(CardController.CardType cardtype, int count, List<CardController.AttackType> attackList = null, List<CardController.DefenceType> defenceList = null)
    {
        //TODO respect attackList and defenceList mask

        System.Random random = new System.Random();
        List<CardController> cards = new List<CardController>();
        CardController controller = null;
        for (int i = 0; i< count; i++)
        {
            GameObject card = SpawnManager.Instance.CreateCardObject(SpawnManager.PrefabType.Card);
            if(card != null)
            {
                controller = card.GetComponent<CardController>();
                if (controller != null)
                {
                    CardController.CardSetting setting = new CardController.CardSetting();
                    if (cardtype == CardController.CardType.Defence)
                    {
                        Array defence = Enum.GetValues(typeof(CardController.DefenceType));
                        setting.CardType = CardController.CardType.Defence;
                        setting.DefenceType = (CardController.DefenceType)defence.GetValue(random.Next(0, defence.Length - 1));
                        setting.AttackType = CardController.AttackType.None;
                    }
                    if(cardtype == CardController.CardType.Attack)
                    {
                        Array defence = Enum.GetValues(typeof(CardController.AttackType));
                        setting.CardType = CardController.CardType.Attack;
                        setting.DefenceType = CardController.DefenceType.None;
                        setting.AttackType = (CardController.AttackType)defence.GetValue(random.Next(0, defence.Length - 1));
                    }
                    controller.SetCard(setting);
                    cards.Add(controller);
                }
            }
        }

        return cards.Count > 0 ? cards : null;
    }
    public void MakeMove()
    {

    }

    public void StartFight()
    {
        if(GameManager.Instance.CurrentFighter != null)
        {
            if (PlayerTwo == null)
                PlayerTwo = SaveManager.Instance.CreateNewFighter("NPC fighter", 1);
            SetFight(FightType.regular, GameManager.Instance.CurrentFighter, PlayerTwo);
        }
    }
}
