using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] Transform headTransform;
    [SerializeField] float shotDelay;

    [Header("Projectiles")]
    [SerializeField] Transform projectileBlue;
    [SerializeField] Transform projectileOrange;
    [SerializeField] Transform muzzleEnd;

    [SerializeField] float ballFlightTime = 0.5f;

    float nextShot;
    private RaycastHit hit;

    PlayerPortalManager ppm;
    Animator myAnimator;

    private void OnEnable()
    {
        ppm = GetComponent<PlayerPortalManager>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") != 0 && Time.time >= nextShot)
        {
            Shoot(PortalColor.BLUE);
            nextShot = Time.time + shotDelay;
        }
        if (Input.GetAxis("Fire2") != 0 && Time.time >= nextShot)
        {
            Shoot(PortalColor.ORANGE);
            nextShot = Time.time + shotDelay;
        }
    }

    private void Shoot(PortalColor portalToPlaceColor)
    {
        myAnimator.SetTrigger("Shoot");
        Debug.DrawRay(headTransform.position, headTransform.forward, Color.green, 2f);
        if (Physics.Raycast(headTransform.position, headTransform.forward, out hit))
        {
            print(hit.transform.name);
			ShotGraphics(portalToPlaceColor, hit);
			ppm.PlacePortal(portalToPlaceColor, hit);
        }
    }

    void ShotGraphics(PortalColor portalToPlaceColor, RaycastHit hit)
    {
        Transform projectilePrefab = portalToPlaceColor ==
            PortalColor.BLUE ? projectileBlue : projectileOrange;

        // Obliczyæ kierun
        Vector3 kierun = hit.point - muzzleEnd.position;
        float odleglosc = kierun.magnitude;
        float predkosc = odleglosc / ballFlightTime;

        // Stworzyæ pocisk (w dobrym miejscu i sobie zapisaæ do neigo odwolanie)
        var projectile = Instantiate(projectilePrefab, muzzleEnd.position, Quaternion.identity);

        // Nadaæ si³ê
        Rigidbody rb = projectile.GetComponent <Rigidbody>();
        rb.AddForce(kierun.normalized * predkosc, ForceMode.VelocityChange);

    }
}
