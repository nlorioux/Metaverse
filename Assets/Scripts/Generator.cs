using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public List<GameObject> prefabs;

	protected void Start()
	{
		for(int i = 0; i < prefabs.Count; ++i) {
			float x = Random.Range(-7f, 7f);
			float z = Random.Range(-7f, 7f);

			GameObject new_prefab = Instantiate(prefabs[i]);

			new_prefab.transform.parent = transform;
			new_prefab.transform.localPosition = new Vector3(x, 5f, z);
			new_prefab.transform.localRotation = Random.rotation;
			new_prefab.name = prefabs[i].name;
			new_prefab.layer = (int) Layer.OBJECT;
		}
	}
}
