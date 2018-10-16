using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ExportOBJ : MonoBehaviour {

	// Save UI texture
	public GameObject saveUIText;

	// Increment object number
	public int numObject = 0;


	// Export scene to .obj file
	public static string MeshToString()
	{
		int j = 0;
		int saveJ = 0;

		StringBuilder sb = new StringBuilder();

		// For each exportable objects in the scene
		foreach (GameObject meshObjects in GameObject.FindGameObjectsWithTag("ExportableObject"))
		{
			// Take all data (position, rotation of vertices, material color)
			Transform tr = meshObjects.transform;
			Vector3[] vertices = meshObjects.GetComponent<MeshFilter>().mesh.vertices;
			Mesh m = meshObjects.GetComponent<MeshFilter>().mesh;
			Material mat = meshObjects.GetComponent<Renderer>().material;

			// and convert to .obj data
			sb.Append("g ").Append(meshObjects.name).Append("\n");

			foreach (Vector3 vert in vertices)
			{
				Vector3 v = tr.TransformPoint(vert);
				sb.Append(string.Format("v {0} {1} {2} {3} {4} {5}\n", (v.x), v.y, v.z, 0.984314, 0.764706, 1.000000));
			}

			sb.Append("\n");
			foreach (Vector3 v in m.normals)
			{
				sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
			}

			sb.Append("\n");
			foreach (Vector3 v in m.uv)
			{
				sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
			}

			// .obj data for material color
			for (int material = 0 ; material < m.subMeshCount ; material++)
			{
				sb.Append("\n");

				int[] triangles = m.GetTriangles(material);
				for (int i = 0 ; i < triangles.Length ; i += 3)
				{
					sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
						triangles[i] + 1 + saveJ, triangles[i + 1] + 1 + saveJ, triangles[i + 2] + 1 + saveJ));
					if (j < triangles[i + 2] + 1 + saveJ)
					{
						j = triangles[i + 2] + 1 + saveJ;
					}
					if (j < triangles[i] + 1 + saveJ)
					{
						j = triangles[i] + 1 + saveJ;
					}
					if (j < triangles[i + 1] + 1 + saveJ)
					{
						j = triangles[i + 1] + 1 + saveJ;
					}
				}
			}
			sb.Append("\n");
			saveJ = j;
		}
		return sb.ToString();
	}

	// Write the data in a .obj file
	public static void MeshToFile(string filename)
	{
		using (StreamWriter sw = new StreamWriter(filename))
		{
			sw.Write(MeshToString());
		}
	}

	// Folder of the .obj file
	public void ExportModel()
	{
		MeshToFile("Save/save.obj");
		StartCoroutine(SaveFile());
	}

	// Display the save UI texture
	IEnumerator SaveFile()
	{
		saveUIText.SetActive(true);
		yield return new WaitForSeconds(3f);
		saveUIText.SetActive(false);
	}


}