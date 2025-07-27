using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data;
using System.Data.SqlClient;

using System.Reflection;
using System.Collections;
using PL.Fwk.Entities;


namespace PL.Fwk.DataAccess
{
    public static class ObjectContextExtension
    {
        #region GetStoredProcCommand

        /// <summary>
        /// Creates a DbCommand for a stored procedure.
        /// </summary>       
        public static DbCommand GetStoredProcCommand(this ObjectContext ctx, String storedProcedureName)
        {

            ctx.ValidateInstance();

            DbConnection connection = ((EntityConnection)ctx.Connection).StoreConnection;
            //Se crea el comando que setea el contexto
            DbCommand command = connection.CreateCommand();
            //command.CommandTimeout = 240;
            command.CommandTimeout = 480;
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        public static DbCommand GetStoredProcCommand(this ObjectContext ctx, string storedProcedureName,
                                                      params object[] parameterValues)
        {
            ctx.ValidateInstance();
            DbCommand command = ctx.GetStoredProcCommand(storedProcedureName);
            ctx.SetParameters(command);
            ctx.AssignParameterValues(command, parameterValues);            
            return command;
        }

        #endregion

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(this ObjectContext ctx, DbCommand command)
        {
            ctx.ValidateInstance();

            if (ctx.Connection.State != ConnectionState.Open)
                ctx.Connection.Open();

            return command.ExecuteNonQuery();

        }

        public static int ExecuteNonQuery(this ObjectContext ctx, String storedProcedureName, Int32 Int32Parameter)
        {

            return ctx.ExecuteNonQuery(storedProcedureName, new object[] { Int32Parameter });

        }

        public static int ExecuteNonQuery(this ObjectContext ctx, String storedProcedureName, params object[] parameters)
        {
            using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName, parameters))
            {
                return ctx.ExecuteNonQuery(command);
            }
        }

        #endregion

        #region ExecuteReaders

        public static DbDataReader ExecuteDataReader(this ObjectContext ctx, DbCommand command)
        {
            ctx.ValidateInstance();

            if (ctx.Connection.State != ConnectionState.Open)
                ctx.Connection.Open();

            return command.ExecuteReader();
        }

        public static List<T> ExecuteReader<T>(this ObjectContext ctx, DbCommand command, IDataReaderMapper<T> mapper) where T : class, new()
        {
            ctx.ValidateInstance();
            try
            {
                List<T> resultList = new List<T>();

                DbDataReader dataReader = ctx.ExecuteDataReader(command);

                while (dataReader.Read())
                {
                    T objectToMap = new T();
                    mapper.MaterializeReader(dataReader, objectToMap);
                    resultList.Add(objectToMap);
                }

                dataReader.Close();

                return resultList;
            }
            catch
            {
                throw;
            }            
        }

        public static List<O> ExecuteReader<O>(this ObjectContext ctx, String storedProcedureName) 
            where O : class, new()
            
