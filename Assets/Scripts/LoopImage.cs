using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveImage : MonoBehaviour
{
    public Image image; // The image you want to move
    public float speed = 1f; // The speed at which the image moves

    private float resetPosition; // The x position at which the image resets
    private float startPosition; // The x position to which the image resets

    void Start()
    {
        // Calculate the reset and start positions based on the width of the canvas
        Canvas canvas = image.canvas;
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float imageWidth = image.rectTransform.rect.width;
        float extraBounds = 100f; // Adjust this value to increase or decrease the out of bounds distance
        startPosition = -canvasWidth / 2 - imageWidth - extraBounds;
        resetPosition = canvasWidth / 2 + imageWidth + extraBounds;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = image.rectTransform.localPosition + new Vector3(speed * Time.deltaTime, 0, 0);

        // If the image has reached the reset position, set its x position to the start position
        if (newPosition.x - image.rectTransform.rect.width / 2 >= resetPosition)
        {
            StartCoroutine(WaitAndReset());
        }
        else
        {
            // Set the image's position to the new position
            image.rectTransform.localPosition = newPosition;
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0);
        Vector3 startPositionVector = new Vector3(startPosition, image.rectTransform.localPosition.y, image.rectTransform.localPosition.z);
        image.rectTransform.localPosition = startPositionVector;
    }
}



