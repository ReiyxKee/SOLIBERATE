using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class SL9 : MonoBehaviour
{
    public ParticleSystem Kaboom;
    public GameObject _25;
    public GameObject _50;
    public GameObject _75;
    public GameObject _100;

    public bool Phase1;
    
    public bool Phase2;
    
    public bool Phase3;

    public bool Kaboomed;

    public bool Hit;

    public float endGame;


    public bool Started;
    public bool CD;
    public float TimeCount;

    public GameObject UI_EndGame;

    public float maxHP;
    public float HP;

    public Camera ARCam;
    public Planet_Rotation rot;
    public ParticleSystem inAtmosphere;

    public bool OnTap;
    public float TapDamage_Min;
    public float TapDamage_Max;

    public Transform Canvas;
    public GameObject TapDamage_Prefab;
    public GameObject KaboomPrefab;

    public Slider HPBar;
    public TextMeshProUGUI HPText;

    public int Combo;
    public float ComboReset_Timer;

    public GameObject ComboParent;
    public TextMeshProUGUI ComboText;

    public Color32 Combo0;
    public Color32 Combo50;
    public Color32 Combo100;
    public Color32 Combo250;
    public Color32 Combo500;
    public Color32 Combo1000;

    public float CurrentMinDMG;
    // Start is called before the first frame update
    void Start()
    {
        Started = false;
        CD = false;
        rot.Self_Rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        ComboParent.GetComponent<TextMeshProUGUI>().color = ComboText.color;

        CurrentMinDMG = (TapDamage_Min + (TapDamage_Max - TapDamage_Min) * Mathf.Clamp(Combo, 0, 999) / 999);

        if (Started)
        {
            CD = true;
            rot.Self_Rotate = true;
        }

        HPBar.value = HP;
        HPText.text = HP.ToString("00") + " / " + maxHP.ToString("00");

        if (ARCam == null)
        {
            if (GameObject.Find("AR Camera"))
            {
                ARCam = GameObject.Find("AR Camera").GetComponent<Camera>();
            }
        }

        if (Input.touchCount == 0 && OnTap)
        {
            OnTap = false;
        }
        
        if (Input.touchCount >= 1 && !OnTap && Started)
        {
            OnTap = true;
            //if (!IsPointerOverUIObject())
            {
                Touch screenTouch = Input.GetTouch(0);
                Ray ray = ARCam.ScreenPointToRay(screenTouch.position);
                RaycastHit hit;

                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Meteor")
                    {
                        float HitDamage = Random.Range((TapDamage_Min + (TapDamage_Max - TapDamage_Min) * Mathf.Clamp(Combo, 0, 999) / 999), TapDamage_Max);
                        GameObject Bomb = Instantiate(KaboomPrefab, hit.point, Quaternion.identity);
                        GameObject HitNumber = Instantiate(TapDamage_Prefab, Input.touches[0].position + new Vector2(Random.Range(-50, 50), 0), Quaternion.identity);

                        HitNumber.transform.SetParent(Canvas);
                        HitNumber.GetComponent<Tap_SL9>().ScaleRatio = HitDamage / TapDamage_Min;
                        HitNumber.GetComponent<TextMeshProUGUI>().text = HitDamage.ToString("00");
                        HitNumber.GetComponent<TextMeshProUGUI>().color = new Color32(255, (byte)(255 - 255 * (HitDamage - TapDamage_Min) / (TapDamage_Max - TapDamage_Min)), 0, 255);

                        HP -= HitDamage;

                        Combo++;
                        ComboReset_Timer = 1.5f;
                    }
                }
            }
        }


        if (Combo > 0)
        {
            ComboReset_Timer -= Time.deltaTime;
        }

        if (ComboReset_Timer <= 0)
        {
            Combo = 0;
        }


        switch (Combo)
        {
            case 0:
                ComboText.color = Combo0;
                break;
            case 25:
                ComboText.color = Combo50;
                break;
            case 50:
                ComboText.color = Combo100;
                break;
            case 100:
                ComboText.color = Combo250;
                break;
            case 250:
                ComboText.color = Combo500;
                break;
            case 500:
                ComboText.color = Combo1000;
                break;
        }


        ComboParent.SetActive(Combo > 0 ? true : false);
        ComboText.text = Combo.ToString();
        if (CD && HP > 0)
        {
            TimeCount += Time.deltaTime;
        }

        if (TimeCount >= 120)
        {
            TimeCount = 120;
        }


        if (Hit)
        {
            rot.Self_Rotate = false;
            _100.SetActive(false);
            _75.SetActive(false);
            _50.SetActive(false);
            _25.SetActive(false);
            inAtmosphere.Stop();

        }

        if (!Hit)
        {
            if (HP > 0.75 * maxHP)
            {
                _100.SetActive(true);
                _75.SetActive(false);
                _50.SetActive(false);
                _25.SetActive(false);
            }
            else if (0.5 * maxHP < HP && HP < 0.75 * maxHP)
            {
                _100.SetActive(false);
                _75.SetActive(true);
                _50.SetActive(false);
                _25.SetActive(false);
            }
            else if (0.25 * maxHP < HP && HP < 0.5 * maxHP)
            {
                _100.SetActive(false);
                _75.SetActive(false);
                _50.SetActive(true);
                _25.SetActive(false);
            }
            else if (0 < HP && HP < 0.25 * maxHP)
            {
                _100.SetActive(false);
                _75.SetActive(false);
                _50.SetActive(false);
                _25.SetActive(true);
            }
            else
            {
                inAtmosphere.Stop();
                HP = 0;
                _100.SetActive(false);
                _75.SetActive(false);
                _50.SetActive(false);
                _25.SetActive(false);
            }
        }
        if (Hit)
        {
            if (endGame < 4.5f)
            {
                endGame += Time.deltaTime;
                if (!Kaboomed)
                {
                    Kaboom.Play();
                    Kaboomed = true;
                }
            }
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

}
