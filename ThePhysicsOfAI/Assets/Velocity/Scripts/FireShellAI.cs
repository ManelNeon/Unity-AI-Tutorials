using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShellAI : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public Transform turretBase;
    public GameObject enemy;

    [SerializeField] float rotSpeed = 5.0f;

    [SerializeField] float bulletSpeed = 15f;

    [SerializeField] float moveSpeed = 1f;

    void CreateBullet() {

        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

        shell.GetComponent<Rigidbody>().velocity = bulletSpeed * turretBase.forward; 
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        
        float y = targetDir.y;

        targetDir.y = 0f;

        float x = targetDir.magnitude - 1;

        float gravity = 9.8f;

        float sSqr = bulletSpeed * bulletSpeed;

        float underTheSqrRoot = sSqr * sSqr - gravity * (gravity * x * x + 2 * y * sSqr);

        if(underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngle(true);
        if(angle != null)
        {
            turretBase.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }

        return angle;
    }

    void Update() {

        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

        float? angle = RotateTurret();

        if (angle != null) {
            CreateBullet();
        }
        else
        {
            this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
    }
}
