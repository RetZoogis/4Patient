using System;
using System.Reflection;
using System.Collections.Generic;

namespace FourPatient.DataAccess
{
    class Map
    {
        // Copy Table into Entity
        public static dynamic Entity(dynamic n, int searchDepth = 0)
        {
            if (n == null)
                return null;
            string S = n.GetType().AssemblyQualifiedName.Replace("Domain", "DataAccess").Replace("Tables", "Entities");
            Type T = Type.GetType(S);
            if (T == null)
            {
                throw new Exception("Type " + S + " not found.");
            }
            dynamic N = Activator.CreateInstance(T);

            foreach (var prop in n.GetType().GetProperties())
            {
                if (prop.GetValue(n, null) == null)
                    continue;
                PropertyInfo targetProperty = N.GetType().GetProperty(prop.Name);

                //This copies all properties that can be copied
                //Additional properties can be added without updating this code
                if (targetProperty.PropertyType.IsAssignableFrom(prop.PropertyType)
                    && prop.GetValue(n, null) != null)
                {
                    targetProperty.SetValue(N, prop.GetValue(n, null));
                }
                else if (searchDepth < 1)
                {
                    if (prop.GetValue(n, null) != null && !prop.PropertyType.IsInterface)
                        targetProperty.SetValue(N, Entity(prop.GetValue(n, null), searchDepth + 1));
                    else if (prop.PropertyType.IsInterface)
                    {
                        //if (n.Reviews == null)
                        //    continue;
                        ICollection<Entities.Review> R = new List<Entities.Review>();
                        foreach (var review in n.Reviews)
                        {
                            R.Add(Entity(review, searchDepth + 1));
                        }
                        N.Reviews = R;
                    }
                }
            }

            return N;
        }

        // Copy Entity into Table
        public static dynamic Table(dynamic n, int searchDepth = 0)
        {
            if (n == null)
                return null;
            string S = n.GetType().AssemblyQualifiedName.Replace("DataAccess", "Domain").Replace("Entities", "Tables");
            Type T = Type.GetType(S);
            if (T == null)
            {
                throw new Exception("Type " + S + " not found.");
            }
            dynamic N = Activator.CreateInstance(T);

            foreach (var prop in n.GetType().GetProperties())
            {
                if (prop.GetValue(n, null) == null)
                    continue;

                PropertyInfo targetProperty = N.GetType().GetProperty(prop.Name);

                //This copies all properties that can be copied
                //Additional properties can be added without updating this code
                if (targetProperty.PropertyType.IsAssignableFrom(prop.PropertyType))
                {
                    targetProperty.SetValue(N, prop.GetValue(n, null));
                }
                else if (searchDepth < 1)
                {
                    if (prop.GetValue(n, null) != null && !prop.PropertyType.IsInterface)
                        targetProperty.SetValue(N, Table(prop.GetValue(n, null), searchDepth + 1));
                    else if (prop.PropertyType.IsInterface)
                    {
                        if (n.Reviews == null)
                            continue;
                        ICollection<Domain.Tables.Review> R = new List<Domain.Tables.Review>();
                        foreach (var review in n.Reviews)
                        {
                            R.Add(Table(review, searchDepth + 1));
                        }
                        N.Reviews = R;
                    }
                }
            }

            return N;
        }
    }
}