        {
            ctx.ValidateInstance();
            using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName))
            {
                return ctx.ExecuteReader<O>(command, new DataReaderMapper<O>());
            }
        }

        public static List<O> ExecuteReader<O>(this ObjectContext ctx, String storedProcedureName, params object[] parameterValues) 
            where O : class, new()
            
        {
            return ctx.ExecuteReader<O>(storedProcedureName,new DataReaderMapper<O>(),parameterValues);
        }

        public static String ExecuteSqlXml(this ObjectContext ctx, String storedProcedureName, params object[] parameterValues) {
            ctx.ValidateInstance();

            DbCommand dbCommand = ctx.GetStoredProcCommand(storedProcedureName, parameterValues);

            SqlDataReader sqlDataReader = (SqlDataReader)ctx.ExecuteDataReader(dbCommand);

            sqlDataReader.Read();

            return sqlDataReader.GetSqlString(0).Value;
        }

        public static List<O> ExecuteReader<O>(this ObjectContext ctx, String storedProcedureName, IDataReaderMapper<O> dataReaderMapper , params object[] parameterValues) 
            where O : class, new()
        {
            ctx.ValidateInstance();
            using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ctx.ExecuteReader<O>(command,dataReaderMapper );
            }
        }

        

        /// <summary>
        /// Ejecuta un SP que posee varios SELECT. 
        /// </summary>
        /// <typeparam name="T">Clase principal que devuelve el método</typeparam>
        /// <param name="ctx">ObjectContext</param>
        /// <param name="storedProcedureName">Nombre del SP</param>
        /// <param name="list">Contiene todas las propiedades de T que se deseean llenar.
        /// Si el SP ademas completa la entidad T se debe pasar un string con el nombre de T</param>
        /// <param name="parameterValues">Parámtros del SP</param>
        /// <returns>Devuelve una instancia de la clase T</returns>
        public static O ExecuteReader<O>(this ObjectContext ctx, String storedProcedureName,
                                        List<String> list, params Object[] parameterValues) 
            where O : class, new()
            
        {

            ctx.ValidateInstance();
            using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                try
                {
                    //REFACTOR: Esta implementación es temporal
                    O result = new O();
                    DbDataReader dataReader = ctx.ExecuteDataReader(command);
                    DataReaderMapper<O> mapper = new DataReaderMapper<O>();
                    Object objectToMap;

                    Boolean nextResult = true;
                    Int32 index = 0;
                    while (nextResult)
                    {
                        String propertyName = list[index];
                        index++;
                        while (dataReader.Read())
                        {
                            PropertyInfo pi = result.GetType().GetProperty(propertyName);
                            if (pi == null)
                            {
                                // Si es la entidad padre
                                if (result.ToString().Contains(propertyName))
                                {
                                    objectToMap = result;
                                    mapper.MaterializeReader(dataReader, objectToMap);
                                }
                            }
                            else
                            {
                                //Si es una lista de la entidad padre
                                PropertyInfo listPropertyInfo = pi.PropertyType.GetProperty("Item");
                                if (listPropertyInfo != null)
                                {
                                    objectToMap = listPropertyInfo.PropertyType.GetConstructor(new Type[] { }).Invoke(null);

                                    IList listProperty = (IList)pi.GetValue(result, null);
                                    if (listProperty == null)
                                    {
                                        pi.SetValue(result, pi.PropertyType.GetConstructor(new Type[] { }).Invoke(null), null);
                                        listProperty = (IList)pi.GetValue(result, null);
                                    }
                                    // TODO: Ver que para hacer esta implementación temporal se agrego esta sobrecarga que
                                    //       no utiliza un T sino un Object.
                                    mapper.MaterializeReader(dataReader, objectToMap);
                                    listProperty.Add(objectToMap);
                                }
                            }
                        }
                        nextResult = dataReader.NextResult();
                    }

                    dataReader.Close();

                    return result;
                }
                catch
                {
                    throw;
                }
            }
        }


        ///// <summary>
        ///// Ejecuta un Stored Procedure.
        ///// </summary>
        ///// <typeparam name="T">Tipo de valor devuelto (debe implementar la interfaz IIdentificable).</typeparam>
        ///// <param name="ctx">Contexto actual.</param>
        ///// <param name="storedProcedureName">Nombre del Stored Procedure de origen.</param>
        ///// <param name="list">Lista de Path de navegación.</param>
        ///// <param name="parameterValues">Parámetros del Stored Procedure.</param>
        ///// <returns>Lista de del tipo T (en donde T implementa la interfaz IIdentificable), con el resultado del stored procedure.</returns>
        //public static List<E> ReadList<E>(   this ObjectContext ctx, String storedProcedureName,
        //                                                List<String> list, 
        //                                                params Object[] parameterValues
        //                                 )
        //    where E : IIdentifiable, new()
            
        //{

        //    ctx.ValidateInstance();

        //    using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName, parameterValues))
        //    {
        //        try
        //        {
        //            E result = new E();
        //            DbDataReader dataReader = ctx.ExecuteDataReader(command);
        //            EntityDataReaderMapper<E> mapper = new EntityDataReaderMapper<E>();

        //            if (typeof(E).IsSubclassOf(typeof(BusinessEntity<T>)))
        //            {
        //                //Nos suscribimos a los eventos de comienzo y fin de mapeo
        //                mapper.BegginMapping += new EventHandler<ItemMappingEventArgs<T>>(mapper_BegginMapping);
        //                mapper.EndMapping += new EventHandler<ItemMappingEventArgs<T>>(mapper_EndMapping);
        //            }

        //            Boolean nextResult = true;
        //            Int32 index = 0;

        //            while (nextResult)
        //            {
        //                if (index == 0)
        //                {
        //                    mapper.MaterializeReader(dataReader, result, String.Empty);
        //                }
        //                else
        //                {
        //                    if ((index-1) < list.Count)
        //                    {
        //                        mapper.MaterializeReader(dataReader, result, list[index - 1]);
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("La cantidad de propiedades mapeadas no coincide con la cantidad de consultas");
        //                    }
        //                }

        //                index+= 1;
        //                nextResult = dataReader.NextResult();
        //            }

        //            dataReader.Close();

        //            if (typeof(E).IsSubclassOf(typeof(BusinessEntity<T>)))
        //            {
        //                //Nos dessuscribimos a los eventos de mapeo.-
        //                mapper.BegginMapping -= mapper_BegginMapping;
        //                mapper.EndMapping -= mapper_EndMapping;
        //            }

        //            return mapper.GetEntityMapped();
        //        }
        //        catch
        //        {
        //            throw;
        //        }               
        //    }
        //}


        //private static void mapper_EndMapping<T>(object sender, ItemMappingEventArgs<T> e)
        //    where T : struct, IComparable<T>, IEquatable<T>
        //{

        //    BusinessEntity<T> businessEntity = e.ItemMapping as BusinessEntity<T>;

        //    if (businessEntity != null)
        //    {
        //        businessEntity.Loading = false;
        //        businessEntity.SetAsRestored();
        //    }                        
        //}

        //private static void mapper_BegginMapping<T>(object sender, ItemMappingEventArgs<T> e)
        //    where T : struct, IComparable<T>, IEquatable<T>
        //{
        //    BusinessEntity<T> businessEntity = e.ItemMapping as BusinessEntity<T>;

        //    if (businessEntity != null)
        //        businessEntity.Loading = true;
        //}


        //public static E Read<E, T>(this ObjectContext ctx, String storedProcedureName,
        //                                                List<String> list,
        //                                                params Object[] parameterValues
        //                                            ) 
        //    where E : IIdentifiable, new()
        //    where T : struct, IComparable<T>, IEquatable<T>
        //{
        //    return ReadList<E, T>(ctx, storedProcedureName, list, parameterValues).FirstOrDefault<E>();

        //}

        //public static E ReadEntity<E, T>(this ObjectContext ctx, String storedProcedureName,
        //                                                List<String> list,
        //                                                params Object[] parameterValues
        //                                            ) 
        //    where E : BusinessEntity<T>, new()
        //    where T : struct, IComparable<T>, IEquatable<T>
        //{

        //    return ReadList<E, T>(ctx, storedProcedureName, list, parameterValues).FirstOrDefault<E>();

        //}


        #endregion

        #region Execute Scalar

        public static T ExecuteScalar<T>(this ObjectContext ctx, DbCommand command)
        {
            ctx.ValidateInstance();

            if (ctx.Connection.State != ConnectionState.Open)
                ctx.Connection.Open();

            return (T)command.ExecuteScalar();

        }

        public static T ExecuteScalar<T>(this ObjectContext ctx, String storedProcedureName, Int32 Int32Parameter)
        {

            return ctx.ExecuteScalar<T>(storedProcedureName, new object[] { Int32Parameter });

        }

        public static T ExecuteScalar<T>(this ObjectContext ctx, String storedProcedureName, params object[] parameters)
        {
            using (DbCommand command = ctx.GetStoredProcCommand(storedProcedureName, parameters))
            {
                return ctx.ExecuteScalar<T>(command);
            }
        }

        #endregion

        #region Helpers

        public static void DeriveParameters(this ObjectContext ctx, DbCommand discoveryCommand)
        {
            ctx.ValidateInstance();
            SqlCommand sqlCommand = discoveryCommand as SqlCommand;

            if (sqlCommand != null)
            {
                if (ctx.Connection.State != ConnectionState.Open)
                    ctx.Connection.Open();

                SqlCommandBuilder.DeriveParameters(sqlCommand);
            }
            //TODO: Agregar otras implementaciones para cuando no sea SQL (si hace falta)
        }

        public static void ValidateInstance(this ObjectContext ctx)
        {
            if (ctx == null)
            {
                throw new Exception("Se requiere una instancia de ObjectContext");
            }
        }

        public static void SetParameters(this ObjectContext ctx, DbCommand command)
        {
            ctx.ValidateInstance();
            if (command == null)
                throw new ArgumentNullException("command");

            if (ctx.AlreadyCached(command))
            {
                ctx.AddParametersFromCache(command);
            }
            else
            {
                ctx.DeriveParameters(command);

                //IDataParameter[] copyOfParameters = CreateParameterCopy(command);

                //this.cache.AddParameterSetToCache(command, copyOfParameters);
            }
        }

        public static void AddParametersFromCache(this ObjectContext ctx, DbCommand command)
        {
            throw new NotImplementedException();
        }

        public static bool AlreadyCached(this ObjectContext ctx, DbCommand command)
        {
            return false;
        }

        public static void AssignParameterValues(this ObjectContext ctx, DbCommand command, Object[] values)
        {
            Int32 parameterIndexShift = 1;

            for (Int32 i = 0; i < values.Length; i++)
            {
                IDataParameter parameter = command.Parameters[i + parameterIndexShift];
                // There used to be code here that checked to see if the parameter was input or input/output
                // before assigning the value to it. We took it out because of an operational bug with
                // deriving parameters for a stored procedure. It turns out that output parameters are set
                // to input/output after discovery, so any direction checking was unneeded. Should it ever
                // be needed, it should go here, and check that a parameter is input or input/output before
                // assigning a value to it.
                Boolean removeParameter = ctx.SetParameterValue(command, parameter.ParameterName, values[i]);
                if (removeParameter)
                {
                    parameterIndexShift--;
                }
            }
        }


        public static Boolean SetParameterValue(this ObjectContext ctx, DbCommand command,
                                              string parameterName,
                                              object value)
        {
            //Verificar el Tipo de Valor del parametro

            //Int32
            if (value is Int32 && (Int32)value == Int32.MinValue)
            {
                value = DBNull.Value;
            }

            //Verificar si es un TableParameter
            //DBTableParameter dbTableParameter = value as DBTableParameter;

            //if (dbTableParameter != null)
            //{
            //    dbTableParameter.SetParameterToCommand(command, parameterName);
            //    return true;
            //}

            command.Parameters[parameterName].Value = value ?? DBNull.Value;

            return false;
        }

        #endregion
    }
}