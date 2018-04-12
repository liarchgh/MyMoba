using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionCamera : MonoBehaviour {
    public Camera RefCamera;
    public Material RefMat;
    private Transform Panel;

    void RenderRefection()
    {
        Vector3 normal = Panel.up;
        float d = -Vector3.Dot(normal, Panel.position);
        Matrix4x4 refMatrix = new Matrix4x4();
        refMatrix.m00 = 1 - 2 * normal.x * normal.x;
        refMatrix.m01 = -2 * normal.x * normal.y;
        refMatrix.m02 = -2 * normal.x * normal.z;
        refMatrix.m03 = -2 * d * normal.x;

        refMatrix.m10 = -2 * normal.x * normal.y;
        refMatrix.m11 = 1 - 2 * normal.y * normal.y;
        refMatrix.m12 = -2 * normal.y * normal.z;
        refMatrix.m13 = -2 * d * normal.y;

        refMatrix.m20 = -2 * normal.x * normal.z;
        refMatrix.m21 = -2 * normal.y * normal.z;
        refMatrix.m22 = 1 - 2 * normal.z * normal.z;
        refMatrix.m23 = -2 * d * normal.z;

        refMatrix.m30 = 0;
        refMatrix.m31 = 0;
        refMatrix.m32 = 0;
        refMatrix.m33 = 1;

        RefCamera.worldToCameraMatrix = Camera.main.worldToCameraMatrix * refMatrix;
        //在计算漫反射等光照效果时，需要使用顶点的normal和view向量，view跟摄像机位置有关，所以我们也对refcamera做反射变换
        RefCamera.transform.position = refMatrix.MultiplyPoint(Camera.main.transform.position);
        //以下部分是变换摄像机的方向向量，当然其实这里没有必要，你可以删掉它
        Vector3 forward = Camera.main.transform.forward;
        //Vector3 up = Camera.main.transform.up;
        forward = refMatrix.MultiplyVector(forward);
        //up = refMatrix.MultiplyVector(up);
        //Quaternion refQ = Quaternion.LookRotation (forward, up);
        //RefCamera.transform.rotation = refQ;
        RefCamera.transform.forward = forward;

        GL.invertCulling = true;
        RefCamera.Render();
        GL.invertCulling = false;

        //将贴图传递给shader
        RefCamera.targetTexture.wrapMode = TextureWrapMode.Repeat;
        RefMat.SetTexture("_RefTexture", RefCamera.targetTexture);
    }
    // Use this for initialization
    void Start () {
        Panel = this.transform;
		if(null == RefCamera)
		{
			GameObject go = new GameObject();
			go.name = "refCamera";
			RefCamera = go.AddComponent<Camera>();
			RefCamera.CopyFrom(Camera.main);
			RefCamera.enabled = false;
            RefCamera.cullingMask = ~(1 << LayerMask.NameToLayer("Water"));
            RefCamera.cullingMask +=  ~(1 << LayerMask.NameToLayer("UI"));
		}
		if(null == RefMat)
		{
			RefMat = this.GetComponent<Renderer>().sharedMaterial;
		}
		RenderTexture refTexture = new RenderTexture(Mathf.FloorToInt(Camera.main.pixelWidth),
		                                     Mathf.FloorToInt(Camera.main.pixelHeight), 24);
		refTexture.hideFlags = HideFlags.DontSave;
		RefCamera.targetTexture = refTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnWillRenderObject() {
        //Debug.Log("render func");
        RenderRefection();
    }
}
