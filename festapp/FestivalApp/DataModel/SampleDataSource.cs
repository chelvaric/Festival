using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization;
using models;
using System.Threading.Tasks;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace FestivalApp.Data
{



  
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon : FestivalApp.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private String _image = null;
        private String _imagePath = null;
        public String Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    //this._image = new BitmapImage(new Uri(SampleDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        public SampleDataItem()
            : base(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        {
            
           
        }
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, string facebook,string twitter, List<SampleDataGroup> group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._facebook = facebook;
            this._twitter = twitter;
            this.Group = group;
            
        }

        private string _twitter = string.Empty;
        public string Twitter
        {
            get { return this._twitter; }
            set { this.SetProperty(ref this._twitter, value); }
        }



        private string _facebook= string.Empty;
        public string Facebook
        {
            get { return this._facebook; }
            set { this.SetProperty(ref this._facebook, value); }
        }

        private List<SampleDataGroup> _group;
        public List<SampleDataGroup> Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup : SampleDataCommon
    {
        public SampleDataGroup()
            : base(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }
        public SampleDataGroup(String uniqueId, String title)
            : base(uniqueId, title, String.Empty, String.Empty, String.Empty)
        {
           
        }
     

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }

        
        public IEnumerable<SampleDataItem> TopItems
        {
            get {return this.Items.Take(12); }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _allGroups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<SampleDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return _sampleDataSource.AllGroups;
        }

        public static SampleDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() != 0) return matches.First();
            return null;
        }
        public static async Task HaalGenre()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:5069//api/Data");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<Genre>));
                List<Genre> list = dxml.ReadObject(stream) as List<Genre>;
                CreateGroup(list);

            }



        }

        public static async Task HaalData()
        {

            await SampleDataSource.HaalGenre();
            await SampleDataSource.HaalBand();
        }

        public static async Task HaalBand()
        {
        
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:5069//api/Band");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<Band>));
                List<Band> list = dxml.ReadObject(stream) as List<Band>;
                CreateItem(list);

            }



        }
        public static void CreateItem(List<Band> bands)
        {
           
            foreach (Band ban in bands)
            {
                SampleDataItem item = new SampleDataItem();
                item.Group = new List<SampleDataGroup>();
                item.UniqueId = ban.ID;
                item.Title = ban.Name;
                item.Image = ban.Picture;
                item.Description = ban.Description;
                item.Facebook = ban.Facebook;
                item.Twitter = ban.Twitter;
                foreach (Genre id in ban.Genres)
                {
                   
                    foreach (SampleDataGroup temp in GetGroups("AllGroups"))
                    {
                        if (temp.UniqueId == id.ID.ToString())
                        {
                            item.Group.Add(temp);
                            temp.Items.Add(item);
                        
                        }
                    
                    }
                   
                } 
            
            }
        
        }

        public static void CreateGroup(List<Genre> genres)
        {
            
            foreach (Genre genre in genres)
            {
                SampleDataGroup Genre = new SampleDataGroup();
                Genre.Title = genre.Name;
                Genre.UniqueId = genre.ID.ToString();

                _sampleDataSource.AllGroups.Add(Genre);
            }
           
        }
    }
}
