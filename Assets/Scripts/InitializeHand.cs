using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class InitializeHand : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private string url;
    [SerializeField] private Vector2 imageSize;
    [TextArea] [SerializeField] private string descriptionPlaceholder;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private int minAmount = 6;
    [SerializeField] private int maxAmount = 9;

    private void Awake()
    {
        Hand.Init();
    }

    private void Start()
    {
        StartCoroutine(InitHand());
    }

    private IEnumerator InitHand()
    {
        for (int i = 0; i < Random.Range(minAmount, maxAmount + 1); i++)
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(SetImage(url));
        }
    }

    private IEnumerator SetImage(string url)
    {
        url += imageSize.x + "/" + imageSize.y;
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();

        Sprite sprite;

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(uwr.error);
            sprite = Sprite.Create(Texture2D.blackTexture,
                new Rect(0, 0, imageSize.x, imageSize.y), Vector2.zero);
        }
        else
        {
            sprite = Sprite.Create(DownloadHandlerTexture.GetContent(uwr),
                new Rect(0, 0, imageSize.x, imageSize.y), Vector2.zero);
        }
        CreateCard(sprite, Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10));
    }

    private void CreateCard(Sprite sprite, int manaPoints, int attackPoints, int healthPoints)
    {
        Card card = new Card(manaPoints, attackPoints, healthPoints, sprite,
            "CardName" + Random.Range(1, 100), descriptionPlaceholder);
        var cardView = Instantiate(cardPrefab, cardHolder).GetComponent<CardView>();
        cardView.Init(card, cardHolder.childCount);
        Hand.Add(card);
    }
}