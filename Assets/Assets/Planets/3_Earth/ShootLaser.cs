using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ShootLaser : MonoBehaviour
{
    public float bulletSpeed;
    public float Catridge = 200;
    public float Catridge_Max = 200;
    public bool _Reloading;

    public Slider Catridge_Visual;
    public TextMeshProUGUI Catridge_Number;

    public float ShootRefresh;
    public bool Shootok;

    public bool ShootHold;

    public GameObject ShootButton;
    public GameObject Crosshair;
    public Camera Cam;
    public GameObject BulletPrefab;

    public float Accuracy;
    public int Shots;
    public int Hits;
    // Start is called before the first frame update
    void Start()
    {
        ShootRefresh = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Shots > 0)
        {
            Accuracy = (float)Hits / (float)Shots;
        }

        if (Cam == null && GameObject.Find("AR Camera"))
        {
            Cam = GameObject.Find("AR Camera").GetComponent<Camera>();
        }

        Catridge_Visual.value = Catridge;
        Catridge_Number.text = ((int)Catridge).ToString();


        if (Catridge <= 0 || _Reloading)
        {
            Reload();
        }

        if (_Reloading && Catridge >= Catridge_Max)
        {
            Catridge = Catridge_Max;
            _Reloading = false;
        }

        if (_Reloading)
        {
            Reloading();
        }

        if (ShootRefresh > 0)
        {
            ShootRefresh -= Time.deltaTime;
        }
        else if(!Shootok)
        {
            Shootok = true;
        }

        if (ShootHold)
        {
            Shoot();
        }

        if (Input.touchCount == 1)
        {
            if (IsPointerOverUIObject())
            {
                Touch screenTouch = Input.GetTouch(0);
                RectTransform buttonRect = ShootButton.GetComponent<RectTransform>();
                if ((screenTouch.position.x < buttonRect.position.x + buttonRect.sizeDelta.x / 2 && screenTouch.position.x > buttonRect.position.x - buttonRect.sizeDelta.x / 2) && (screenTouch.position.y < buttonRect.position.y + buttonRect.sizeDelta.y / 2 && screenTouch.position.y > buttonRect.position.y - buttonRect.sizeDelta.y / 2))
                {
                    ShootHold = true;
                }
            }
        }

        if (Input.touchCount == 0)
        {
            ShootHold = false;
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public void Shoot()
    {
        if (!_Reloading && Shootok)
        {
            GameObject bullet = Instantiate(BulletPrefab, Cam.ScreenToWorldPoint(Crosshair.GetComponent<RectTransform>().position), Cam.transform.rotation);
            bullet.GetComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<Bullet>().sl = this;
            bullet.GetComponent<Bullet>().LifeTime = 2.5f;
            bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            ShootRefresh = 0.05f;
            Shootok = false;
            Catridge--;
            Shots++;
        }
    }

    public void Reload()
    {
        _Reloading = true;
    }

    public void Reloading()
    {
        if (Catridge < Catridge_Max)
        {
            Catridge += 150 * Time.deltaTime;
        }

    }
}
