using System;
using UnityEngine;

public class Card
{
    public event Action<int> OnManaPointChange;
    public event Action<int> OnAttackPointChange;
    public event Action<int> OnHealthPointChange;
    public event Action OnCardHealthExpired;

    private int manaPoints;
    private int attackPoints;
    private int healthPoints;

    public Card(int manaPoints, int attackPoints, int healthPoints, Sprite artSprite, string cardName, string description)
    {
        this.ArtSprite = artSprite;
        this.manaPoints = manaPoints;
        this.attackPoints = attackPoints;
        this.healthPoints = healthPoints;
        this.CardName = cardName;
        this.Description = description;
    }
    
    public Sprite ArtSprite { get; }

    public string CardName { get; }
    
    public string Description { get; }

    public int ManaPoints
    {
        get => manaPoints;
        set
        {
            manaPoints = value;
            OnManaPointChange?.Invoke(value);
        }
    }

    public int AttackPoints
    {
        get => attackPoints;
        set
        {
            attackPoints = value;
            OnAttackPointChange?.Invoke(value);
        }
    }

    public int HealthPoints
    {
        get => healthPoints;
        set
        {
            healthPoints = value;
            OnHealthPointChange?.Invoke(value);
            if(value <= 0) OnCardHealthExpired?.Invoke();
        }
    }
}
