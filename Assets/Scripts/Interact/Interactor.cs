using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactor
{
    List<ActionType> GetActions(Interactable interactable);
}
