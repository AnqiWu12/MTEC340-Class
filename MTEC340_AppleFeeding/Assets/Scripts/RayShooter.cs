using System;
using System.Collections;
using UnityEngine;

// 从屏幕正中心射出一条射线来"扔苹果"：打中动物就喂它一口，
// 打到别的地方就放个小球标记一下命中点。
public class RayShooter : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        // 拿当前正在渲染的相机
        _cam = Camera.main;

        // 锁住并隐藏鼠标，第一人称瞄准就靠屏幕中心的准星
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 从屏幕正中心射出射线
            Vector3 screenMiddle = new(_cam.pixelWidth * 0.5f, _cam.pixelHeight * 0.5f, 0);
            Ray ray = _cam.ScreenPointToRay(screenMiddle);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObj = hit.transform.gameObject;

                // 动物的模型是子物体、脚本在父物体上，所以往父级找 Animal
                Animal animal = hitObj.GetComponentInParent<Animal>();

                if (animal)
                {
                    // 打中动物就喂一口
                    animal.Feed();
                }
                else
                {
                    // 打到墙或地面，放个小球标记命中点
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    // 在命中点生成一个小球，一秒后消失
    IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        yield return new WaitForSeconds(1.0f);
        Destroy(sphere);
    }
}