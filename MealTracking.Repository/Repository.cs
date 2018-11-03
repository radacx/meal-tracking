using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MealTracking.Contract.Models.Base;
using MealTracking.Contract.Models.Days;
using MealTracking.Contract.Models.Foods;
using MealTracking.Contract.Models.Meals;
using MealTracking.Contract.Repositories;
using MealTracking.Repository.Misc;

namespace MealTracking.Repository
{
    public class Repository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DatabaseConfiguration _configuration;
        
        static Repository()
        {
            var mapper = new CustomMapper();

            mapper.Entity<DefaultFoodUnit>().DbRef(x => x.FoodUnit).Ignore(x => x.NutrientsPer1G).Ignore(x => x.NutrientsPer100G);
            mapper.Entity<Food>().DbRef(x => x.Units);
            mapper.Entity<MealFood>().DbRef(x => x.Food).DbRef(x => x.FoodUnit).Ignore(x => x.Nutrients);
            mapper.Entity<MealTemplate>().Ignore(x => x.Nutrients);
            mapper.Entity<EatingDay>().Ignore(x => x.Nutrients);
            
            mapper.Entity<Macros>()
                .Ignore(x => x.Calories)
                .Ignore(x => x.CarbsPercentage)
                .Ignore(x => x.ProteinPercentage)
                .Ignore(x => x.FatsPercentage)
                .Ignore(x => x.AlcoholPercentage);
            
            BsonMapper.Global = mapper;
        }
        
        public Repository(DatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string FileName => _configuration.FileName;
        
        protected void Execute(Action<LiteCollection<TEntity>> action)
        {
            using (var db = new LiteDatabase(FileName))
            {
                var collection = db.GetCollection<TEntity>();
                action(collection);
            }    
        }
        
        protected TResult Execute<TResult>(Func<LiteCollection<TEntity>, TResult> action)
        {
            using (var db = new LiteDatabase(FileName))
            {
                var collection = db.GetCollection<TEntity>();
                return action(collection);
            }    
        }

        public TEntity Get(int id) => Execute(collection => collection.FindById(id));
        
        public virtual IEnumerable<TEntity> GetAll() => Execute(collection => collection.FindAll().ToList());

        public void Create(TEntity item) => Execute(collection => collection.Insert(item));
        
        public void CreateRange(IEnumerable<TEntity> items) => Execute(collection => collection.Insert(items));

        public void Update(TEntity item) => Execute(collection => collection.Update(item));

        public void UpdateRange(IEnumerable<TEntity> items) => Execute(
            collection =>
            {
                foreach (var item in items)
                {
                    collection.Update(item);
                }
            }
        );
        
        public void Delete(TEntity item) => Execute(collection => collection.Delete(item.Id));
        
        public void DeleteRange(IEnumerable<TEntity> items) => Execute(
            collection =>
            {
                foreach (var item in items)
                {
                    collection.Delete(item.Id);
                }
            });

        public void DeleteAll()
        {
            using (var db = new LiteDatabase(FileName))
            {
                var collection = db.GetCollection<TEntity>();
                
                db.DropCollection(collection.Name);
            }   
        }
    }
}
