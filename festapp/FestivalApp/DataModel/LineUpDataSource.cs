using models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.DataModel
{

    public class LineUps
    { 
      private static List<LineUp> _lijst = new List<LineUp>();
      public static List<LineUp> Lijst
      {
          get { return _lijst; }

          set { _lijst = value; }
      
      
      }
    
    }

    class LineUpDataSource
    {

        

        public static async Task HaalLineUp()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:5069//api/LineUp");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<LineUp>));
                List<LineUp> list = dxml.ReadObject(stream) as List<LineUp>;

                LineUps.Lijst = list;
            }
        }
    }
}
