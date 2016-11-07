using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(PolygonCollider2D))]
public class PolygonTerrain : MonoBehaviour {

	private MeshFilter _filter;
	private MeshFilter Filter { get { if (_filter == null) _filter = GetComponent<MeshFilter>(); return _filter; } }

	private MeshRenderer _renderer;
	private MeshRenderer Renderer { get { if (_renderer == null) _renderer = GetComponent<MeshRenderer>(); return _renderer; } }

	private PolygonCollider2D _collider;
	private PolygonCollider2D Collider { get { if (_collider == null) _collider = GetComponent<PolygonCollider2D>(); return _collider; } }



	public void CreateTerrain() {
		
		Debug.Log("Collider path - points: "+ Collider.points.Length + ", pathCount: " + Collider.pathCount + ", shapeCount: " + Collider.shapeCount + ", Collider.GetTotalPointCount(): " + Collider.GetTotalPointCount());
		for (int i = 0; i < Collider.points.Length; i++) {
			Debug.Log("Point " + i + ": " + Collider.points[i]);
		}
		for (int i = 0; i < Collider.pathCount; i++) {
			Debug.Log("Path " + i + ": " + Collider.GetPath(i).ToStr());
		}

		var verts = new Vector3[Collider.points.Length];
		
		int iterations = verts.Length / 2 - 1;
        var triangles = new int[(verts.Length - 2) * 3];
        var uvs = new Vector2[verts.Length];

		for (int i = 0; i < Collider.points.Length; i++) {
			verts[i] = Collider.points[i];
		}

		for (int i = 0; i < iterations; ++i)
        {
            int i2 = i * 6;
            int i3 = i * 2;
            
            triangles[i2] = i3 + 2;
            triangles[i2 + 1] = i3 + 1;
            triangles[i2 + 2] = i3 + 0;
            
            triangles[i2 + 3] = i3 + 2;
            triangles[i2 + 4] = i3 + 3;
            triangles[i2 + 5] = i3 + 1;
        }

		for (int i=0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts[i].x, verts[i].y);
        }

		Debug.Log("Mesh verts: " + verts.ToStr());
		Debug.Log("Mesh triangles: " + triangles.ToStr());
		Debug.Log("Mesh uv: " + uvs.ToStr());

		Filter.mesh.Clear();
		Filter.mesh.vertices = verts;
		Filter.mesh.triangles = triangles;
		Filter.mesh.uv = uvs;
	}
	
}


#if UNITY_EDITOR
[CustomEditor(typeof(PolygonTerrain))]
public class PolygonTerrainEditor : Editor {

	private PolygonTerrain Target { get { return (PolygonTerrain) target; } }

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if (GUILayout.Button("Create")) {
			Target.CreateTerrain();
		}
	}
}
#endif
