using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private Interact _interact;

    public Texture _item_tex;

    void DrawItem(float x, float y)
    {
        GUI.DrawTexture(new Rect(x, y, 64, 64), _item_tex, ScaleMode.ScaleToFit, true);
    }

    void OnGUI()
    {
        // check if the texture has been assigned
        if(!_item_tex)
        {
            // Print out error
            Debug.LogError("Assign a texture within the inspector");
            return;
        }

        /* switch (_interact.GetGameObject().name)
        {
        case "Briefcase":
            // Draw the item on to the canvas
            DrawItem(10, 10);
            break;
        } */
    }

}
