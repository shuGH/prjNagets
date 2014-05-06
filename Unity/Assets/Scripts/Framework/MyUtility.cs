// --------------------------------------------------------------------------------
// ユーティリティ
// @author shuzo.i
// --------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyUtility {

	// --------------------------------------------------------------------------------
	static public Vector2 ToVector2 (Vector3 vec3) {
		return new Vector2(vec3.x, vec3.y);
	}

	static public Vector3 ToVector3 (Vector2 vec2, float z=0f) {
		return new Vector3(vec2.x, vec2.y, z);
	}

    // --------------------------------------------------------------------------------
	static public void SetPositionX (Transform trans, float x) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(x, pos.y, pos.z);
	}
	static public void SetPositionY (Transform trans, float y) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x, y, pos.z);
	}
	static public void SetPositionZ (Transform trans, float z) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x, pos.y, z);
	}

	static public void AddPositionX (Transform trans, float x) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x + x, pos.y, pos.z);
	}
	static public void AddPositionY (Transform trans, float y) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x, pos.y + y, pos.z);
	}
	static public void AddPositionZ (Transform trans, float z) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x, pos.y, pos.z + z);
	}

	static public void SetPosition2D (Transform trans, Vector2 vec2) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(vec2.x, vec2.y, pos.z);
	}
	static public void AddPosition2D (Transform trans, Vector2 vec2) {
		Vector3 pos = trans.localPosition;
		trans.localPosition = new Vector3(pos.x + vec2.x, pos.y + vec2.y, pos.z);
	}

	static public void AddRandomPosition (Transform trans, float rangeX, float rangeY, float rangeZ) {
		float rndX = (rangeX != 0f) ? Random.Range(-rangeX / 2f, rangeX / 2f) : 0f;
		float rndY = (rangeY != 0f) ? Random.Range(-rangeY / 2f, rangeY / 2f) : 0f;
		float rndZ = (rangeZ != 0f) ? Random.Range(-rangeZ / 2f, rangeZ / 2f) : 0f;
		trans.localPosition = new Vector3(trans.localPosition.x + rndX, trans.localPosition.y + rndY, trans.localPosition.z + rndZ);
	}

	// --------------------------------------------------------------------------------
	static public void SetAngleX (Transform trans, float x) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(x, ang.y, ang.z);
	}
	static public void SetAngleY (Transform trans, float y) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x, y, ang.z);
	}
	static public void SetAngleZ (Transform trans, float z) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x, ang.y, z);
	}

	static public void AddAngleX (Transform trans, float x) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x + x, ang.y, ang.z);
	}
	static public void AddAngleY (Transform trans, float y) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x, ang.y + y, ang.z);
	}
	static public void AddAngleZ (Transform trans, float z) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x, ang.y, ang.z + z);
	}

	static public void SetAngle2D (Transform trans, Vector2 vec2) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(vec2.x, vec2.y, ang.z);
	}
	static public void AddAngle2D (Transform trans, Vector2 vec2) {
		Vector3 ang = trans.localEulerAngles;
		trans.localEulerAngles = new Vector3(ang.x + vec2.x, ang.y + vec2.y, ang.z);
	}

	static public void AddRandomRotate (Transform trans, float rangeX, float rangeY, float rangeZ) {
		float rndX = (rangeX != 0f) ? Random.Range(-rangeX / 2f, rangeX / 2f) : 0f;
		float rndY = (rangeY != 0f) ? Random.Range(-rangeY / 2f, rangeY / 2f) : 0f;
		float rndZ = (rangeZ != 0f) ? Random.Range(-rangeZ / 2f, rangeZ / 2f) : 0f;
		trans.localEulerAngles = new Vector3(trans.localEulerAngles.x + rndX, trans.localEulerAngles.y + rndY, trans.localEulerAngles.z + rndZ);
	}

	// --------------------------------------------------------------------------------
	static public void SetScaleX (Transform trans, float x) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(x, scl.y, scl.z);
	}
	static public void SetScaleY (Transform trans, float y) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x, y, scl.z);
	}
	static public void SetScaleZ (Transform trans, float z) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x, scl.y, z);
	}

	static public void AddScaleX (Transform trans, float x) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x + x, scl.y, scl.z);
	}
	static public void AddScaleY (Transform trans, float y) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x, scl.y + y, scl.z);
	}
	static public void AddScaleZ (Transform trans, float z) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x, scl.y, scl.z + z);
	}

	static public void SetScale2D (Transform trans, Vector2 vec2) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(vec2.x, vec2.y, scl.z);
	}
	static public void AddScale2D (Transform trans, Vector2 vec2) {
		Vector3 scl = trans.localScale;
		trans.localScale = new Vector3(scl.x + vec2.x, scl.y + vec2.y, scl.z);
	}

	static public void AddRandomScale (Transform trans, float rangeX, float rangeY, float rangeZ) {
		float rndX = (rangeX != 0f) ? Random.Range(-rangeX / 2f, rangeX / 2f) : 0f;
		float rndY = (rangeY != 0f) ? Random.Range(-rangeY / 2f, rangeY / 2f) : 0f;
		float rndZ = (rangeZ != 0f) ? Random.Range(-rangeZ / 2f, rangeZ / 2f) : 0f;
		trans.localScale = new Vector3(trans.localScale.x + rndX, trans.localScale.y + rndY, trans.localScale.z + rndZ);
	}
	
	// --------------------------------------------------------------------------------
	static public T AddChild<T> (GameObject parent, GameObject prefab) where T : Component {
		GameObject obj = GameObject.Instantiate(prefab) as GameObject;

		if (parent != null && obj != null) {
			Transform trans = obj.transform;
			Vector3 pos = trans.localPosition;
			Quaternion rot = trans.localRotation;
			Vector3 scl = trans.lossyScale;

			trans.parent = parent.transform;
			trans.localPosition = pos;
			trans.localRotation = rot;
			trans.localScale = scl;
			SetLayerRecursive(parent.layer, obj);
		}

		T comp = obj.GetComponent<T>();
		return comp;
	}

	static public void SetLayerRecursive (int lyr, GameObject obj) {
		obj.layer = lyr;
		foreach (Transform trans in obj.transform) {
			SetLayerRecursive(lyr, trans.gameObject);
		}
	}

	// --------------------------------------------------------------------------------
	static public void ShuffleList<T> (List<T> list) {
		int len = list.Count;
		while (len > 1) {
			len--;
			int rnd = Random.Range(0, len);
			T tmp = list[rnd];
			list[rnd] = list[len];
			list[len] = tmp;
		}
	}
}
