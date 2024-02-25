using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCamera : MonoBehaviour
{
    //[SerializeField] private Transform settingsScreen;
    //[SerializeField] private Transform mainScreen;
    //[SerializeField] private Transform cardTable;
    [SerializeField] public Animator anim;

    private float speed = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLeft()
    {
		transform.rotation = new Quaternion(0, -90, 0, 0);
	}

	public void TurnRight()
	{
		transform.rotation = new Quaternion(0, 0, 0, 0);
	}

	public void TurnBack()
    {
		transform.rotation = new Quaternion(0, 180, 0, 0);
	}

    public void EndGame()
    {

    }
}


