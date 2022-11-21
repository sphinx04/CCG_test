using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image artImage;
    [SerializeField] private float fontSizeMultiplier = 1.5f;

    private Card card;
    private int manaPoints;
    private int attackPoints;
    private int healthPoints;
    
    public void Init(Card card, int orderInLayer = 0)
    {
        this.card = card;
        
        card.OnManaPointChange += UpdateMana;
        card.OnAttackPointChange += UpdateAttack;
        card.OnHealthPointChange += UpdateHealth;
        card.OnCardHealthExpired += DestroyCard;
        
        artImage.sprite = card.ArtSprite;
        manaText.text = card.ManaPoints.ToString();
        attackText.text = card.AttackPoints.ToString();
        healthText.text = card.HealthPoints.ToString();
        nameText.text = card.CardName;
        descriptionText.text = card.Description;

        manaPoints = card.ManaPoints;
        attackPoints = card.AttackPoints;
        healthPoints = card.HealthPoints;
        
        artImage.canvas.sortingOrder = orderInLayer;
    }

    private void UpdateMana(int newValue)
    {
        StartCoroutine(ScrollToNumber(manaPoints, newValue, manaText));
        manaPoints = newValue;
    }

    private void UpdateAttack(int newValue) 
    {
        StartCoroutine(ScrollToNumber(attackPoints, newValue, attackText));
        attackPoints = newValue;
    }
    private void UpdateHealth(int newValue) 
    {
        StartCoroutine(ScrollToNumber(healthPoints, newValue, healthText));
        healthPoints = newValue;
    }

    private IEnumerator ScrollToNumber(int oldValue, int newValue, TextMeshProUGUI text)
    {
        float t = 0.0f;
        float moveDuration = 1f;
        int currentValue = oldValue;
        float waitTime = 1f / Mathf.Abs(newValue - oldValue);


        if (currentValue == newValue) yield return null;

        text.canvas.sortingOrder += 50;
        text.fontSize *= fontSizeMultiplier;
        
        while (currentValue != newValue)
        {
            if (currentValue > newValue) currentValue--;
            else currentValue++;
            text.text = currentValue.ToString();
            yield return new WaitForSeconds(waitTime);
        }

        text.fontSize /= fontSizeMultiplier;
        text.canvas.sortingOrder -= 50;
        
        text.text = newValue.ToString();
    }

    private void DestroyCard()
    {
        transform.parent = null;
        Hand.Remove(card);
        Destroy(gameObject);
    }
}
