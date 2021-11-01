using System;
using System.Reflection;
using System.Collections.Generic;

namespace FourPatient.WebAPI
{
    class Map
    {
        // Copy Table into Model
        public static dynamic Model(dynamic n, int searchDepth = 0)
        {
            if (n == null)
                return null;
            string S = n.GetType().AssemblyQualifiedName.Replace("Domain", "WebAPI").Replace("Tables", "Models");
            Type T = Type.GetType(S);
            if (T == null)
            {
                throw new Exception("Type " + S + " not found.");
            }
            dynamic N = Activator.CreateInstance(T);

            foreach (var prop in n.GetType().GetProperties())
            {
                PropertyInfo targetProperty = N.GetType().GetProperty(prop.Name);

                //This copies all properties that can be copied
                //Additional properties can be added without updating this code
                if (targetProperty.PropertyType.IsAssignableFrom(prop.PropertyType))
                {
                    targetProperty.SetValue(N, prop.GetValue(n, null));
                }
                else if (searchDepth < 1)
                {
                    if (prop.GetValue(n, null) == null)
                        continue;
                    if (prop.GetValue(n, null) != null && !prop.PropertyType.IsInterface)
                        targetProperty.SetValue(N, Model(prop.GetValue(n, null), searchDepth + 1));
                    else if (prop.PropertyType.IsInterface)
                    {
                        if (n.Reviews == null)
                            continue;
                        ICollection<Models.Review> R = new List<Models.Review>();
                        foreach (var review in n.Reviews)
                        {
                            R.Add(Model(review, searchDepth + 1));
                        }
                        N.Reviews = R;
                    }
                }
            }

            return N;
        }
        // Copy Model into Table
        public static dynamic Table(dynamic n, int searchDepth = 0)
        {
            if (n == null)
                return null;
            string S = n.GetType().AssemblyQualifiedName.Replace("WebAPI", "Domain").Replace("Models", "Tables");
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