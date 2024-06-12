using UnityEngine;
using System.Collections;

public class MoveGameObject : MonoBehaviour
{
    public GameObject obj; // The GameObject you want to move
    public float speed = 1f; // The speed at which the GameObject moves

    private float leftBound; // The x position of the left bound of the canvas
    private float rightBound; // The x position of the right bound of the canvas

    void Start()
    {
        // Calculate the left and right bounds based on the width of the canvas
        Canvas canvas = obj.GetComponentInParent<Canvas>();
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        leftBound = -canvasWidth / 2;
        rightBound = canvasWidth / 2;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = obj.transform.localPosition + new Vector3(speed * Time.deltaTime, 0, 0);

        // If the GameObject has reached the right bound, set its x position to the left bound
        if (newPosition.x >= rightBound)
        {
            StartCoroutine(WaitAndReset());
        }
        else
        {
            // Set the GameObject's position to the new position
            obj.transform.localPosition = newPosition;
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0);
        Vector3 startPositionVector = new Vector3(leftBound, obj.transform.localPosition.y, obj.transform.localPosition.z);
        obj.transform.localPosition = startPositionVector;
    }
}
