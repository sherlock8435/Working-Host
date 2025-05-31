using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class CityDB : DBFunctions
    {
        private CityList list = new CityList();
        private Cities CreateModel(Cities c)
        {
            c.CityID=(int)reader["cityID"];
            c.Cityname = reader["cityname"].ToString();
            return c;

        }
        public Cities SelectCityByName(string cityName)
        {
            list = SelectAllCities();
            Cities c = list.Find(item => item.Cityname == cityName);
            return c;
        }
        public int SelectCityIDByName(string cityName)
        {
            list = SelectAllCities();
            int c = list.Find(item => item.Cityname == cityName).CityID;
            return c;
        }
        public Cities SelectCityById(int id)
        {
            list = SelectAllCities();
            Cities c = list.Find(item => item.CityID == id);
            return c;
        }
        public List<Cities> OrderByCityName()
        {
            list = SelectAllCities();
            return list.OrderBy(item => item.Cityname).ToList();
        }
        public CityList SelectAllCities()
        {
            try
            {
                string sqlStr="SELECT * FROM Citytbl";
                cmd=GenerateOleDBCommand(sqlStr,"DB.accdb");
                conObj.Open();
                reader=cmd.ExecuteReader();
                while(reader.Read())
                {
                    Cities c= new Cities ();
                    list.Add(CreateModel(c));
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if(reader!=null)
                    reader.Close();
                if(this.conObj.State== System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }
    }
}
