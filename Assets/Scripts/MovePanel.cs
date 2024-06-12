using UnityEngine;


public class MovePanel : MonoBehaviour
{
    public RectTransform panel; // The panel you want to move
    public float speed = 1f; // The speed at which the panel moves

    private float resetPosition; // The x position at which the panel resets
    private float startPosition; // The x position to which the panel resets

    void Start()
    {
        // Calculate the reset and start positions based on the width of the panel
        float panelWidth = panel.rect.width;
        resetPosition = panel.localPosition.x;
        startPosition = panel.localPosition.x - panelWidth;
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = panel.localPosition + new Vector3(speed * Time.deltaTime, 0, 0);

        // If the panel has reached the reset position, set its x position to the start position
        if (newPosition.x >= resetPosition)
        {
            newPosition.x = startPosition;
        }

        // Set the panel's position to the new position
        panel.localPosition = newPosition;
    }
}


