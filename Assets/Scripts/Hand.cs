using System;
using System.Collections.Generic;

public static class Hand
{
    public static event Action OnHandChanged;
    private static List<Card> cards;
    
    public static void Add (Card card)
    {
        OnHandChanged?.Invoke();
        cards.Add(card);
    }

    public static void Remove(Card card)
    {
        OnHandChanged?.Invoke();
        cards.Remove(card);
    }

    public static List<Card> Get()
    {
        return cards;
    }

    public static void Init()
    {
        cards = new List<Card>();
    }
}