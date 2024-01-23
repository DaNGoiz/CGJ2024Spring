using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// sqlite数据库中Global数据表的数据库操作类
    /// </summary>
    public class GlobalDAO
    {

        /// <summary>
        /// 根据全局数据名称获取全局数据数值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetDataByName(string name)
        {
            SqliteDataReader reader = null;
            string query = "SELECT * FROM global where name=@name";
            SqliteCommand dbCommand;
            dbCommand = SqlManager.Instance.dbConnection.CreateCommand();
            dbCommand.Parameters.AddWithValue("@name", name);
            dbCommand.CommandText = query;
            reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                string data = "";
                if (!reader.IsDBNull(2))
                    data = reader.GetString(2);

                return data;
            }
            return null;
        }
        /// <summary>
        /// 判断全局数据名称是否存在
        /// </summary>
        /// <param name="name">全局数据名称</param>
        /// <returns></returns>
        public bool JudgeNameExit(string name)
        {
            SqliteDataReader reader = null;
            string query = "SELECT * FROM global where name=@name";
            SqliteCommand dbCommand;
            dbCommand = SqlManager.Instance.dbConnection.CreateCommand();
            dbCommand.Parameters.AddWithValue("@name", name);
            dbCommand.CommandText = query;
            reader = dbCommand.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加全局数据
        /// </summary>
        /// <param name="name">全局数据名称</param>
        /// <param name="data">全局数据数值</param>
        /// <returns></returns>
        public bool AddGlobalData(string name, string data)
        {
            string query = "INSERT INTO global(name,data) values (@name,@data)";
            SqliteCommand dbCommand;
            dbCommand = SqlManager.Instance.dbConnection.CreateCommand();
            dbCommand.CommandText = query;
            dbCommand.Parameters.AddWithValue("@name", name);
            dbCommand.Parameters.AddWithValue("@data", data);
            dbCommand.ExecuteReader();
            return true;
        }

        /// <summary>
        /// 根据name删除全局字段
        /// </summary>
        /// <param name="name">全局数据名称</param>
        public void DeleteGlobalByName(string name)
        {
            string query = "delete from global where name=@name";
            SqliteCommand dbCommand;
            dbCommand = SqlManager.Instance.dbConnection.CreateCommand();
            dbCommand.CommandText = query;
            dbCommand.Parameters.AddWithValue("@name", name);
            dbCommand.ExecuteReader();
        }

        /// <summary>
        /// 更新全局数据
        /// </summary>
        /// <param name="name">全局数据名称</param>
        /// <param name="data">全局数据数值</param>
        public void UpdateGlobal(string name, string data)
        {
            string query = "update global set data=@data where name=@name";
            SqliteCommand dbCommand;
            dbCommand = SqlManager.Instance.dbConnection.CreateCommand();
            dbCommand.CommandText = query;
            dbCommand.Parameters.AddWithValue("@name", name);
            dbCommand.Parameters.AddWithValue("@data", data);

            dbCommand.ExecuteReader();
        }
    }
}