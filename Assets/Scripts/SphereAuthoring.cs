using UnityEngine;
using Unity.Entities;

[RequiresEntityConversion]
public class SphereAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SphereTagComponentData());
        dstManager.AddComponentData(entity, new Force());
    }
}
