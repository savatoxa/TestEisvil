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
	public float averageMoveSpeed = 10f;
	public float moveSpeedDispersion = 0.5f;
	public Material sphereMat;
	public Material cubeMat;
	public GameObject CubeCollider;
	
	GameObject sphere;
	GameObject cube;
	public ArrayList spheres;
	public ArrayList cubes;

	private Dictionary<GameObject, Vector3> rotationDirections = new Dictionary<GameObject, Vector3>();
	private Dictionary<GameObject,Vector3> moveDirections = new Dictionary<GameObject, Vector3>();
	private Dictionary<GameObject,float> moveSpeeds = new Dictionary<GameObject, float>();

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
			moveDirections[sphere] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
			moveSpeeds[sphere] = Random.Range(averageMoveSpeed * (1 - moveSpeedDispersion), averageMoveSpeed * (1 + moveSpeedDispersion));

			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.GetComponent<Renderer>().material = cubeMat;
			cube.transform.position = new Vector3(((Random.value - 0.5f) * xrange), ((Random.value - 0.5f) * yrange), 0f);
			cube.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
			rotationDirections[cube] = new Vector3(randomRotX, randomRotY, 0).normalized;
			moveDirections[cube] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
			moveSpeeds[cube] = Random.Range(averageMoveSpeed * (1 - moveSpeedDispersion), averageMoveSpeed * (1 + moveSpeedDispersion));

			spheres.Add(sphere);
			cubes.Add(cube);
		}
	}

	Vector3 GetNormal(GameObject obj)
	{
		Bounds bounds = CubeCollider.GetComponent<BoxCollider>().bounds;
		Vector3 pos = obj.transform.position;

		if (pos.x <= bounds.min.x) return Vector3.right;
		if (pos.x >= bounds.max.x) return Vector3.left;
		if (pos.y <= bounds.min.y) return Vector3.up;
		if (pos.y >= bounds.max.y) return Vector3.down;
		if (pos.z <= bounds.min.z) return Vector3.forward;
		if (pos.z >= bounds.max.z) return Vector3.back;
		return Vector3.zero;
	}

	bool IsOutBounds(GameObject obj)
    {
		return !CubeCollider.GetComponent<BoxCollider>().bounds.Contains(obj.transform.position);
    }
	void Update()
    {
		foreach (GameObject sphere in spheres)
		{
			sphere.transform.Rotate(rotationDirections[sphere] * rotationSpeed * Time.deltaTime);
			sphere.transform.Translate(moveDirections[sphere] * moveSpeeds[sphere] * Time.deltaTime);
			if (IsOutBounds(sphere))
			{
				//moveSpeeds[sphere] = 0;
				moveDirections[sphere] = Vector3.Reflect(moveDirections[sphere], GetNormal(sphere));
			}
		}
		foreach (GameObject cube in cubes)
		{
			cube.transform.Rotate(rotationDirections[cube] * rotationSpeed * Time.deltaTime);
			cube.transform.Translate(moveDirections[cube] * moveSpeeds[cube] * Time.deltaTime);
			if (IsOutBounds(cube))
			{
				//moveSpeeds[cube] = 0;
				moveDirections[cube] = Vector3.Reflect(moveDirections[cube], GetNormal(cube));
			}

		}
	}
}
