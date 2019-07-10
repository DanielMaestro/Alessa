using System.Linq;
using Alessa.Core.Helpers;
using Alessa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alessa.Core.EntityFramework.Extensions
{
    /// <summary>
    /// Extension class for saving an entity.
    /// </summary>
    public static class SavingExtensions
    {
        /// <summary>
        /// Saves the specified entity into the Database.
        /// </summary>
        /// <typeparam name="E">Entity type.</typeparam>
        /// <param name="func">Delegate to generate a custom primary key.</param>
        /// <param name="entity">Entity to save.</param>
        /// <param name="commitChanges">Indicates whther the changes must be commited or not.</param>
        /// <param name="dbContext">Data context.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task SaveEntityAsync<E>(this DbContext dbContext, E entity, bool commitChanges = true, System.Action<E> func = null)
            where E : class, new()
        {
            await TrySaveEntityAsync(dbContext, entity, null, commitChanges, func);
        }

        /// <summary>
        /// Saves the specified entity into the Database specifying the user.
        /// </summary>
        /// <typeparam name="E">Entity type.</typeparam>
        /// <param name="func">Delegate to generate a custom primary key.</param>
        /// <param name="entity">Entity to save.</param>
        /// <param name="commitChanges">Indicates whther the changes must be commited or not.</param>
        /// <param name="dbContext">Data context.</param>
        /// <param name="userId">User id.</param>
        /// <remarks>To use this method correctly the <typeparamref name="E"/> entity must implement <see cref="ICreate"/> and/or <see cref="IUpdate"/> 
        /// interfaces.</remarks>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task SaveEntityAsync<E>(this DbContext dbContext, E entity, string userId, bool commitChanges = true, System.Action<E> func = null)
            where E : class, ICreate, IUpdate, new()
        {
            await TrySaveEntityAsync(dbContext, entity, userId, commitChanges, func);
        }
        /// <summary>
        /// Tries to save an entity into te database.
        /// </summary>
        /// <typeparam name="E">Type of entity.</typeparam>
        /// <param name="dbContext">Data context.</param>
        /// <param name="entity">Entity to save.</param>
        /// <param name="userId">User id.</param>
        /// <param name="commitChanges">Indicates whther the changes must be commited or not.</param>
        /// <param name="func">Delegate to generate a custom primary key.</param>
        /// <returns></returns>
        private static async System.Threading.Tasks.Task TrySaveEntityAsync<E>(DbContext dbContext, E entity, string userId, bool commitChanges, System.Action<E> func)
        where E : class, new()
        {
            var keyProperties = EntityHelper.GetKeyProperties<E>();

            var values = (from keyProperty in keyProperties
                          select keyProperty.GetValue(entity)).ToArray();

            var actualEntity = await dbContext.Set<E>().FindAsync(values);

            // Updates the created and updated properties.
            UpdateEntity(ref entity, actualEntity, userId);

            // The entity is new.
            if (actualEntity == null)
            {
                // If the custom PK generator is not specified
                if (func == null)
                {
                    await SetKeyValueAsync(dbContext, entity, keyProperties);
                }
                else
                {
                    func(entity);
                }


                await dbContext.AddAsync<E>(entity);
            }
            else
            {
                await CopyEntityValuesAsync(entity, actualEntity, keyProperties.Select(e => e.Name), false);
                dbContext.Update(actualEntity);
            }

            // Everything is ready, now it validate if must commit the changes or not.
            if (commitChanges)
            {
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="newEntity">Entity to update.</param>
        /// <param name="userId">User Id</param>
        /// <param name="actualEntity">Actual entity</param>
        private static void UpdateEntity<E>(ref E newEntity, E actualEntity, string userId)
        {
            bool mustContainUser = false;


            // If can be created.
            var created = newEntity as ICreate;
            if (created != null)
            {
                mustContainUser = true;

                if (actualEntity == null)
                {
                    created.CreatedBy = userId;
                    created.CreatedDate = System.DateTime.Now;
                }
                else
                {
                    var old = actualEntity as ICreate;
                    created.CreatedDate = old.CreatedDate;
                    created.CreatedBy = old.CreatedBy;
                }
            }

            // If can be updated.
            var updated = newEntity as IUpdate;
            if (updated != null)
            {
                updated.UpdatedBy = userId;
                updated.UpdatedDate = System.DateTime.Now;
                mustContainUser = true;
            }

            // If the user is null and must contain user then it throws an exception.
            if (mustContainUser && string.IsNullOrWhiteSpace(userId))
            {
                throw new System.ArgumentException("The user must be specified when an entity inherits from IUpdate or ICreate.", "userId");
            }

        }

        /// <summary>
        /// Copies the entity values into the target. this is useful when updating entities.
        /// </summary>
        /// <typeparam name="E">Entity Type</typeparam>
        /// <param name="source">Source.</param>
        /// <param name="target">Target.</param>
        /// <param name="propertySet">If this value is specified, the it excludes the properties with the names contained in this collection.</param>
        /// <param name="includeOrExclude">Indicates if the <paramref name="propertySet"/> collection must be included (true) or excluded (false) when the properties are set.</param>
        /// <returns></returns>
        private static async System.Threading.Tasks.Task CopyEntityValuesAsync<E>(E source, E target, System.Collections.Generic.IEnumerable<string> propertySet = null, bool includeOrExclude = false)
        //where E : class, new()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> properties;
                if (propertySet != null)
                {
                    properties = from property in EntityHelper.GetProperties<E>()
                                 from name in propertySet
                                 where includeOrExclude ? property.Name.Equals(name) : !property.Name.Equals(name)
                                 select property;

                }
                else
                {
                    properties = EntityHelper.GetProperties<E>();
                }

                System.Threading.Tasks.Parallel.ForEach(properties, (e) =>
                {
                    if (e.CanWrite)
                    {
                        var sValue = e.GetValue(source);
                        e.SetValue(target, sValue);
                    }
                });
            });
        }

        private static object GetKeyValue<E>(ref E entity, System.Reflection.PropertyInfo property)
        //where E : class, new()
        {
            object result = null;

            if (property.PropertyType == typeof(long) /*|| property.PropertyType == typeof(float) || property.PropertyType == typeof(double)*/)
            {
                var actualValue = System.Convert.ToInt64(property.GetValue(entity));
                result = ++actualValue;
            }
            else if (property.PropertyType == typeof(int))
            {
                var actualValue = System.Convert.ToInt32(property.GetValue(entity));
                result = ++actualValue;
            }
            else if (property.PropertyType == typeof(short))
            {
                var actualValue = System.Convert.ToInt16(property.GetValue(entity));
                result = ++actualValue;
            }
            else if (property.PropertyType == typeof(byte))
            {
                var actualValue = System.Convert.ToByte(property.GetValue(entity));
                result = ++actualValue;
            }
            else if (property.PropertyType == typeof(System.Guid))
            {
                result = System.Guid.NewGuid();
            }
            else if (property.PropertyType == typeof(System.DateTime))
            {
                result = System.DateTime.Now;
            }
            //}
            return result;
        }

        private static object GetDefaultValue(System.Type t)
        {
            if (t.IsValueType)
                return System.Activator.CreateInstance(t);

            return null;
        }

        private static async System.Threading.Tasks.Task SetKeyValueAsync<E>(DbContext dbContext, E entity, System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> properties)
            where E : class, new()
        {
            // Creates an ORDER BY statement.
            var strOrder = string.Join(",", properties.Select(e => e.Name).ToArray()) + " DESC";
            // Gets the las entity in the table sorted by the Primary keys.
            var lastEntity = await System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(dbContext.Set<E>(), strOrder).FirstOrDefaultAsync();
            // If null then it means its a new entity.
            if (lastEntity == null)
            {
                lastEntity = new E();
            }

            System.Threading.Tasks.Parallel.ForEach(properties, (p) =>
            {
                var val = p.GetValue(entity);

                if (val.Equals(GetDefaultValue(p.PropertyType)))
                {
                    val = GetKeyValue(ref lastEntity, p);
                }

                p.SetValue(entity, val);
            });
        }
    }
}
