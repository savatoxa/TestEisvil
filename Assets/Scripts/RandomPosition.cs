using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
	public int sphereNums = 14;
	public int cubeNums = 14;
	public float xrange = 15f;
	public float yrange = 15f;
	public float rotationSpeed = 10f;
	public Material sphereMat;
	public Material cubeMat;
	
	GameObject sphere;
	GameObject cube;
	public ArrayList spheres;
	public ArrayList cubes;

	private Dictionary<GameObject, Vector3> rotationDirections = new Dictionary<GameObject, Vector3>();

	void Start()
	{
		spheres = new ArrayList();
		cubes = new ArrayList();

		for (int i = 0; i < sphereNums; i++)
		{
			float randomRotX = Random.Range(-1f, 1f);
			float randomRotY = Random.Range(-1f, 1f);
			float randomScale = Random.Range(1f, 2f);

			sphere = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			sphere.GetComponent<Renderer>().material = sphereMat;
			sphere.transform.position = new Vector3( ((Random.value - 0.5f)* xrange), ((Random.value - 0.5f)* yrange), 0f );
			sphere.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
			rotationDirections[sphere] = new Vector3(randomRotX, randomRotY, 0).normalized;

			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.GetComponent<Renderer>().material = cubeMat;
			cube.transform.position = new Vector3(((Random.value - 0.5f) * xrange), ((Random.value - 0.5f) * yrange), 0f);
			cube.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
			rotationDirections[cube] = new Vector3(randomRotX, randomRotY, 0).normalized;
			
			spheres.Add(sphere);
			cubes.Add(cube);
		}
	}
	void Update()
    {
		foreach(GameObject sphere in spheres)
			sphere.transform.Rotate(rotationDirections[sphere] * rotationSpeed * Time.deltaTime);
		foreach (GameObject cube in cubes)
			cube.transform.Rotate(rotationDirections[cube] * rotationSpeed * Time.deltaTime);
	}
}
