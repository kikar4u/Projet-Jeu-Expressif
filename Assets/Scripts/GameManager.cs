﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    GameBoard board = default;
    [SerializeField] LayerMask lMask;
   
    public GameObject objectToSpawn;

    [SerializeField]
    Camera currentCamera;
    RaycastHit hit;
    public float timeLeft = 3.0f;
    //public Text startText; //used for showing countdown from 3,2,1 
    void Awake()
    {
        //board.Initialize(boardSize);
        lMask = ~lMask;
    }
    void OnValidate()
    {
        if (boardSize.x < 2)
        {
            boardSize.x = 2;
        }
        if (boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timeLeft -= Time.deltaTime % 60;
        //Debug.Log(timeLeft);
        //startText.text = (timeLeft).ToString("0");

        if (Input.GetButtonDown("Fire1"))
        {
            spawnEntity(objectToSpawn);
        }
        //Ray ray = currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z * 10));
        

        //Debug.Log(Input.mousePosition);

    }
    public void spawnEntity(GameObject a)
    {

        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);


        mousePos.z = -currentCamera.transform.position.z;       // we want 2m away from the camera position
        Vector3 objectPos = currentCamera.ScreenToWorldPoint(mousePos);
        Ray ray = currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z * 10));
        RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        bool isThereEntity = false;
        bool isThereUI = false;
        if(hit2.collider != null)
        {   
            if(hit2.transform.gameObject.tag == "Entity")
            {
                Debug.Log(hit2.transform.gameObject.name);
                isThereEntity = true;
            }
            else
            {
                isThereEntity = false;
            }

        }
        if(Physics.Raycast(ray, out hit))
        {
                Transform objectHit = hit.transform;


                if (hit.transform.gameObject.name == "UIBounds")
                {
                    // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
                    //Debug.Log("touch UI / entity");
                    isThereUI = true;
                }
                else
                {
                    isThereUI = false;
                }
                // Do something with the object that was hit by the raycast.
        }
        if (isThereEntity == false && isThereUI == false)
        {

            //Debug.Log("devrait spawn");
            Instantiate(a, objectPos, Quaternion.identity);
        }




    }
}
