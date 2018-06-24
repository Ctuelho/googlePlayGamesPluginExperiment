using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControls : MonoBehaviour {

    private bool canMove = true;
    private int pos = 1;

    //float leftLimit = -1;
    //float rightLimit = 1;

    float displacement = 1.7f;
    float duration = 0.2f;

    float leftTouchRange = Screen.width * 0.4f;
    float rightTouchRange = Screen.width * 0.6f;

    void OnEnable()
    {
        LevelGenerator.Retry += Retry;
        LevelGenerator.EndLevel += EndLevel;
    }

    void OnDisable()
    {
        LevelGenerator.Retry -= Retry;
        LevelGenerator.EndLevel -= EndLevel;
    }

    // Use this for initialization
    void Retry () {
        transform.DOKill();
        transform.position = new Vector3(0, transform.position.y, 0);
        canMove = true;
        pos = 1;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void EndLevel()
    {
        canMove = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    bool willMove = false;
                    float toWhere = 0;
                    if(touch.position.x > rightTouchRange)
                    {
                        if (pos != 2)
                        {
                            willMove = true;
                            pos++;
                            toWhere = 1;
                        }
                    }
                    else if(touch.position.x < leftTouchRange)
                    {
                        if(pos != 0)
                        {
                            willMove = true;
                            pos--;
                            toWhere = -1;
                        }
                    }
                    if (willMove)
                    {
                        transform.DOMoveX(transform.position.x + (displacement * toWhere), duration)
                            .SetEase(Ease.OutQuad)
                            .OnComplete(AllowMovement);
                    }
                }
            }
            var movedirection = Input.GetAxis("Horizontal");
            //Debug.Log(movedirection);
            bool vaiMover = false;
            float praOnde = 0;
            if (movedirection > 0)
            {
                if (pos != 2)
                {
                    vaiMover = true;
                    pos++;
                    praOnde = 1;
                }
            }
            else if (movedirection < 0)
            {
                if (pos != 0)
                {
                    vaiMover = true;
                    pos--;
                    praOnde = -1;
                }
            }
            if (vaiMover)
            {
                Debug.Log("Moving");
                canMove = false;
                transform.DOMoveX(transform.position.x + (displacement * praOnde), duration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(AllowMovement);
            }
        }

        
    }

    private void AllowMovement()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("entrou");
        Interactable interactable = collider.GetComponent<Interactable>();
        if(interactable != null)
        {
            interactable.Interact();
        }
    }
}
