using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 1;
    public float turnSpeed = 90;
    public float blockSize = 1;
    public Path path;
    public GameNode node;
    public GameNode startNode;
    public InputField m_TextField;

    private Rigidbody playerRigidbody;
    private Vector3 destination;
    private Quaternion rotation;
    private Quaternion destRotation;
    private bool isMove = false;
    private bool isTurn = false;
    private float t = 0;
    private int m_i;
    private int m_check;
    private int m_looptmp;
    private string[] sa;

	private int currentDirection = GameNode.EAST;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        rotation = Quaternion.identity;

        node = startNode;

        transform.position = node.transform.position;
		transform.rotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));
        rotation = transform.rotation;
        path = node.adjacencyNode[currentDirection];
    }
	
	// Update is called once per frame
	void Update () {
		getInput();
		if (isMove || isTurn || GameController.isFinish())
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            //destination = transform.position + transform.forward * blockSize;
            //isMove = true;

            if (node.adjacencyNode[currentDirection].destination[1] != null)
            {
                isMove = true;
                /*print("Move");
                m_i++;*/
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Vector3 euler = rotation.eulerAngles;
            //euler.y = Mathf.Repeat(euler.y - 90, 360);
            //destRotation = Quaternion.Euler(euler);

            currentDirection = (currentDirection + 3) % 4;
            //while (currentDirection > 3) { 
            //    currentDirection -= 4;
            //}

            destRotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));

            isTurn = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Vector3 euler = rotation.eulerAngles;
            //euler.y = Mathf.Repeat(euler.y + 90, 360);
            //destRotation = Quaternion.Euler(euler);

            currentDirection = (currentDirection + 1) % 4;
            //while (currentDirection > 3) {
            //    currentDirection -= 4;
            //}

            destRotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));

            isTurn = true;
        }
    }

	void getInput() {
		if (isMove || isTurn || m_check == 0) {
			return;
		}

		if (sa[m_i].Equals("Move")) {
			//destination = transform.position + transform.forward * blockSize;
			//isMove = true;

			print("Move");
			m_i++;

			if (node.adjacencyNode[currentDirection].destination[1] != null) {
				isMove = true;
				/*print("Move");
                m_i++;*/
			}
		}
		else if (sa[m_i].Equals("Turn Left")) {
			//Vector3 euler = rotation.eulerAngles;
			//euler.y = Mathf.Repeat(euler.y - 90, 360);
			//destRotation = Quaternion.Euler(euler);
			print("Turn Left");
			m_i++;

			currentDirection = (currentDirection + 3) % 4;
			//while (currentDirection > 3) { 
			//    currentDirection -= 4;
			//}

			destRotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));

			isTurn = true;
		}
		else if (sa[m_i].Equals("Turn Right")) {
			//Vector3 euler = rotation.eulerAngles;
			//euler.y = Mathf.Repeat(euler.y + 90, 360);
			//destRotation = Quaternion.Euler(euler);
			print("Turn Right");
			m_i++;

			currentDirection = (currentDirection + 1) % 4;
			//while (currentDirection > 3) {
			//    currentDirection -= 4;
			//}

			destRotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));

			isTurn = true;
		}
		else if (sa[m_i].Equals("Loop")) {
			m_looptmp = m_i;
			m_i++;

		}
		else if (sa[m_i].Equals("End Loop")) {
			m_i = m_looptmp;
			m_i++;

		}
		if (m_i == sa.Length) {
			m_check = 0;
			//print(sa.Length);
		}
	}

    void FixedUpdate() {
        if (isMove) {
            Move();
            transform.forward = path.GetDirection(t);
            transform.position = path.GetPoint(t);
            return;
        }
        if (isTurn) {
            Turn();
            transform.rotation = rotation;
            return;
        }
        //playerRigidbody.rotation = rotation;
    }

    void Move() {
        //playerRigidbody.position = (Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime));
        //if (transform.position.Equals(destination)) {
        //    transform.position = destination;
        //    isMove = false;
        //}
        //playerRigidbody.position = path.GetPoint(t);
        t = Mathf.MoveTowards(t, 1, Time.deltaTime * speed / path.GetVelocity(t).magnitude);
        if (t == 1) {
            isMove = false;

            GameNode lastNode = node;
            node = path.destination[1];

            //check for current direction in new node
            for (int i = 0; i < 4; i++) {
                if (node.adjacencyNode[i].destination[1] == lastNode) {
                    currentDirection = (i + 2) % 4;
                }
            }
            path = node.adjacencyNode[currentDirection];
            rotation = Quaternion.LookRotation(path.GetDirection(0));

            t = 0;
        }
    }

    void Turn() {
        rotation = Quaternion.RotateTowards(rotation, destRotation, turnSpeed * Time.deltaTime);
        if (rotation.eulerAngles.Equals(destRotation.eulerAngles)) { 
            isTurn = false;
            path = node.adjacencyNode[currentDirection];
        }
    }

    public void Run() {
        string s = m_TextField.text;
        sa = s.Split('\n');
        m_i = 0;
        int count = 0;
        for (int i = 0; i < sa.Length; i++)
        {
            if (sa[i].Equals("Move") || sa[i].Equals("Turn Left") || sa[i].Equals("Turn Right"))
            {
            }
            else if (sa[i].Equals("Loop"))
            {
                count++;
            }
            else if (sa[i].Equals("End Loop"))
            {
                if (count == 0)
                {
                    print("Loop error");
                    return;
                }
                count--;
            }
            else
            {
                print("Syntax error");
                return;
            }

        }
        m_check = 1;

    }
}
