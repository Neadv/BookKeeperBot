using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookKeeperBot.Models
{
    public class BookDAO
    {
        private static readonly HttpClient client = new HttpClient();

        private const string url = "https://www.googleapis.com/books/v1/volumes?maxResults=1&q=";

        public async Task<Book> GetBookAsync(string title)
        {
            var param = title.Trim().ToLower().Replace(' ', '+');

            var response = await client.GetStringAsync(url + param);

            var volumeList = JsonConvert.DeserializeObject<VolumeList>(response);

            var book = GetBookFromVolume(volumeList);

            return book;
        }

        private Book GetBookFromVolume(VolumeList volumeList)
        {
            if (volumeList?.Items is { Length: > 0 } && volumeList.Items[0] != null)
            {
                var volumeInfo = volumeList.Items[0].VolumeInfo;
                if (volumeInfo != null)
                {
                    string description = string.Empty;
                    if (volumeInfo.Authors?.Length > 0 && volumeInfo.Description != null)
                    {
                        var authors = string.Join(", ", volumeInfo.Authors);
                        description = $"{volumeInfo.Description}\n\n<strong>{authors}</strong>";
                    }

                    string imgUrl = string.Empty;
                    if (volumeInfo.ImageLinks != null)
                        imgUrl = volumeInfo.ImageLinks.Thumbnail ?? volumeInfo.ImageLinks.SmallThumbnail;

                    return new Book
                    {
                        Title = volumeInfo.Title,
                        Description = description,
                        ImageUrl = imgUrl
                    };
                }
            }
            return null;
        }
    }

    class VolumeList
    {
        public string Kind { get; set; }
        public int TotalItems { get; set; }
        public Volume[] Items { get; set; }
    }

    class Volume
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
    }

    class VolumeInfo
    {
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string Description { get; set; }
        public ImageLinks ImageLinks { get; set; }
    }

    class ImageLinks
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
    }
}