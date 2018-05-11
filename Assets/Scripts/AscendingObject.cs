﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendingObject : MonoBehaviour, Triggerable 
{
    public enum State { retracted, extending, retracting, extended }
    public State state;
    private Vector3 initPosition;
    private GameObject aboveCollider;
    private List<Collider> colliders;
    private bool hasPlayer = false;
    public int deltaMove;
    public float speed;
    void Start () 
    {
        initPosition = transform.position;
        colliders = new List<Collider>();
        aboveCollider = transform.GetChild(0).gameObject;
    }
    
    // Update is called once per frame
    void Update () 
    {
        if(state == State.extending)
        {
            float translation = Time.deltaTime * speed;
            if(transform.position.y + translation > initPosition.y + deltaMove)
            {
                translation = initPosition.y + deltaMove - transform.position.y;
                state = State.extended;
                aboveCollider.layer = 2;
            }
                 
            transform.Translate(Vector3.up * translation);
            foreach (Collider collider in colliders)
            {
                if(collider.GetComponent<Movement>().state != Movement.State.falling)
                    collider.transform.Translate(Vector3.up * translation);
            }
        }
        else if (state == State.retracting)
        {
            float translation = - Time.deltaTime * speed;
            if(transform.position.y + translation < initPosition.y)
            {
                translation = initPosition.y - transform.position.y;
                state = State.retracted;
                aboveCollider.layer = 2;
            }
                 
            transform.Translate(Vector3.up * translation);
            foreach (Collider collider in colliders)
            {
                if(collider.GetComponent<Movement>().state != Movement.State.falling)
                    collider.transform.Translate(Vector3.up * translation);
            }
        }

        if (Input.GetKeyDown("k"))
            Trigger();
    }

    public void AddObject(Collider obj)
    {
        colliders.Add(obj);

        if (obj.tag == "Player")
            hasPlayer = true;
    }

    public void RemoveObject(Collider obj)
    {
        colliders.Remove(obj);

        if (obj.tag == "Player")
            hasPlayer = false;
    }

    public void Trigger()
    {
        if(state == State.retracted)
            state = State.extending;
        else if(state == State.extended)
            state = State.retracting;

        aboveCollider.layer = 0;

        checkPartyBreak();
    }

    private void checkPartyBreak()
    {
        if(hasPlayer)
        {
            for (int i = Movement.party.Count - 1; i >= 0; i--)
            {
                if(!colliders.Contains(Movement.party[i].GetComponent<Collider>()))
                {
                    if(i == 0)
                        GameObject.Find("Player").GetComponent<PlayerMovement>().nextInParty = null;
                    else
                        Movement.party[i - 1].GetComponent<PartyMovement>().nextInParty = null;
                    Movement.party.Remove(Movement.party[i]);
                }
                    
            }
        }
        
    }
}