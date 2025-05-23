using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 15;
    [SerializeField] private TextMeshProUGUI ammoText;
    public int currentAmmo;
    [SerializeField] private Transform ammoPos;
    [SerializeField] private Transform SpawnAmmoPos;
    [SerializeField] private Image[] ammoImages;
    [SerializeField] private AudioManager am;
    

    void Start()
    {
        currentAmmo = maxAmmo;
        if(gameObject != null)
           CreateAmmo();
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        GunReload();
        IsCallBoss();
    }

    void RotateGun()
    {
        if (Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height ||
            Input.mousePosition.x < 0 || Input.mousePosition.y < 0)
        {
            return;
        }
        
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotateOffset);

        if (angle > 90 || angle < -90) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(1, -1, 1);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            
            // Làm mờ viên đạn thay vì tắt đi
            FadeOutAmmo(currentAmmo - 1);
            
            currentAmmo--;
            UpdateAmmoText();
            am.PlayShootSound();
        }
    }

    void GunReload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            ResetAmmoUI(); // Khôi phục độ trong suốt của tất cả viên đạn
            currentAmmo = maxAmmo;
            UpdateAmmoText();
            am.PlayReloadSound();
        }
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo > 0 ? currentAmmo.ToString() : "Empty";
        }
    }

    private void CreateAmmo()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            Vector3 positionAmmo = ammoPos.position + new Vector3(i * 0.6f, 0f, 0f);
            ammoImages[i].transform.position = positionAmmo;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            if(ammoImages[i] != null)
               ammoImages[i].enabled = false;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            ammoImages[i].enabled = true;
        }
        Start();
        GunReload();
        currentAmmo = maxAmmo;
        ResetAmmoUI();
        
    }

    // 🔹 Làm mờ viên đạn bằng cách giảm Alpha
    private void FadeOutAmmo(int index)
    {
        if (index >= 0 && index < ammoImages.Length)
        {
            Color tempColor = ammoImages[index].color;
            tempColor.a = 0.2f;  // Giảm alpha để làm mờ viên đạn (có thể chỉnh giá trị thấp hơn)
            ammoImages[index].color = tempColor;
        }
    }

    // 🔹 Khôi phục độ trong suốt của tất cả viên đạn
    private void ResetAmmoUI()
    {
        foreach (Image ammo in ammoImages)
        {
            Color tempColor = ammo.color;
            tempColor.a = 1f;  // Đặt lại alpha về 1 để hiện đạn rõ ràng
            ammo.color = tempColor;
        }
    }

    private void IsCallBoss()
    {
        if(GameManager.bossCalled == true)
            CreateNewAmmo(SpawnAmmoPos);
    }
    public void CreateNewAmmo(Transform bulletPos)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            Vector3 positionAmmo = bulletPos.position + new Vector3(i * 0.6f, 0f, 0f);
            ammoImages[i].transform.position = positionAmmo;
        }
    }
}
