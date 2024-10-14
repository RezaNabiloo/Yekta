using BSG.ADInventory.Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace BSG.ADInventory.WebUI.Helpers
{
    public class DataHelper
    {
        private static JObject _data;
        private static string _filePath;

        static DataHelper()
        {
            _filePath = HttpContext.Current.Server.MapPath("~/App_Data/CustomSetting.json");
            LoadData();
        }

        private static void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _data = JObject.Parse(json);
            }
            else
            {
                _data = new JObject();
            }
        }

        public static JObject GetImageSizeSetting(string size)
        {
            return  _data["ProductImageSizes"]?[size] as JObject;
        }

        public static void UpdateImageSizeSetting(string size, JObject newValue)
        {
            if (_data["ProductImageSizes"] == null)
            {
                _data["ProductImageSizes"] = new JObject();
            }
            _data["ProductImageSizes"][size] = newValue;
            File.WriteAllText(_filePath, _data.ToString());
        }

        public static List<PictureSize> GetAllImageSizes(string imageType)
        {
            var imageSizes = new List<PictureSize>();

            var productImageSizes = _data[imageType] as JObject;
            if (productImageSizes != null)
            {
                foreach (var item in productImageSizes)
                {
                    var pictureSize = item.Value.ToObject<PictureSize>();
                    imageSizes.Add(pictureSize);
                }
            }

            return imageSizes;
        }

        public static Object GetElementSettingByKey(string key)
        {            
            var languageImageSize = _data[key];
            return languageImageSize?.ToObject<PictureSize>();
        }
    }
}