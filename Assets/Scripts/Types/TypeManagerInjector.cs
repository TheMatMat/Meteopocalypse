using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManagerInjector : MonoBehaviour
{
    [SerializeField] private TypeManager _e;
    [SerializeField] private TypeManagerRef _ref;
    
    ISet<TypeManager> RealRef => _ref;


    void Awake()
    {
        RealRef.Set(_e);
    }
}
