using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : AutoTranslate {

	public virtual void Interact()
    {

        Destroy(this);
    }
}
