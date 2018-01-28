using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Linq;


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

    private string[] sa;
    private int[] m_limitloop;
    private int[] m_arlooptmp;
    private int[] m_itloop;
    private int loopCount;
    private int countLoop;


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
        if (isMove || isTurn || m_check == 0) {
            return;
        }
        if (m_i == sa.Length)
        {
            m_check = 0;
            m_i = 0;
            return;
            //print(sa.Length);
        }

        Match match = Regex.Match(sa[m_i],"Loop [0-9]*");
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
        else if (match.Success) {
            //m_looptmp = m_i;

            m_i++;
            countLoop = 1;
            
            /*m_itloop[countLoop]++;
            if (m_isin[countLoop] == 0) {
                m_isin[countLoop] = 1;
                countLoop++;
            }*/

        }
        else if (sa[m_i].Equals("End Loop")) {

            /*if (m_itloop[countLoop] < m_limitloop[countLoop]) {
                m_i = m_looptmp;
                countLoop--;
            }*/

            print(m_limitloop[loopCount]);
            if (countLoop < m_limitloop[loopCount])
            {
                m_i = m_arlooptmp[loopCount];
            }
            else
            {
                loopCount++;
            }
            m_i++;
            countLoop++;

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

        t = Mathf.MoveTowards(t, 1, Time.deltaTime * 0.5f * speed);
        if (t == 1) {
            isMove = false;

            GameNode lastNode = node;
            node = node.adjacencyNode[currentDirection].destination[1];

            //check for current direction in new node
            for (int i = 0; i < 4; i++) {
                if (node.adjacencyNode[i].destination[1] == lastNode) {
                    currentDirection = (i + 2) % 4;
                }
            }
            path = node.adjacencyNode[currentDirection];

            t = 0;
        }
    }

    void Turn() {
        rotation = Quaternion.RotateTowards(rotation, destRotation, turnSpeed * Time.deltaTime);
        if (rotation.Equals(destRotation)) { 
            isTurn = false;
            path = node.adjacencyNode[currentDirection];
        }
    }

    public void Run() {
        string s = m_TextField.text;
        sa = s.Split('\n');
        int count = 0;
        loopCount = 0;
        Array.Resize(ref m_limitloop, 10);
        Array.Resize(ref m_arlooptmp, 10);
        for (int i = 0; i < sa.Length; i++)
        {
            Match match = Regex.Match(sa[i], "Loop [0-9]");
            if (sa[i].Equals("Move") || sa[i].Equals("Turn Left") || sa[i].Equals("Turn Right"))
            {
            }
            else if (match.Success)
            {
                
                string[] tmps = sa[i].Split(' ');
                print(tmps[1]);
                count++;

                m_arlooptmp[loopCount] = i;
                m_limitloop[loopCount] = Int32.Parse(tmps[1]);
                print(m_limitloop[loopCount]); 
                loopCount++;

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

        loopCount = 0;
        m_i = 0;
        m_check = 1;

    }
}
