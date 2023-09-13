namespace TAF.API.Models.ResponceModels
{
    public class AllBooksFromAudioBibleModel
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string bibleId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public string nameLong { get; set; }
        public Chapter[] chapters { get; set; }
    }

    public class Chapter
    {
        public string id { get; set; }
        public string bibleId { get; set; }
        public string number { get; set; }
        public string bookId { get; set; }
        public string reference { get; set; }
    }
}