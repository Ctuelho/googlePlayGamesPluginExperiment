using UnityEngine;

public class AutoTranslate : MonoBehaviour {

    bool canTranslate = true;
    public float yStart = 8;
    public float yEnd = -7;

	void FixedUpdate () {
        if(canTranslate)
            transform.Translate(Vector3.down * Time.deltaTime * LevelGenerator.Instance.speed);

        if (transform.position.y < yEnd)
            Destroy(gameObject);
    }
}
