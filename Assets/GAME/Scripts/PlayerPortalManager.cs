using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalManager : MonoBehaviour
{
    [Header("Tagi")]
    [SerializeField] string NoPortalTag;
    [SerializeField] string YesPortalTag;

    [Header("Portal prefabs")]
    [SerializeField] Transform portalBlue_open;
    [SerializeField] Transform portalBlue_closed;
    [Space()]
    [SerializeField] Transform portalOrange_open;
    [SerializeField] Transform portalOrange_closed;

    [Header("Current Portals")]
    //[HideInInspector] public Transform OrangeCurrent;
    //[HideInInspector] public Transform BlueCurrent;

    public PlayerUIManager puim;


    private Transform _orangeCurrent;
    public Transform OrangeCurrent {
        get
        {
            return _orangeCurrent;
        }
        set
        {

            puim.UpdateOrange( true );
            _orangeCurrent = value;
        } 
    }

    private Transform _blueCurrent;
    public Transform BlueCurrent
    {
        get
        {
            return _blueCurrent;
        }
        set
        {
            //puim.UpdateBlue(true);
            _blueCurrent = value;
        }
    }

    public void PlacePortal(PortalColor pc, RaycastHit hit)
    {
        // Sprawdzamy czy w tym miejscu da siê postawiæ portal
        if (!CanPlace(hit)) return;

        // Jaki portal mam postawiæ? (otwarty/zamkniêty)
        Transform currentPortalPrefab;

        if (pc == PortalColor.BLUE)
        {
            if (OrangeCurrent != null)
            {
                currentPortalPrefab = portalBlue_open;

                // Zmieniæ orange na open
                Transform currentTransform = OrangeCurrent.transform;
                Destroy(OrangeCurrent.gameObject);
                OrangeCurrent = Instantiate(portalOrange_open, currentTransform.position, currentTransform.rotation);
            }
            else
            {
                currentPortalPrefab = portalBlue_closed;
            }

            // Zamknij stary, jeœli istnieje
            if(BlueCurrent != null)
            {
                Destroy(BlueCurrent.gameObject);
            }
        }
        else // Stawiamy orange
        {
            if (BlueCurrent != null)
            {
                currentPortalPrefab = portalOrange_open;

                // Zmieniæ blue na open
                Transform currentTransform = BlueCurrent.transform;
                Destroy(BlueCurrent.gameObject);
                BlueCurrent = Instantiate(portalBlue_open, currentTransform.position, currentTransform.rotation);
            }
            else
            {
                currentPortalPrefab = portalOrange_closed;
            }

            // Zamknij stary, jeœli istnieje
            if (OrangeCurrent != null)
            {
                Destroy(OrangeCurrent.gameObject);
            }
        }

        
        // Pojaw portal
        var portal = Instantiate(currentPortalPrefab, hit.point, Quaternion.identity);
        portal.rotation = Quaternion.LookRotation(hit.normal);
        portal.transform.Rotate(0, 90, 0);

        // Ustaw go jako odpowiedni "Current"
        if (pc == PortalColor.BLUE)
        {
            BlueCurrent = portal;
        }
        else
        {
            OrangeCurrent = portal;
        }
    }

    private bool CanPlace(RaycastHit hit)
    {
        if (hit.transform.CompareTag(YesPortalTag)) return true;
        return false;
    }
}
