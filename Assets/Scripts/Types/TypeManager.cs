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
        public EType type;
    }

    [Serializable]
    public struct SubTypeStruct
    {
        public Sprite typeSprite;
        public ESubType subType;
    }

    [Serializable]
    public struct CategoryType
    {
        public EType type;
        public List<ESubType> subTypes;
    }
    
    [SerializeField] private List<TypeStruct> types;
    [SerializeField] private List<SubTypeStruct> subTypes;
    [SerializeField] private List<CategoryType> categoriesType;

    public Sprite GetSpriteByType(EType type) => types.FirstOrDefault(typeStruct => typeStruct.type == type).typeSprite;
    public EType GetTypeBySprite(Sprite sprite) => types.FirstOrDefault(typeStruct => typeStruct.typeSprite == sprite).type;
    public CategoryType GetCategoryStructByType(EType category) => categoriesType.FirstOrDefault(categoryStruct => categoryStruct.type == category);
    public Sprite GetSpriteBySubType(ESubType subType) => subTypes.FirstOrDefault(subTypeStruct => subTypeStruct.subType == subType).typeSprite;
}
