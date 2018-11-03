using System;

namespace MealTracking.Contract.Models.Base
{
    public interface IEntity : IEquatable<IEntity>
    {
        int Id { get; set; }
    }
}
