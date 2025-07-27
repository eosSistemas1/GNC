using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using PL.Fwk.Entities;

namespace PL.Fwk.DataAccess
{
    public class DataReaderMapper<O> : IDataReaderMapper<O>
       
    {

        protected Dictionary<Type, PropertyInfo[]> ReflectionCache { get; set; }

        public DataReaderMapper()
        {
            this.ReflectionCache = new Dictionary<Type, PropertyInfo[]>();
        }

        public void MaterializeReader(DbDataReader dr, O objectToMap)
        {
            //Obtener las propiedades del objeto a Mapear
            System.Reflection.PropertyInfo[] properties = null;

            if (this.ReflectionCache.ContainsKey(objectToMap.GetType()))
            {
                properties = this.ReflectionCache[objectToMap.GetType()];
            }
            else
            {
                properties = objectToMap.GetType().GetProperties();
                this.ReflectionCache.Add(objectToMap.GetType(), properties);
            }
            //<Indice,ColumnName> de los fields del DataReader
            Dictionary<int, String> dataReaderFields = this.GetDataReaderFields(dr);

            //Por cada propiedad del objeto
            foreach (System.Reflection.PropertyInfo prop in properties)
            {
                MethodInfo setMethod = prop.GetSetMethod();

                if (prop.CanWrite && setMethod != null)
                {
                    // TODO: Esto está hecho para evitar campos en ingles en la base de datos (Description).
                    if (prop.Name == "Description")
                    {
                        if (dataReaderFields.ContainsValue("Descripcion") &&
                            !dr.IsDBNull(dataReaderFields.Where(pair => pair.Value == "Descripcion").First<KeyValuePair<Int32, String>>().Key))
                        {
                            setMethod.Invoke(objectToMap, new Object[] { dr["Descripcion"] });
                        }
                    }
                    if (dataReaderFields.ContainsValue(prop.Name) //Verificar que exista la columna en el DR
                        && !dr.IsDBNull(dataReaderFields.Where(pair => pair.Value == prop.Name).First<KeyValuePair<Int32, String>>().Key) //Verificar qeu no sea DBNULL
                        )
                    {
                        setMethod.Invoke(objectToMap, new Object[] { dr[prop.Name] });

                    }
                    if (dataReaderFields.ContainsValue("ID_" + prop.Name) &&
                        dataReaderFields.ContainsValue("Descripcion_" + prop.Name) &&
                        dr["ID_" + prop.Name] != DBNull.Value)
                    {

                        ViewEntity viewEntity = (ViewEntity)Activator.CreateInstance(prop.PropertyType);

                        viewEntity.ID = (Guid)dr["ID_" + prop.Name];

                        if (dr["Descripcion_" + prop.Name] != DBNull.Value)
                            viewEntity.Descripcion = (String)dr["Descripcion_" + prop.Name];
                       
                        setMethod.Invoke(objectToMap, new Object[] { viewEntity });
                    }
                }
            }
        }

        //TODO: Se agrego este método de forma temporal para:
        // se deberia crear otro dataReaderMapper que sepa materializar tambien los hijos cuando tenga otros resulset
        // y ahi poner este metodo con otro nombre. (mauricio.zamboni)
        //      public static T ExecuteReader<T>(this ObjectContext ctx, String storedProcedureName,
        //                                       List<String> list, params Object[] parameterValues) where T : class, new()
        public void MaterializeReader(DbDataReader dr, Object objectToMap)
        {
            //Obtener las propiedades del objeto a Mapear
            System.Reflection.PropertyInfo[] properties = null;

            if (this.ReflectionCache.ContainsKey(objectToMap.GetType()))
            {
                properties = this.ReflectionCache[objectToMap.GetType()];
            }
            else
            {
                properties = objectToMap.GetType().GetProperties();
                this.ReflectionCache.Add(objectToMap.GetType(), properties);
            }
            //<Indice,ColumnName> de los fields del DataReader
            Dictionary<int, String> dataReaderFields = this.GetDataReaderFields(dr);

            //Por cada propiedad del objeto
            foreach (System.Reflection.PropertyInfo prop in properties)
            {
                MethodInfo setMethod = prop.GetSetMethod();

                if (prop.CanWrite && setMethod != null //Verificar que se pueda escribir la propiedad y tenga Metodo Set
                     && dataReaderFields.ContainsValue(prop.Name) //Verificar que exista la columna en el DR
                     && !dr.IsDBNull(dataReaderFields.Where(pair => pair.Value == prop.Name).First<KeyValuePair<int, String>>().Key) //Verificar qeu no sea DBNULL
                     )
                {
                    setMethod.Invoke(objectToMap, new Object[] { dr[prop.Name] });
                }
            }
        }

        /// <summary>
        /// Devuelve un Dictionary con el para (indice, ColumnName) que contiene el DR
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Dictionary<int, String> GetDataReaderFields(DbDataReader dr)
        {
            Dictionary<int, String> dataReaderFields = new Dictionary<int, string>();
            for (Int32 i = 0; i < (dr.FieldCount); i++)
            {
                dataReaderFields.Add(i, dr.GetName(i));
            }
            return dataReaderFields;
        }
    }



}
