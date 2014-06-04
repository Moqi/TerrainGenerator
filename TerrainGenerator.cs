using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	
	Color32[] cols;
	int width=2048;
	int height=2048;
	float WidthHeightP;
	int GRAIN=4;
	Texture2D texture;
	float R;
	
	void Start () 
	{
		Terrain terrain = FindObjectOfType<Terrain> ();
		int resolution = width;
		float[,] heights = new float[resolution,resolution]; 
		WidthHeightP= (float)width+height;
		texture = new Texture2D(width, height);
		cols = new Color32[width*height];
		drawPlasma(width, height);
		texture.SetPixels32(cols);
		R = 0.1f;
		for (int i=0; i<width; i++) {
			for (int k=0;k<height; k++){
				heights[i,k] = texture.GetPixel(i,k).grayscale*R;
			}
		}
						
		terrain.terrainData.size = new Vector3(width, width, height);
		terrain.terrainData.heightmapResolution = resolution;
		terrain.terrainData.SetHeights(0, 0, heights);
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Terrain terrain = FindObjectOfType<Terrain> ();
			int resolution = 128;
			float[,] heights = new float[resolution,resolution]; 
			texture = new Texture2D(width, height);
			cols = new Color32[width*height];
			drawPlasma(width, height);
			texture.SetPixels32(cols);
			R = 0.2f;
			for (int i=0; i<128; i++) {
				for (int k=0;k<128; k++){
					heights[i,k] = texture.GetPixel(i,k).grayscale*R;
				}
			}
			
			terrain.terrainData.size = new Vector3(width, width, width);
			terrain.terrainData.heightmapResolution = resolution;
			terrain.terrainData.SetHeights(0, 0, heights);

		}
	}
	float displace(float num)
	{
		float max = num / WidthHeightP * GRAIN;
		return Random.Range(-0.5f, 0.5f)* max;
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
			cols[(int)x+(int)y*width] = new Color(c, c, c);
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
