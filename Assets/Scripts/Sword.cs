using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    private float rotateOffset = 270f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private Transform bulletPos2;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 15;
   [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image swordImageBorder;
    [SerializeField] private Transform swordPos;
    [SerializeField] private Transform swordSpawnPos;
      [SerializeField] private AudioManager am;
      [SerializeField] private GameObject swordAnim;
    public int currentAmmo;
   

    void Start()
    {
        
        currentAmmo = maxAmmo;
        CreateAmmo();
        UpdateSwordImage();
      //  UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
      //  GunReload();
      UpdateSwordImage();
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

        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) &&  Time.time > nextShot)
        {
            am.CutSound();
            if (transform.localScale.x == -1)
            {
                Slash(bulletPos.position, bulletPos.rotation);
            }
            else
            {
                Slash(bulletPos2.position, bulletPos2.rotation);
            }
            nextShot = Time.time + shotDelay;
            if (transform.localScale.x == -1)
            {
                Instantiate(bulletPrefab, bulletPos2.position, bulletPos2.rotation);
            }
            else Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            
            // Làm mờ viên đạn thay vì tắt đi
            currentAmmo--;
            UpdateSwordImage();
           // UpdateAmmoText();
        }
    }

    void GunReload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            am.ReCutSound();
            currentAmmo = maxAmmo;
            UpdateSwordImage();
         //   UpdateAmmoText();
        }
    }

    // private void UpdateAmmoText()
    // {
    //     if (ammoText != null)
    //     {
    //         ammoText.text = currentAmmo > 0 ? currentAmmo.ToString() : "Empty";
    //     }
    // }

    private void UpdateSwordImage()
    {
        if (swordImage != null)
        {
            // Tính tỷ lệ thời gian đã trôi qua kể từ lần bắn gần nhất
            float cooldownProgress = Mathf.Clamp01((Time.time - nextShot + shotDelay) / shotDelay);
        
            // Gán trực tiếp thay vì cộng dồn
            swordImage.fillAmount = cooldownProgress;
        }
    }
    private void CreateAmmo()
    {

            Vector3 positionAmmo = swordPos.position ;
            swordImage.transform.position = positionAmmo;
            swordImageBorder.transform.position = positionAmmo;
        
    }
    private void IsCallBoss()
    {
        if(GameManager.bossCalled == true)
            CreateNewAmmo(swordSpawnPos);
    }
    public void CreateNewAmmo(Transform swordSpawnPos)
    {
        if (swordImage != null && swordImageBorder != null)
        {

            Vector3 positionAmmo = swordSpawnPos.position;
            swordImage.transform.position = positionAmmo;
            swordImageBorder.transform.position = positionAmmo;
        }
    }
    

    public void OnDisable()
    {
       if(swordImage != null && swordImageBorder != null) 
       {
            swordImage.enabled = false;
        swordImageBorder.enabled = false; 
        ammoText.enabled = true;
       }
    }

    private void OnEnable()
    {
        swordImage.enabled = true;
        swordImageBorder.enabled = true;
        Start();
        currentAmmo = maxAmmo;
        ammoText.enabled = false;
        UpdateSwordImage();
    }

    public void Slash(Vector3 position, Quaternion rotation)
    {
        GameObject effect = Instantiate(swordAnim, position, rotation); // Tạo hiệu ứng chém
        Animator anim = effect.GetComponent<Animator>(); // Lấy animator nếu có

        if (anim != null)
        {
            float duration = anim.GetCurrentAnimatorStateInfo(0).length; // Lấy thời gian animation
            Destroy(effect, duration); // Xóa effect sau khi hoàn thành
        }
        else
        {
            Destroy(effect, 1f); // Nếu không có animator, xóa sau 1 giây
        }
    }

}
