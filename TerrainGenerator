using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	
	Color32[] cols;
	int width=128;
	int height=128;
	float WidthHeightP;
	int GRAIN=8;
	Texture2D texture;
	
	void Start () 
	{
		
		WidthHeightP= (float)width+height;
		
        texture = new Texture2D(width, height);
        renderer.material.mainTexture = texture;
		
		cols = new Color32[width*height];

		drawPlasma(width, height);
		texture.SetPixels32(cols);

		texture.Apply();
	}

	void GenMesh(){
		Mesh mesh = new Mesh();
		mesh.name = "Terrain";
		mesh.Clear();
		mesh.vertices = new Vector3[16384];
		for (int i = 0; i<width; i++) {
			for(int k = 0;k<height;k++){

			}
		}

	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			drawPlasma(width, height);
			texture.SetPixels32(cols);

			texture.Apply();
		}
	}
	float displace(float num)
	{
		float max = num / WidthHeightP * GRAIN;
		return Random.Range(-0.5f, 0.5f)* max;
	}

	Color computeColor(float c)
	{      
	   return new Color(c, c, c);
	}
	
	void drawPlasma(float w, float h)
	{
	   float c1, c2, c3, c4;
			 
	   c1 = Random.value;
	   c2 = Random.value;
	   c3 = Random.value;
	   c4 = Random.value;
		
	   divide(0.0f, 0.0f, w , h , c1, c2, c3, c4);
	}
	
	void divide(float x, float y, float w, float h, float c1, float c2, float c3, float c4)
	{
	 
	   float newWidth = w * 0.5f;
	   float newHeight = h * 0.5f;
	 
	   if (w < 1.0f || h < 1.0f)
	   {
		 float c = (c1 + c2 + c3 + c4) * 0.25f;
			cols[(int)x+(int)y*width] = computeColor(c);
	   }
	   else
	   {
		 float middle =(c1 + c2 + c3 + c4) * 0.25f + displace(newWidth + newHeight);
		 float edge1 = (c1 + c2) * 0.5f;
		 float edge2 = (c2 + c3) * 0.5f;
		 float edge3 = (c3 + c4) * 0.5f;
		 float edge4 = (c4 + c1) * 0.5f;
	 
		 if (middle <= 0)
		 {
		   middle = 0;
		 }
		 else if (middle > 1.0f)
		 {
		   middle = 1.0f;
		 }                
		 divide(x, y, newWidth, newHeight, c1, edge1, middle, edge4);
		 divide(x + newWidth, y, newWidth, newHeight, edge1, c2, edge2, middle);
		 divide(x + newWidth, y + newHeight, newWidth, newHeight, middle, edge2, c3, edge3);
		 divide(x, y + newHeight, newWidth, newHeight, edge4, middle, edge3, c4);
	   }
	}
}
