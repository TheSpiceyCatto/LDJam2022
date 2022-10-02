using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject mesh;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource deathSfx;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private int maxHp = 100;
    [SerializeField, Range(0.01f, 0.5f)] private float iFrames = 0.01f;
    [SerializeField, Range(0.001f, 5f)] private float abberationStep = 0.01f;

    [HideInInspector] public float hp;
    private bool canHurt = true;
    private Rigidbody gunRb;
    private VolumeProfile profile;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    
    private void Awake()
    {
        profile = GameObject.Find("Global Volume").GetComponent<UnityEngine.Rendering.Volume>().profile;
        profile.TryGet(out chromaticAberration);
        profile.TryGet(out vignette);
        hp = maxHp;
        StartCoroutine(HalveHpTime());
        gunRb = gun.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (canHurt && collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(ReduceHp(iFrames));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            StartCoroutine(ReduceHp(iFrames));
            other.GetComponent<EnemyBullet>().DestroyBullet();
        }
    }

    private void UpdateUI(string hpText)
    {
        text.text = string.Format("HP: {0}", hpText);
    }
    private void HalveHp()
    {
        hp = Mathf.Round(hp * 0.5f);
    }

    private IEnumerator HalveHpTime()
    {
        UpdateUI(hp.ToString());
        yield return new WaitForSeconds(10);
        HalveHp();
        DeathCheck();
        UpdateUI(hp.ToString());
        StartCoroutine(HalveHpTime());
    }

    private IEnumerator ReduceHp(float iTime)
    {
        canHurt = false;
        hp -= 1;
        UpdateUI(hp.ToString());
        DeathCheck();
        yield return new WaitForSeconds(iTime);
        canHurt = true;
    }

    public void GainHp(int heal)
    {
        hp += heal;
        if (hp > 100)
            hp = 100;
        UpdateUI(hp.ToString());
    }

    private void Update()
    {
        float desiredAbberation =
            Mathf.MoveTowards(chromaticAberration.intensity.value, (maxHp - hp) / maxHp * 0.5f, abberationStep);
        chromaticAberration.intensity.Override(desiredAbberation);
        vignette.intensity.Override(desiredAbberation);
    }

    private void DeathCheck()
    {
        if (hp <= 0)
        {
            hp = 0;
            deathSfx.Play();
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            UpdateUI(hp.ToString());
            mesh.SetActive(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<PlayerShoot>().enabled = false;
            gun.SetActive(false);
            //gunRb.isKinematic = false;
            //gunRb.useGravity = true;
            //gunRb.velocity = new Vector3(Random.Range(-5f, 5f),Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            //gunRb.rotation = Random.rotation;
            //gun.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
