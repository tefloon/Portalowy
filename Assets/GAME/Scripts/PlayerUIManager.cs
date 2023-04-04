using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Sprite blueClosedSprite;
    public Sprite blueOpenSprite;

    public Sprite orangeFilledSprite;
    public Sprite orangeOutlineSprite;

    public Image blueImage;
    public Image orangeImage;

    public PlayerPortalManager ppm;

    public void UpdateOrange(bool doesExist)
    {
        if (doesExist)
        {
            orangeImage.sprite = orangeFilledSprite;
        }
        else
        {
            orangeImage.sprite = orangeOutlineSprite;
        }

        print("wywo�a�em funkcj� updateOrange");
    }

    public void UpdateBlue()
    {
        print("wywo�a�em funkcj� updateBlue");
    }
}
