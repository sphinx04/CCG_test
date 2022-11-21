using System.Collections;
using UnityEngine;

public class PositionHand : MonoBehaviour
{
    [SerializeField] private Transform cardHolder;
    [SerializeField] private float cardOffsetX = -1.5f;
    [SerializeField] private float cardOffsetMultiplierY = 1f;
    [SerializeField] private float cardRotationMultiplier = 3f;
    [SerializeField] private float moveDuration;
    [SerializeField] private AnimationCurve curve;
    
    private void Awake()
    {
        Hand.OnHandChanged += () =>
        {
            StopAllCoroutines();
            SetPositions();
        };
    }

    private void SetPositions()
    {
        for (int i = 0; i < cardHolder.childCount; i++)
        {
            float xOffset = cardOffsetX * i - cardOffsetX/2f * (cardHolder.childCount - 1);
            float yOffset = curve.Evaluate((i + .5f) / cardHolder.childCount);

            StartCoroutine(MoveCoroutine(cardHolder.GetChild(i),
                new Vector3(xOffset, yOffset * cardOffsetMultiplierY, 0),
                new Quaternion(0, 0, -xOffset * cardRotationMultiplier, 1)));
        }
    }
    
    private IEnumerator MoveCoroutine(Transform cardTransform, Vector3 targetPosition, Quaternion targetRotation)
    {
        yield return new WaitForSeconds(.1f);
        float t = 0.0f;
        Vector3 startPos = cardTransform.localPosition;
        Quaternion startRot = cardTransform.localRotation;
        Vector3 endPos = targetPosition;
        Quaternion endRot = targetRotation;
 
        while ( t < moveDuration )
        {
            t += Time.deltaTime;
            cardTransform.localPosition = Vector3.Lerp( startPos, endPos, t/moveDuration);
            cardTransform.localRotation = Quaternion.Lerp( startRot, endRot, t/moveDuration);
            yield return null;
        }
    }
}