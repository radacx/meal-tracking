using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiteDB;
using MealTracking.Contract.Models.Base;

namespace MealTracking.Repository.Misc
{
    internal class CustomMapper : BsonMapper
    {
        private static Type GetEntityInterface(Type type)
        {
            var interfaces = type.GetInterfaces();
            var entityInterface = interfaces.FirstOrDefault(typ => typ == typeof(IEntity));

            return entityInterface;
        }
        
        protected override IEnumerable<MemberInfo> GetTypeMembers(Type type)
        {
            var memberInfos = base.GetTypeMembers(type).ToList();

            var hasId = memberInfos.OfType<PropertyInfo>()
                .ToList()
                .Exists(
                    property => property.PropertyType == typeof(IEntity).GetProperty(nameof(IEntity.Id))?.PropertyType
                );

            if (hasId)
            {
                return memberInfos;
            }

            var entityInterface = GetEntityInterface(type);

            return entityInterface != null ? memberInfos.Concat(base.GetTypeMembers(entityInterface)) : memberInfos;
        }
    }
}