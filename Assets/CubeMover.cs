using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {
	
	public float speed;
	public int life;
	public string level;
	
	// Use this for initialization
	void Start () {		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance = speed * Time.deltaTime;
		float dx = distance*Input.GetAxis("Horizontal");
		float dy = distance*Input.GetAxis("Vertical");
		/*if(transform.position.y+dy >= -4 && transform.position.y+dy <= -3)		   
		{*/
			transform.Translate(0f,dy,0f);	
		//}
		
		/*if(transform.position.x+dx >= -5 && transform.position.x+dx <= 5)
		{*/
			transform.Translate(dx,0f,0f);	
		//}
	}
}
