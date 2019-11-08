using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidSnapCheck : MonoBehaviour {

    private bool _isValid = false;

    private List<Collider> _collisions = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if(!_collisions.Contains(other))
            _collisions.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(_collisions.Contains(other))
            _collisions.Remove(other);
    }

    public bool IsValid()
    {
        return _collisions.Count <= 0;
    }
}
