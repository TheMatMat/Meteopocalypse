using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TypeManager : MonoBehaviour
{
    [Serializable]
    public struct TypeStruct
    {
        public Sprite typeSprite;
        public MISSION_TYPE type;
    }

    [Serializable]
    public struct SubTypeStruct
    {
        public Sprite typeSprite;
        public MISSION_SUBTYPE subType;
    }

    [Serializable]
    public struct CategoryType
    {
        public MISSION_TYPE type;
        public List<MISSION_SUBTYPE> subTypes;
    }
    
    [SerializeField] private List<TypeStruct> types;
    [SerializeField] private List<SubTypeStruct> subTypes;
    [SerializeField] private List<CategoryType> categoriesType;

    public Sprite GetSpriteByType(MISSION_TYPE type) => types.FirstOrDefault(typeStruct => typeStruct.type == type).typeSprite;
    public MISSION_TYPE GetTypeBySprite(Sprite sprite) => types.FirstOrDefault(typeStruct => typeStruct.typeSprite == sprite).type;
    public CategoryType GetCategoryStructByType(MISSION_TYPE category) => categoriesType.FirstOrDefault(categoryStruct => categoryStruct.type == category);
    public Sprite GetSpriteBySubType(MISSION_SUBTYPE subType) => subTypes.FirstOrDefault(subTypeStruct => subTypeStruct.subType == subType).typeSprite;

    public MISSION_SUBTYPE GetSubTypeBySprite(Sprite sprite) => subTypes.FirstOrDefault(subTypeStruct => subTypeStruct.typeSprite == sprite).subType;
}
