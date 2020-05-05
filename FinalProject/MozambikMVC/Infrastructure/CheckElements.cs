using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MozambikMVC.Infrastructure
{
    public static class OperationWithDbContext  
    {
        public static bool IsElementInDatabase<T>(this DbContext context,T model,string propName)where T:class
        {
            try
            {
                var type = context.Set<T>().ToList();
                foreach (var item in type)
                {
                    var dbModel = item.GetType().GetProperty(propName).GetValue(item);
                    var comingModel = model.GetType().GetProperty(propName).GetValue(model);
                    if (dbModel.Equals(comingModel))
                    {
                        return true;
                    }
                }
               
            }
            catch
            {
                return false;
            }
            return false;
        }
        public static async Task CreateElementAsync<T>(this DbContext context, T model, int createrID) where T:class
        {
            try
            {
                model.GetType().GetProperty("CreatedDate").SetValue(model, DateTime.Now);
                model.GetType().GetProperty("CreatedID").SetValue(model, createrID);
                await context.Set<T>().AddAsync(model);
                await context.SaveChangesAsync();
            }
            catch
            {

                throw;
            }
          
        }
        public static async Task UpdateElementAsync<T>(this DbContext context,int modifiedID, T model) where T : class
        {
            try
            {
                model.GetType().GetProperty("ModifiedDate").SetValue(model, DateTime.Now);
                model.GetType().GetProperty("ModifiedID").SetValue(model, modifiedID);
                context.Entry<T>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {

                throw new Exception("Update emeliyyati ugursuzdur!");
            }
          
        }
        public static async Task DeleteElementAsync<T>(this DbContext context, int deletedID, T model) where T : class
        {
            try
            {
                model.GetType().GetProperty("DeletedDate").SetValue(model, DateTime.Now);
                model.GetType().GetProperty("DeletedID").SetValue(model, deletedID);
                context.Set<T>().Update(model);
                await context.SaveChangesAsync();
            }
            catch
            {

                throw;
            }

        }

        public static IEnumerable<T> GetElement<T>(this DbContext context) where T : class
        {
            var elements = context.Set<T>().ToList();
            List<T> items = new List<T>();
            foreach (var item in elements)
            {
                if(item.GetType().GetProperty("DeletedID").GetValue(item,null) == null)
                {
                    items.Add(item);
                }

            }
            return items;
            
        }
        
        public static bool IsElementChanged<T>(this DbContext context,T changedElement) where T : class
        {
            PropertyInfo[] properties = changedElement.GetType().GetProperties();
            var id = changedElement.GetType().GetProperty("ID").GetValue(changedElement, null);
           var unChangedElement =  context.Set<T>().Find(id);
            foreach(PropertyInfo property in properties)
            {
                var changedElementValue = changedElement.GetType().GetProperty(property.Name).GetValue(changedElement, null);    
                var unChangedElementValue = unChangedElement.GetType().GetProperty(property.Name).GetValue(unChangedElement, null);
               
                if (!changedElementValue.Equals(unChangedElementValue)&&changedElementValue!=null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsPropertyValueChanged<T>(this DbContext context, T changedElement,string properyName) where T : class
        {
            var id = changedElement.GetType().GetProperty("ID").GetValue(changedElement, null);
            var unChangedElement = context.Set<T>().Find(id);
            var changedElementValue = changedElement.GetType().GetProperty(properyName).GetValue(changedElement, null);
            var unChangedElementValue = unChangedElement.GetType().GetProperty(properyName).GetValue(unChangedElement, null);
            if (changedElementValue.Equals(unChangedElementValue))
             {
                    return true;
             }
            
            return false;
        }
    }
}
