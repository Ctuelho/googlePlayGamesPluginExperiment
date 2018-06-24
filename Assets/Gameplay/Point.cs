using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : Interactable {

	void Awake () {
        SetPosition();
	}
	
    private void SetPosition()
    {
        transform.position = new Vector3(LevelGenerator
            .Instance.positions[Random.Range(0, LevelGenerator.Instance.positions.Length)]
            , yStart, 0);
    }

    public override void Interact()
    {
        LevelHUD.Instance.AddScore(10);
        Clear();
    }

    public void Clear()
    {
        Destroy(this.gameObject);
    }

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

    public void Retry()
    {
        Clear();
    }

    public void EndLevel()
    {
    }
}
