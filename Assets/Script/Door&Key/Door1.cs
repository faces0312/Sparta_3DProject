using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : Door
{
    public GameObject directionLight;

    public override void OpenEffect()
    {
        //StartCoroutine(RotateObject());
        directionLight.transform.rotation = Quaternion.Euler(270f, -30f, 0);
        //전체적인 빛과 반사되는 빛도 함께 조절
        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
    }
    private IEnumerator RotateObject()
    {
        for (int i = 0; i <= 220; i++)
        {
            directionLight.transform.Rotate(Vector3.right);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
