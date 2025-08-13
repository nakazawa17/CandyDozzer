using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int shotPower = MaxShotPower;
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public CandyManager candyManager;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    // 端っこをクリックした場合にbaseの範囲に飛ばせるようにする
    Vector3 GetInstantiatePosition()
    {
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        // シュータの座標を基準に補正する(-2.5 ~ 2.5の範囲の補正)
        return transform.position + new Vector3(x, 0, 0);
    }
    public void Shot()
    {
        if (candyManager.GetCandyAmount() <= 0)
        {
            return;
        }
        if (shotPower <= 0)
        {
            return;
        }
        // インスタンス生成(プレハブ、アタッチ先の座標、クォータ(姿勢制御))
        // GameObject candy = Instantiate(candyPrefabs, transform.position, Quaternion.identity);

        // 今のバージョンではダウンキャストは必要ない
        GameObject candy = (GameObject)Instantiate(SampleCandy(), GetInstantiatePosition(), Quaternion.identity);

        candy.transform.parent = candyParentTransform;

        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        // transform.forward:Z軸が向いている方向
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        candyManager.ConsumeCandy();
        ConsumePower();
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        string label = "";
        for (int i = 0; i < shotPower; i++)
        {
            label = label + "+";
        }
        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    void ConsumePower()
    {
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower()
    {
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }

}
